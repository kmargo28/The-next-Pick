using TheNextPick.Models;

namespace TheNextPick.Engine;

public static class CatalogoDecisionesRPG
{
    private static readonly List<int> IdsDilemasVistos = new();

    private static readonly List<DilemaRPG> BaseDeCartas = new()
    {
        // ====================================================================
        // AÑOS 1 Y 2: PICK TARDÍO / SEGUNDA RONDA (#15 AL #60)
        // ====================================================================
        new()
        {
            Id = 1,
            Rareza = RarezaCarta.Comun,
            BannerTag = "PASAN COSAS",
            Titulo = "NO ESTÁS JUGANDO",
            Contexto = "Llevas medio año sumando migajas. Podés pedir salir a préstamo a la filial de G-League para jugar todo, o quedarte a pelear el puesto donde estás.",
            ImageUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80",
            MinTemporada = 1, MaxTemporada = 2,
            MinPickDraft = 15, MaxPickDraft = 60,
            Opciones = new()
            {
                new()
                {
                    Titulo = "PEDIR SALIR A PRÉSTAMO",
                    Descripcion = "Minutos garantizados. Bajás de categoría temporalmente para tener la pelota.",
                    BadgesPositivas = new() { "+3 TIRO EXTERIOR", "+3 VELOCIDAD" },
                    BadgesPeligro = new() { "⚡ -15 ENERGÍA", "-5 HYPE" },
                    DeltaTiroExterior = 3, DeltaAtletismo = 3, DeltaEnergia = -15, DeltaHype = -5, DeltaConfianzaDT = 10,
                    Retroalimentacion = "Promediaste 24 puntos en la filial. Vuelves con ritmo competitivo sólido."
                },
                new()
                {
                    Titulo = "QUEDARTE A PELEARLA",
                    Descripcion = "Defender al titular en cada práctica para forzar la consideración del cuerpo técnico.",
                    BadgesPositivas = new() { "+4 DEFENSA", "+15 CONFIANZA DT" },
                    BadgesPeligro = new() { "⚠️ PODÉS PASAR OTRO AÑO MIRANDO" },
                    DeltaDefensa = 4, DeltaConfianzaDT = 15, DeltaEnergia = -10,
                    Retroalimentacion = "El entrenador valoró tu constancia y te otorga 12 minutos en la rotación regular."
                }
            }
        },

        new()
        {
            Id = 2,
            Rareza = RarezaCarta.Rara,
            BannerTag = "PRETEMPORADA ROOKIE",
            Titulo = "HORAS EXTRAS EN EL GIMNASIO",
            Contexto = "El preparador físico te propone un plan de choque antes del inicio del calendario regular.",
            ImageUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80",
            MinTemporada = 1, MaxTemporada = 2,
            Opciones = new()
            {
                new()
                {
                    Titulo = "PIERNAS NUEVAS TRAS CADA PRÁCTICA",
                    Descripcion = "Doble turno de pesas y sentadillas para ganar aceleración y potencia.",
                    BadgesPositivas = new() { "+4 ATLETISMO", "+3 REBOTE" },
                    BadgesPeligro = new() { "⚡ -20 ENERGÍA" },
                    DeltaAtletismo = 4, DeltaRebote = 3, DeltaEnergia = -20,
                    Retroalimentacion = "Sientes las piernas livianas y explosivas en los contraataques."
                },
                new()
                {
                    Titulo = "MÉTODO REPETITIVO DE LANZAMIENTO",
                    Descripcion = "Aparecer donde va a caer la pelota ensayando el tiro mil veces.",
                    BadgesPositivas = new() { "+4 TIRO EXTERIOR", "+3 TIRO MEDIA" },
                    BadgesPeligro = new() { "⚡ -15 ENERGÍA" },
                    DeltaTiroExterior = 4, DeltaTiroMedia = 3, DeltaEnergia = -15,
                    Retroalimentacion = "Mecánica limpia y automática desde las esquinas de la cancha."
                }
            }
        },

        new()
        {
            Id = 3,
            Rareza = RarezaCarta.Epica,
            BannerTag = "MOMENTO CLUTCH",
            Titulo = "LA POSESIÓN FINAL DEL TERCER CUARTO",
            Contexto = "Quedan 18 segundos de posesión. La estrella rival no para de anotar y el DT pide a gritos que alguien dé la cara en el tabloncillo.",
            ImageUrl = "https://images.unsplash.com/photo-1519766304817-4f37bda74a29?w=700&auto=format&fit=crop&q=80",
            MinTemporada = 1, MaxTemporada = 2,
            Opciones = new()
            {
                new()
                {
                    Titulo = "PEDIR EL BALÓN Y ASUMIR EL TIRO (MINIJUEGO)",
                    Descripcion = "Entrar a la cancha y jugar el tiro arcade bajo presión.",
                    BadgesPositivas = new() { "🏀 MINIJUEGO EN CANCHA", "+25 HYPE", "+15 DT" },
                    BadgesPeligro = new() { "⚠️ SI FALLAS: RIESGO TRASPASO" },
                    ActivaMinijuegoEnCancha = true,
                    AumentaRiesgoTraspaso = true,
                    Retroalimentacion = "El balón está en tus manos ante 20,000 fanáticos rugiendo."
                },
                new()
                {
                    Titulo = "PERMANECER EN EL BANCO SIN FORZAR",
                    Descripcion = "Dejar que los veteranos cierren el cuarto sin arriesgar tu posición.",
                    BadgesPositivas = new() { "+5 ENERGÍA" },
                    BadgesPeligro = new() { "-10 HYPE" },
                    DeltaEnergia = 5, DeltaHype = -10,
                    Retroalimentacion = "El equipo mantuvo la renta sin alterar tu posición en la rotación."
                }
            }
        },

        // ====================================================================
        // AÑOS 1 Y 2: TOP 3 PICKS (PRESIÓN MEDIÁTICA INMEDIATA)
        // ====================================================================
        new()
        {
            Id = 4,
            Rareza = RarezaCarta.Legendaria,
            BannerTag = "EVENTO DE LOTERÍA",
            Titulo = "LA SOMBRA DEL PICK NÚMERO 1",
            Contexto = "Como Top Pick del Draft, las cadenas de televisión nacional transmiten cada uno de tus errores. La afición exige salvar a la franquicia hoy mismo.",
            ImageUrl = "https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=700&auto=format&fit=crop&q=80",
            MinTemporada = 1, MaxTemporada = 2,
            MinPickDraft = 1, MaxPickDraft = 3,
            Opciones = new()
            {
                new()
                {
                    Titulo = "ASUMIR EL EMPAREJAMIENTO ESTELAR (MINIJUEGO)",
                    Descripcion = "Marcar posesión a posesión al líder anotador del oponente.",
                    BadgesPositivas = new() { "🏀 MINIJUEGO", "+5 OVR GLOBAL", "+35 HYPE" },
                    BadgesPeligro = new() { "⚠️ RIESGO TRASPASO AL FALLAR", "🏥 RIESGO LESIÓN" },
                    ActivaMinijuegoEnCancha = true,
                    ProbabilidadLesionDirecta = 25,
                    AumentaRiesgoTraspaso = true,
                    Retroalimentacion = "Duelo estelar televisado a nivel mundial."
                },
                new()
                {
                    Titulo = "DECLARAR CON HUMILDAD ANTE LOS MICRÓFONOS",
                    Descripcion = "\"Ellos son un equipo armado, nosotros estamos construyendo química\".",
                    BadgesPositivas = new() { "+15 CONFIANZA DT", "+10 QUÍMICA" },
                    BadgesPeligro = new() { "-5 HYPE" },
                    DeltaConfianzaDT = 15, DeltaHype = -5,
                    Retroalimentacion = "El DT elogió tu temple para calmar las exigencias de la prensa."
                }
            }
        },

        new()
        {
            Id = 5,
            Rareza = RarezaCarta.Comun,
            BannerTag = "CONTRATOS NOVATO",
            Titulo = "EL CONTRATO COMERCIAL DE CALZADO",
            Contexto = "Varias marcas deportivas buscan tu firma. Como novato, ninguna marca te ofrece zapatillas con tu propio nombre todavía; ofrecen calzado de línea estándar con incentivos.",
            ImageUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80",
            MinTemporada = 1, MaxTemporada = 2,
            Opciones = new()
            {
                new()
                {
                    Titulo = "FIRMAR CON EL GIGANTE NORTEAMERICANO",
                    Descripcion = "Usar zapatillas comerciales de catálogo con bono por minutos jugados.",
                    BadgesPositivas = new() { "+10 HYPE", "💰 +$500K FONDOS" },
                    BadgesPeligro = new() { "SIN ZAPATILLA PROPIA AÚN" },
                    DeltaHype = 10, DeltaConfianzaDT = 5,
                    Retroalimentacion = "Firmaste por 2 años como embajador de línea básica de la marca."
                },
                new()
                {
                    Titulo = "APOSTAR POR UNA MARCA EMERGENTE ASIÁTICA",
                    Descripcion = "Te pagan el triple de dinero directo a cambio de jugar en giras de pretemporada.",
                    BadgesPositivas = new() { "💰 +$1.5M FONDOS", "+5 ENERGÍA" },
                    BadgesPeligro = new() { "-5 HYPE EN EE.UU." },
                    DeltaHype = -5, DeltaEnergia = 5,
                    Retroalimentacion = "Tu nombre es un éxito comercial en Shanghái y Tokio."
                }
            }
        },

        // ====================================================================
        // AÑOS 3 EN ADELANTE: PRIME, NEGOCIOS Y ESTRELLATO
        // ====================================================================
        new()
        {
            Id = 6,
            Rareza = RarezaCarta.Rara,
            BannerTag = "NEGOCIOS & SNEAKERS",
            Titulo = "LA PRIMERA LÍNEA DE ZAPATILLAS SIGNATURE",
            Contexto = "Tras consolidarte con más de 85 de OVR, la marca presenta tu primer prototipo oficial con tu logo y número dorsal bordados.",
            ImageUrl = "https://images.unsplash.com/photo-1552346154-21d32810aba3?w=700&auto=format&fit=crop&q=80",
            MinTemporada = 3, MinOVR = 85,
            Opciones = new()
            {
                new()
                {
                    Titulo = "LANZAR MODELO PERFORMANCE DE ÉLITE",
                    Descripcion = "Suela con compuesto reactivo de nitrógeno para mejorar los cortes.",
                    BadgesPositivas = new() { "+4 ATLETISMO", "+25 HYPE", "💰 +$2.5M FONDOS" },
                    BadgesPeligro = new() { "⚡ -10 ENERGÍA" },
                    DeltaAtletismo = 4, DeltaHype = 25, DeltaEnergia = -10,
                    Retroalimentacion = "El calzado se agotó en la preventa internacional en solo minutos."
                },
                new()
                {
                    Titulo = "CONTRATO CLÁSICO SIN IMPLICACIÓN EN DISEÑO",
                    Descripcion = "Cobrar las regalías correspondientes sin invertir tiempo en presentaciones.",
                    BadgesPositivas = new() { "💰 +$1.2M FONDOS", "+10 ENERGÍA" },
                    BadgesPeligro = new() { "-5 HYPE" },
                    DeltaEnergia = 10, DeltaHype = -5,
                    Retroalimentacion = "Ingresos seguros en tu cuenta corriente sin compromisos publicitarios."
                }
            }
        },

        new()
        {
            Id = 7,
            Rareza = RarezaCarta.Epica,
            BannerTag = "OFICINA CENTRAL",
            Titulo = "EXIGIR TRASPASOS AL GENERAL MANAGER",
            Contexto = "El equipo no compite por el anillo. Como referente, puedes exigir un pívot defensor o amenazar con pedir el traspaso.",
            ImageUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80",
            MinTemporada = 3, MinOVR = 83,
            Opciones = new()
            {
                new()
                {
                    Titulo = "EXIGIR REFUERZOS INTERIORES INMEDIATOS",
                    Descripcion = "Condicionar tu permanencia al fichaje de veteranos de nivel de campeonato.",
                    BadgesPositivas = new() { "+4 REBOTE", "+4 DEFENSA" },
                    BadgesPeligro = new() { "⚠️ RIESGO TRASPASO SI SE NIEGAN", "-15 CONFIANZA DT" },
                    DeltaRebote = 4, DeltaDefensa = 4, DeltaConfianzaDT = -15, AumentaRiesgoTraspaso = true,
                    Retroalimentacion = "La gerencia ejecutó el traspaso por un pívot protector del aro."
                },
                new()
                {
                    Titulo = "CONFIAR EN EL GRUPO ACTUAL Y ENTRENAR",
                    Descripcion = "Liderar a los jóvenes en silencio sin crear tensiones contractuales.",
                    BadgesPositivas = new() { "+20 CONFIANZA DT", "+15 QUÍMICA" },
                    BadgesPeligro = new() { "⚡ -15 ENERGÍA" },
                    DeltaConfianzaDT = 20, DeltaEnergia = -15,
                    Retroalimentacion = "El grupo reconoció tu compromiso y la química de vestuario subió."
                }
            }
        },

        new()
        {
            Id = 8,
            Rareza = RarezaCarta.Legendaria,
            BannerTag = "EL CLUTCH DEFINITIVO",
            Titulo = "POSESIÓN GANADORA EN LA BOCINA",
            Contexto = "Final de Conferencia, Partido 7. Empatados a 104 con 3 segundos por disputarse. Todo el pabellón de pie.",
            ImageUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80",
            MinTemporada = 3, MinOVR = 85,
            Opciones = new()
            {
                new()
                {
                    Titulo = "PEDIR EL BALÓN PARA EL TIRO FINAL (MINIJUEGO)",
                    Descripcion = "Asumir el tiro en suspensión en el minijuego clutch arcade.",
                    BadgesPositivas = new() { "🏀 MINIJUEGO CLUTCH", "+5 TIRO EXT", "+40 HYPE", "🏆 PASE A FINALES" },
                    BadgesPeligro = new() { "⚠️ SI FALLAS: FRACASO HISTÓRICO" },
                    ActivaMinijuegoEnCancha = true,
                    AumentaRiesgoTraspaso = true,
                    Retroalimentacion = "Lanzamiento histórico en suspensión que sella la temporada."
                },
                new()
                {
                    Titulo = "DOBLAR EL PASE AL TIRADOR EN LA ESQUINA",
                    Descripcion = "Atraer a dos defensores para dejar solo al alero tirador.",
                    BadgesPositivas = new() { "+4 PASE", "+15 QUÍMICA" },
                    BadgesPeligro = new() { "-10 HYPE" },
                    DeltaPase = 4, DeltaHype = -10,
                    Retroalimentacion = "Pase de campeonato para una canasta limpia con la sirena."
                }
            }
        }
    };

