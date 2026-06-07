let apiBaseUrl = '';
let apiReady = false;

let voicePreferences = {
    voiceName: '',
    rate: 1.0,
    pitch: 1.0,
    volume: 1.0
};

let currentSequenceId = 0;
let currentAudio = null;
let currentObjectUrl = null;

function clampNumber(value, min, max, fallback) {
    const parsed = Number(value);
    if (Number.isNaN(parsed))
        return fallback;

    return Math.min(max, Math.max(min, parsed));
}

function normalizePreferences(raw) {
    if (!raw || typeof raw !== 'object')
        return;

    voicePreferences.voiceName = String(raw.voiceName ?? raw.VoiceName ?? '');
    voicePreferences.rate = clampNumber(raw.rate ?? raw.Rate, 0.6, 1.6, 1.0);
    voicePreferences.pitch = clampNumber(raw.pitch ?? raw.Pitch, 0.5, 1.5, 1.0);
    voicePreferences.volume = clampNumber(raw.volume ?? raw.Volume, 0, 1, 1.0);
}

function loadPreferences() {
    try {
        const stored = localStorage.getItem('game_host_voice_prefs');
        if (stored) {
            normalizePreferences(JSON.parse(stored));
        }
    } catch (e) {
        console.error('Failed to load voice preferences', e);
    }
}

function apiUrl(path) {
    const base = (apiBaseUrl ?? '').replace(/\/$/, '');
    return base ? `${base}${path}` : path;
}

function releaseCurrentAudio() {
    if (currentAudio) {
        currentAudio.pause();
        currentAudio.src = '';
        currentAudio = null;
    }

    if (currentObjectUrl) {
        URL.revokeObjectURL(currentObjectUrl);
        currentObjectUrl = null;
    }
}

function splitForNaturalSpeech(text) {
    if (!text)
        return [];

    return text
        .replace(/\s+/g, ' ')
        .trim()
        .split(/(?<=[.!?…])\s+|(?<=\u2014)\s+/)
        .map(part => part.trim())
        .filter(Boolean);
}

function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

export function configure(options) {
    apiBaseUrl = String(options?.baseUrl ?? options?.BaseUrl ?? '').trim();
}

export async function isSupported() {
    if (typeof Audio === 'undefined')
        return false;

    try {
        const response = await fetch(apiUrl('/api/tts/health'), { method: 'GET' });
        apiReady = response.ok;
        return apiReady;
    } catch {
        apiReady = false;
        return false;
    }
}

export function cancel() {
    currentSequenceId++;
    releaseCurrentAudio();
}

export function getVoicePreferences() {
    return {
        voiceName: voicePreferences.voiceName,
        rate: voicePreferences.rate,
        pitch: voicePreferences.pitch,
        volume: voicePreferences.volume
    };
}

export function updateVoicePreferences(prefs) {
    if (!prefs)
        return;

    normalizePreferences(prefs);

    try {
        localStorage.setItem('game_host_voice_prefs', JSON.stringify(voicePreferences));
    } catch (e) {
        console.error('Failed to save voice preferences', e);
    }
}

export async function getAvailableVoices() {
    if (!apiReady) {
        const ok = await isSupported();
        if (!ok)
            return [];
    }

    try {
        const response = await fetch(apiUrl('/api/tts/voices'), { method: 'GET' });
        if (!response.ok)
            return [];

        const voices = await response.json();
        return (voices ?? []).map(v => ({
            name: v.name ?? v.Name ?? '',
            lang: v.lang ?? v.Lang ?? 'pt-BR',
            displayName: v.displayName ?? v.DisplayName ?? v.name ?? v.Name ?? ''
        }));
    } catch (e) {
        console.error('Failed to load GCP voices', e);
        return [];
    }
}

async function synthesizeAndPlay(text, rateMultiplier, pitchMultiplier, volumeMultiplier, seqId) {
    if (!text?.trim())
        return false;

    if (seqId !== currentSequenceId)
        return false;

    const payload = {
        text: text.trim(),
        voiceName: voicePreferences.voiceName || null,
        rate: voicePreferences.rate * (typeof rateMultiplier === 'number' ? rateMultiplier : 1.0),
        pitch: voicePreferences.pitch * (typeof pitchMultiplier === 'number' ? pitchMultiplier : 1.0),
        volume: voicePreferences.volume * (typeof volumeMultiplier === 'number' ? volumeMultiplier : 1.0)
    };

    let response;
    try {
        response = await fetch(apiUrl('/api/tts/speak'), {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });
    } catch (e) {
        console.error('TTS request failed', e);
        return false;
    }

    if (!response.ok || seqId !== currentSequenceId)
        return false;

    const blob = await response.blob();
    if (!blob || blob.size === 0)
        return false;

    releaseCurrentAudio();
    currentObjectUrl = URL.createObjectURL(blob);

    return new Promise(resolve => {
        if (seqId !== currentSequenceId) {
            releaseCurrentAudio();
            resolve(false);
            return;
        }

        const audio = new Audio(currentObjectUrl);
        currentAudio = audio;
        audio.volume = 1;

        const finish = (success) => {
            if (currentAudio === audio) {
                releaseCurrentAudio();
            }
            resolve(success);
        };

        audio.onended = () => finish(true);
        audio.onerror = () => finish(false);
        audio.play().catch(() => finish(false));
    });
}

export async function speak(text, rateMultiplier, pitchMultiplier, volumeMultiplier) {
    if (!text || !apiReady)
        return false;

    cancel();
    const seqId = currentSequenceId;
    return synthesizeAndPlay(text, rateMultiplier, pitchMultiplier, volumeMultiplier, seqId);
}

export async function speakSequence(parts, rateMultiplier, pitchMultiplier, volumeMultiplier, _lang, pauseMs) {
    if (!parts || parts.length === 0 || !apiReady)
        return false;

    cancel();
    const seqId = currentSequenceId;
    const pause = typeof pauseMs === 'number' ? pauseMs : 420;

    for (let i = 0; i < parts.length; i++) {
        const part = parts[i];
        if (!part?.trim())
            continue;

        if (seqId !== currentSequenceId)
            return false;

        const success = await synthesizeAndPlay(
            part.trim(),
            rateMultiplier,
            pitchMultiplier,
            volumeMultiplier,
            seqId);

        if (!success) {
            if (seqId !== currentSequenceId)
                return false;
        }

        if (i < parts.length - 1) {
            await delay(pause);
            if (seqId !== currentSequenceId)
                return false;
        }
    }

    return true;
}

export async function speakNaturally(text, rateMultiplier, pitchMultiplier, volumeMultiplier, lang, pauseMs) {
    const parts = splitForNaturalSpeech(text);
    if (parts.length <= 1)
        return speak(text, rateMultiplier, pitchMultiplier, volumeMultiplier);

    return speakSequence(parts, rateMultiplier, pitchMultiplier, volumeMultiplier, lang, pauseMs);
}

loadPreferences();
