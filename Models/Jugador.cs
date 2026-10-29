namespace TheNextPick.Models;

public class Jugador
{
    public string Nombre { get; set; } = string.Empty;
    public int NumeroDorsal { get; set; } = 23;
    public Posicion Posicion { get; set; }
    public string EquipoActual { get; set; } = "Agente Libre";
    public int Edad { get; set; } = 19;
    public int AniosEnLiga { get; set; } = 0;

    // Atributos de juego (40 - 99)
    public int TiroExterior { get; set; }
    public int TiroMedia { get; set; }
    public int Pase { get; set; }
    public int Defensa { get; set; }
    public int Rebote { get; set; }
    public int Atletismo { get; set; }

    // Estadísticas acumuladas de carrera
    public int PuntosTotales { get; set; } = 0;
    public int RebotesTotales { get; set; } = 0;
    public int AsistenciasTotales { get; set; } = 0;
    public int PartidosTotales { get; set; } = 0;

    public double PromedioPuntos => PartidosTotales > 0 ? Math.Round((double)PuntosTotales / PartidosTotales, 1) : 0;
    public double PromedioRebotes => PartidosTotales > 0 ? Math.Round((double)RebotesTotales / PartidosTotales, 1) : 0;
    public double PromedioAsistencias => PartidosTotales > 0 ? Math.Round((double)AsistenciasTotales / PartidosTotales, 1) : 0;
}

public class JugadorBuilder
{
    private readonly Jugador _jugador = new();

    public JugadorBuilder ConDatosBasicos(string nombre, int dorsal, Posicion posicion)
    {
        _jugador.Nombre = string.IsNullOrWhiteSpace(nombre) ? "Novato Promesa" : nombre;
        _jugador.NumeroDorsal = dorsal;
        _jugador.Posicion = posicion;
        return this;
    }

    public JugadorBuilder AsignarAtributosPorPosicion()
    {
        switch (_jugador.Posicion)
        {
            case Posicion.Base:
                _jugador.TiroExterior = 75;
                _jugador.TiroMedia = 72;
                _jugador.Pase = 80;
                _jugador.Defensa = 65;
                _jugador.Rebote = 45;
                _jugador.Atletismo = 78;
                break;

            case Posicion.Escolta:
                _jugador.TiroExterior = 78;
                _jugador.TiroMedia = 76;
                _jugador.Pase = 68;
                _jugador.Defensa = 68;
                _jugador.Rebote = 50;
                _jugador.Atletismo = 75;
                break;

            case Posicion.Alero:
                _jugador.TiroExterior = 70;
                _jugador.TiroMedia = 72;
                _jugador.Pase = 65;
                _jugador.Defensa = 74;
                _jugador.Rebote = 68;
                _jugador.Atletismo = 76;
                break;

            case Posicion.AlaPivot:
                _jugador.TiroExterior = 58;
                _jugador.TiroMedia = 70;
                _jugador.Pase = 55;
                _jugador.Defensa = 75;
                _jugador.Rebote = 78;
                _jugador.Atletismo = 72;
                break;

            case Posicion.Pivot:
                _jugador.TiroExterior = 42;
                _jugador.TiroMedia = 60;
                _jugador.Pase = 50;
                _jugador.Defensa = 82;
                _jugador.Rebote = 84;
                _jugador.Atletismo = 68;
                break;
        }
        return this;
    }

    public Jugador Build() => _jugador;
}
