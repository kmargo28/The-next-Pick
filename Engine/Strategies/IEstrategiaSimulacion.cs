using TheNextPick.Models;

namespace TheNextPick.Engine.Strategies;

public record ResultadoBloquePartidos(int Puntos, int Rebotes, int Asistencias, bool FueVictoria, int DesgasteEnergia);

public interface IEstrategiaSimulacion
{
    string NombreModalidad { get; }
    ResultadoBloquePartidos SimularBloque(Jugador jugador, int quimica, int energia);
}

public class EstrategiaSimulacionDetallada : IEstrategiaSimulacion
{
    public string NombreModalidad => "Simulación Detallada";

    public ResultadoBloquePartidos SimularBloque(Jugador jugador, int quimica, int energia)
    {
        int bonusQuimica = quimica >= 70 ? 4 : 0;
        int pts = Random.Shared.Next(16, 32) + bonusQuimica;
        int reb = Random.Shared.Next(4, 11);
        int ast = Random.Shared.Next(4, 12);
        int probVictoria = (jugador.OVR / 2) + 22 + (energia < 30 ? -8 : 0);
        bool victoria = Random.Shared.Next(1, 101) <= probVictoria;

        return new ResultadoBloquePartidos(pts, reb, ast, victoria, DesgasteEnergia: 10);
    }
}

public class EstrategiaSimulacionRapida : IEstrategiaSimulacion
{
    public string NombreModalidad => "Simulación Rápida";

    public ResultadoBloquePartidos SimularBloque(Jugador jugador, int quimica, int energia)
    {
        int pts = (jugador.OVR / 4) + Random.Shared.Next(4, 12);
        int reb = (jugador.Rebote / 12) + Random.Shared.Next(1, 5);
        int ast = (jugador.Pase / 12) + Random.Shared.Next(1, 5);
        int probVictoria = (jugador.OVR / 2) + 20;
        bool victoria = Random.Shared.Next(1, 101) <= probVictoria;

        return new ResultadoBloquePartidos(pts, reb, ast, victoria, DesgasteEnergia: 7);
    }
}
