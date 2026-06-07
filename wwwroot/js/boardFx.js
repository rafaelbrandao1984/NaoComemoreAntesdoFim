let canvas;
let ctx;
let particles = [];
let rafId = 0;

const colors = ['#f5e6b8', '#c9a227', '#e8d5a3', '#ffffff', '#ffb74d', '#142680'];

function ensureCanvas() {
    if (canvas)
        return canvas;

    canvas = document.createElement('canvas');
    canvas.className = 'board-confetti-canvas';
    canvas.setAttribute('aria-hidden', 'true');
    document.body.appendChild(canvas);
    ctx = canvas.getContext('2d');
    resizeCanvas();
    window.addEventListener('resize', resizeCanvas);
    return canvas;
}

function resizeCanvas() {
    if (!canvas)
        return;

    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
}

function spawnParticle(x, y, power = 1) {
    const angle = Math.random() * Math.PI * 2;
    const speed = (2 + Math.random() * 5) * power;
    return {
        x,
        y,
        vx: Math.cos(angle) * speed,
        vy: Math.sin(angle) * speed - (2 * power),
        size: 4 + Math.random() * 6,
        color: colors[Math.floor(Math.random() * colors.length)],
        rotation: Math.random() * 360,
        spin: (Math.random() - 0.5) * 12,
        life: 1
    };
}

function burst(originX, originY, count, power) {
    ensureCanvas();
    for (let i = 0; i < count; i++) {
        particles.push(spawnParticle(originX, originY, power));
    }

    if (!rafId)
        rafId = requestAnimationFrame(tick);
}

function tick() {
    if (!ctx || !canvas)
        return;

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    particles = particles.filter(p => {
        p.x += p.vx;
        p.y += p.vy;
        p.vy += 0.12;
        p.rotation += p.spin;
        p.life -= 0.012;

        if (p.life <= 0)
            return false;

        ctx.save();
        ctx.translate(p.x, p.y);
        ctx.rotate((p.rotation * Math.PI) / 180);
        ctx.globalAlpha = Math.max(p.life, 0);
        ctx.fillStyle = p.color;
        ctx.fillRect(-p.size / 2, -p.size / 4, p.size, p.size / 2);
        ctx.restore();
        return true;
    });

    if (particles.length > 0) {
        rafId = requestAnimationFrame(tick);
    } else {
        rafId = 0;
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    }
}

export function burstConfetti() {
    const cx = window.innerWidth * 0.5;
    const cy = window.innerHeight * 0.35;
    burst(cx, cy, 120, 1.2);
    burst(window.innerWidth * 0.25, cy, 40, 0.9);
    burst(window.innerWidth * 0.75, cy, 40, 0.9);
}

export function burstConfettiSmall() {
    const cx = window.innerWidth * 0.5;
    const cy = window.innerHeight * 0.42;
    burst(cx, cy, 45, 0.85);
}
