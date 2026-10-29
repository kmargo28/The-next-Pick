namespace TheNextPick.Models;

public class OpcionDilema
{
    public string Texto { get; set; } = string.Empty;
    public int DeltaEnergia { get; set; }
    public int DeltaDT { get; set; }
    public int DeltaQuimica { get; set; }
    public int DeltaHype { get; set; }
    public string Retroalimentacion { get; set; } = string.Empty;
}

public class Dilema
{
    public int Id { get; set; }
    public string Categoria { get; set; } = "Vestuario"; // Prensa, DT, Vida Personal, Partido
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public OpcionDilema OpcionA { get; set; } = new();
    public OpcionDilema OpcionB { get; set; } = new();
    public bool EsMomentoPartido { get; set; } = false;
}
