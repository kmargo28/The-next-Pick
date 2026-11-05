namespace TheNextPick.Models;

public enum CategoriaPremio
{
    TrofeoMayor,     // MVP, Rookie of the Year (ROY), DPOY, Finals MVP
    QuintetoIdeal,   // All-NBA 1st Team, All-NBA 2nd Team, All-Defensive, All-Rookie
    AllStar,         // All-Star Game, Concurso de Triples, Rising Stars MVP
    Campeonato       // Anillo Larry O'Brien
}

public class PremioIndividual
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nombre { get; set; } = "";
    public string TemporadaTexto { get; set; } = "2026-27";
    public CategoriaPremio Categoria { get; set; } = CategoriaPremio.TrofeoMayor;
    public string Icono { get; set; } = "🏆";
    public string Descripcion { get; set; } = "";
    public string ColorBorde { get; set; } = "#FFC72C";
}
