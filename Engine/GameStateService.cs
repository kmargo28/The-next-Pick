using System.Text.Json;
using TheNextPick.Models;

namespace TheNextPick.Engine;

public enum FaseJuego
{
    Creacion,
    DraftCombine,
    EleccionEquipo,
    TemporadaRegular,
    MinijuegoTiro,
    AlertaTraspaso,
    AlertaLesion,
    Playoffs,
    FinTemporada,
    AgenciaLibre,
    Retiro
}

public enum FormatoPlayoffs
{
    PizarraYFinalClutch,    // Rondas 1-3: Pizarra Táctica | Finales NBA: Shot Clock Clutch
    SenasYTirosLibres,      // Rondas 1-3: Seña del DT | Finales NBA: Tiros Libres Hostiles
    TirosLibresYClutch,     // Rondas 1-3: Tiros Libres Hostiles | Finales NBA: Shot Clock Clutch
    CampanaClutchCompleta   // Rondas 1-4: Shot Clock Clutch Puro (Escalada extrema)
}

public class GameStateService
{
    public Jugador? JugadorActivo { get; set; }
    public FaseJuego FaseActual { get; set; } = FaseJuego.Creacion;
    public RitmoSimulacion RitmoElegido { get; set; } = RitmoSimulacion.Detallada;

    public List<Insignia> CatalogoInsignias { get; set; } = new();

    public int PuntosCombine { get; set; } = 0;
    public int PickAsignado { get; set; } = 60;
    public EquipoNBA? EquipoQueTeEligio { get; set; }

    public int MinijuegosFalladosConsecutivos { get; set; } = 0;
    public bool EnRiesgoTraspaso { get; set; } = false;
    public string NoticiaTraspaso { get; set; } = "";
    public string DiagnosticoLesion { get; set; } = "";

    public double FansMillones { get; set; } = 1.2;
    public int SalarioAnual { get; set; } = 4_500_000;
    public int DineroDisponible { get; set; } = 4_500_000;

    public int Energia { get; set; } = 100;
    public int ConfianzaDT { get; set; } = 55;
    public int Quimica { get; set; } = 50;
    public int Hype { get; set; } = 30;

    public int TemporadaActual { get; set; } = 1;
    public int Victorias { get; set; } = 0;
    public int Derrotas { get; set; } = 0;
    public int PartidosJugados => Victorias + Derrotas;
    public int DecisionesTomadasTemporada { get; set; } = 0;
    public int TotalDecisionesTemporada => RitmoElegido == RitmoSimulacion.Detallada ? 4 : 2;
    public int PuntosDesarrollo { get; set; } = 0;

    // Playoffs
    public RondaPlayoffs RondaActualPlayoffs { get; set; } = RondaPlayoffs.PrimeraRonda;
    public int RondaIndicePlayoffs { get; set; } = 1; // 1: 1ra Ronda, 2: Semis, 3: Final Conf, 4: Finales NBA
    public int SeedPlayoffs { get; set; } = 4;
    public FormatoPlayoffs FormatoPlayoffTemporada { get; set; } = FormatoPlayoffs.PizarraYFinalClutch;
    public int VictoriasSeriePlayoff { get; set; } = 0;
    public int DerrotasSeriePlayoff { get; set; } = 0;
    public string RivalPlayoffs { get; set; } = "Denver Nuggets";

    public string NombreRonda(RondaPlayoffs ronda) => ronda switch
    {
        RondaPlayoffs.PrimeraRonda => "Primera Ronda de Conferencia",
        RondaPlayoffs.SemifinalesConferencia => "Semifinales de Conferencia",
        RondaPlayoffs.FinalesConferencia => "Finales de Conferencia",
        RondaPlayoffs.FinalesNBA => "Finales de la NBA",
        _ => "Postemporada"
    };

    public string NombreRondaActual() => RondaIndicePlayoffs switch
    {
        1 => "Primera Ronda",
        2 => "Semifinales de Conferencia",
        3 => "Finales de Conferencia",
        4 => "Finales de la NBA",
        _ => "Playoffs"
    };

    public int UltimosPuntos { get; set; } = 0;
    public int UltimosRebotes { get; set; } = 0;
    public int UltimasAsistencias { get; set; } = 0;
    public bool UltimoFueVictoria { get; set; } = false;

