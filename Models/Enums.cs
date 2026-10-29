namespace TheNextPick.Models;

public enum Posicion
{
    Base,
    Escolta,
    Alero,
    AlaPivot,
    Pivot
}

public enum RitmoSimulacion
{
    Detallada, // Temporada por temporada (8 partidos + playoffs completos)
    Expres     // Cada 2 temporadas (simulación acelerada para carreras largas)
}

public enum RondaPlayoffs
{
    PrimeraRonda = 1,
    SemifinalesConferencia = 2,
    FinalesConferencia = 3,
    FinalesNBA = 4
}
