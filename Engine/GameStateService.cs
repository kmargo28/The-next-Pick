using System.Text.Json;
using TheNextPick.Models;
using TheNextPick.Engine.Strategies;
using TheNextPick.Engine.Observer;

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
    AllStarWeekend,
    Playoffs,
    FinTemporada,
    AgenciaLibre,
    Retiro
}

public enum FormatoPlayoffs
{
    PizarraYFinalClutch,
    SenasYTirosLibres,
    TirosLibresYClutch,
    CampanaClutchCompleta
}

// SUJETO OBSERVABLE (ISujetoObservable) + CONTEXTO DE SIMULACIÓN (IEstrategiaSimulacion)
public class GameStateService : ISujetoObservable
{
    private readonly List<IObservadorJuego> _observadores = new();
    private IEstrategiaSimulacion _estrategiaSimulacion = new EstrategiaSimulacionDetallada();

    public string UltimoEventoNotificado { get; private set; } = "Inicio";
    public int CantidadObservadoresActivos => _observadores.Count;

    public Jugador? JugadorActivo { get; set; }
    public FaseJuego FaseActual { get; set; } = FaseJuego.Creacion;
    public RitmoSimulacion RitmoElegido { get; set; } = RitmoSimulacion.Detallada;

    public List<Insignia> CatalogoInsignias { get; set; } = new();
    public List<PremioIndividual> HistorialPremios { get; set; } = new();

    public int PuntosCombine { get; set; } = 0;
    public int PickAsignado { get; set; } = 60;
    public EquipoNBA? EquipoQueTeEligio { get; set; }

    public int MinijuegosFalladosConsecutivos { get; set; } = 0;
    public int InfraccionesAcumuladas { get; set; } = 0;
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
    public int TotalDecisionesTemporada => 6;
    public int PuntosDesarrollo { get; set; } = 0;
    public bool AllStarDisputadoEsteAno { get; set; } = false;

    public RondaPlayoffs RondaActualPlayoffs { get; set; } = RondaPlayoffs.PrimeraRonda;
    public int RondaIndicePlayoffs { get; set; } = 1;
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
    public string UltimaNoticia { get; set; } = "Comienza la temporada regular.";
    private OpcionDecisionRPG? opcionPendienteMinijuego;

    public event Action? OnStateChanged;

    public GameStateService()
    {
        InicializarInsignias();
    }

    // MÉTODOS DEL PATRÓN OBSERVER
    public void Suscribir(IObservadorJuego observador)
    {
        if (!_observadores.Contains(observador))
            _observadores.Add(observador);
    }

    public void Desuscribir(IObservadorJuego observador)
    {
        _observadores.Remove(observador);
    }

    public void Notificar(string eventoOrigen = "CambioDeEstado")
    {
        UltimoEventoNotificado = eventoOrigen;
        foreach (var obs in _observadores.ToList())
        {
            obs.Actualizar(eventoOrigen);
        }
        OnStateChanged?.Invoke();
    }