    public int AnillosCampeon { get; set; } = 0;
    public int PremiosMVP { get; set; } = 0;
    public bool EnSalonDeLaFama { get; set; } = false;
    public List<string> PremiosGanadosTemporada { get; set; } = new();

    public DilemaRPG? DilemaRPGActual { get; set; }
    public string UltimaNoticia { get; set; } = "Comienza la temporada NBA 2026-2027.";
    private OpcionDecisionRPG? opcionPendienteMinijuego;

    public event Action? OnStateChanged;

    public GameStateService()
    {
        InicializarInsignias();
    }

    private void InicializarInsignias()
    {
        CatalogoInsignias = new()
        {
            new() { Id = "microondas", Nombre = "Microondas", Icono = "🔥", BeneficioTexto = "+15 Confianza DT al inicio de temporada.", Descripcion = "Gana 3 minijuegos seguidos sin fallar." },
            new() { Id = "sniper", Nombre = "Francotirador de Élite", Icono = "🎯", BeneficioTexto = "+4 permanente a Tiro Exterior.", Descripcion = "Alcanza 88+ en Tiro Exterior." },
            new() { Id = "cerrojo", Nombre = "Cerrojo Perimetral", Icono = "🛡️", BeneficioTexto = "+4 permanente a Defensa.", Descripcion = "Alcanza 85+ en Defensa." }
        };
    }

    public void IniciarCreacion()
    {
        JugadorActivo = null;
        FaseActual = FaseJuego.Creacion;
        AnillosCampeon = 0;
        PremiosMVP = 0;
        EnSalonDeLaFama = false;
        TemporadaActual = 1;
        FansMillones = 1.0;
        MinijuegosFalladosConsecutivos = 0;
        EnRiesgoTraspaso = false;
        InicializarInsignias();
        Notificar();
    }

    public void IrADraftCombine(Jugador jugador, RitmoSimulacion ritmo)
    {
        JugadorActivo = jugador;
        RitmoElegido = ritmo;
        PuntosCombine = 0;
        FaseActual = FaseJuego.DraftCombine;
        Notificar();
    }

    public void FinalizarCombine(int puntos)
    {
        PuntosCombine = puntos;
        int total = EquipoNBA.Franquicias.Count;
        if (total == 0) return;

        if (puntos >= 88)
        {
            PickAsignado = Random.Shared.Next(1, 4);
            int max = Math.Clamp(3, 1, total);
            EquipoQueTeEligio = EquipoNBA.Franquicias[Random.Shared.Next(0, max)];
            Hype = 90;
            FansMillones = 2.5;
        }
        else if (puntos >= 70)
        {
            PickAsignado = Random.Shared.Next(4, 11);
            int min = Math.Clamp(1, 0, total - 1);
            int max = Math.Clamp(total / 2 + 1, min + 1, total);
            EquipoQueTeEligio = EquipoNBA.Franquicias[Random.Shared.Next(min, max)];
            Hype = 70;
            FansMillones = 1.6;
        }
        else if (puntos >= 50)
        {
            PickAsignado = Random.Shared.Next(11, 26);
            int min = Math.Clamp(total / 3, 0, total - 1);
            int max = Math.Clamp(total, min + 1, total);
            EquipoQueTeEligio = EquipoNBA.Franquicias[Random.Shared.Next(min, max)];
            Hype = 50;
            FansMillones = 1.1;
        }
        else if (puntos >= 30)
        {
            PickAsignado = Random.Shared.Next(26, 31);
            int min = Math.Clamp(total / 2, 0, total - 1);
            EquipoQueTeEligio = EquipoNBA.Franquicias[Random.Shared.Next(min, total)];
            Hype = 35;
            FansMillones = 0.8;
        }
        else
        {
            PickAsignado = Random.Shared.Next(31, 61);
            int min = Math.Clamp(total / 2, 0, total - 1);
            EquipoQueTeEligio = EquipoNBA.Franquicias[Random.Shared.Next(min, total)];
            Hype = 20;
            FansMillones = 0.4;
        }

        if (EquipoQueTeEligio == null)
        {
            EquipoQueTeEligio = EquipoNBA.Franquicias[0];
        }

        AsignarContratoRookie(EquipoQueTeEligio.Nombre, PickAsignado);
        FaseActual = FaseJuego.EleccionEquipo;
        Notificar();
    }

