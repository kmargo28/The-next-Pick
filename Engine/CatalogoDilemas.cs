using TheNextPick.Models;

namespace TheNextPick.Engine;

public static class CatalogoDilemas
{
    private static readonly List<Dilema> DilemasBase = new()
    {
        // 1 - 10: TÁCTICA Y PARTIDO
        new() {
            Id = 1, Categoria = "Táctica", Titulo = "Cobertura Drop vs Bloqueo Directo",
            Descripcion = "El base rival castiga tirando tras el pick and roll. El cuerpo técnico pide cambiar la defensa a cambio agresivo.",
            OpcionA = new() { Texto = "Cambiar la marca y puntear el tiro", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -12, DeltaDT = +12, DeltaQuimica = +5, DeltaHype = +2, Retroalimentacion = "Ajuste táctico cumplido con éxito en el perímetro." },
            OpcionB = new() { Texto = "Quedarse en la pintura protegiendo el rebote", ImagenUrl = "https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = -10, DeltaQuimica = -4, DeltaHype = -4, Retroalimentacion = "El rival anotó dos triples cómodos por falta de cobertura." }
        },
        new() {
            Id = 2, Categoria = "Partido (Clutch)", Titulo = "Posesión Definitiva con 3 Segundos",
            Descripcion = "Marcador 108-109. Tienes el balón en la cabecera con el reloj agotándose.", EsMomentoPartido = true,
            OpcionA = new() { Texto = "Tomar el tiro en suspensión forzado", ImagenUrl = "https://images.unsplash.com/photo-1519766304817-4f37bda74a29?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -14, DeltaDT = -4, DeltaQuimica = -6, DeltaHype = +24, Retroalimentacion = "Asumiste la máxima responsabilidad bajo los reflectores." },
            OpcionB = new() { Texto = "Doblar pase al alero en la esquina", ImagenUrl = "https://images.unsplash.com/photo-1577471488278-16eec37ffcc2?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = +16, DeltaQuimica = +14, DeltaHype = +6, Retroalimentacion = "Pase quirúrgico para el triple ganador en la bocina." }
        },
        new() {
            Id = 3, Categoria = "Táctica", Titulo = "Defensa en Zona 2-3 Rival",
            Descripcion = "El oponente se encierra taponando la pintura e invitando al tiro exterior.",
            OpcionA = new() { Texto = "Romper la zona a base de triples lejanos", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -10, DeltaDT = +4, DeltaQuimica = 0, DeltaHype = +14, Retroalimentacion = "Encestaste tres bombazos que obligaron a disolver la zona." },
            OpcionB = new() { Texto = "Cortar por el centro para alimentar al poste", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -8, DeltaDT = +14, DeltaQuimica = +12, DeltaHype = +2, Retroalimentacion = "Lectura colectiva de veterano consagrado." }
        },
        new() {
            Id = 4, Categoria = "Partido (Clutch)", Titulo = "Falta Intencional Estratégica",
            Descripcion = "Ganas por 3 puntos a falta de 6 segundos. ¿Cometer falta para impedir un triple?", EsMomentoPartido = true,
            OpcionA = new() { Texto = "Cometer falta inmediata y mandar a libres", ImagenUrl = "https://images.unsplash.com/photo-1519766304817-4f37bda74a29?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = +15, DeltaQuimica = +4, DeltaHype = -5, Retroalimentacion = "Decisión analítica inteligente; aseguraste la victoria." },
            OpcionB = new() { Texto = "Defender posesión completa sin tocar", ImagenUrl = "https://images.unsplash.com/photo-1577471488278-16eec37ffcc2?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -12, DeltaDT = -8, DeltaQuimica = 0, DeltaHype = +12, Retroalimentacion = "Tensión extrema; el rival falló el triple forzado." }
        },
        new() {
            Id = 5, Categoria = "Táctica", Titulo = "Aislarse en Aclarado (Isolation)",
            Descripcion = "Tu defensor directo tiene problemas de faltas personales. Puedes pedir aclarado.",
            OpcionA = new() { Texto = "Pedir a todos abrirse y atacar en 1 vs 1", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -14, DeltaDT = -2, DeltaQuimica = -6, DeltaHype = +18, Retroalimentacion = "Le sacaste la sexta falta y terminaste con mate." },
            OpcionB = new() { Texto = "Mover el balón dentro del sistema ofensivo", ImagenUrl = "https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = +12, DeltaQuimica = +10, DeltaHype = +2, Retroalimentacion = "El juego colectivo desarmó a la defensa oponente." }
        },
        new() {
            Id = 6, Categoria = "Táctica", Titulo = "Presión a Toda Cancha (Full Court Press)",
            Descripcion = "El rival entra desconcentrado en el tercer cuarto. ¿Aumentar la agresividad?",
            OpcionA = new() { Texto = "Presionar desde el saque de fondo", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -22, DeltaDT = +14, DeltaQuimica = +8, DeltaHype = +10, Retroalimentacion = "Dos robos de balón seguidos que liquidaron el encuentro." },
            OpcionB = new() { Texto = "Esperar ordenados en media duela", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +5, DeltaDT = +4, DeltaQuimica = +4, DeltaHype = -2, Retroalimentacion = "Defensa conservadora sin desgaste innecesario." }
        },
        new() {
            Id = 7, Categoria = "Partido (Clutch)", Titulo = "Marcar a la Superestrella Rival",
            Descripcion = "Quedan 2 minutos y la estrella rival lleva 38 puntos. ¿Pides asignarte su marca?", EsMomentoPartido = true,
            OpcionA = new() { Texto = "Pedir asignación defensiva personal", ImagenUrl = "https://images.unsplash.com/photo-1519766304817-4f37bda74a29?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -18, DeltaDT = +16, DeltaQuimica = +12, DeltaHype = +20, Retroalimentacion = "Secaste a la estrella en los tiros definitivos." },
            OpcionB = new() { Texto = "Confiar en las ayudas del esquema", ImagenUrl = "https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = +4, DeltaQuimica = +4, DeltaHype = 0, Retroalimentacion = "Defensa de equipo disciplinada." }
        },
        new() {
            Id = 8, Categoria = "Táctica", Titulo = "Ritmo de Juego: Correr o Pausar",
            Descripcion = "La banca rival es lenta en transición defensiva.",
            OpcionA = new() { Texto = "Correr contraataque en cada rebote", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -16, DeltaDT = +8, DeltaQuimica = +6, DeltaHype = +14, Retroalimentacion = "Showtime en la duela; puntos rápidos en velocidad." },
            OpcionB = new() { Texto = "Armar ofensiva paciente de 20 segundos", ImagenUrl = "https://images.unsplash.com/photo-1577471488278-16eec37ffcc2?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = +10, DeltaQuimica = +6, DeltaHype = -2, Retroalimentacion = "Controlaste el tiempo del partido a tu antojo." }
        },
        new() {
            Id = 9, Categoria = "Táctica", Titulo = "Lanzar en Transición (Pull-Up 3)",
            Descripcion = "Llegas solo en contraataque a la línea de 3. El DT prefiere asegurar bandeja.",
            OpcionA = new() { Texto = "Frenar en seco y soltar el triple", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -8, DeltaDT = -8, DeltaQuimica = -4, DeltaHype = +22, Retroalimentacion = "¡Balón adentro! El estadio entero se puso de pie." },
            OpcionB = new() { Texto = "Penetrar hasta el aro para bandeja fácil", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -8, DeltaDT = +12, DeltaQuimica = +6, DeltaHype = +4, Retroalimentacion = "Canasta segura respetando la pizarra." }
        },
        new() {
            Id = 10, Categoria = "Partido (Clutch)", Titulo = "El Tiro Libre con Técnica Rival",
            Descripcion = "Técnica pitada a la banca oponente. El DT te designa lanzador oficial.", EsMomentoPartido = true,
            OpcionA = new() { Texto = "Cobrar el tiro libre con calma", ImagenUrl = "https://images.unsplash.com/photo-1519766304817-4f37bda74a29?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -2, DeltaDT = +8, DeltaQuimica = +4, DeltaHype = +6, Retroalimentacion = "Punto anotado sin pestañear." },
            OpcionB = new() { Texto = "Ceder el tiro al veterano del equipo", ImagenUrl = "https://images.unsplash.com/photo-1577471488278-16eec37ffcc2?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +10, DeltaQuimica = +14, DeltaHype = -2, Retroalimentacion = "Gesto de respeto celebrado en el vestuario." }
        },

        // 11 - 20: VESTUARIO Y LIDERAZGO
        new() {
            Id = 11, Categoria = "Vestuario", Titulo = "La Novatada Tradicional",
            Descripcion = "Los veteranos piden que cargues con las maletas y compres donuts rosas.",
            OpcionA = new() { Texto = "Cumplir la tradición con una sonrisa", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = 0, DeltaQuimica = +16, DeltaHype = +4, Retroalimentacion = "Te ganaste el aprecio incondicional del núcleo veterano." },
            OpcionB = new() { Texto = "Negarse y exigir respeto profesional", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -6, DeltaQuimica = -18, DeltaHype = +8, Retroalimentacion = "Ambiente tenso en las duchas tras tu desplante." }
        },
        new() {
            Id = 12, Categoria = "Vestuario", Titulo = "Tensión por Tiros Intentados",
            Descripcion = "El segundo anotador del plantel se queja de tu alto volumen de tiros.",
            OpcionA = new() { Texto = "Reunirse a cenar y acordar jugadas juntos", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = +10, DeltaQuimica = +18, DeltaHype = 0, Retroalimentacion = "Nació una sociedad temible dentro del tabloncillo." },
            OpcionB = new() { Texto = "\"El mejor anotador debe tirar, así es la NBA\"", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -8, DeltaQuimica = -20, DeltaHype = +12, Retroalimentacion = "Guerra de egos declarada internamente." }
        },
        new() {
            Id = 13, Categoria = "Vestuario", Titulo = "El Novato Aislado",
            Descripcion = "El recién llegado de segunda ronda pasa los viajes solo en su asiento.",
            OpcionA = new() { Texto = "Integrarlo a tus sesiones y jugar videojuegos", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = +8, DeltaQuimica = +16, DeltaHype = +4, Retroalimentacion = "Demostraste madera de capitán de franquicia." },
            OpcionB = new() { Texto = "No intervenir y enfocarte en tu juego", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +4, DeltaDT = 0, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Mantuviste tu rutina personal sin cambios." }
        },
        new() {
            Id = 14, Categoria = "Vestuario", Titulo = "Reunión de Crisis a Puerta Cerrada",
            Descripcion = "Racha de 4 derrotas consecutivas. Solo jugadores en el vestuario.",
            OpcionA = new() { Texto = "Tomar la palabra y exigir defensa colectiva", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -8, DeltaDT = +10, DeltaQuimica = +18, DeltaHype = +6, Retroalimentacion = "El equipo reaccionó ganando el siguiente partido." },
            OpcionB = new() { Texto = "Quedarse callado para no generar roces", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -4, DeltaQuimica = -8, DeltaHype = -4, Retroalimentacion = "La reunión terminó sin conclusiones claras." }
        },
        new() {
            Id = 15, Categoria = "Vestuario", Titulo = "Celebración Excesiva de Visitante",
            Descripcion = "Tu compañero baila provocando a la banca rival tras una canasta.",
            OpcionA = new() { Texto = "Llevarlo del brazo hacia atrás con calma", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -2, DeltaDT = +12, DeltaQuimica = +8, DeltaHype = 0, Retroalimentacion = "Evitaste una falta técnica colectiva." },
            OpcionB = new() { Texto = "Unirte al baile desafiando al estadio", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = -10, DeltaQuimica = +6, DeltaHype = +20, Retroalimentacion = "Video viral en TikTok pero multa de la liga." }
        },
        new() {
            Id = 16, Categoria = "Vestuario", Titulo = "El Compañero en Transferibles",
            Descripcion = "Tu amigo más cercano en el plantel suena en rumores de traspaso.",
            OpcionA = new() { Texto = "Apoyarlo públicamente ante los micrófonos", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -8, DeltaQuimica = +16, DeltaHype = +6, Retroalimentacion = "El vestuario te ve como un hermano leal." },
            OpcionB = new() { Texto = "No meterte en asuntos de la directiva", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +8, DeltaQuimica = -10, DeltaHype = 0, Retroalimentacion = "Postura corporativa correcta." }
        },
        new() {
            Id = 17, Categoria = "Vestuario", Titulo = "Música en el Vestuario Pre-Partido",
            Descripcion = "Discusión por quién controla los altavoces antes de salir a calentar.",
            OpcionA = new() { Texto = "Dejar que el DJ oficial del equipo elija", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +4, DeltaQuimica = +8, DeltaHype = 0, Retroalimentacion = "Buena vibra y concentración general." },
            OpcionB = new() { Texto = "Poner tus auriculares personales y aislarte", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +4, DeltaDT = 0, DeltaQuimica = -4, DeltaHype = 0, Retroalimentacion = "Zona de concentración individual impenetrable." }
        },
        new() {
            Id = 18, Categoria = "Vestuario", Titulo = "Regalo Navideño al Equipo",
            Descripcion = "Llega diciembre y como líder debes tener un detalle con tus compañeros.",
            OpcionA = new() { Texto = "Comprar relojes de lujo personalizados", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +4, DeltaQuimica = +22, DeltaHype = +14, Retroalimentacion = "Tus compañeros quedaron maravillados por tu generosidad." },
            OpcionB = new() { Texto = "Organizar una cena privada íntima", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = +6, DeltaQuimica = +14, DeltaHype = +4, Retroalimentacion = "Charla cercana que fortaleció los lazos." }
        },
        new() {
            Id = 19, Categoria = "Vestuario", Titulo = "El Titular Lesionado Recuperado",
            Descripcion = "Regresa el titular de tu posición. El DT piensa enviarte a la banca.",
            OpcionA = new() { Texto = "Aceptar el rol de sexto hombre con humildad", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +6, DeltaDT = +16, DeltaQuimica = +12, DeltaHype = -4, Retroalimentacion = "El DT destacó tu madurez deportiva." },
            OpcionB = new() { Texto = "Luchar por la titularidad en cada práctica", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -14, DeltaDT = +8, DeltaQuimica = -8, DeltaHype = +10, Retroalimentacion = "Competitividad feroz en los entrenamientos." }
        },
        new() {
            Id = 20, Categoria = "Vestuario", Titulo = "La Charla Táctica del Entrenador",
            Descripcion = "El DT plantea un esquema que tú sabes que no funcionará ante el rival.",
            OpcionA = new() { Texto = "Opinar respetuosamente en la pizarra", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -2, DeltaDT = +10, DeltaQuimica = +8, DeltaHype = 0, Retroalimentacion = "El DT valoró tu visión táctica y ajustó." },
            OpcionB = new() { Texto = "Guardar silencio y obedecer la orden", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +4, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Disciplina estricta sin contradecir al líder." }
        },

        // 21 - 30: PRENSA, REDES Y POPULARIDAD
        new() {
            Id = 21, Categoria = "Prensa", Titulo = "Críticas de una Leyenda en Televisión",
            Descripcion = "Shaquille O'Neal o Charles Barkley dicen que juegas \"blando\" en TV.",
            OpcionA = new() { Texto = "\"Tengo mucho respeto por él, responderé en la cancha\"", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +14, DeltaQuimica = +8, DeltaHype = +4, Retroalimentacion = "Respuesta templada y elegante de superestrella." },
            OpcionB = new() { Texto = "Publicar meme burlándote de sus tiros libres", ImagenUrl = "https://images.unsplash.com/photo-1505666287802-9da132c02f41?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -12, DeltaQuimica = -6, DeltaHype = +26, Retroalimentacion = "Trending topic mundial durante tres días." }
        },
        new() {
            Id = 22, Categoria = "Redes Sociales", Titulo = "El Directo de Instagram a Madrugada",
            Descripcion = "No puedes dormir en la concentración del hotel.",
            OpcionA = new() { Texto = "Abrir directo con fans respondiendo preguntas", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -8, DeltaDT = -6, DeltaQuimica = 0, DeltaHype = +18, Retroalimentacion = "Conexión masiva con los fanáticos jóvenes." },
            OpcionB = new() { Texto = "Apagar pantallas y hacer respiraciones", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +12, DeltaDT = +6, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Descanso reparador antes del choque." }
        },
        new() {
            Id = 23, Categoria = "Prensa", Titulo = "Pregunta sobre la Agencia Libre",
            Descripcion = "Te preguntan si te gustaría jugar en Los Angeles o New York.",
            OpcionA = new() { Texto = "\"Amo esta ciudad y quiero retirarme aquí\"", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +16, DeltaQuimica = +14, DeltaHype = +10, Retroalimentacion = "La ciudad te ovacionó de pie en el estadio." },
            OpcionB = new() { Texto = "\"Nunca se sabe qué depara el futuro del negocio\"", ImagenUrl = "https://images.unsplash.com/photo-1505666287802-9da132c02f41?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -14, DeltaQuimica = -12, DeltaHype = +18, Retroalimentacion = "Incertidumbre total en los foros de fanáticos." }
        },
        new() {
            Id = 24, Categoria = "Prensa", Titulo = "La Terna Arbitral Polemiza",
            Descripcion = "Los árbitros te pitaron tres faltas dudosas que costaron el partido.",
            OpcionA = new() { Texto = "\"Nosotros debimos liquidarlo antes, no culpo a los jueces\"", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +12, DeltaQuimica = +6, DeltaHype = -2, Retroalimentacion = "Evitaste la multa de $25,000 de la NBA." },
            OpcionB = new() { Texto = "\"El arbitraje de hoy fue una falta de respeto vergonzosa\"", ImagenUrl = "https://images.unsplash.com/photo-1505666287802-9da132c02f41?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -10, DeltaQuimica = +8, DeltaHype = +22, Retroalimentacion = "Multa confirmada pero respaldo total de la hinchada." }
        },
        new() {
            Id = 25, Categoria = "Prensa", Titulo = "Entrevista con Rachel Nichols / Malika Andrews",
            Descripcion = "Te ofrecen entrevista estelar exclusiva para emitir en el descanso del partido nacional.",
            OpcionA = new() { Texto = "Aceptar y hablar de tus orígenes humildes", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = +4, DeltaQuimica = +4, DeltaHype = +24, Retroalimentacion = "Tu historia conmovió a millones de espectadores." },
            OpcionB = new() { Texto = "Declinar para concentrarte en el estiramiento", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +8, DeltaDT = +10, DeltaQuimica = +4, DeltaHype = -4, Retroalimentacion = "Cuerpo técnico feliz con tu concentración." }
        },
        new() {
            Id = 26, Categoria = "Prensa", Titulo = "Rumor Falso de Fiesta Nocturna",
            Descripcion = "Un blog publica que te vieron de fiesta antes del partido de ayer.",
            OpcionA = new() { Texto = "Desmentir con pruebas de tu presencia en el gimnasio", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +14, DeltaQuimica = +8, DeltaHype = +4, Retroalimentacion = "Rumor desmantelado; reputación intachable." },
            OpcionB = new() { Texto = "Ignorar las noticias falsas y no darles foco", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = 0, DeltaQuimica = -4, DeltaHype = 0, Retroalimentacion = "El tema se disipó con el paso de los días." }
        },
        new() {
            Id = 27, Categoria = "Prensa", Titulo = "Trash Talk del Rival en Medios",
            Descripcion = "El escolta rival dice en rueda de prensa que no puedes defender a nadie.",
            OpcionA = new() { Texto = "\"Hablaremos en la duela mañana a las 8 PM\"", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +10, DeltaQuimica = +8, DeltaHype = +18, Retroalimentacion = "El duelo particular generó récords de audiencia televisiva." },
            OpcionB = new() { Texto = "Contestar con estadísticas de robos", ImagenUrl = "https://images.unsplash.com/photo-1505666287802-9da132c02f41?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -4, DeltaQuimica = 0, DeltaHype = +10, Retroalimentacion = "Debate acalorado entre analistas." }
        },
        new() {
            Id = 28, Categoria = "Prensa", Titulo = "Podcast de Paul George / Draymond Green",
            Descripcion = "Te invitan como estrella especial a grabar un episodio de dos horas.",
            OpcionA = new() { Texto = "Ir y contar anécdotas divertidas de novato", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -8, DeltaDT = -4, DeltaQuimica = +4, DeltaHype = +22, Retroalimentacion = "Episodio número 1 en Spotify y YouTube." },
            OpcionB = new() { Texto = "Agradecer la invitación para otro momento", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +6, DeltaDT = +6, DeltaQuimica = 0, DeltaHype = -2, Retroalimentacion = "Cuidaste tus horas de descanso." }
        },
        new() {
            Id = 29, Categoria = "Prensa", Titulo = "La Foto del Poster Dunk",
            Descripcion = "Le hiciste una volcada salvaje al pívot rival y la foto es icónica.",
            OpcionA = new() { Texto = "Enmarcarla y firmarla para subasta benéfica", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -2, DeltaDT = +8, DeltaQuimica = +6, DeltaHype = +16, Retroalimentacion = "Recaudaste $50,000 para causas sociales locales." },
            OpcionB = new() { Texto = "Subirla a tus redes con burla al pívot", ImagenUrl = "https://images.unsplash.com/photo-1505666287802-9da132c02f41?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -10, DeltaQuimica = -4, DeltaHype = +24, Retroalimentacion = "El pívot rival prometió revancha con contacto físico duro." }
        },
        new() {
            Id = 30, Categoria = "Prensa", Titulo = "Votación de All-Star de los Fanáticos",
            Descripcion = "Estás a 20,000 votos de ser titular en el All-Star Game.",
            OpcionA = new() { Texto = "Hacer campaña activa con sorteo de camisetas", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = -2, DeltaQuimica = 0, DeltaHype = +26, Retroalimentacion = "¡Elegido titular en el quinteto del All-Star!" },
            OpcionB = new() { Texto = "Dejar que el juego hable por sí solo", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +4, DeltaDT = +8, DeltaQuimica = +4, DeltaHype = +4, Retroalimentacion = "Entraste por selección de los entrenadores." }
        },

        // 31 - 40: PREPARACIÓN FÍSICA Y LESIONES
        new() {
            Id = 31, Categoria = "Preparación", Titulo = "Sobrecarga en el Tendón Rotuliano",
            Descripcion = "Sientes pinchazo fuerte tras el entrenamiento previo al viaje.",
            OpcionA = new() { Texto = "Avisar a fisioterapeutas y guardar reposo", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +24, DeltaDT = +4, DeltaQuimica = 0, DeltaHype = -6, Retroalimentacion = "Evitaste un desgarro grave a tiempo." },
            OpcionB = new() { Texto = "Infiltrarse con analgésicos y jugar", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -35, DeltaDT = +14, DeltaQuimica = +10, DeltaHype = +14, Retroalimentacion = "Jugaste con dolor heroico pero arriesgaste tu tendón." }
        },
        new() {
            Id = 32, Categoria = "Preparación", Titulo = "Sesión de Crioterapia Matutina",
            Descripcion = "El club instala cabinas de recuperación a -110°C opcionales.",
            OpcionA = new() { Texto = "Madrugar e ingresar a la sesión fría", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +16, DeltaDT = +8, DeltaQuimica = +2, DeltaHype = 0, Retroalimentacion = "Músculos desinflamados al 100% para el back-to-back." },
            OpcionB = new() { Texto = "Dormir dos horas más en la cama", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +10, DeltaDT = 0, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Descanso clásico de cama." }
        },
        new() {
            Id = 33, Categoria = "Preparación", Titulo = "Entrenamiento Privado de Tiro con Chris Brickley",
            Descripcion = "El gurú del tiro en New York ofrece una sesión nocturna privada.",
            OpcionA = new() { Texto = "Pagar la sesión y pulir el muñequeo", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -16, DeltaDT = +8, DeltaQuimica = 0, DeltaHype = +12, Retroalimentacion = "Mejora visible en la fluidez de tu mecánica." },
            OpcionB = new() { Texto = "Apegarte a las rutinas oficiales del club", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +6, DeltaDT = +4, DeltaQuimica = +2, DeltaHype = 0, Retroalimentacion = "Progreso regular sin sobrecarga." }
        },
        new() {
            Id = 34, Categoria = "Preparación", Titulo = "Cambio de Dieta a Vegana / Alta Proteína",
            Descripcion = "Un nutricionista de élite plantea reestructurar tu plan de alimentación.",
            OpcionA = new() { Texto = "Adoptar la nueva dieta estricta", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +15, DeltaDT = +8, DeltaQuimica = 0, DeltaHype = +4, Retroalimentacion = "Mayor agilidad y velocidad en los desplazamientos." },
            OpcionB = new() { Texto = "Mantener tus hábitos alimenticios actuales", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = 0, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Cero alteraciones en tu peso corporal." }
        },
        new() {
            Id = 35, Categoria = "Preparación", Titulo = "El Gimnasio en Día Libre",
            Descripcion = "El calendario da 24 horas libres antes de una gira de tres partidos.",
            OpcionA = new() { Texto = "Hacer 500 tiros y trabajo de pesas", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -20, DeltaDT = +16, DeltaQuimica = +4, DeltaHype = +8, Retroalimentacion = "Ética de trabajo de Mamba Mentality." },
            OpcionB = new() { Texto = "Desconexión total con amigos y familia", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +25, DeltaDT = 0, DeltaQuimica = 0, DeltaHype = -2, Retroalimentacion = "Salud mental y energía recargadas." }
        },
        new() {
            Id = 36, Categoria = "Preparación", Titulo = "Estudio de Video Nocturno",
            Descripcion = "El rival de mañana cambia bloqueos en el 80% de posesiones.",
            OpcionA = new() { Texto = "Ver 2 horas de grabaciones tácticas", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = +16, DeltaQuimica = +6, DeltaHype = 0, Retroalimentacion = "Anticipaste 3 robos leyendo sus jugadas de pizarra." },
            OpcionB = new() { Texto = "Confiar en tus instintos de juego", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +6, DeltaDT = -6, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Tardaste un cuarto entero en descifrar su defensa." }
        },
        new() {
            Id = 37, Categoria = "Preparación", Titulo = "Dolor Lumbar en Playoffs",
            Descripcion = "Espasmos en la espalda baja a 4 horas del séptimo juego.",
            OpcionA = new() { Texto = "Tratamiento intenso con aguja seca y jugar", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -28, DeltaDT = +18, DeltaQuimica = +14, DeltaHype = +16, Retroalimentacion = "Jugaste con corazón de guerrero y ganaron el pase." },
            OpcionB = new() { Texto = "Pedir salir desde la banca con minutos limitados", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +10, DeltaDT = -8, DeltaQuimica = -6, DeltaHype = -10, Retroalimentacion = "Cuidaste la columna pero el equipo sintió tu ausencia." }
        },
        new() {
            Id = 38, Categoria = "Preparación", Titulo = "Uso de Botas de Compresión NormaTec",
            Descripcion = "En los vuelos de costa a costa, se sugiere usar botas presurizadas.",
            OpcionA = new() { Texto = "Usar las botas durante todo el vuelo", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +12, DeltaDT = +6, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Piernas ligeras listas para el aterrizaje." },
            OpcionB = new() { Texto = "Dormir de lado incómodo sin botas", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = -2, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Rigidez muscular en el primer cuarto." }
        },
        new() {
            Id = 39, Categoria = "Preparación", Titulo = "Práctica Opcional Tras Victoria",
            Descripcion = "Ganaron por 25 puntos anoche. La práctica de tiro de hoy no es obligatoria.",
            OpcionA = new() { Texto = "Ser el primero en llegar al gimnasio", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -12, DeltaDT = +18, DeltaQuimica = +10, DeltaHype = +6, Retroalimentacion = "Los entrenadores te aplaudieron como ejemplo de franquicia." },
            OpcionB = new() { Texto = "Aprovechar para dormir hasta tarde", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +16, DeltaDT = 0, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Descanso aprovechado." }
        },
        new() {
            Id = 40, Categoria = "Preparación", Titulo = "Fisura en un Dedo de la Mano de Tiro",
            Descripcion = "El dedo anular de tu mano hábil está inflamado por un balonazo.",
            OpcionA = new() { Texto = "Vendarlo junto al dedo medio y jugar", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -14, DeltaDT = +12, DeltaQuimica = +8, DeltaHype = +10, Retroalimentacion = "Ajustaste la muñeca y encestaste con vendaje." },
            OpcionB = new() { Texto = "Parar dos partidos para consolidar hueso", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +15, DeltaDT = -6, DeltaQuimica = -4, DeltaHype = -6, Retroalimentacion = "Curación perfecta sin secuelas crónicas." }
        },

        // 41 - 50: NEGOCIOS, CONTRATOS Y AGENTES
        new() {
            Id = 41, Categoria = "Negocios", Titulo = "Oferta de Extensión Multianual",
            Descripcion = "Tu franquicia ofrece $90M por 3 años antes de entrar a la agencia libre.",
            OpcionA = new() { Texto = "Firmar la extensión garantizada ya", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +16, DeltaQuimica = +16, DeltaHype = +12, Retroalimentacion = "Seguridad financiera asegurada de por vida." },
            OpcionB = new() { Texto = "Apostar por ti mismo y esperar al verano", ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -10, DeltaQuimica = -8, DeltaHype = +20, Retroalimentacion = "Apuesta de alto riesgo por un contrato Supermax." }
        },
        new() {
            Id = 42, Categoria = "Patrocinios", Titulo = "Portada de NBA 2K",
            Descripcion = "2K Sports te elige como imagen de portada global del videojuego.",
            OpcionA = new() { Texto = "Aceptar sesión de captura de movimiento (Mocap)", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -12, DeltaDT = -4, DeltaQuimica = 0, DeltaHype = +35, Retroalimentacion = "Tu cara en millones de consolas alrededor del mundo." },
            OpcionB = new() { Texto = "Rechazar por superstición de maldición de portada", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +4, DeltaDT = +4, DeltaQuimica = +2, DeltaHype = -4, Retroalimentacion = "Decisión conservadora pero 100% enfocada en ganar." }
        },
        new() {
            Id = 43, Categoria = "Negocios", Titulo = "El Agente Quiere Forzar un Traspaso",
            Descripcion = "Tu representante cree que en un mercado grande ganarías el doble en patrocinios.",
            OpcionA = new() { Texto = "Frenar a tu agente: \"Aquí estoy construyendo algo\"", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +20, DeltaQuimica = +20, DeltaHype = +4, Retroalimentacion = "La ciudad entera te declaró ídolo histórico." },
            OpcionB = new() { Texto = "Darle luz verde para negociar en secreto", ImagenUrl = "https://images.unsplash.com/photo-1505666287802-9da132c02f41?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = -25, DeltaQuimica = -20, DeltaHype = +16, Retroalimentacion = "Filtraciones tóxicas en ESPN arruinaron el vestuario." }
        },
        new() {
            Id = 44, Categoria = "Patrocinios", Titulo = "Comercial de Bebida Energética",
            Descripcion = "Gatorade te ofrece un comercial mundial junto a Luka Dončić y Anthony Edwards.",
            OpcionA = new() { Texto = "Grabar en el día de descanso", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -14, DeltaDT = -4, DeltaQuimica = -2, DeltaHype = +28, Retroalimentacion = "Spot televisivo de impacto cultural enorme." },
            OpcionB = new() { Texto = "Postergar el rodaje al verano", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +8, DeltaDT = +8, DeltaQuimica = +4, DeltaHype = -4, Retroalimentacion = "El entrenador aplaudió tus prioridades competitivas." }
        },
        new() {
            Id = 45, Categoria = "Negocios", Titulo = "Inversión en Equipo de Fútbol / Pickleball",
            Descripcion = "Se abre una ronda de inversión para ser copropietario de una franquicia deportiva.",
            OpcionA = new() { Texto = "Invertir $2.5M y ser accionista público", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = 0, DeltaQuimica = 0, DeltaHype = +18, Retroalimentacion = "Perfil de empresario a lo LeBron James consolidado." },
            OpcionB = new() { Texto = "Guardar tu dinero en fondos seguros", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +2, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Cero distracciones financieras." }
        },
        new() {
            Id = 46, Categoria = "Patrocinios", Titulo = "Diseño de la Camiseta City Edition",
            Descripcion = "La marca oficial te pide seleccionar el diseño retro de la camiseta de la ciudad.",
            OpcionA = new() { Texto = "Elegir diseño clásico con colores vintage", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -2, DeltaDT = +4, DeltaQuimica = +6, DeltaHype = +20, Retroalimentacion = "La camiseta más vendida de la temporada en la tienda NBA." },
            OpcionB = new() { Texto = "Dejar que marketing elija el diseño", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = 0, DeltaQuimica = 0, DeltaHype = +4, Retroalimentacion = "Ventas moderadas estándar." }
        },
        new() {
            Id = 47, Categoria = "Negocios", Titulo = "Clínic de Baloncesto Infantil en tu País",
            Descripcion = "Tu fundación propone abrir una academia gratuita en tu ciudad natal.",
            OpcionA = new() { Texto = "Financiar el campus y dar clases en persona", ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -10, DeltaDT = +8, DeltaQuimica = +8, DeltaHype = +22, Retroalimentacion = "Héroe nacional indiscutido para los niños de tu país." },
            OpcionB = new() { Texto = "Enviar donación sin viajar", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +6, DeltaDT = +2, DeltaQuimica = 0, DeltaHype = +6, Retroalimentacion = "Donación económica bien recibida." }
        },
        new() {
            Id = 48, Categoria = "Patrocinios", Titulo = "Sesión de Zapatillas Personalizadas con Diseñador",
            Descripcion = "Un diseñador urbano plantea zapatillas inspiradas en tu estilo pixel/arcade.",
            OpcionA = new() { Texto = "Lanzar edición limitada de 1,000 pares", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -6, DeltaDT = -2, DeltaQuimica = 0, DeltaHype = +24, Retroalimentacion = "Agotadas en 40 segundos en internet." },
            OpcionB = new() { Texto = "Mantener solo las zapatillas de juego regulares", ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80", DeltaEnergia = +4, DeltaDT = +2, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Cero distracciones de moda." }
        },
        new() {
            Id = 49, Categoria = "Negocios", Titulo = "La Gerencia Consulta por un Fichaje",
            Descripcion = "El General Manager te pide opinión secreta antes de traspasar por un veterano.",
            OpcionA = new() { Texto = "\"Fíchalo de inmediato, nos dará el campeonato\"", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -2, DeltaDT = +16, DeltaQuimica = +12, DeltaHype = +8, Retroalimentacion = "El traspaso se concretó elevando el nivel del roster." },
            OpcionB = new() { Texto = "\"Prefiero no opinar, ese es su trabajo gerencial\"", ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +4, DeltaQuimica = 0, DeltaHype = 0, Retroalimentacion = "Mantuviste tu distancia con la oficina central." }
        },
        new() {
            Id = 50, Categoria = "Negocios", Titulo = "El Nuevo Contrato de Transmisión de la NBA",
            Descripcion = "Sube el tope salarial un 10%. Tu representante pide renegociar bonos.",
            OpcionA = new() { Texto = "Exigir incentivos por llegar a Finales y ser MVP", ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80", DeltaEnergia = -4, DeltaDT = -4, DeltaQuimica = 0, DeltaHype = +14, Retroalimentacion = "Bonos de $5,000,000 firmados en la cláusula." },
            OpcionB = new() { Texto = "Aceptar condiciones estándar sin conflicto", ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80", DeltaEnergia = 0, DeltaDT = +10, DeltaQuimica = +6, DeltaHype = 0, Retroalimentacion = "Relación idílica con la directiva." }
        }
    };

    public static List<Dilema> ObtenerMazoBarajado()
    {
        return DilemasBase.OrderBy(_ => Random.Shared.Next()).ToList();
    }
}
