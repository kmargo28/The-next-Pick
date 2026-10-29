using System.Text.Json;
using TheNextPick.Models;

namespace TheNextPick.Engine;

public enum FaseJuego
{
    Creacion,
    DraftCombine,
    EleccionEquipo,
    TemporadaRegular,
    FinTemporada,
    AgenciaLibre,
    Retiro
}

public class GameStateService
{
    public Jugador? JugadorActivo { get; set; }
    public FaseJuego FaseActual { get; set; } = FaseJuego.Creacion;

    public int PuntosCombine { get; set; } = 0;
    public int PickAsignado { get; set; } = 60;
    public List<string> EquiposDisponiblesDraft { get; set; } = new();

    // 4 Medidores de equilibrio (0 - 100)
    public int Energia { get; set; } = 100;
    public int ConfianzaDT { get; set; } = 50;
    public int Quimica { get; set; } = 50;
    public int Hype { get; set; } = 30;

    // Métricas de temporada
    public int TemporadaActual { get; set; } = 1;
    public int Victorias { get; set; } = 0;
    public int Derrotas { get; set; } = 0;
    public int PartidosJugados => Victorias + Derrotas;
    public int TotalPartidosTemporada { get; } = 8;
    public int PuntosDesarrollo { get; set; } = 0;

    // Box Score del último partido simulado
    public int UltimosPuntos { get; set; } = 0;
    public int UltimosRebotes { get; set; } = 0;
    public int UltimasAsistencias { get; set; } = 0;
    public bool UltimoFueVictoria { get; set; } = false;

    // Vitrina de trofeos
    public int AnillosCampeon { get; set; } = 0;
    public int PremiosMVP { get; set; } = 0;
    public bool GanadorROTY { get; set; } = false;
    public bool EnSalonDeLaFama { get; set; } = false;
    public List<string> PremiosGanadosTemporada { get; set; } = new();

    // Cartas y noticias
    public Dilema? DilemaActual { get; set; }
    public string UltimaNoticia { get; set; } = "Comienza la temporada de la NBA.";
    private readonly List<Dilema> _mazoDilemas = new();
    private int _indiceDilema = 0;

    public event Action? OnStateChanged;

    public void IniciarCreacion()
    {
        JugadorActivo = null;
        FaseActual = FaseJuego.Creacion;
        AnillosCampeon = 0;
        PremiosMVP = 0;
        GanadorROTY = false;
        EnSalonDeLaFama = false;
        TemporadaActual = 1;
        Notificar();
    }

    public void IrADraftCombine(Jugador jugador)
    {
        JugadorActivo = jugador;
        PuntosCombine = 0;
        FaseActual = FaseJuego.DraftCombine;
        Notificar();
    }

    public void IniciarRutaAleatoria(Jugador jugador, string equipo)
    {
        JugadorActivo = jugador;
        JugadorActivo.EquipoActual = equipo;
        PickAsignado = Random.Shared.Next(15, 45);
        IniciarTemporada();
    }

    public void FinalizarCombine(int puntos)
    {
        PuntosCombine = puntos;

        if (puntos >= 80)
        {
            PickAsignado = Random.Shared.Next(1, 4);
            EquiposDisponiblesDraft = new() { "Detroit Pistons", "Houston Rockets", "San Antonio Spurs" };
            Hype = 80;
        }
        else if (puntos >= 50)
        {
            PickAsignado = Random.Shared.Next(4, 15);
            EquiposDisponiblesDraft = new() { "Chicago Bulls", "Golden State Warriors", "Miami Heat" };
            Hype = 60;
        }
        else if (puntos >= 25)
        {
            PickAsignado = Random.Shared.Next(15, 31);
            EquiposDisponiblesDraft = new() { "Boston Celtics", "Denver Nuggets", "Milwaukee Bucks" };
            Hype = 45;
        }
        else
        {
            PickAsignado = Random.Shared.Next(31, 59);
            EquiposDisponiblesDraft = new() { "Memphis Grizzlies", "Indiana Pacers", "Utah Jazz" };
            Hype = 25;
        }

        FaseActual = FaseJuego.EleccionEquipo;
        Notificar();
    }