    // CAMBIO DE ESTRATEGIA DE SIMULACIÓN EN TIEMPO DE EJECUCIÓN
    public void EstablecerEstrategiaSimulacion(RitmoSimulacion ritmo)
    {
        RitmoElegido = ritmo;
        _estrategiaSimulacion = ritmo != RitmoSimulacion.Detallada
            ? new EstrategiaSimulacionRapida()
            : new EstrategiaSimulacionDetallada();
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

    public string TemporadaTextoActual => $"{2025 + TemporadaActual}-{((2026 + TemporadaActual) % 100):D2}";

    public void AgregarPremio(string nombre, CategoriaPremio cat, string icono, string desc, string color = "#FFC72C")
    {
        if (HistorialPremios.Any(p => p.Nombre == nombre && p.TemporadaTexto == TemporadaTextoActual))
            return;

        HistorialPremios.Add(new()
        {
            Nombre = nombre,
            TemporadaTexto = TemporadaTextoActual,
            Categoria = cat,
            Icono = icono,
            Descripcion = desc,
            ColorBorde = color
        });
    }

    public void EvaluarPremiosTemporada()
    {
        if (JugadorActivo == null) return;
        int ovr = JugadorActivo.OVR;
        string temp = TemporadaTextoActual;

        if (TemporadaActual == 1)
        {
            if (ovr >= 76 || PuntosCombine >= 70)
            {
                AgregarPremio("Rookie del Año (ROY)", CategoriaPremio.TrofeoMayor, "🏆",
                    "Elegido como el mejor novato de la clase del Draft tras una campaña dominante.", "#FFC72C");
                AgregarPremio("NBA All-Rookie First Team", CategoriaPremio.QuintetoIdeal, "⭐",
                    "Miembro del Mejor Quinteto de Novatos de la liga.", "#10b981");
            }
            else
            {
                AgregarPremio("NBA All-Rookie Second Team", CategoriaPremio.QuintetoIdeal, "⭐",
                    "Miembro del Segundo Mejor Quinteto de Novatos de la liga.", "#38bdf8");
            }
        }

        if (ovr >= 88 && Victorias >= 4)
        {
            PremiosMVP++;
            AgregarPremio("Most Valuable Player (MVP)", CategoriaPremio.TrofeoMayor, "👑",
                $"Galardonado con el trofeo Michael Jordan al jugador más determinante de la NBA ({temp}).", "#FFC72C");
        }

        if (ovr >= 86 && Victorias >= 3)
        {
            AgregarPremio("All-NBA First Team", CategoriaPremio.QuintetoIdeal, "🥇",
                "Seleccionado en el Primer Quinteto Ideal oficial de la liga entre los 5 mejores del mundo.", "#FFC72C");
        }
        else if (ovr >= 80)
        {
            AgregarPremio("All-NBA Second Team", CategoriaPremio.QuintetoIdeal, "🥈",
                "Seleccionado en el Segundo Quinteto Ideal de la NBA.", "#94a3b8");
        }
        else if (ovr >= 76 && Victorias >= 4)
        {
            AgregarPremio("All-NBA Third Team", CategoriaPremio.QuintetoIdeal, "🥉",
                "Reconocido entre los mejores 15 jugadores de la temporada regular.", "#b45309");
        }

        if (JugadorActivo.Defensa >= 88)
        {
            AgregarPremio("Defensor del Año (DPOY)", CategoriaPremio.TrofeoMayor, "🛡️",
                "Elegido el mejor ancla defensivo del campeonato.", "#00f0ff");
            AgregarPremio("NBA All-Defensive First Team", CategoriaPremio.QuintetoIdeal, "🛡️",
                "Titular en el Mejor Quinteto Defensivo de la NBA.", "#00f0ff");
        }
        else if (JugadorActivo.Defensa >= 82)
        {
            AgregarPremio("NBA All-Defensive Second Team", CategoriaPremio.QuintetoIdeal, "🛡️",
                "Integrante del Segundo Mejor Quinteto Defensivo.", "#64748b");
        }

        if (Hype >= 55 && ovr >= 78)
        {
            AgregarPremio("NBA All-Star", CategoriaPremio.AllStar, "⭐",
                $"Seleccionado para el All-Star Game ({temp}).", "#38bdf8");
        }
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
        InfraccionesAcumuladas = 0;
        EnRiesgoTraspaso = false;
        HistorialPremios.Clear();
        InicializarInsignias();
        CatalogoDecisionesRPG.ReiniciarMemoriaHistorica();
        Notificar("NuevaCarrera");
    }

    public void IrADraftCombine(Jugador jugador, RitmoSimulacion ritmo)
    {
        if (jugador == null) throw new ArgumentNullException(nameof(jugador));
        JugadorActivo = jugador;
        EstablecerEstrategiaSimulacion(ritmo);
        PuntosCombine = 0;
        FaseActual = FaseJuego.DraftCombine;
        Notificar("InicioDraftCombine");
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
            EquipoQueTeEligio = EquipoNBA.Franquicias[0];

        AsignarContratoRookie(EquipoQueTeEligio.Nombre, PickAsignado);
        FaseActual = FaseJuego.EleccionEquipo;
        Notificar("CeremoniaDraft");
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
        if (JugadorActivo == null)
            throw new InvalidOperationException("No se puede iniciar temporada sin un prospecto creado.");
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
        InfraccionesAcumuladas = 0;
        MinijuegosFalladosConsecutivos = 0;
        EnRiesgoTraspaso = false;
        AllStarDisputadoEsteAno = false;

        CatalogoDecisionesRPG.IniciarNuevaTemporada();
        SiguienteDilema();
        FaseActual = FaseJuego.TemporadaRegular;
        Notificar("InicioTemporada");
    }

    public void TomarDecision(OpcionDecisionRPG opcion)
    {
        if (JugadorActivo == null) return;

        if (opcion.DesencadenaArcoId > 0)
            CatalogoDecisionesRPG.ArcosDesencadenados.Add(opcion.DesencadenaArcoId);

        if (opcion.ActivaMinijuegoEnCancha)
        {
            opcionPendienteMinijuego = opcion;
            FaseActual = FaseJuego.MinijuegoTiro;
            Notificar("InicioMinijuegoClutch");
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
                ConfianzaDT = Math.Clamp(ConfianzaDT + 12, 0, 100);
                Hype = Math.Clamp(Hype + 10, 0, 100);
                UltimaNoticia = "Actuación clutch determinante en la posesión final.";
            }
            else
            {
                MinijuegosFalladosConsecutivos++;
                InfraccionesAcumuladas++;
                ConfianzaDT = Math.Clamp(ConfianzaDT - 18, 0, 100);
                Hype = Math.Clamp(Hype - 10, 0, 100);

                if ((EnRiesgoTraspaso || MinijuegosFalladosConsecutivos >= 2) && !JugadorActivo!.EsJugadorFranquicia)
                {
                    EjecutarTraspasoForzado("Fallas reiteradas en momentos decisivos y pérdida total de confianza del DT.");
                    return;
                }

                EnRiesgoTraspaso = true;
                UltimaNoticia = "⚠️ ATENCIÓN: Fallaste en el clutch. La directiva te pone en la LISTA DE TRASPASOS.";
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

            if (opcion.ConvierteEnJugadorFranquicia)
            {
                JugadorActivo.EsJugadorFranquicia = true;
                EnRiesgoTraspaso = false;
                InfraccionesAcumuladas = 0;
            }

            Energia = Math.Clamp(Energia + opcion.DeltaEnergia, 0, 100);
            ConfianzaDT = Math.Clamp(ConfianzaDT + opcion.DeltaConfianzaDT, 0, 100);
            Hype = Math.Clamp(Hype + opcion.DeltaHype, 0, 100);
            Quimica = Math.Clamp(Quimica + opcion.DeltaQuimica, 0, 100);
            DineroDisponible += opcion.DeltaDinero;

            UltimaNoticia = opcion.Retroalimentacion;

            if (opcion.AumentaRiesgoTraspaso && !JugadorActivo.EsJugadorFranquicia)
            {
                InfraccionesAcumuladas++;
                if (EnRiesgoTraspaso || InfraccionesAcumuladas >= 2)
                {
                    EjecutarTraspasoForzado("Ruptura total con el cuerpo técnico y la directiva tras reiteradas indisciplinas.");
                    return;
                }
                EnRiesgoTraspaso = true;
            }
            else if (ConfianzaDT >= 50 && Quimica >= 45 && InfraccionesAcumuladas == 0)
            {
                EnRiesgoTraspaso = false;
            }
        }
        else
        {
            MinijuegosFalladosConsecutivos++;
            InfraccionesAcumuladas++;
            ConfianzaDT = Math.Clamp(ConfianzaDT - 22, 0, 100);
            Hype = Math.Clamp(Hype - 15, 0, 100);
            UltimaNoticia = "Fallaste en el momento cumbre. La prensa y el DT cuestionan tu nivel.";

            if ((EnRiesgoTraspaso || InfraccionesAcumuladas >= 2) && !JugadorActivo.EsJugadorFranquicia)
            {
                EjecutarTraspasoForzado("Decepcionante rendimiento en el tramo clutch y quiebre de confianza con el club.");
                return;
            }

            if (!JugadorActivo.EsJugadorFranquicia)
                EnRiesgoTraspaso = true;
        }

        if ((ConfianzaDT <= 15 || Quimica <= 12) && !JugadorActivo.EsJugadorFranquicia)
        {
            EjecutarTraspasoForzado("Relación irreconciliable con el vestuario y la dirección deportiva.");
            return;
        }

        if ((opcion.ProbabilidadLesionDirecta > 0 && Random.Shared.Next(1, 101) <= opcion.ProbabilidadLesionDirecta) ||
            (Energia < 20 && Random.Shared.Next(1, 101) <= 35))
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
            DiagnosticoLesion = "Desgarro en los isquiotibiales por fatiga acumulada. Dos fechas de baja.";
        }
        else
        {
            JugadorActivo.LesionActual = TipoLesion.RoturaLigamentosACL;
            JugadorActivo.PartidosRestantesLesion = 99;
            DiagnosticoLesion = "¡ROTURA DE LIGAMENTOS! Cirugía confirmada. TE PIERDES EL RESTO DEL AÑO.";
        }

        FaseActual = FaseJuego.AlertaLesion;
        Notificar("AlertaLesion");
    }