    private void AsignarContratoRookie(string equipo, int pick)
    {
        if (JugadorActivo == null) return;
        JugadorActivo.EquipoActual = equipo;
        JugadorActivo.TipoContrato = TipoContrato.Rookie;
        JugadorActivo.AniosRestantesContrato = 4;
        JugadorActivo.AniosEnEquipoActual = 1;
        JugadorActivo.EsJugadorFranquicia = false;
        JugadorActivo.SalarioContrato = pick <= 3 ? 10_500_000 : (pick <= 14 ? 6_800_000 : 3_200_000);
        SalarioAnual = JugadorActivo.SalarioContrato;
        DineroDisponible = SalarioAnual;
    }

    public void ConfirmarDraftYComenzar()
    {
        IniciarTemporada();
    }

    private void IniciarTemporada()
    {
        Energia = 100;
        ConfianzaDT = CatalogoInsignias.Any(b => b.Id == "microondas" && b.Desbloqueada) ? 70 : 55;
        Quimica = 50;
        Victorias = 0;
        Derrotas = 0;
        DecisionesTomadasTemporada = 0;

        SiguienteDilema();
        FaseActual = FaseJuego.TemporadaRegular;
        Notificar();
    }

    public void TomarDecision(OpcionDecisionRPG opcion)
    {
        if (JugadorActivo == null) return;

        if (opcion.ActivaMinijuegoEnCancha)
        {
            opcionPendienteMinijuego = opcion;
            FaseActual = FaseJuego.MinijuegoTiro;
            Notificar();
            return;
        }

        AplicarConsecuenciasOpcion(opcion, true);
    }

    public void ResolverMinijuego(bool exitoso)
    {
        if (opcionPendienteMinijuego != null)
        {
            AplicarConsecuenciasOpcion(opcionPendienteMinijuego, exitoso);
            opcionPendienteMinijuego = null;
        }
        else
        {
            if (exitoso)
            {
                MinijuegosFalladosConsecutivos = 0;
                EnRiesgoTraspaso = false;
                ConfianzaDT = Math.Clamp(ConfianzaDT + 10, 0, 100);
                Hype = Math.Clamp(Hype + 10, 0, 100);
                UltimaNoticia = "Actuación clutch determinante en la posesión final.";
            }
            else
            {
                MinijuegosFalladosConsecutivos++;
                ConfianzaDT = Math.Clamp(ConfianzaDT - 15, 0, 100);
                if (MinijuegosFalladosConsecutivos >= 2 && !JugadorActivo!.EsJugadorFranquicia)
                {
                    EnRiesgoTraspaso = true;
                    UltimaNoticia = "⚠️ ATENCIÓN: La directiva duda de ti. Estás en la LISTA DE TRASPASOS.";
                }
                if (MinijuegosFalladosConsecutivos >= 3 && !JugadorActivo!.EsJugadorFranquicia)
                {
                    EjecutarTraspasoForzado();
                    return;
                }
            }
            FaseActual = FaseJuego.TemporadaRegular;
            ContinuarBucleTemporada();
        }
    }

    private void AplicarConsecuenciasOpcion(OpcionDecisionRPG opcion, bool exito)
    {
        if (JugadorActivo == null) return;
        DecisionesTomadasTemporada++;

        if (exito)
        {
            JugadorActivo.TiroExterior = Math.Clamp(JugadorActivo.TiroExterior + opcion.DeltaTiroExterior, 40, 99);
            JugadorActivo.TiroMedia = Math.Clamp(JugadorActivo.TiroMedia + opcion.DeltaTiroMedia, 40, 99);
            JugadorActivo.Pase = Math.Clamp(JugadorActivo.Pase + opcion.DeltaPase, 40, 99);
            JugadorActivo.Defensa = Math.Clamp(JugadorActivo.Defensa + opcion.DeltaDefensa, 40, 99);
            JugadorActivo.Rebote = Math.Clamp(JugadorActivo.Rebote + opcion.DeltaRebote, 40, 99);
            JugadorActivo.Atletismo = Math.Clamp(JugadorActivo.Atletismo + opcion.DeltaAtletismo, 40, 99);

            Energia = Math.Clamp(Energia + opcion.DeltaEnergia, 0, 100);
            ConfianzaDT = Math.Clamp(ConfianzaDT + opcion.DeltaConfianzaDT, 0, 100);
            Hype = Math.Clamp(Hype + opcion.DeltaHype, 0, 100);
            UltimaNoticia = opcion.Retroalimentacion;
            MinijuegosFalladosConsecutivos = 0;
            EnRiesgoTraspaso = false;
        }
        else
        {
            MinijuegosFalladosConsecutivos++;
            ConfianzaDT = Math.Clamp(ConfianzaDT - 20, 0, 100);
            Hype = Math.Clamp(Hype - 15, 0, 100);
            UltimaNoticia = "Fallaste en el momento cumbre. La prensa y el DT cuestionan tu nivel.";

            if (opcion.AumentaRiesgoTraspaso || MinijuegosFalladosConsecutivos >= 2)
            {
                EnRiesgoTraspaso = true;
            }

            if (MinijuegosFalladosConsecutivos >= 3 && !JugadorActivo.EsJugadorFranquicia)
            {
                EjecutarTraspasoForzado();
                return;
            }
        }

        if ((opcion.ProbabilidadLesionDirecta > 0 && Random.Shared.Next(1, 101) <= opcion.ProbabilidadLesionDirecta) ||
            (Energia < 22 && Random.Shared.Next(1, 101) <= 35))
        {
            GenerarLesion();
            return;
        }

        FaseActual = FaseJuego.TemporadaRegular;
        ContinuarBucleTemporada();
    }