    public void SeleccionarEquipoDraft(string equipo)
    {
        if (JugadorActivo != null)
        {
            JugadorActivo.EquipoActual = equipo;
        }
        IniciarTemporada();
    }

    private void IniciarTemporada()
    {
        Energia = 100;
        ConfianzaDT = 55;
        Quimica = 50;
        Victorias = 0;
        Derrotas = 0;
        _indiceDilema = 0;
        FaseActual = FaseJuego.TemporadaRegular;
        CargarMazoTemporada();
        SiguienteDilema();
    }

    public void TomarOpcion(OpcionDilema opcion)
    {
        Energia = Math.Clamp(Energia + opcion.DeltaEnergia, 0, 100);
        ConfianzaDT = Math.Clamp(ConfianzaDT + opcion.DeltaDT, 0, 100);
        Quimica = Math.Clamp(Quimica + opcion.DeltaQuimica, 0, 100);
        Hype = Math.Clamp(Hype + opcion.DeltaHype, 0, 100);
        UltimaNoticia = opcion.Retroalimentacion;

        if (DilemaActual?.EsMomentoPartido == true || _indiceDilema % 2 == 0)
        {
            SimularPartido();
        }

        if (PartidosJugados >= TotalPartidosTemporada)
        {
            ProcesarFinDeTemporada();
            return;
        }

        SiguienteDilema();
    }

    private void SimularPartido()
    {
        if (JugadorActivo == null) return;

        // Minutos en cancha dependientes de la confianza del DT
        int minutosEstimados = Math.Clamp((ConfianzaDT * 38) / 100, 12, 38);

        // Generar estadísticas individuales proporcionales a atributos y minutos
        int basePuntos = (JugadorActivo.TiroExterior + JugadorActivo.TiroMedia) / 8;
        UltimosPuntos = Random.Shared.Next(basePuntos - 4, basePuntos + 10) * (minutosEstimados / 20);
        UltimosPuntos = Math.Max(2, UltimosPuntos);

        int baseRebotes = JugadorActivo.Rebote / 10;
        UltimosRebotes = Random.Shared.Next(Math.Max(1, baseRebotes - 3), baseRebotes + 5);

        int baseAsistencias = JugadorActivo.Pase / 12;
        UltimasAsistencias = Random.Shared.Next(Math.Max(1, baseAsistencias - 2), baseAsistencias + 6);

        // Acumular en la carrera del deportista
        JugadorActivo.PuntosTotales += UltimosPuntos;
        JugadorActivo.RebotesTotales += UltimosRebotes;
        JugadorActivo.AsistenciasTotales += UltimasAsistencias;
        JugadorActivo.PartidosTotales++;

        // Simular resultado grupal
        int mediaJugador = (JugadorActivo.TiroExterior + JugadorActivo.Pase + JugadorActivo.Defensa) / 3;
        int probVictoria = 25 + (mediaJugador / 4) + (Quimica / 4) + (ConfianzaDT / 4);

        if (Energia < 30) probVictoria -= 25;

        UltimoFueVictoria = Random.Shared.Next(1, 101) <= probVictoria;

        if (UltimoFueVictoria) Victorias++;
        else Derrotas++;

        Energia = Math.Clamp(Energia - 12, 0, 100);
    }

    private void ProcesarFinDeTemporada()
    {
        FaseActual = FaseJuego.FinTemporada;
        PremiosGanadosTemporada.Clear();

        PuntosDesarrollo += Victorias >= 5 ? 4 : 2;

        var reglaCampeon = new CondicionAnd(
            new CondicionCriterio(e => e.Victorias >= 6),
            new CondicionCriterio(e => e.Quimica >= 50)
        );

        var reglaMVP = new CondicionAnd(
            new CondicionCriterio(e => e.Hype >= 75),
            new CondicionCriterio(e => e.Victorias >= 6)
        );

        var reglaROTY = new CondicionAnd(
            new CondicionCriterio(e => e.TemporadaActual == 1),
            new CondicionCriterio(e => e.PickAsignado <= 20),
            new CondicionCriterio(e => e.Hype >= 55)
        );

        if (reglaCampeon.Cumple(this))
        {
            AnillosCampeon++;
            PremiosGanadosTemporada.Add("💍 Anillo de Campeón de la NBA");
        }

        if (reglaMVP.Cumple(this))
        {
            PremiosMVP++;
            PremiosGanadosTemporada.Add("🏆 Jugador Más Valioso (MVP)");
        }

        if (!GanadorROTY && reglaROTY.Cumple(this))
        {
            GanadorROTY = true;
            PremiosGanadosTemporada.Add("🌟 Novato del Año (ROTY)");
        }

        Notificar();
    }