    public void ContinuarTrasLesion()
    {
        if (JugadorActivo?.LesionActual == TipoLesion.RoturaLigamentosACL)
        {
            Derrotas += 8;
            Victorias += 2;
            ProcesarFinDeTemporada();
            return;
        }

        JugadorActivo!.PartidosRestantesLesion = 0;
        JugadorActivo.LesionActual = TipoLesion.Ninguna;
        Energia = 80;
        FaseActual = FaseJuego.TemporadaRegular;
        ContinuarBucleTemporada();
    }

    public void EjecutarTraspasoForzado(string motivo = "Bajo rendimiento e indisciplina en el club.")
    {
        if (JugadorActivo == null || JugadorActivo.EsJugadorFranquicia) return;
        var otros = EquipoNBA.Franquicias.Where(f => f.Nombre != JugadorActivo.EquipoActual).ToList();
        var destino = otros[Random.Shared.Next(otros.Count)];

        string anterior = JugadorActivo.EquipoActual;
        JugadorActivo.EquipoActual = destino.Nombre;
        JugadorActivo.AniosEnEquipoActual = 1;
        JugadorActivo.EsJugadorFranquicia = false;
        MinijuegosFalladosConsecutivos = 0;
        InfraccionesAcumuladas = 0;
        EnRiesgoTraspaso = false;
        ConfianzaDT = 40;

        NoticiaTraspaso = $"¡BOMBAZO EN LA NBA! {anterior} te ha traspasado formalmente a {destino.Nombre.ToUpper()}. Motivo oficial: {motivo}";
        FaseActual = FaseJuego.AlertaTraspaso;
        Notificar("AlertaTraspaso");
    }

