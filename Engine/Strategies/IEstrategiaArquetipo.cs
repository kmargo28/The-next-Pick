using TheNextPick.Models;

namespace TheNextPick.Engine.Strategies;

public interface IEstrategiaArquetipo
{
    string Clave { get; }
    string Nombre { get; }
    string PoseVestuario { get; }
    int OvrProyectado { get; }
    Dictionary<string, int> ObtenerStatsPreview();
    void AplicarBonificaciones(Jugador jugador);
}

public class EstrategiaTirador : IEstrategiaArquetipo
{
    public string Clave => "Tirador";
    public string Nombre => "Tirador Letal";
    public string PoseVestuario => "Bote";
    public int OvrProyectado => 74;

    public Dictionary<string, int> ObtenerStatsPreview() => new()
    {
        { "EXT", 84 }, { "MED", 82 }, { "PAS", 68 }, { "DEF", 67 }, { "REB", 62 }, { "ATL", 72 }
    };

    public void AplicarBonificaciones(Jugador jugador)
    {
        jugador.TiroExterior = Math.Min(99, jugador.TiroExterior + 8);
        jugador.TiroMedia = Math.Min(99, jugador.TiroMedia + 5);
    }
}

public class EstrategiaPlaymaker : IEstrategiaArquetipo
{
    public string Clave => "Playmaker";
    public string Nombre => "Playmaker Puro";
    public string PoseVestuario => "Bote";
    public int OvrProyectado => 74;

    public Dictionary<string, int> ObtenerStatsPreview() => new()
    {
        { "EXT", 70 }, { "MED", 72 }, { "PAS", 85 }, { "DEF", 67 }, { "REB", 62 }, { "ATL", 76 }
    };

    public void AplicarBonificaciones(Jugador jugador)
    {
        jugador.Pase = Math.Min(99, jugador.Pase + 10);
        jugador.Atletismo = Math.Min(99, jugador.Atletismo + 4);
    }
}

public class EstrategiaAtleta : IEstrategiaArquetipo
{
    public string Clave => "Atleta";
    public string Nombre => "Atleta Explosivo";
    public string PoseVestuario => "Celebrando";
    public int OvrProyectado => 75;

    public Dictionary<string, int> ObtenerStatsPreview() => new()
    {
        { "EXT", 70 }, { "MED", 72 }, { "PAS", 68 }, { "DEF", 67 }, { "REB", 78 }, { "ATL", 85 }
    };

    public void AplicarBonificaciones(Jugador jugador)
    {
        jugador.Atletismo = Math.Min(99, jugador.Atletismo + 10);
        jugador.Rebote = Math.Min(99, jugador.Rebote + 6);
    }
}

public class EstrategiaCerrojo : IEstrategiaArquetipo
{
    public string Clave => "Cerrojo";
    public string Nombre => "Cerrojo Defensivo";
    public string PoseVestuario => "Defendiendo";
    public int OvrProyectado => 74;

    public Dictionary<string, int> ObtenerStatsPreview() => new()
    {
        { "EXT", 70 }, { "MED", 72 }, { "PAS", 68 }, { "DEF", 86 }, { "REB", 75 }, { "ATL", 72 }
    };

    public void AplicarBonificaciones(Jugador jugador)
    {
        jugador.Defensa = Math.Min(99, jugador.Defensa + 10);
        jugador.Rebote = Math.Min(99, jugador.Rebote + 4);
    }
}