    public void MejorarAtributo(string atributo)
    {
        if (JugadorActivo == null || PuntosDesarrollo <= 0) return;

        switch (atributo)
        {
            case "TiroExterior":
                if (JugadorActivo.TiroExterior < 99) { JugadorActivo.TiroExterior += 3; PuntosDesarrollo--; }
                break;
            case "Pase":
                if (JugadorActivo.Pase < 99) { JugadorActivo.Pase += 3; PuntosDesarrollo--; }
                break;
            case "Defensa":
                if (JugadorActivo.Defensa < 99) { JugadorActivo.Defensa += 3; PuntosDesarrollo--; }
                break;
            case "Atletismo":
                if (JugadorActivo.Atletismo < 99) { JugadorActivo.Atletismo += 3; PuntosDesarrollo--; }
                break;
        }
        Notificar();
    }

    public void AvanzarSiguienteTemporada()
    {
        if (JugadorActivo == null) return;

        TemporadaActual++;
        JugadorActivo.Edad++;
        JugadorActivo.AniosEnLiga++;

        if (JugadorActivo.Edad >= 30)
        {
            JugadorActivo.Atletismo = Math.Max(40, JugadorActivo.Atletismo - 2);
        }

        if (TemporadaActual % 3 == 0)
        {
            FaseActual = FaseJuego.AgenciaLibre;
        }
        else
        {
            IniciarTemporada();
        }
        Notificar();
    }

    public void FirmarAgenciaLibre(string nuevoEquipo)
    {
        if (JugadorActivo != null)
        {
            JugadorActivo.EquipoActual = nuevoEquipo;
        }
        IniciarTemporada();
    }

    public void RetirarJugador()
    {
        var reglaHallOfFame = new CondicionOr(
            new CondicionCriterio(e => e.AnillosCampeon >= 2),
            new CondicionCriterio(e => e.PremiosMVP >= 1),
            new CondicionAnd(
                new CondicionCriterio(e => e.TemporadaActual >= 5),
                new CondicionCriterio(e => e.AnillosCampeon >= 1),
                new CondicionCriterio(e => e.Hype >= 70)
            )
        );

        EnSalonDeLaFama = reglaHallOfFame.Cumple(this);
        FaseActual = FaseJuego.Retiro;
        Notificar();
    }

    public string ExportarEstadoJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public void ImportarEstadoJson(string json)
    {
        var cargado = JsonSerializer.Deserialize<GameStateService>(json);
        if (cargado != null)
        {
            JugadorActivo = cargado.JugadorActivo;
            FaseActual = cargado.FaseActual;
            PuntosCombine = cargado.PuntosCombine;
            PickAsignado = cargado.PickAsignado;
            EquiposDisponiblesDraft = cargado.EquiposDisponiblesDraft;
            Energia = cargado.Energia;
            ConfianzaDT = cargado.ConfianzaDT;
            Quimica = cargado.Quimica;
            Hype = cargado.Hype;
            TemporadaActual = cargado.TemporadaActual;
            Victorias = cargado.Victorias;
            Derrotas = cargado.Derrotas;
            PuntosDesarrollo = cargado.PuntosDesarrollo;
            AnillosCampeon = cargado.AnillosCampeon;
            PremiosMVP = cargado.PremiosMVP;
            GanadorROTY = cargado.GanadorROTY;
            EnSalonDeLaFama = cargado.EnSalonDeLaFama;
            PremiosGanadosTemporada = cargado.PremiosGanadosTemporada;
            UltimosPuntos = cargado.UltimosPuntos;
            UltimosRebotes = cargado.UltimosRebotes;
            UltimasAsistencias = cargado.UltimasAsistencias;
            UltimoFueVictoria = cargado.UltimoFueVictoria;
            UltimaNoticia = cargado.UltimaNoticia;

            CargarMazoTemporada();
            SiguienteDilema();
            Notificar();
        }
    }

