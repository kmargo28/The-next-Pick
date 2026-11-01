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
    Detallada,
    Expres
}

public enum RondaPlayoffs
{
    PrimeraRonda = 1,
    SemifinalesConferencia = 2,
    FinalesConferencia = 3,
    FinalesNBA = 4
}

public enum TipoContrato
{
    Rookie,      // Escala novato obligatoria (4 años)
    Veterano,    // Contrato multianual estándar (2 a 4 años)
    Supermax     // Jugador Franquicia / Superestrella (4 a 5 años, $45M+)
}

public enum TipoLesion
{
    Ninguna,
    EsguinceTobillo,     // Fuera 1-2 partidos
    DesgarroMuscular,    // Fuera 3-4 partidos
    RoturaLigamentosACL  // ¡Toda la temporada perdida!
}