    private void GenerarLesion()
    {
        if (JugadorActivo == null) return;
        int tipo = Random.Shared.Next(1, 101);

        if (tipo <= 60)
        {
            JugadorActivo.LesionActual = TipoLesion.EsguinceTobillo;
            JugadorActivo.PartidosRestantesLesion = 1;
            DiagnosticoLesion = "Esguince de tobillo en la caída. El cuerpo médico te inhabilita 1 partido clave.";
        }
        else if (tipo <= 85)
        {
            JugadorActivo.LesionActual = TipoLesion.DesgarroMuscular;
            JugadorActivo.PartidosRestantesLesion = 2;
            DiagnosticoLesion = "Desgarro en los isquiotibiales por fatiga. Baja médica confirmada por dos fechas.";
        }
        else
        {
            JugadorActivo.LesionActual = TipoLesion.RoturaLigamentosACL;
            JugadorActivo.PartidosRestantesLesion = 99;
            DiagnosticoLesion = "¡ROTURA DE LIGAMENTOS! Cirugía confirmada. TE PIERDES EL RESTO DEL AÑO.";
        }

        FaseActual = FaseJuego.AlertaLesion;
        Notificar();
    }

    public void ContinuarTrasLesion()
    {
        if (JugadorActivo?.LesionActual == TipoLesion.RoturaLigamentosACL)
        {
            Derrotas += 3;
            Victorias += 1;
            ProcesarFinDeTemporada();
            return;
        }

        JugadorActivo!.PartidosRestantesLesion = 0;
        JugadorActivo.LesionActual = TipoLesion.Ninguna;
        Energia = 80;
        FaseActual = FaseJuego.TemporadaRegular;
        ContinuarBucleTemporada();
    }

    private void EjecutarTraspasoForzado()
    {
        if (JugadorActivo == null) return;
        var otros = EquipoNBA.Franquicias.Where(f => f.Nombre != JugadorActivo.EquipoActual).ToList();
        var destino = otros[Random.Shared.Next(otros.Count)];

        string anterior = JugadorActivo.EquipoActual;
        JugadorActivo.EquipoActual = destino.Nombre;
        JugadorActivo.AniosEnEquipoActual = 1;
        JugadorActivo.EsJugadorFranquicia = false;
        MinijuegosFalladosConsecutivos = 0;
        EnRiesgoTraspaso = false;
        ConfianzaDT = 40;

        NoticiaTraspaso = $"¡BOMBAZO EN LA LIGA! {anterior} te ha traspasado forzosamente a {destino.Nombre} tras decepcionar en momentos de presión.";
        FaseActual = FaseJuego.AlertaTraspaso;
        Notificar();
    }

    public void ContinuarTrasTraspaso()
    {
        FaseActual = FaseJuego.TemporadaRegular;
        ContinuarBucleTemporada();
    }

    private void ContinuarBucleTemporada()
    {
        SimularBloquePartidos();

        if (DecisionesTomadasTemporada >= TotalDecisionesTemporada)
        {
            if (Victorias >= 3)
            {
                IniciarPlayoffsSimples();
            }
            else
            {
                ProcesarFinDeTemporada();
            }
            return;
        }

        SiguienteDilema();
    }