    private void SiguienteDilema()
    {
        if (_indiceDilema < _mazoDilemas.Count)
        {
            DilemaActual = _mazoDilemas[_indiceDilema];
            _indiceDilema++;
        }
        else
        {
            _indiceDilema = 0;
            DilemaActual = _mazoDilemas[Random.Shared.Next(_mazoDilemas.Count)];
        }
        Notificar();
    }

    private void CargarMazoTemporada()
    {
        _mazoDilemas.Clear();
        _mazoDilemas.AddRange(new[]
        {
            new Dilema
            {
                Id = 1,
                Categoria = "Vestuario",
                Titulo = "Choque de egos con el titular",
                Descripcion = "El líder del equipo se queja de que tomas demasiados tiros.",
                OpcionA = new OpcionDilema { Texto = "Cederle el balón", DeltaEnergia = -5, DeltaDT = +10, DeltaQuimica = +15, DeltaHype = -5, Retroalimentacion = "Priorizaste el vestuario antes que tus números." },
                OpcionB = new OpcionDilema { Texto = "Exigir tu espacio", DeltaEnergia = 0, DeltaDT = -10, DeltaQuimica = -20, DeltaHype = +15, Retroalimentacion = "Hubo tensión en la práctica, pero la afición ama tu carácter." }
            },
            new Dilema
            {
                Id = 2,
                Categoria = "Partido (Clutch)",
                Titulo = "Minuto final en clásico de visita",
                Descripcion = "Marcador empatado. Te doblan la marca saliendo del bloqueo.",
                EsMomentoPartido = true,
                OpcionA = new OpcionDilema { Texto = "Forzar el tiro en suspensión", DeltaEnergia = -15, DeltaDT = -5, DeltaQuimica = -10, DeltaHype = +20, Retroalimentacion = "¡Anotaste sobre la chicharra! Eres el héroe de la jornada." },
                OpcionB = new OpcionDilema { Texto = "Asistir al pívot desmarcado", DeltaEnergia = -5, DeltaDT = +15, DeltaQuimica = +15, DeltaHype = +5, Retroalimentacion = "Pase quirúrgico para la bandeja ganadora. Canasta fácil." }
            },
            new Dilema
            {
                Id = 3,
                Categoria = "Prensa y Redes",
                Titulo = "Polémica en Twitter tras una derrota",
                Descripcion = "Los hinchas rivales se burlan de tu bajo porcentaje de tiro de ayer.",
                OpcionA = new OpcionDilema { Texto = "Contestarles con arrogancia", DeltaEnergia = 0, DeltaDT = -15, DeltaQuimica = -10, DeltaHype = +25, Retroalimentacion = "El tweet se hizo viral en todo el país. El DT te llamó la atención." },
                OpcionB = new OpcionDilema { Texto = "Apagar el teléfono y entrenar", DeltaEnergia = -10, DeltaDT = +15, DeltaQuimica = +10, DeltaHype = -5, Retroalimentacion = "Te enfocaste en mejorar tu tiro en silencio." }
            },
            new Dilema
            {
                Id = 4,
                Categoria = "Salud / Fatiga",
                Titulo = "Molestia en el tobillo en Playoffs",
                Descripcion = "Sientes dolor fuerte en el tendón antes de un partido de eliminación.",
                OpcionA = new OpcionDilema { Texto = "Infiltrarse y jugar con dolor", DeltaEnergia = -40, DeltaDT = +20, DeltaQuimica = +10, DeltaHype = +20, Retroalimentacion = "Jugaste al límite del desgarro pero demostraste compromiso total." },
                OpcionB = new OpcionDilema { Texto = "Pedir descanso médico", DeltaEnergia = +25, DeltaDT = -15, DeltaQuimica = -5, DeltaHype = -15, Retroalimentacion = "Cuidaste tu integridad física pero el equipo cayó derrotado." }
            }
        });
    }

    private void Notificar() => OnStateChanged?.Invoke();
}