    public void ContinuarTrasTraspaso()
    {
        FaseActual = FaseJuego.TemporadaRegular;
        ContinuarBucleTemporada();
    }

    private void ContinuarBucleTemporada()
    {
        SimularBloquePartidos();

        if (DecisionesTomadasTemporada == 3 && !AllStarDisputadoEsteAno)
        {
            AllStarDisputadoEsteAno = true;
            FaseActual = FaseJuego.AllStarWeekend;
            Notificar("AllStarWeekend");
            return;
        }

        if (DecisionesTomadasTemporada >= TotalDecisionesTemporada)
        {
            EvaluarPremiosTemporada();

            if (Victorias >= 4)
                IniciarPlayoffsSimples();
            else
                ProcesarFinDeTemporada();
            return;
        }

        SiguienteDilema();
    }

    public void ContinuarTrasAllStar()
    {
        FaseActual = FaseJuego.TemporadaRegular;
        SiguienteDilema();
    }

    private void SiguienteDilema()
    {
        int ovr = JugadorActivo?.OVR ?? 70;
        DilemaRPGActual = CatalogoDecisionesRPG.ObtenerSiguienteDilema(TemporadaActual, ovr, PickAsignado, Hype, EnRiesgoTraspaso);
        Notificar("NuevaDecision");
    }

    public void IniciarPlayoffsSimples()
    {
        FaseActual = FaseJuego.Playoffs;
        RondaIndicePlayoffs = 1;
        VictoriasSeriePlayoff = 0;
        DerrotasSeriePlayoff = 0;

        if (Victorias >= 5) SeedPlayoffs = Random.Shared.Next(1, 3);
        else if (Victorias >= 4) SeedPlayoffs = Random.Shared.Next(3, 6);
        else SeedPlayoffs = Random.Shared.Next(6, 9);

        var formatos = Enum.GetValues<FormatoPlayoffs>();
        FormatoPlayoffTemporada = (FormatoPlayoffs)formatos.GetValue(Random.Shared.Next(formatos.Length))!;

        ActualizarRivalRonda();
        Notificar("InicioPlayoffs");
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

            AgregarPremio("Campeón de la NBA (Anillo Larry O'Brien)", CategoriaPremio.Campeonato, "💍",
                $"Campeón absoluto de la National Basketball Association ({TemporadaTextoActual}).", "#FFC72C");

            if ((JugadorActivo?.OVR ?? 70) >= 80)
            {
                AgregarPremio("Bill Russell Finals MVP", CategoriaPremio.TrofeoMayor, "🏆",
                    "Elegido Jugador Más Valioso de las Finales de la NBA.", "#FFC72C");
            }

            ProcesarFinDeTemporada();
            return;
        }

        RondaIndicePlayoffs++;
        ActualizarRivalRonda();
        Notificar("SiguienteRondaPlayoffs");
    }

    // DELEGA EL CÁLCULO DE PARTIDOS A LA ESTRATEGIA ACTIVA (IEstrategiaSimulacion)
    private void SimularBloquePartidos()
    {
        if (JugadorActivo == null) return;
        var resultado = _estrategiaSimulacion.SimularBloque(JugadorActivo, Quimica, Energia);

        UltimosPuntos = resultado.Puntos;
        UltimosRebotes = resultado.Rebotes;
        UltimasAsistencias = resultado.Asistencias;
        UltimoFueVictoria = resultado.FueVictoria;

        JugadorActivo.PuntosTotales += UltimosPuntos;
        JugadorActivo.PartidosTotales++;

        if (UltimoFueVictoria) Victorias++;
        else Derrotas++;

        Energia = Math.Clamp(Energia - resultado.DesgasteEnergia, 0, 100);
    }

    private void ProcesarFinDeTemporada()
    {
        FaseActual = FaseJuego.FinTemporada;
        DineroDisponible += SalarioAnual;
        PuntosDesarrollo += (Victorias >= 4 ? 6 : 4);
        TemporadaActual++;
        Notificar("FinTemporada");
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
        Notificar("Retiro");
    }
}

