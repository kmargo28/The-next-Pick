using TheNextPick.Models;

namespace TheNextPick.Engine.Strategies;

public interface IEstrategiaArquetipo
{
    string Clave { get; }
    string Nombre { get; }
    string PoseVestuario { get; }
    void AplicarBonificaciones(Jugador jugador);
}

public class EstrategiaTirador : IEstrategiaArquetipo
{
    public string Clave => "Tirador";
    public string Nombre => "Tirador Letal";
    public string PoseVestuario => "Bote";


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


    public void AplicarBonificaciones(Jugador jugador)
    {
        jugador.Defensa = Math.Min(99, jugador.Defensa + 10);
        jugador.Rebote = Math.Min(99, jugador.Rebote + 4);
    }
}
