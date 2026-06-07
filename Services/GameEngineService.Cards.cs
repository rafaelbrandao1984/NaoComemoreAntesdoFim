// Gerado a partir do PDF: Cartões para jogo de tabuleiro.pdf
using NaoComemoreAntesdoFim.Models;

namespace NaoComemoreAntesdoFim.Services;

public partial class GameEngineService
{
    public static List<Card> LoadCards()
    {
        var cards = new List<Card>();
        cards.AddRange(LoadQuestionCards());
        cards.AddRange(LoadActionCards());
        return cards;
    }

    private static IEnumerable<Card> LoadQuestionCards() =>
    [
        new Card
        {
            Id = 1,
            Type = CardType.Question,
            Text = "Cite o nome do profeta atual e dos dois que o precederam.",
            Answer = "Dallin H. Oaks (atual, desde 2025), Russell M. Nelson (2018–2025) e Thomas S. Monson (2008–2018).",
            OptionA = "Dallin H. Oaks, Russell M. Nelson e Thomas S. Monson",
            OptionB = " Russell M. Nelson, Thomas S. Monson e Gordon B. Hinckley",
            OptionC = "Thomas S. Monson, Gordon B. Hinckley e Howard W. Hunter",
            CorrectOption = 'A'
        },
        new Card
        {
            Id = 2,
            Type = CardType.Question,
            Text = "Em que ano a Igreja de Jesus Cristo dos Santos dos Últimos Dias foi organizada?",
            Answer = "1830 (6 de abril de 1830). | Doutrina & Convênios 20:1",
            OptionA = "1820",
            OptionB = "1830",
            OptionC = "1844",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 3,
            Type = CardType.Question,
            Text = "Quantos livros tem o Livro de Mórmon?",
            Answer = "15 livros. | Índice do Livro de Mórmon",
            OptionA = "12 livros",
            OptionB = "15 livros",
            OptionC = "17 livros",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 4,
            Type = CardType.Question,
            Text = "Cite os nomes das três Testemunhas do Livro de Mórmon.",
            Answer = "Oliver Cowdery, David Whitmer e Martin Harris. | O Depoimento de Três Testemunhas",
            OptionA = "Oliver Cowdery, David Whitmer e Martin Harris",
            OptionB = "Hyrum Smith, Oliver Cowdery e Martin Harris",
            OptionC = "David Whitmer, Parley Pratt e Oliver Cowdery",
            CorrectOption = 'A'
        },
        new Card
        {
            Id = 5,
            Type = CardType.Question,
            Text = "Aonde os doze apóstolos são enviados para pregar o evangelho?",
            Answer = "A todas as nações (ao mundo inteiro). | Doutrina e Convênios 107:23",
            OptionA = "Apenas às nações de língua inglesa",
            OptionB = "A todas as nações (ao mundo inteiro)",
            OptionC = "Somente às nações de Israel",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 6,
            Type = CardType.Question,
            Text = "Qual é o primeiro livro do Novo Testamento, de acordo com o plano de estudo da Igreja?",
            Answer = "Evangelho segundo Mateus. | Manual do Vem, E Segue-Me",
            OptionA = "Evangelho segundo Lucas",
            OptionB = "Atos dos Apóstolos",
            OptionC = "Evangelho segundo Mateus",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 7,
            Type = CardType.Question,
            Text = "Qual o nome do Hino de n° 25?",
            Answer = "Bela Sião. | Hinário oficial da Igreja",
            OptionA = "Vinde Santos",
            OptionB = "Bela Sião",
            OptionC = "Hino do Sacerdócio",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 8,
            Type = CardType.Question,
            Text = "Cite três das doze tribos de Israel mencionadas nas escrituras.",
            Answer = "Rúben, Simeão, Levi, Judá, Dã, Naftali, Gade, Aser, Issacar, Zebulom, José ou Benjamim. | Genesis 49",
            OptionA = "Rúben, Simeão e Fineias",
            OptionB = "Rúben, Simeão e Levi",
            OptionC = "Josué, Calebe e Gade",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 9,
            Type = CardType.Question,
            Text = "Qual é a designação para um grupo de membros que formam uma unidade geográfica básica na Igreja?",
            Answer = "Ala (ou Ramo). | Manual Geral de Instruções",
            OptionA = "Estaca",
            OptionB = "Distrito",
            OptionC = "Ala (ou Ramo)",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 10,
            Type = CardType.Question,
            Text = "Em que ano a Programação à Família foi lida e apresentada ao mundo?",
            Answer = "1995. | A Família: Proclamação ao Mundo",
            OptionA = "1990",
            OptionB = "1995",
            OptionC = "2000",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 11,
            Type = CardType.Question,
            Text = "Quem foi o primeiro profeta a pregar no Livro de Mórmon?",
            Answer = "Leí. | 1° Néfi 1:4-5, 18",
            OptionA = "Néfi",
            OptionB = "Leí",
            OptionC = "Enos",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 12,
            Type = CardType.Question,
            Text = "Como se chamava o filho de Mosias que não queria ser rei?",
            Answer = "Aarão (Filho de Mosias). | Mosias 29:1-3",
            OptionA = "Aarão (Filho de Mosias)",
            OptionB = "Amôn",
            OptionC = "Helamã",
            CorrectOption = 'A'
        },
        new Card
        {
            Id = 13,
            Type = CardType.Question,
            Text = "Qual Profeta viu o Senhor em um navio de fogo?",
            Answer = "O Irmão de Jarede.  | Éter 3:6-13",
            OptionA = "Néfi",
            OptionB = "Alma",
            OptionC = "O Irmão de Jarede",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 14,
            Type = CardType.Question,
            Text = "Qual foi o rei que pregou de cima de uma torre?",
            Answer = "Rei Benjamim. | Mosias 2:7-8",
            OptionA = "Rei Mosias",
            OptionB = "Rei Benjamim",
            OptionC = "Rei Noé",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 15,
            Type = CardType.Question,
            Text = "Em qual maneira Abinádi foi martirizado?",
            Answer = "Queimado. | Mosias 17:13-20",
            OptionA = "Pedrada",
            OptionB = "Queimado",
            OptionC = "Pisoteado",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 16,
            Type = CardType.Question,
            Text = "Quem foi o líder dos lamanitas que se converteu, em alma 19?",
            Answer = "Rei Lamôni. | Alma 19",
            OptionA = "Lamã",
            OptionB = "Rei Lamôni",
            OptionC = "Anti-Néfi-Leí",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 17,
            Type = CardType.Question,
            Text = "Qual profeta escreveu as últimas palavras nas placas de ouro?",
            Answer = "Morôni. | Moróni 10",
            OptionA = "Mórmon",
            OptionB = "Néfi",
            OptionC = "Morôni",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 18,
            Type = CardType.Question,
            Text = "Qual livro contém o sermão de Benjamim sobre servir?",
            Answer = "Livro de Mosias. | Mosias 2:17",
            OptionA = "Livro de Alma",
            OptionB = "Livro de Mosias",
            OptionC = "Livro de Néfi",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 19,
            Type = CardType.Question,
            Text = "Em qual local os nefitas se reuniram para ouvir o sermão de Jesus?",
            Answer = "No Templo de Abundância. | 3 Néfi 11:1",
            OptionA = "No Templo de Zaraenla",
            OptionB = "No Monte Sinai",
            OptionC = "No Templo de Abundância",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 20,
            Type = CardType.Question,
            Text = "Quem viu as placas e as traduziu em Palmyra?",
            Answer = "Joseph Smith. | | História de Joseph Smith",
            OptionA = "Oliver Cowdery",
            OptionB = "Joseph Smith",
            OptionC = "Martin Harris",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 21,
            Type = CardType.Question,
            Text = "Qual o nome do rei que libertou os nefitas do cativeiro lamanita?",
            Answer = "Rei Mosias (povo de Limi). | Alma 22:18",
            OptionA = "Rei Benjamim",
            OptionB = "Rei Limí",
            OptionC = "Rei Mosias",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 22,
            Type = CardType.Question,
            Text = "Qual foi o último profeta a registrar palavras nas placas de ouro?",
            Answer = "Morôni. | Mórmon 8:1",
            OptionA = "Mórmon",
            OptionB = "Morôni",
            OptionC = "Jarede",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 23,
            Type = CardType.Question,
            Text = "No Velho Testamento qual livro vem após Esdras?",
            Answer = "Neemias. | Neemias 2:1-6",
            OptionA = "Neemias",
            OptionB = "Ester",
            OptionC = "Salmos",
            CorrectOption = 'A'
        },
        new Card
        {
            Id = 24,
            Type = CardType.Question,
            Text = "Qual o nome do irmão mais velho de Néfi",
            Answer = "Lamã. | 1° Néfi 18:8",
            OptionA = "Lemuel",
            OptionB = "Lamã",
            OptionC = "Sam",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 25,
            Type = CardType.Question,
            Text = "Qual profeta pregou sobre a vinda de Cristo na Muralha de Zaraenla?",
            Answer = "Samuel. | Mosias 16:2",
            OptionA = "Alma",
            OptionB = "Helamã",
            OptionC = "Samuel",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 26,
            Type = CardType.Question,
            Text = "Qual é o primeiro livro do Livro de Mórmon?",
            Answer = "1° Néfi. | 1° Néfi 1:1",
            OptionA = "2 Néfi",
            OptionB = "1 Néfi",
            OptionC = "Éter",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 27,
            Type = CardType.Question,
            Text = "No Livro de Mórmon, quem fez um arco de madeira para conseguir alimento no deserto?",
            Answer = "Néfi. | 2° Néfi 33:4",
            OptionA = "Lamã",
            OptionB = "Leí",
            OptionC = "Néfi",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 28,
            Type = CardType.Question,
            Text = "Quem foi rei após o seu pai, Rei Benjamim?",
            Answer = "Rei Mosias. | Mosias 2:17",
            OptionA = "Rei Helamã",
            OptionB = "Rei Alma",
            OptionC = "Rei Mosias",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 29,
            Type = CardType.Question,
            Text = "Qual o nome do irmão mais novo de José?",
            Answer = "Benjamim. | Gênesis 35:18",
            OptionA = "Rúben",
            OptionB = "Judá",
            OptionC = "Benjamim",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 30,
            Type = CardType.Question,
            Text = "Qual o primeiro livro do Velho Testamento?",
            Answer = "Gênesis. | Gênisis 1:1",
            OptionA = "Êxodo",
            OptionB = "Gênesis",
            OptionC = "Deuteronômio",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 31,
            Type = CardType.Question,
            Text = "Onde Jesus Cristo nasceu?",
            Answer = "Belém. | Lucas 2:4-7",
            OptionA = "Nazaré",
            OptionB = "Belém",
            OptionC = "Jerusalém",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 32,
            Type = CardType.Question,
            Text = "Como Davi matou Golias",
            Answer = "Com uma funda e uma pedra. | 1° Samuel 17:49",
            OptionA = "Com uma espada",
            OptionB = "Com uma lança",
            OptionC = "Com uma funda e uma pedra",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 33,
            Type = CardType.Question,
            Text = "Quantas pragas no total foram lançadas sob o Egito?",
            Answer = "10 pragas. | Êxodo 20:1-17",
            OptionA = "7 pragas",
            OptionB = "10 pragas",
            OptionC = "12 pragas",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 34,
            Type = CardType.Question,
            Text = "Quem interpretou o sonho do rei Nabucodonosor?",
            Answer = "Daniel. | Daniel 6:16-23",
            OptionA = "Esdras",
            OptionB = "Isaías",
            OptionC = "Daniel",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 35,
            Type = CardType.Question,
            Text = "Quantas pessoas foram salvas na Arca de Noé, incluindo ele?",
            Answer = "8 pessoas. | 1° Pedro 3:20",
            OptionA = "6 pessoas",
            OptionB = "8 pessoas",
            OptionC = "10 pessoas",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 36,
            Type = CardType.Question,
            Text = "Qual apóstolo negou Jesus 3 vezes?",
            Answer = "Pedro. | Mateus 26:69-75",
            OptionA = "João",
            OptionB = "Tomé",
            OptionC = "Pedro",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 37,
            Type = CardType.Question,
            Text = "Qual era a profissão de Mateus antes de se tornar discípulo de Jesus Cristo?",
            Answer = "Publicano (Cobrador de impostos). | Mateus 9:9",
            OptionA = "Pescador",
            OptionB = "Carpinteiro",
            OptionC = "Publicano (cobrador de impostos)",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 38,
            Type = CardType.Question,
            Text = "Quantos pães e peixes foram multiplicados por Jesus?",
            Answer = "5 pães e 2 peixes. | Mateus 14:17-21",
            OptionA = "7 pães e 2 peixes",
            OptionB = "5 pães e 2 peixes",
            OptionC = "5 pães e 5 peixes",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 39,
            Type = CardType.Question,
            Text = "Onde Moisés recebeu os 10 mandamentos?",
            Answer = "Monte Sinai. | Êxodo 19:20",
            OptionA = "Monte das Oliveiras",
            OptionB = "Monte Carmelo",
            OptionC = "Monte Sinai",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 40,
            Type = CardType.Question,
            Text = "Qual apóstolo era publicano ao ser convidado por Jesus para ser um dos discípulos?",
            Answer = "Mateus. | Mateus 1:1",
            OptionA = "Pedro",
            OptionB = "Tiago",
            OptionC = "Mateus",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 41,
            Type = CardType.Question,
            Text = "De qual profeta Salomé pediu que entregasse sua cabeça numa bandeja?",
            Answer = "João Batista. | Mateus 3:13-17",
            OptionA = "Elias",
            OptionB = "João Batista",
            OptionC = "Isaías",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 42,
            Type = CardType.Question,
            Text = "Qual nome e sobrenome do discípulo que traiu Jesus com um beijo no rosto?",
            Answer = "Judas Iscariotes. | Mateus 26:14-15",
            OptionA = "Pedro Iscariotes",
            OptionB = "Simão de Cirene",
            OptionC = "Judas Iscariotes",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 43,
            Type = CardType.Question,
            Text = "Nome do homem escolhido por Deus que foi engolido por uma baleia?",
            Answer = "Jonas. | Jonas 1:17",
            OptionA = "Elias",
            OptionB = "Amós",
            OptionC = "Jonas",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 44,
            Type = CardType.Question,
            Text = "Como se chamava a esposa de Moisés?",
            Answer = "Zípora. | Êxodo 2:21",
            OptionA = "Rebeca",
            OptionB = "Zípora",
            OptionC = "Miriã",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 45,
            Type = CardType.Question,
            Text = "Qual foi o mar que foi aberto para a passagem dos Israelitas na “fulga” do Egito?",
            Answer = "Mar Vermelho. Êxodo 14:21-22",
            OptionA = "Mar Mediterrâneo",
            OptionB = "Mar Morto",
            OptionC = "Mar Vermelho",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 46,
            Type = CardType.Question,
            Text = "Como se chamava a esposa de Abraão?",
            Answer = "Sara (ou Sarai). | Gênesis 12:5",
            OptionA = "Lea",
            OptionB = "Raquel",
            OptionC = "Sara (ou Sarai)",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 47,
            Type = CardType.Question,
            Text = "Qual nome Abraão deu a seu primeiro filho, com sua esposa Sara?",
            Answer = "Isaque. | Gênesis 21:1-3",
            OptionA = "Ismael",
            OptionB = "Isaque",
            OptionC = "Jacó",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 48,
            Type = CardType.Question,
            Text = "Quais são os 4 evangelhos canônicos?",
            Answer = "Mateus, Marcos, Lucas e João. | Mateus 28:18-30",
            OptionA = "Mateus, Marcos, Lucas e Paulo",
            OptionB = "Mateus, Marcos, Lucas e João",
            OptionC = "Marcos, Lucas, João e Atos",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 49,
            Type = CardType.Question,
            Text = "Quem liderou a construção do muro de Jerusalém?",
            Answer = "Neemias. | Neemias 2:11-18",
            OptionA = "Esdras",
            OptionB = "Isaías",
            OptionC = "Neemias",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 50,
            Type = CardType.Question,
            Text = "Qual rei ficou no lugar de Davi após sua morte?",
            Answer = "Salomão. | 1° Reis 3:12",
            OptionA = "Roboão",
            OptionB = "Saul",
            OptionC = "Salomão",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 51,
            Type = CardType.Question,
            Text = "Qual o nome da Judia que se casou com o rei da Pérsia?",
            Answer = "Ester. | Ester 2:17",
            OptionA = "Rute",
            OptionB = "Ester",
            OptionC = "Débora",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 52,
            Type = CardType.Question,
            Text = "Qual apóstolo escreveu o livro de Atos?",
            Answer = "Lucas. | Atos 1:1",
            OptionA = "Paulo",
            OptionB = "João",
            OptionC = "Lucas",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 53,
            Type = CardType.Question,
            Text = "Em qual cidade Jesus foi morar com sua família para que a profecia fosse cumprida?",
            Answer = "Nazaré. | Mateus 2:23",
            OptionA = "Belém",
            OptionB = "Cafarnaum",
            OptionC = "Nazaré",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 54,
            Type = CardType.Question,
            Text = "Quantas parábolas foram contadas por Jesus, aproximadamente?",
            Answer = "Aproximadamente 40. | (Alguns sites (Google, etc)",
            OptionA = "Aproximadamente 20",
            OptionB = "Aproximadamente 40",
            OptionC = "Aproximadamente 60",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 55,
            Type = CardType.Question,
            Text = "O que foi anunciado por Maria Madalena logo após sua visita ao túmulo de Jesus?",
            Answer = "A ressurreição (o túmulo vazio). | Mateus 28:1-10 | Marcos 16:1-8 | Lucas 24:1-12 | João 20:1-18",
            OptionA = "O túmulo estava guardado",
            OptionB = "A ressurreição — o túmulo vazio",
            OptionC = "Jesus havia ascendido ao céu",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 56,
            Type = CardType.Question,
            Text = "Qual dos discípulos era conhecido como “O amado”",
            Answer = "João. | João 13:23, 21:7, 20-24",
            OptionA = "Pedro",
            OptionB = "Paulo",
            OptionC = "João",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 57,
            Type = CardType.Question,
            Text = "Quantas seções tem o Livro Doutrina & Convênios?",
            Answer = "138 seções + duas declarações oficiais.",
            OptionA = "100 secoes + duas declaracoes",
            OptionB = "138 secoes + duas declaracoes",
            OptionC = "176 secoes + uma declaracao",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 58,
            Type = CardType.Question,
            Text = "Em números 13:16, antes de se chamado Josué, qual era seu nome?",
            Answer = "Oseias. | Números 13:16",
            OptionA = "Calebe",
            OptionB = "Fineias",
            OptionC = "Oseias",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 59,
            Type = CardType.Question,
            Text = "Como foi a morte do rei Saúl?",
            Answer = "Suicidou-se com a própria espada. | 1° Samuel 31:4-6",
            OptionA = "Foi morto por um inimigo",
            OptionB = "Morreu de velhice",
            OptionC = "Suicidou-se com a espada",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 60,
            Type = CardType.Question,
            Text = "Quantos ossos do corpo de Jesus foram quebrados na crucificação?",
            Answer = "Nenhum osso.| João 19:36",
            OptionA = "Três ossos",
            OptionB = "Nenhum osso",
            OptionC = "Um osso",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 61,
            Type = CardType.Question,
            Text = "Qual apóstolo duvidou da ressurreição de Jesus até ver as marcas em suas mãos?",
            Answer = "Tomé. | João 20:24-29",
            OptionA = "Pedro",
            OptionB = "Filipe",
            OptionC = "Tomé",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 62,
            Type = CardType.Question,
            Text = "Quem foi lançado na cova dos leões por orar a Deus?",
            Answer = "Daniel. | Daniel 6:16",
            OptionA = "Jeremias",
            OptionB = "Ezequiel",
            OptionC = "Daniel",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 63,
            Type = CardType.Question,
            Text = "Qual era a profissão de Lucas, o autor de um dos Evangelhos?",
            Answer = "Médico. | Colossenses 4:14",
            OptionA = "Pescador",
            OptionB = "Médico",
            OptionC = "Carpinteiro",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 64,
            Type = CardType.Question,
            Text = "Quem foi o homem mais velho mencionado na Bíblia?",
            Answer = "Matusalém (969 anos). | Gênesis 5:27",
            OptionA = "Adão (930 anos)",
            OptionB = "Noé (950 anos)",
            OptionC = "Matusalém (969 anos)",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 65,
            Type = CardType.Question,
            Text = "O que Deus usou para criar Eva?",
            Answer = "Uma costela de Adão. | Gênesis 2:21",
            OptionA = "O pó da terra",
            OptionB = "Uma costela de Adão",
            OptionC = "A água dos rios",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 66,
            Type = CardType.Question,
            Text = "Quantos dias e noites choveu durante o dilúvio nos dias de Noé?",
            Answer = "40 dias e 40 noites. | Gênesis 7:12",
            OptionA = "20 dias e 20 noites",
            OptionB = "40 dias e 40 noites",
            OptionC = "70 dias e 70 noites",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 67,
            Type = CardType.Question,
            Text = "Quem foi o homem que lavou as mãos durante o julgamento de Jesus?",
            Answer = "Pôncio Pilatos. | Mateus 27:24",
            OptionA = "Herodes",
            OptionB = "Caifás",
            OptionC = "Pôncio Pilatos",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 68,
            Type = CardType.Question,
            Text = "Em que rio Jesus Cristo foi batizado por João Batista?",
            Answer = "Rio Jordão. | Mateus 3:13",
            OptionA = "Rio Nilo",
            OptionB = "Rio Eufrates",
            OptionC = "Rio Jordão",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 69,
            Type = CardType.Question,
            Text = "Qual é o último livro do Novo Testamento?",
            Answer = "Apocalipse. | Índice da Bíblia",
            OptionA = "Hebreus",
            OptionB = "Judas",
            OptionC = "Apocalipse",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 70,
            Type = CardType.Question,
            Text = "Quem foi a esposa do patriarca Isaque?",
            Answer = "Rebeca. | Gênesis 24:67",
            OptionA = "Sara",
            OptionB = "Rebeca",
            OptionC = "Lea",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 71,
            Type = CardType.Question,
            Text = "Qual profeta subiu ao céu em um redemoinho de fogo?",
            Answer = "Elias. | 2 Reis 2:11",
            OptionA = "Moisés",
            OptionB = "Elias",
            OptionC = "Isaías",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 72,
            Type = CardType.Question,
            Text = "Quem interpretou os sonhos do Faraó no Egito, garantindo que o país se preparasse para a fome?",
            Answer = "José. | Gênesis 41",
            OptionA = "Moisés",
            OptionB = "Aarão",
            OptionC = "José",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 73,
            Type = CardType.Question,
            Text = "Qual cidade teve suas muralhas derrubadas após os israelitas marcharem ao redor dela por sete dias?",
            Answer = "Jericó. | Josué 6",
            OptionA = "Ai",
            OptionB = "Jericó",
            OptionC = "Jerusalém",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 74,
            Type = CardType.Question,
            Text = "Quem foi o primeiro rei coroado em Israel?",
            Answer = "Saul. | 1 Samuel 10:1",
            OptionA = "Davi",
            OptionB = "Salomão",
            OptionC = "Saul",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 75,
            Type = CardType.Question,
            Text = "Qual animal falou com o profeta Balaão após ver o anjo do Senhor?",
            Answer = "Uma jumenta. | Números 22:28",
            OptionA = "Uma serpente",
            OptionB = "Um leão",
            OptionC = "Uma jumenta",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 76,
            Type = CardType.Question,
            Text = "Como se chamava o jardim onde Adão e Eva viveram antes da queda?",
            Answer = "Jardim do Éden. | Gênesis 2:8",
            OptionA = "Jardim das Oliveiras",
            OptionB = "Jardim do Éden",
            OptionC = "Jardim de Getsêmani",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 77,
            Type = CardType.Question,
            Text = "O que os israelitas comeram no deserto que caía do céu todas as manhãs?",
            Answer = "Maná. | Êxodo 16:15",
            OptionA = "Codornizes",
            OptionB = "Maná",
            OptionC = "Pão ázimo",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 78,
            Type = CardType.Question,
            Text = "Quem foi o profeta que sucedeu Moisés e liderou o povo de Israel na travessia do Rio Jordão?",
            Answer = "Josué. | Josué 1",
            OptionA = "Calebe",
            OptionB = "Aarão",
            OptionC = "Josué",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 79,
            Type = CardType.Question,
            Text = "Quem cortou o cabelo de Sansão, fazendo-o perder sua força?",
            Answer = "Dalila (ou um homem a pedido dela). | Juízes 16:19",
            OptionA = "Rute",
            OptionB = "Jezabel",
            OptionC = "Dalila",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 80,
            Type = CardType.Question,
            Text = "Quem era o governador romano na época do nascimento de Jesus?",
            Answer = "César Augusto. | Lucas 2:1",
            OptionA = "Pôncio Pilatos",
            OptionB = "César Augusto",
            OptionC = "Herodes Antipas",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 81,
            Type = CardType.Question,
            Text = "Quem escondeu as placas de ouro no Monte Cumora para que Joseph Smith as encontrasse no futuro?",
            Answer = "Morôni. | Mórmon 8:14",
            OptionA = "Mórmon",
            OptionB = "Néfi",
            OptionC = "Morôni",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 82,
            Type = CardType.Question,
            Text = "Qual profeta orou o dia inteiro e a noite toda na floresta por sua alma?",
            Answer = "Enos. | Enos 1:4",
            OptionA = "Alma",
            OptionB = "Enos",
            OptionC = "Jacom",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 83,
            Type = CardType.Question,
            Text = "Quem rasgou o próprio manto para fazer o \"Estandarte da Liberdade\"?",
            Answer = "Capitão Morôni. | Alma 46:12",
            OptionA = "Helamã",
            OptionB = "Morôni",
            OptionC = "Capitão Morôni",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 84,
            Type = CardType.Question,
            Text = "Qual era o nome do objeto que funcionava como uma \"bússola\" para a família de Leí no deserto?",
            Answer = "A Liahona. | Alma 37:38",
            OptionA = "A Urim e Tumim",
            OptionB = "A Liahona",
            OptionC = "As Placas de Bronze",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 85,
            Type = CardType.Question,
            Text = "Qual missionário nefita defendeu o rebanho do rei Lamôni cortando os braços dos saqueadores?",
            Answer = "Amon. | Alma 17:37",
            OptionA = "Arão",
            OptionB = "Omner",
            OptionC = "Amon",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 86,
            Type = CardType.Question,
            Text = "Qual mulher no Livro de Mórmon foi serva do rei Lamôni, em alma 19, convertida ao evangelho?",
            Answer = "Abis. | Alma 19:16",
            OptionA = "Isabel",
            OptionB = "Saraia",
            OptionC = "Abis",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 87,
            Type = CardType.Question,
            Text = "Onde Alma, batizou centenas de conversos após fugir do Rei Noé?",
            Answer = "Nas Águas de Mórmon. | Mosias 18:8",
            OptionA = "Nas Águas de Mórmon",
            OptionB = "No Rio Jordão",
            OptionC = "No Lago de Tiberíades",
            CorrectOption = 'A'
        },
        new Card
        {
            Id = 88,
            Type = CardType.Question,
            Text = "Quantos dos jovens guerreiros de Helamã morreram em batalha?",
            Answer = "Nenhum. | Alma 57:25",
            OptionA = "Dois mil",
            OptionB = "Duzentos",
            OptionC = "Nenhum",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 89,
            Type = CardType.Question,
            Text = "Qual era a moeda de menor valor no sistema monetário nefita?",
            Answer = "Leá (metade de um siblum). | Alma 11",
            OptionA = "Sena",
            OptionB = "Leá (metade de um siblum)",
            OptionC = "Ezrom",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 90,
            Type = CardType.Question,
            Text = "Quem construiu um navio para atravessar o oceano, apesar da zombaria de seus irmãos mais velhos?",
            Answer = "Néfi. | 1 Néfi 17:8",
            OptionA = "Leí",
            OptionB = "Sam",
            OptionC = "Néfi",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 91,
            Type = CardType.Question,
            Text = "Quem o rei Noé mandou matar na fogueira por pregar o arrependimento ao seu povo?",
            Answer = "Abinadi. | Mosias 17:20",
            OptionA = "Alma",
            OptionB = "Abinadi",
            OptionC = "Amuleque",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 92,
            Type = CardType.Question,
            Text = "Onde o rei Benjamim reuniu seu povo, armando tendas, para ouvir seu último discurso?",
            Answer = "Ao redor do templo. | Mosias 2:6",
            OptionA = "No deserto",
            OptionB = "Às margens do Rio Sidônia",
            OptionC = "Ao redor do templo",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 93,
            Type = CardType.Question,
            Text = "Qual era o nome do pai do profeta Alma, o Filho?",
            Answer = "Alma, Pai. | Mosias 27",
            OptionA = "Helamã",
            OptionB = "Alma, Pai",
            OptionC = "Mosias",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 94,
            Type = CardType.Question,
            Text = "Qual é o último livro contido dentro do Livro de Mórmon?",
            Answer = "Livro de Morôni. | Índice",
            OptionA = "Livro de Éter",
            OptionB = "Livro de Mórmon",
            OptionC = "Livro de Morôni",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 95,
            Type = CardType.Question,
            Text = "Quem era o líder (capitão) dos 2.000 jovens guerreiros ammonitas?",
            Answer = "Helamã. | Alma 53:19",
            OptionA = "Morôni",
            OptionB = "Teâncum",
            OptionC = "Helamã",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 96,
            Type = CardType.Question,
            Text = "Que irmão de Néfi frequentemente murmurava contra o pai e contra Deus junto com Lamã?",
            Answer = "Lemuel. | 1 Néfi 2:11",
            OptionA = "Sam",
            OptionB = "Lemuel",
            OptionC = "Jacom",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 97,
            Type = CardType.Question,
            Text = "O irmão de Jarede viu o dedo do Senhor tocando em quais objetos para que brilhassem nos barcos?",
            Answer = "16 pedras (transparentes/claras). | Éter 3:6",
            OptionA = "8 tochas de prata",
            OptionB = "16 pedras transparentes",
            OptionC = "12 cristais sagrados",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 98,
            Type = CardType.Question,
            Text = "Quem foi o primeiro juiz supremo escolhido entre os nefitas após o fim do reinado dos reis?",
            Answer = "Alma, o Filho. | Mosias 29:42",
            OptionA = "Helamã",
            OptionB = "Néfi filho de Helamã",
            OptionC = "Alma, o Filho",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 99,
            Type = CardType.Question,
            Text = "De acordo com as palavras de Cristo no Livro de Mórmon, qual animal protege os pintinhos embaixo das asas?",
            Answer = "Galinha. | 3 Néfi 10:4",
            OptionA = "A águia",
            OptionB = "O pato",
            OptionC = "Galinha",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 100,
            Type = CardType.Question,
            Text = "Quem cortou a cabeça de Siz na batalha final dos jareditas?",
            Answer = "Coriântumr. | Éter 15:30",
            OptionA = "Shared",
            OptionB = "Lib",
            OptionC = "Coriântumr",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 101,
            Type = CardType.Question,
            Text = "Em qual seção de Doutrina e Convênios encontramos a revelação de saúde conhecida como a \"Palavra de Sabedoria\"?",
            Answer = "Seção 89. | D&C 89",
            OptionA = "Seção 76",
            OptionB = "Seção 89",
            OptionC = "Seção 121",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 102,
            Type = CardType.Question,
            Text = "Em qual prisão o Profeta Joseph Smith e seu irmão Hyrum foram martirizados?",
            Answer = "Cadeia de Carthage. | D&C 135",
            OptionA = "Cadeia de Liberty",
            OptionB = "Cadeia de Carthage",
            OptionC = "Prisão de Nauvoo",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 103,
            Type = CardType.Question,
            Text = "Quem foi o segundo Presidente da Igreja, que liderou os santos para o Vale de Salt Lake?",
            Answer = "Brigham Young. | História da Igreja",
            OptionA = "John Taylor",
            OptionB = "Wilford Woodruff",
            OptionC = "Brigham Young",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 104,
            Type = CardType.Question,
            Text = "Qual anjo apareceu a Joseph Smith e Oliver Cowdery para restaurar o Sacerdócio Aarônico?",
            Answer = "João Batista. | D&C 13",
            OptionA = "Morôni",
            OptionB = "Gabriel",
            OptionC = "João Batista",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 105,
            Type = CardType.Question,
            Text = "Quais três antigos apóstolos restauraram o Sacerdócio de Melquisedeque na Terra?",
            Answer = "Pedro, Tiago e João. | D&C 27:12",
            OptionA = "Paulo, Barnabé e Silas",
            OptionB = "Pedro, Tiago e João",
            OptionC = "Mateus, Marcos e Lucas",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 106,
            Type = CardType.Question,
            Text = "Em qual estado americano a Igreja foi oficialmente organizada no dia 6 de abril de 1830?",
            Answer = "Nova York (Fayette). | D&C 20:1",
            OptionA = "Ohio",
            OptionB = "Illinois",
            OptionC = "Nova York",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 107,
            Type = CardType.Question,
            Text = "Quem perdeu as 116 páginas do manuscrito original traduzido do Livro de Mórmon?",
            Answer = "Martin Harris. | D&C 3",
            OptionA = "Oliver Cowdery",
            OptionB = "Martin Harris",
            OptionC = "David Whitmer",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 108,
            Type = CardType.Question,
            Text = "Em que ano os pioneiros chegaram ao Vale do Lago Salgado pela primeira vez?",
            Answer = "1847 (24 de julho). | História da Igreja",
            OptionA = "1830",
            OptionB = "1847",
            OptionC = "1852",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 109,
            Type = CardType.Question,
            Text = "Qual templo pioneiro demorou exatamente 40 anos para ser construído e finalizado?",
            Answer = "Templo de Salt Lake. | História da Igreja",
            OptionA = "Templo de Nauvoo",
            OptionB = "Templo de Kirtland",
            OptionC = "Templo de Salt Lake",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 110,
            Type = CardType.Question,
            Text = "Em qual rio o Sacerdócio Aarônico foi restaurado e os primeiros batismos foram realizados?",
            Answer = "Rio Susquehanna. | História da Igreja",
            OptionA = "Rio Mississipi",
            OptionB = "Rio Jordão",
            OptionC = "Rio Susquehanna",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 111,
            Type = CardType.Question,
            Text = "Qual seção de Doutrina e Convênios é conhecida como \"A Visão\", que descreve os três reinos de glória?",
            Answer = "Seção 76. | D&C 76",
            OptionA = "Seção 89",
            OptionB = "Seção 76",
            OptionC = "Seção 138",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 112,
            Type = CardType.Question,
            Text = "Como se chamava o bosque onde Joseph Smith viu Deus o Pai e Jesus Cristo na Primeira Visão?",
            Answer = "Bosque Sagrado. | História da Igreja",
            OptionA = "Bosque de Kirtland",
            OptionB = "Bosque Sagrado",
            OptionC = "Bosque de Nauvoo",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 113,
            Type = CardType.Question,
            Text = "Qual foi o nome da primeira esposa do profeta Joseph Smith?",
            Answer = "Emma Hale Smith. | História da Igreja",
            OptionA = "Mary Whitmer Smith",
            OptionB = "Emma Hale Smith",
            OptionC = "Lucy Mack Smith",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 114,
            Type = CardType.Question,
            Text = "Em qual cidade os santos construíram um lindo templo que posteriormente foi incendiado após serem expulsos?",
            Answer = "Nauvoo (Illinois). | História da Igreja",
            OptionA = "Far West (Missouri)",
            OptionB = "Nauvoo (Illinois)",
            OptionC = "Kirtland (Ohio)",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 115,
            Type = CardType.Question,
            Text = "Quem era o profeta quando a Igreja recebeu a revelação de 1978 estendendo o sacerdócio a todos os homens dignos?",
            Answer = "Spencer W. Kimball. | Declaração Oficial 2",
            OptionA = "Joseph Fielding Smith",
            OptionB = "Harold B. Lee",
            OptionC = "Spencer W. Kimball",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 116,
            Type = CardType.Question,
            Text = "O que significa a famosa sigla \"CTR\" vista frequentemente nos anéis das crianças da Primária?",
            Answer = "Conservar a Tua Rota (Choose The Right). | Primária",
            OptionA = "Conservar a Tua Rota",
            OptionB = "Cristo Te Recompensará",
            OptionC = "Confiar Testemunhar Rezar",
            CorrectOption = 'A'
        },
        new Card
        {
            Id = 117,
            Type = CardType.Question,
            Text = "Qual profeta da restauração teve a visão do mundo espiritual registrada na Seção 138 de D&C?",
            Answer = "Joseph F. Smith. | D&C 138",
            OptionA = "Wilford Woodruff",
            OptionB = "Joseph F. Smith",
            OptionC = "Gordon B. Hinckley",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 118,
            Type = CardType.Question,
            Text = "Quem foi o primeiro bispo chamado na Igreja restaurada?",
            Answer = "Edward Partridge. | D&C 41:9",
            OptionA = "Sidney Rigdon",
            OptionB = "Martin Harris",
            OptionC = "Edward Partridge",
            CorrectOption = 'C'
        },
        new Card
        {
            Id = 119,
            Type = CardType.Question,
            Text = "Em qual cadeia Joseph Smith escreveu a famosa carta que hoje compõe as seções 121, 122 e 123 de D&C?",
            Answer = "Cadeia de Liberty. | D&C 121",
            OptionA = "Cadeia de Carthage",
            OptionB = "Cadeia de Liberty",
            OptionC = "Prisão de Independence",
            CorrectOption = 'B'
        },
        new Card
        {
            Id = 120,
            Type = CardType.Question,
            Text = "Qual organização para mulheres foi fundada em Nauvoo em 1842?",
            Answer = "A Sociedade de Socorro. | História da Igreja",
            OptionA = "A Organização das Mulheres",
            OptionB = "A Sociedade de Socorro",
            OptionC = "A Associação das Mulheres",
            CorrectOption = 'B'
        },
    ];