    public static DilemaRPG ObtenerSiguienteDilema(int temporada, int ovr, int pick, int hype)
    {
        var validos = BaseDeCartas
            .Where(c => c.EsCompatible(temporada, ovr, pick, hype) && !IdsDilemasVistos.Contains(c.Id))
            .ToList();

        // Si ya se mostraron todas las cartas compatibles, reiniciar el registro para ciclar
        if (!validos.Any())
        {
            IdsDilemasVistos.Clear();
            validos = BaseDeCartas.Where(c => c.EsCompatible(temporada, ovr, pick, hype)).ToList();
            if (!validos.Any())
            {
                validos = BaseDeCartas.Where(c => temporada <= 2 ? c.MaxTemporada <= 2 : c.MinTemporada >= 3).ToList();
            }
        }

        // Ponderación de probabilidades según rareza
        int tirada = Random.Shared.Next(1, 101);
        RarezaCarta rarezaElegida = tirada switch
        {
            <= 50 => RarezaCarta.Comun,
            <= 80 => RarezaCarta.Rara,
            <= 95 => RarezaCarta.Epica,
            _ => RarezaCarta.Legendaria
        };

        var candidatos = validos.Where(c => c.Rareza == rarezaElegida).ToList();
        if (!candidatos.Any()) candidatos = validos;

        var elegido = candidatos[Random.Shared.Next(candidatos.Count)];
        IdsDilemasVistos.Add(elegido.Id);
        return elegido;
    }
}
