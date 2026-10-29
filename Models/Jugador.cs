namespace TheNextPick.Models;

public class Jugador
{
    public string Nombre { get; set; } = string.Empty;
    public string Pais { get; set; } = "Colombia";
    public int NumeroDorsal { get; set; } = 23;
    public Posicion Posicion { get; set; }
    public string EquipoActual { get; set; } = "Agente Libre";
    public int Edad { get; set; } = 19;
    public int AniosEnLiga { get; set; } = 0;

    // Atributos base (40 - 99)
    public int TiroExterior { get; set; }
    public int TiroMedia { get; set; }
    public int Pase { get; set; }
    public int Defensa { get; set; }
    public int Rebote { get; set; }
    public int Atletismo { get; set; }

    // Media general (OVR)
    public int OVR => Math.Clamp((int)Math.Round((TiroExterior + TiroMedia + Pase + Defensa + Rebote + Atletismo) / 6.0), 40, 99);

    // Estadísticas globales de carrera
    public int PuntosTotales { get; set; } = 0;
    public int RebotesTotales { get; set; } = 0;
    public int AsistenciasTotales { get; set; } = 0;
    public int RobosTotales { get; set; } = 0;
    public int PartidosTotales { get; set; } = 0;
    public int VictoriasTotales { get; set; } = 0;
    public int DerrotasTotales { get; set; } = 0;

    public double PromedioPuntos => PartidosTotales > 0 ? Math.Round((double)PuntosTotales / PartidosTotales, 1) : 0;
    public double PromedioRebotes => PartidosTotales > 0 ? Math.Round((double)RebotesTotales / PartidosTotales, 1) : 0;
    public double PromedioAsistencias => PartidosTotales > 0 ? Math.Round((double)AsistenciasTotales / PartidosTotales, 1) : 0;
    public double PorcentajeVictorias => PartidosTotales > 0 ? Math.Round(((double)VictoriasTotales / PartidosTotales) * 100, 1) : 0;
}

public class JugadorBuilder
{
    private readonly Jugador _jugador = new();

    public JugadorBuilder ConDatosBasicos(string nombre, string pais, int dorsal, Posicion posicion)
    {
        _jugador.Nombre = string.IsNullOrWhiteSpace(nombre) ? "Novato Promesa" : nombre;
        _jugador.Pais = string.IsNullOrWhiteSpace(pais) ? "Colombia" : pais;
        _jugador.NumeroDorsal = dorsal;
        _jugador.Posicion = posicion;
        return this;
    }

    public JugadorBuilder AsignarAtributosPorPosicion()
    {
        switch (_jugador.Posicion)
        {
            case Posicion.Base:
                _jugador.TiroExterior = 76;
                _jugador.TiroMedia = 74;
                _jugador.Pase = 82;
                _jugador.Defensa = 68;
                _jugador.Rebote = 48;
                _jugador.Atletismo = 78;
                break;

            case Posicion.Escolta:
                _jugador.TiroExterior = 80;
                _jugador.TiroMedia = 78;
                _jugador.Pase = 70;
                _jugador.Defensa = 70;
                _jugador.Rebote = 52;
                _jugador.Atletismo = 76;
                break;

            case Posicion.Alero:
                _jugador.TiroExterior = 74;
                _jugador.TiroMedia = 75;
                _jugador.Pase = 68;
                _jugador.Defensa = 76;
                _jugador.Rebote = 70;
                _jugador.Atletismo = 77;
                break;

            case Posicion.AlaPivot:
                _jugador.TiroExterior = 62;
                _jugador.TiroMedia = 72;
                _jugador.Pase = 58;
                _jugador.Defensa = 78;
                _jugador.Rebote = 80;
                _jugador.Atletismo = 74;
                break;

            case Posicion.Pivot:
                _jugador.TiroExterior = 45;
                _jugador.TiroMedia = 64;
                _jugador.Pase = 54;
                _jugador.Defensa = 84;
                _jugador.Rebote = 86;
                _jugador.Atletismo = 70;
                break;
        }
        return this;
    }

    public Jugador Build() => _jugador;
}