    private static IEnumerable<Card> LoadActionCards() =>
    [
        new Card
        {
            Id = 121,
            Type = CardType.Action,
            Text = "Parabéns! Beba um copo de água.",
            ActionCode = "DrinkWaterSelf"
        },
        new Card
        {
            Id = 122,
            Type = CardType.Penalty,
            Text = "Ah! Que pena, devolva todos os chocalates para a caixa. Caso não tenha, escolha alguém para devolver.",
            ActionCode = "LoseAllChocolates"
        },
        new Card
        {
            Id = 123,
            Type = CardType.Reward,
            Text = "Oba! Pegue todos os chocolates de alguém, exceto da caixa.",
            ActionCode = "StealAllChocolates"
        },
        new Card
        {
            Id = 124,
            Type = CardType.Action,
            Text = "Vamos lá! Cite uma escritura, sem consulta heim.",
            ActionCode = "ReciteScripture"
        },
        new Card
        {
            Id = 125,
            Type = CardType.Action,
            Text = "Que legal! Vamos cantar seu hino favorito.",
            ActionCode = "SingHymn"
        },
        new Card
        {
            Id = 126,
            Type = CardType.Action,
            Text = "Hora de compartilhar, passe todos os seus chocolates para algum dos participantes.",
            ActionCode = "GiveAllChocolates"
        },
        new Card
        {
            Id = 127,
            Type = CardType.Action,
            Text = "Escolha alguém para doar todos os chocolates e quem vai receber, mas não pode ser para você.",
            ActionCode = "DonateAllChocolates"
        },
        new Card
        {
            Id = 128,
            Type = CardType.Reward,
            Text = "Que legal, você ganhou 3 chocolates. Ainda não os coma!",
            ActionCode = "GiveChocolates",
            Amount = 3
        },
        new Card
        {
            Id = 129,
            Type = CardType.Reward,
            Text = "Que delícia! Coma um chocolate.",
            ActionCode = "EatChocolate",
            Amount = 1
        },
        new Card
        {
            Id = 130,
            Type = CardType.Action,
            Text = "Escolha uma pessoa para ficar sem jogar uma rodada.",
            ActionCode = "SkipOtherTurn"
        },
        new Card
        {
            Id = 131,
            Type = CardType.Action,
            Text = "Passou a vez!",
            ActionCode = "SkipTurn"
        },
        new Card
        {
            Id = 132,
            Type = CardType.Action,
            Text = "Você é o seu amor comem 1(um) chocolate de alguém",
            ActionCode = "StealChocolate",
            Amount = 1
        },
        new Card
        {
            Id = 133,
            Type = CardType.Action,
            Text = "Escolha alguém para beber um copo de água.",
            ActionCode = "DrinkWater"
        },

    ];
}
