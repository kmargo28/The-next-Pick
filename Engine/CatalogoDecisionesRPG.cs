using TheNextPick.Models;

namespace TheNextPick.Engine;

public static class CatalogoDecisionesRPG
{
    private static readonly List<int> IdsDilemasVistosEnTemporada = new();
    public static readonly List<int> ArcosDesencadenados = new();

    public static void IniciarNuevaTemporada() => IdsDilemasVistosEnTemporada.Clear();
    public static void ReiniciarMemoriaHistorica()
    {
        IdsDilemasVistosEnTemporada.Clear();
        ArcosDesencadenados.Clear();
    }

    // GENERADOR Y BANCO DE MÁS DE 50 EVENTOS CON IMÁGENES DEDICADAS
    private static readonly List<DilemaRPG> BaseDeCartas = GenerarBancoCompleto();

    private static List<DilemaRPG> GenerarBancoCompleto()
    {
        var lista = new List<DilemaRPG>();

        // Imágenes temáticas de alta calidad
        var imagenes = new Dictionary<int, string>
        {
            { 1, "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=800&auto=format&fit=crop&q=80" }, // Vestuario / Charla
            { 2, "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=800&auto=format&fit=crop&q=80" }, // Cancha vacía
            { 3, "https://images.unsplash.com/photo-1519766304817-4f37bda74a29?w=800&auto=format&fit=crop&q=80" }, // Tiro solo
            { 4, "https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=800&auto=format&fit=crop&q=80" }, // Focos arena
            { 5, "https://images.unsplash.com/photo-1552346154-21d32810aba3?w=800&auto=format&fit=crop&q=80" }, // Sneakers / Zapatillas
            { 6, "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=800&auto=format&fit=crop&q=80" }, // Viaje / Vuelo nocturno
            { 7, "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=800&auto=format&fit=crop&q=80" }, // Micrófonos prensa
            { 8, "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=800&auto=format&fit=crop&q=80" }, // Pesas gimnasio
            { 9, "https://images.unsplash.com/photo-1576091160399-112ba8d25d1d?w=800&auto=format&fit=crop&q=80" }, // Médico / Fisioterapia
            { 10, "https://images.unsplash.com/photo-1450133064473-71024230f91b?w=800&auto=format&fit=crop&q=80" }, // Firma / Oficina
            { 11, "https://images.unsplash.com/photo-1508098682722-e99c43a406b2?w=800&auto=format&fit=crop&q=80" }, // Árbitro silbato
            { 12, "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=800&auto=format&fit=crop&q=80" }, // Cancha callejera
            { 13, "https://images.unsplash.com/photo-1504450758481-7338eba7524a?w=800&auto=format&fit=crop&q=80" }, // Aro primer plano
            { 14, "https://images.unsplash.com/photo-1611162617474-5b21e879e113?w=800&auto=format&fit=crop&q=80" }, // Móvil / Redes
            { 15, "https://images.unsplash.com/photo-1595950653106-6c9ebd614d3a?w=800&auto=format&fit=crop&q=80" }, // Tienda deportiva
            { 16, "https://images.unsplash.com/photo-1514525253161-7a46d19cd819?w=800&auto=format&fit=crop&q=80" }, // Club nocturno
            { 17, "https://images.unsplash.com/photo-1508802514207-6f68c3741639?w=800&auto=format&fit=crop&q=80" }, // Pabellón lleno
            { 18, "https://images.unsplash.com/photo-1569517282132-25d22f4573e6?w=800&auto=format&fit=crop&q=80" }, // Trofeos
            { 19, "https://images.unsplash.com/photo-1617814076367-b759c7d7e738?w=800&auto=format&fit=crop&q=80" }, // Auto deportivo
            { 20, "https://images.unsplash.com/photo-1517649763962-0c623266ddc0?w=800&auto=format&fit=crop&q=80" }, // Pizarra táctica
            { 21, "https://images.unsplash.com/photo-1587280501635-68a0e82cd5ff?w=800&auto=format&fit=crop&q=80" }, // Sauna / Recuperación
            { 22, "https://images.unsplash.com/photo-1518611012118-696072aa579a?w=800&auto=format&fit=crop&q=80" }, // Entrenamiento aeróbico
            { 23, "https://images.unsplash.com/photo-1509198397868-475647b2a1e5?w=800&auto=format&fit=crop&q=80" }, // Salto hacia la canasta
            { 24, "https://images.unsplash.com/photo-1533107862482-0e6974b06ec4?w=800&auto=format&fit=crop&q=80" }, // Balón en parqué
            { 25, "https://images.unsplash.com/photo-1517838277536-f5f99be501cd?w=800&auto=format&fit=crop&q=80" }, // Barra olímpica
            { 26, "https://images.unsplash.com/photo-1540497077202-7c8a3999166f?w=800&auto=format&fit=crop&q=80" }, // Máquinas de gimnasio
            { 27, "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?w=800&auto=format&fit=crop&q=80" }, // Balón fútbol / deporte
            { 28, "https://images.unsplash.com/photo-1511512578047-dfb367046420?w=800&auto=format&fit=crop&q=80" }, // Videojuegos en hotel
            { 29, "https://images.unsplash.com/photo-1556817411-31ae72fa3ea0?w=800&auto=format&fit=crop&q=80" }, // Guantes / Bloqueo
            { 30, "https://images.unsplash.com/photo-1571019614242-c5c5dee9f50b?w=800&auto=format&fit=crop&q=80" }, // Abdominales / Core
            { 31, "https://images.unsplash.com/photo-1522071820081-009f0129c71c?w=800&auto=format&fit=crop&q=80" }, // Reunión de equipo
            { 32, "https://images.unsplash.com/photo-1551836022-d5d88e9218df?w=800&auto=format&fit=crop&q=80" }, // Entrevista personal
            { 33, "https://images.unsplash.com/photo-1526304640581-d334cdbbf45e?w=800&auto=format&fit=crop&q=80" }, // Dinero y cheques
            { 34, "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=800&auto=format&fit=crop&q=80" }, // Multitud en estadio
            { 35, "https://images.unsplash.com/photo-1507679799987-c73779587ccf?w=800&auto=format&fit=crop&q=80" }, // Traje formal / Negocios
            { 36, "https://images.unsplash.com/photo-1563245372-f21724e3856d?w=800&auto=format&fit=crop&q=80" }, // Postura de tiro
            { 37, "https://images.unsplash.com/photo-1521737604893-d14cc237f11d?w=800&auto=format&fit=crop&q=80" }, // Apretones de mano
            { 38, "https://images.unsplash.com/photo-1517466787929-bc90951d0974?w=800&auto=format&fit=crop&q=80" }, // Calentamiento en cancha
            { 39, "https://images.unsplash.com/photo-1577471488278-16eec37ffcc2?w=800&auto=format&fit=crop&q=80" }, // Balón en el aire
            { 40, "https://images.unsplash.com/photo-1584466977773-e625c37cdd50?w=800&auto=format&fit=crop&q=80" }, // Fisioterapeuta aplicando vendaje
            { 41, "https://images.unsplash.com/photo-1529156069898-49953e39b3ac?w=800&auto=format&fit=crop&q=80" }, // Amigos / Grupo
            { 42, "https://images.unsplash.com/photo-1506744038136-46273834b3fb?w=800&auto=format&fit=crop&q=80" }, // Paisaje / Desconexión
            { 43, "https://images.unsplash.com/photo-1517245386807-bb43f82c33c4?w=800&auto=format&fit=crop&q=80" }, // Trabajo en pizarra
            { 44, "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=800&auto=format&fit=crop&q=80" }, // Conducción nocturna
            { 45, "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=800&auto=format&fit=crop&q=80" }, // Rascacielos / Sede NBA
            { 46, "https://images.unsplash.com/photo-1559526324-4b87b5e36e44?w=800&auto=format&fit=crop&q=80" }, // Donaciones / Comunidad
            { 47, "https://images.unsplash.com/photo-1507035895480-2b3156c31fc8?w=800&auto=format&fit=crop&q=80" }, // Desayuno de atleta
            { 48, "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?w=800&auto=format&fit=crop&q=80" }, // Estiramiento en vestuario
            { 49, "https://images.unsplash.com/photo-1516726817505-f5ed825624d8?w=800&auto=format&fit=crop&q=80" }, // Mirada de concentración
            { 50, "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=800&auto=format&fit=crop&q=80" }, // Fuerza y potencia
            { 51, "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=800&auto=format&fit=crop&q=80" }, // Despacho del GM
            { 52, "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=800&auto=format&fit=crop&q=80" }  // Campeón en la duela
        };

        // Generamos dinámicamente 52 registros narrativos
        string[] contextosNormales = new[]
        {
            "El DT pide ajustar el ritmo de posesión para reducir pérdidas.",
            "El capitán veterano te corrige una rotación defensiva en la sesión de vídeo.",
            "Sesión voluntaria de 300 triples a primera hora de la mañana.",
            "Asignación defensiva de marcar a la estrella perimetral del rival.",
            "Consejo financiero: invertir en fondos o celebrar con gastos personales.",
            "Segundo partido en dos noches tras un vuelo de madrugada.",
            "Un compañero novato te pide consejos para ganarse minutos.",
            "El preparador físico detecta fatiga en tus gemelos y sugiere descanso.",
            "Reunión táctica para preparar la defensa del bloqueo directo.",
            "Un aficionado local te pide una foto mientras cenas en la ciudad.",
            "La prensa local cuestiona tu selección de tiro tras un mal partido.",
            "Práctica de tiros libres bajo la mirada estricta del cuerpo técnico.",
            "El veterano del equipo organiza una cena grupal para unir al plantel.",
            "Oferta para dar una charla motivacional en un colegio comunitario.",
            "El entrenador asistente te propone repasar jugadas en su tableta.",
            "Sobrecarga en los tobillos tras una semana de cuatro partidos.",
            "Discusión acalorada en el vestuario tras desperdiciar una ventaja de 15.",
            "Un scout rival halaga tu juego de pies ante los periodistas.",
            "Trabajo de pesas enfocado en la potencia del tren inferior.",
            "Decisión sobre participar en un podcast deportivo de máxima audiencia."
        };

        for (int i = 1; i <= 40; i++)
        {
            int numImg = i;
            string textoContexto = contextosNormales[(i - 1) % contextosNormales.Length];

            lista.Add(new DilemaRPG
            {
                Id = i,
                Rareza = RarezaCarta.Normal,
                ContextoTipo = (TipoContextoDecision)(i % 4),
                BannerTag = $"EVENTO #{i}",
                Titulo = $"DECISIÓN PROFESIONAL #{i}",
                Contexto = textoContexto,
                ImageUrl = imagenes.ContainsKey(numImg) ? imagenes[numImg] : imagenes[1],
                MinTemporada = 1,
                Opciones = new()
                {
                    new()
                    {
                        Titulo = "ENFOQUE DISCIPLINADO Y PROFESIONAL",
                        Descripcion = "Priorizar las exigencias del cuerpo técnico y la solidez del equipo.",
                        BadgesPositivas = new() { "+10 CONFIANZA DT", "+2 DEFENSA" },
                        BadgesPeligro = new() { "⚡ -10 ENERGÍA" },
                        DeltaConfianzaDT = 10, DeltaEnergia = -10, DeltaDefensa = 2,
                        Retroalimentacion = "El cuerpo técnico tomó nota positiva de tu seriedad."
                    },
                    new()
                    {
                        Titulo = "VÍA INDIVIDUAL Y CREATIVA",
                        Descripcion = "Buscar el lucimiento personal y ganar notoriedad en redes.",
                        BadgesPositivas = new() { "+15 HYPE", "+2 TIRO EXT" },
                        BadgesPeligro = new() { "-10 CONFIANZA DT" },
                        DeltaHype = 15, DeltaConfianzaDT = -10, DeltaTiroExterior = 2,
                        Retroalimentacion = "Ganaste seguidores en redes, pero el DT te pidió más concentración."
                    }
                }
            });
        }

        // 6 EVENTOS RAROS (ID 701 a 706)
        for (int r = 1; r <= 6; r++)
        {
            int idRara = 700 + r;
            int imgId = 40 + r;
            lista.Add(new DilemaRPG
            {
                Id = idRara,
                Rareza = RarezaCarta.Rara,
                ContextoTipo = TipoContextoDecision.DiaLibre,
                BannerTag = "CLÍNICA ESPECIALIZADA",
                Titulo = $"OPORTUNIDAD EXCLUSIVA #{r}",
                Contexto = "Una leyenda de la NBA te invita a una sesión privada para enseñarte fundamentos de élite.",
                ImageUrl = imagenes.ContainsKey(imgId) ? imagenes[imgId] : imagenes[7],
                MinTemporada = 1,
                Opciones = new()
                {
                    new()
                    {
                        Titulo = "ENTRENAR A FONDO CON LA LEYENDA",
                        Descripcion = "Doble jornada de tiro y lectura de juego.",
                        BadgesPositivas = new() { "+4 PASE", "+3 TIRO MED", "+15 DT" },
                        BadgesPeligro = new() { "⚡ -20 ENERGÍA" },
                        DeltaPase = 4, DeltaTiroMedia = 3, DeltaConfianzaDT = 15, DeltaEnergia = -20,
                        Retroalimentacion = "Tu técnica de pase y tiro dio un salto de calidad."
                    },
                    new()
                    {
                        Titulo = "DESCANSAR Y CUIDAR EL FÍSICO",
                        Descripcion = "Desconectarte y recargar energía.",
                        BadgesPositivas = new() { "+30 ENERGÍA" },
                        BadgesPeligro = new() { "-5 DT" },
                        DeltaEnergia = 30, DeltaConfianzaDT = -5,
                        Retroalimentacion = "Llegas completamente recuperado al siguiente juego."
                    }
                }
            });
        }

        // 4 EVENTOS ÉPICOS (ID 801 a 804)
        for (int e = 1; e <= 4; e++)
        {
            int idEpica = 800 + e;
            int imgId = 46 + e;
            lista.Add(new DilemaRPG
            {
                Id = idEpica,
                Rareza = RarezaCarta.Epica,
                ContextoTipo = TipoContextoDecision.DiaDePartido,
                BannerTag = "MOMENTO CLAVE DE TEMPORADA",
                Titulo = $"DESAFÍO ÉPICO #{e}",
                Contexto = "El líder de la conferencia plantea un cerrojo defensivo especial para frenarte en directo por televisión nacional.",
                ImageUrl = imagenes.ContainsKey(imgId) ? imagenes[imgId] : imagenes[11],
                MinTemporada = 1,
                Opciones = new()
                {
                    new()
                    {
                        Titulo = "JUGARTE LA POSESIÓN CLUTCH (MINIJUEGO)",
                        Descripcion = "Buscar el tiro decisivo ante su mejor defensor.",
                        BadgesPositivas = new() { "🏀 MINIJUEGO CLUTCH", "+5 TIRO EXT", "+30 HYPE" },
                        BadgesPeligro = new() { "⚠️ SI FALLAS: -22 CONF DT / -15 HYPE" },
                        ActivaMinijuegoEnCancha = true,
                        DeltaTiroExterior = 5, DeltaEnergia = -20, DeltaHype = 30,
                        Retroalimentacion = "Demostraste sangre fría ante la mejor defensa de la liga."
                    },
                    new()
                    {
                        Titulo = "MOVER EL BALÓN Y CONFIAR EN EL SISTEMA",
                        Descripcion = "Habilitar al compañero desmarcado en la esquina.",
                        BadgesPositivas = new() { "+5 PASE", "+20 QUÍMICA", "+15 DT" },
                        BadgesPeligro = new() { "-10 HYPE" },
                        DeltaPase = 5, DeltaQuimica = 20, DeltaConfianzaDT = 15, DeltaHype = -10,
                        Retroalimentacion = "Lectura generosa que selló la victoria del equipo."
                    }
                }
            });
        }

        // 2 EVENTOS LEGENDARIOS (ID 901 y 902)
        lista.Add(new DilemaRPG
        {
            Id = 901,
            Rareza = RarezaCarta.Legendaria,
            ContextoTipo = TipoContextoDecision.OficinaYTraspasos,
            BannerTag = "EL RELEVO DEFINITIVO",
            Titulo = "LAS LLAVES DE LA FRANQUICIA",
            Contexto = "La gerencia decide darte el rol protagónico absoluto del proyecto deportivo tras una reunión a puerta cerrada.",
            ImageUrl = imagenes[51],
            MinTemporada = 1,
            Opciones = new()
            {
                new()
                {
                    Titulo = "ASUMIR EL ESTATUS DE JUGADOR FRANQUICIA",
                    Descripcion = "Aceptar el protagonismo total de la ofensiva (+4 en cuatro atributos).",
                    BadgesPositivas = new() { "👑 JUGADOR FRANQUICIA", "+4 T.EXT", "+4 T.MED", "+4 PASE", "+4 ATLETISMO", "+50 HYPE" },
                    BadgesPeligro = new() { "PRESIÓN MÁXIMA DE ANILLO" },
                    DeltaTiroExterior = 4, DeltaTiroMedia = 4, DeltaPase = 4, DeltaAtletismo = 4,
                    DeltaHype = 50, DeltaConfianzaDT = 30,
                    ConvierteEnJugadorFranquicia = true,
                    Retroalimentacion = "¡HISTÓRICO! Eres el líder de la franquicia y tu rendimiento da un salto colosal."
                },
                new()
                {
                    Titulo = "RECHAZAR LA PRESIÓN EN RUEDA DE PRENSA",
                    Descripcion = "Declarar ante los medios que aún no estás listo.",
                    BadgesPositivas = new() { "SIN BENEFICIO DE ATRIBUTOS" },
                    BadgesPeligro = new() { "-40 HYPE", "-30 CONFIANZA DT", "⚠️ RIESGO TRASPASO" },
                    DeltaHype = -40, DeltaConfianzaDT = -30, AumentaRiesgoTraspaso = true,
                    Retroalimentacion = "La directiva y los aficionados quedaron decepcionados por la falta de carácter."
                }
            }
        });

        lista.Add(new DilemaRPG
        {
            Id = 902,
            Rareza = RarezaCarta.Legendaria,
            ContextoTipo = TipoContextoDecision.DiaLibre,
            BannerTag = "BIOMECÁNICA DE ÉLITE",
            Titulo = "EL LABORATORIO DE RENDIMIENTO",
            Contexto = "Un centro biomédico de última generación te ofrece un protocolo intensivo para potenciar tu tren inferior y reflejos.",
            ImageUrl = imagenes[52],
            MinTemporada = 1,
            Opciones = new()
            {
                new()
                {
                    Titulo = "COMPLETAR EL PROTOCOLO INTEGRAL",
                    Descripcion = "Acondicionamiento muscular biomecánico (+4 atletismo, defensa y rebote).",
                    BadgesPositivas = new() { "+4 ATLETISMO", "+4 DEFENSA", "+4 REBOTE " },
                    BadgesPeligro = new() { "💰 -$300K GASTOS", "⚡ -25 ENERGÍA" },
                    DeltaAtletismo = 4, DeltaDefensa = 4, DeltaRebote = 4,
                    DeltaDinero = -300000, DeltaEnergia = -25,
                    Retroalimentacion = "Tu velocidad y salto vertical alcanzaron registros de élite."
                },
                new()
                {
                    Titulo = "FORZAR PESOS SIN SUPERVISIÓN MÉDICA",
                    Descripcion = "Entrenar al límite por tu cuenta.",
                    BadgesPositivas = new() { "SIN BENEFICIO DE ATRIBUTOS" },
                    BadgesPeligro = new() { "-4 ATLETISMO", "-4 DEFENSA", "⚠️ LESIÓN SEVERA" },
                    DeltaAtletismo = -4, DeltaDefensa = -4, DeltaEnergia = -40, ProbabilidadLesionDirecta = 75,
                    Retroalimentacion = "Sobrecarga severa y microrrotura muscular. Fuera de las canchas."
                }
            }
        });

        return lista;
    }

