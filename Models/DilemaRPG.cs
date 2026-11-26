namespace TheNextPick.Models;

public enum RarezaCarta
{
    Normal,       // Decisión estándar: Sin etiquetas de rareza ni halos
    Rara,         // Evento especial poco común (~15% prob)
    Epica,        // Evento especial de gran impacto (~8% prob)
    Legendaria    // Evento extraordinario (~2% prob): Mejoras de atributos / Jugador Franquicia
}

public enum TipoContextoDecision
{
    DiaDePartido,
    DiaLibre,
    VestuarioYCapitania,
    OficinaYTraspasos,
    AllStarWeekend
}

public class OpcionDecisionRPG
{
    public string Titulo { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public List<string> BadgesPositivas { get; set; } = new();
    public List<string> BadgesPeligro { get; set; } = new();

    // Modificadores de Atributos
    public int DeltaTiroExterior { get; set; } = 0;
    public int DeltaTiroMedia { get; set; } = 0;
    public int DeltaPase { get; set; } = 0;
    public int DeltaDefensa { get; set; } = 0;
    public int DeltaRebote { get; set; } = 0;
    public int DeltaAtletismo { get; set; } = 0;

    // Recursos
    public int DeltaEnergia { get; set; } = 0;
    public int DeltaConfianzaDT { get; set; } = 0;
    public int DeltaHype { get; set; } = 0;
    public int DeltaQuimica { get; set; } = 0;
    public int DeltaDinero { get; set; } = 0;

    // Mecánicas Especiales
    public bool ConvierteEnJugadorFranquicia { get; set; } = false;
    public bool ActivaMinijuegoEnCancha { get; set; } = false;
    public bool AumentaRiesgoTraspaso { get; set; } = false;
    public int ProbabilidadLesionDirecta { get; set; } = 0;
    public string Retroalimentacion { get; set; } = "";
    public int DesencadenaArcoId { get; set; } = 0;
}

public class DilemaRPG
{
    public int Id { get; set; }
    public RarezaCarta Rareza { get; set; } = RarezaCarta.Normal;
    public TipoContextoDecision ContextoTipo { get; set; } = TipoContextoDecision.DiaDePartido;
    public string BannerTag { get; set; } = "NBA REGULAR SEASON";
    public string Titulo { get; set; } = "";
    public string Contexto { get; set; } = "";
    public string ImageUrl { get; set; } = "";

    public int MinTemporada { get; set; } = 1;
    public int MaxTemporada { get; set; } = 20;
    public int MinPickDraft { get; set; } = 1;
    public int MaxPickDraft { get; set; } = 60;
    public int MinOVR { get; set; } = 0;
    public int MaxOVR { get; set; } = 99;
    public int MinHype { get; set; } = 0;
    public bool RequiereRiesgoTraspaso { get; set; } = false;
    public int IdArcoRequerido { get; set; } = 0;

    public List<OpcionDecisionRPG> Opciones { get; set; } = new();

    // Solo las cartas especiales tienen rareza activa
    public bool TieneRareza => Rareza != RarezaCarta.Normal;

    public int PorcentajeProbabilidad => Rareza switch
    {
        RarezaCarta.Rara => 15,
        RarezaCarta.Epica => 8,
        RarezaCarta.Legendaria => 2,
        _ => 0
    };

    public string ColorBordeHex => Rareza switch
    {
        RarezaCarta.Rara => "#00f0ff",
        RarezaCarta.Epica => "#c084fc",
        RarezaCarta.Legendaria => "#FFC72C",
        _ => "#273549" // Borde sobrio de tarjeta deportiva estándar
    };

    public string ColorBadgeBg => Rareza switch
    {
        RarezaCarta.Rara => "#0284c7",
        RarezaCarta.Epica => "#9333ea",
        RarezaCarta.Legendaria => "#d97706",
        _ => "#1e293b"
    };

    public bool EsCompatible(int temporada, int ovr, int pick, int hype, bool enRiesgo, List<int> arcosActivos)
    {
        if (temporada < MinTemporada || temporada > MaxTemporada) return false;
        if (pick < MinPickDraft || pick > MaxPickDraft) return false;
        if (ovr < MinOVR || ovr > MaxOVR) return false;
        if (hype < MinHype) return false;
        if (RequiereRiesgoTraspaso && !enRiesgo) return false;
        if (IdArcoRequerido > 0 && !arcosActivos.Contains(IdArcoRequerido)) return false;

        return true;
    }
}
