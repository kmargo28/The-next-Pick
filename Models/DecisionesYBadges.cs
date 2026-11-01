namespace TheNextPick.Models;

public enum RarezaCarta
{
    Comun,      // 50% probabilidad de salir en el mazo
    Rara,       // 30% probabilidad de salir
    Epica,      // 15% probabilidad de salir
    Legendaria  // 5% probabilidad (evento estelar decisivo)
}

public class Insignia
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Icono { get; set; } = "⭐";
    public string BeneficioTexto { get; set; } = string.Empty;
    public bool Desbloqueada { get; set; } = false;
}

public class OpcionDecisionRPG
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    // Badges visuales estilo "El Ídolo"
    public List<string> BadgesPositivas { get; set; } = new(); // Ej: "+3 TIRO EXTERIOR", "+5 VELOCIDAD"
    public List<string> BadgesPeligro { get; set; } = new();   // Ej: "⚠️ RIESGO TRASPASO", "🏥 RIESGO LESIÓN"

    // Disparador de Minijuego interactivo
    public bool ActivaMinijuegoEnCancha { get; set; } = false;

    // Consecuencias en Atributos del Jugador
    public int DeltaTiroExterior { get; set; } = 0;
    public int DeltaTiroMedia { get; set; } = 0;
    public int DeltaPase { get; set; } = 0;
    public int DeltaDefensa { get; set; } = 0;
    public int DeltaRebote { get; set; } = 0;
    public int DeltaAtletismo { get; set; } = 0;

    // Consecuencias en Recursos y Riesgos
    public int DeltaEnergia { get; set; } = -10;
    public int DeltaConfianzaDT { get; set; } = 5;
    public int DeltaHype { get; set; } = 5;
    public bool AumentaRiesgoTraspaso { get; set; } = false;
    public int ProbabilidadLesionDirecta { get; set; } = 0; // % si se toma esta opción

    public string Retroalimentacion { get; set; } = string.Empty;
}

public class DilemaRPG
{
    public int Id { get; set; }
    public RarezaCarta Rareza { get; set; } = RarezaCarta.Comun;
    public string BannerTag { get; set; } = "PASAN COSAS";
    public string Titulo { get; set; } = string.Empty;
    public string Contexto { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    // Filtros de realismo de carrera
    public int MinTemporada { get; set; } = 1;
    public int MaxTemporada { get; set; } = 99;
    public int MinOVR { get; set; } = 40;
    public int MaxOVR { get; set; } = 99;
    public int MinPickDraft { get; set; } = 1;
    public int MaxPickDraft { get; set; } = 60;
    public int MinHype { get; set; } = 0;

    public List<OpcionDecisionRPG> Opciones { get; set; } = new();

    public bool EsCompatible(int temporada, int ovr, int pick, int hype)
    {
        if (temporada < MinTemporada || temporada > MaxTemporada) return false;
        if (ovr < MinOVR || ovr > MaxOVR) return false;
        if (pick < MinPickDraft || pick > MaxPickDraft) return false;
        if (hype < MinHype) return false;
        return true;
    }
}