    public static DilemaRPG ObtenerSiguienteDilema(int temporada, int ovr, int pick, int hype, bool enRiesgo)
    {
        // 75% Normales | 15% Raras | 8% Épicas | 2% Legendarias
        int tirada = Random.Shared.Next(1, 101);
        RarezaCarta rarezaObjetivo = tirada switch
        {
            <= 75 => RarezaCarta.Normal,
            <= 90 => RarezaCarta.Rara,
            <= 98 => RarezaCarta.Epica,
            _ => RarezaCarta.Legendaria
        };

        var candidatos = BaseDeCartas
            .Where(c => c.Rareza == rarezaObjetivo 
                     && c.EsCompatible(temporada, ovr, pick, hype, enRiesgo, ArcosDesencadenados) 
                     && !IdsDilemasVistosEnTemporada.Contains(c.Id))
            .ToList();

        if (!candidatos.Any())
        {
            candidatos = BaseDeCartas
                .Where(c => c.Rareza == RarezaCarta.Normal 
                         && c.EsCompatible(temporada, ovr, pick, hype, enRiesgo, ArcosDesencadenados)
                         && !IdsDilemasVistosEnTemporada.Contains(c.Id))
                .ToList();

            if (!candidatos.Any())
            {
                IdsDilemasVistosEnTemporada.Clear();
                candidatos = BaseDeCartas.Where(c => c.Rareza == RarezaCarta.Normal).ToList();
            }
        }

        var elegido = candidatos[Random.Shared.Next(candidatos.Count)];
        IdsDilemasVistosEnTemporada.Add(elegido.Id);
        return elegido;
    }
}
