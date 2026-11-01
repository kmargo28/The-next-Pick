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
    public int AniosEnEquipoActual { get; set; } = 1;

    // Arquetipo de posición
    public string Arquetipo { get; set; } = "Creador de Juego";

    // Estatus de Franquicia y Contrato
    public bool EsJugadorFranquicia { get; set; } = false;
    public TipoContrato TipoContrato { get; set; } = TipoContrato.Rookie;
    public int AniosRestantesContrato { get; set; } = 4;
    public int SalarioContrato { get; set; } = 4_500_000;

    // Sistema de Lesiones
    public TipoLesion LesionActual { get; set; } = TipoLesion.Ninguna;
    public int PartidosRestantesLesion { get; set; } = 0;
    public bool EstaLesionado => LesionActual != TipoLesion.Ninguna && PartidosRestantesLesion > 0;

    // 6 Atributos Oficiales (40 - 99)
    public int TiroExterior { get; set; }
    public int TiroMedia { get; set; }
    public int Pase { get; set; }
    public int Defensa { get; set; }
    public int Rebote { get; set; }
    public int Atletismo { get; set; }

    // Media general (OVR)
    public int OVR => Math.Clamp((int)Math.Round((TiroExterior + TiroMedia + Pase + Defensa + Rebote + Atletismo) / 6.0), 40, 99);

    // Métricas de Carrera
    public int PuntosTotales { get; set; } = 0;
    public int RebotesTotales { get; set; } = 0;
    public int AsistenciasTotales { get; set; } = 0;
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
                _jugador.Arquetipo = "Base Organizador y Francotirador";
                _jugador.TiroExterior = 82;
                _jugador.TiroMedia = 78;
                _jugador.Pase = 86;
                _jugador.Defensa = 68;
                _jugador.Rebote = 42;
                _jugador.Atletismo = 84;
                break;

            case Posicion.Escolta:
                _jugador.Arquetipo = "Anotador Puro de 3 Niveles";
                _jugador.TiroExterior = 86;
                _jugador.TiroMedia = 82;
                _jugador.Pase = 72;
                _jugador.Defensa = 72;
                _jugador.Rebote = 50;
                _jugador.Atletismo = 80;
                break;

            case Posicion.Alero:
                _jugador.Arquetipo = "Alero All-Around (Two-Way)";
                _jugador.TiroExterior = 78;
                _jugador.TiroMedia = 76;
                _jugador.Pase = 72;
                _jugador.Defensa = 78;
                _jugador.Rebote = 72;
                _jugador.Atletismo = 78;
                break;

            case Posicion.AlaPivot:
                _jugador.Arquetipo = "Ala-Pívot Defensivo y de Poste";
                _jugador.TiroExterior = 66;
                _jugador.TiroMedia = 76;
                _jugador.Pase = 62;
                _jugador.Defensa = 82;
                _jugador.Rebote = 84;
                _jugador.Atletismo = 74;
                break;

            case Posicion.Pivot:
                _jugador.Arquetipo = "Pívot Protector del Aro";
                _jugador.TiroExterior = 45;
                _jugador.TiroMedia = 66;
                _jugador.Pase = 58;
                _jugador.Defensa = 88;
                _jugador.Rebote = 90;
                _jugador.Atletismo = 70;
                break;
        }
        return this;
    }

    public Jugador Build() => _jugador;
}