    private void SiguienteDilema()
    {
        int ovr = JugadorActivo?.OVR ?? 70;
        DilemaRPGActual = CatalogoDecisionesRPG.ObtenerSiguienteDilema(TemporadaActual, ovr, PickAsignado, Hype);
        Notificar();
    }

    public void IniciarPlayoffsSimples()
    {
        FaseActual = FaseJuego.Playoffs;
        RondaIndicePlayoffs = 1;
        VictoriasSeriePlayoff = 0;
        DerrotasSeriePlayoff = 0;

        if (Victorias >= 4) SeedPlayoffs = Random.Shared.Next(1, 3);
        else if (Victorias == 3) SeedPlayoffs = Random.Shared.Next(3, 6);
        else SeedPlayoffs = Random.Shared.Next(6, 9);

        // Cada postemporada selecciona aleatoriamente el formato temático
        var formatos = Enum.GetValues<FormatoPlayoffs>();
        FormatoPlayoffTemporada = (FormatoPlayoffs)formatos.GetValue(Random.Shared.Next(formatos.Length))!;

        ActualizarRivalRonda();
        Notificar();
    }

    public void ActualizarRivalRonda()
    {
        RivalPlayoffs = RondaIndicePlayoffs switch
        {
            1 => SeedPlayoffs <= 2 ? "Sacramento Kings" : "Denver Nuggets",
            2 => "Minnesota Timberwolves",
            3 => "Oklahoma City Thunder",
            4 => "Boston Celtics",
            _ => "Rival de Conferencia"
        };
    }

    public void AvanzarRondaPlayoffs(bool clasificado)
    {
        if (!clasificado)
        {
            UltimaNoticia = $"Eliminados en {NombreRondaActual()} ante {RivalPlayoffs}.";
            ProcesarFinDeTemporada();
            return;
        }

        if (RondaIndicePlayoffs >= 4)
        {
            AnillosCampeon++;
            UltimaNoticia = "¡CAMPEONES DEL MUNDO! Has ganado el anillo de la NBA.";
            ProcesarFinDeTemporada();
            return;
        }

        RondaIndicePlayoffs++;
        ActualizarRivalRonda();
        Notificar();
    }

    public void SimularPartidoPlayoff()
    {
        int media = JugadorActivo?.OVR ?? 75;
        bool ganado = Random.Shared.Next(1, 101) <= (media / 2) + 20;

        if (ganado) VictoriasSeriePlayoff++;
        else DerrotasSeriePlayoff++;

        if (VictoriasSeriePlayoff >= 4)
        {
            AvanzarRondaPlayoffs(true);
        }
        else if (DerrotasSeriePlayoff >= 4)
        {
            AvanzarRondaPlayoffs(false);
        }
        else
        {
            Notificar();
        }
    }

    private void SimularBloquePartidos()
    {
        if (JugadorActivo == null) return;
        UltimosPuntos = Random.Shared.Next(16, 32);
        UltimosRebotes = Random.Shared.Next(4, 11);
        UltimasAsistencias = Random.Shared.Next(4, 12);

        JugadorActivo.PuntosTotales += UltimosPuntos;
        JugadorActivo.PartidosTotales++;

        UltimoFueVictoria = Random.Shared.Next(1, 101) <= (JugadorActivo.OVR / 2) + 22;
        if (UltimoFueVictoria) Victorias++;
        else Derrotas++;

        Energia = Math.Clamp(Energia - 10, 0, 100);
    }

    private void ProcesarFinDeTemporada()
    {
        FaseActual = FaseJuego.FinTemporada;
        DineroDisponible += SalarioAnual;
        PuntosDesarrollo += (Victorias >= 3 ? 6 : 4);
        TemporadaActual++;
        Notificar();
    }

    public void AvanzarSiguienteTemporada()
    {
        IniciarTemporada();
    }

    public void RetirarJugador()
    {
        EnSalonDeLaFama = (AnillosCampeon >= 2 && PremiosMVP >= 1) || 
                         (AnillosCampeon >= 3) || 
                         (PremiosMVP >= 2 && TemporadaActual >= 8);
        FaseActual = FaseJuego.Retiro;
        Notificar();
    }

    public void Notificar() => OnStateChanged?.Invoke();
}
