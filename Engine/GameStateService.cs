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
    MinijuegoCancha = MinijuegoTiro,
    MinijuegoLectura,
    MinijuegoDuelo,
    AllStarInvitacion,
    AllStarTriples,
    PortadaHito,
    Playoffs,
    PlayoffBossBattle,
    PlayoffTirosLibres,
    PlayoffReboteBoxOut,
    PlayoffContraataqueDunk,
    FinTemporada,
    AgenciaLibre,
    Retiro
}

public class GameStateService
{
    public Jugador? JugadorActivo { get; set; }
    public FaseJuego FaseActual { get; set; } = FaseJuego.Creacion;
    public RitmoSimulacion RitmoElegido { get; set; } = RitmoSimulacion.Detallada;

    // Draft y Némesis
    public int PuntosCombine { get; set; } = 0;
    public int PickAsignado { get; set; } = 60;
    public List<EquipoNBA> EquiposDisponiblesDraft { get; set; } = new();
    public string NombreRivalDraft { get; set; } = "Darius Cole";
    public string EquipoRivalDraft { get; set; } = "Boston Celtics";
    public int PpgRivalDraft { get; set; } = 18;

    // Dúo Dinámico (Plantilla NBA 2026-2027)
    public string EstrellaCompanero { get; set; } = "";
    public int QuimicaEstrella { get; set; } = 50;

    // Finanzas ($)
    public int SalarioAnual { get; set; } = 4_500_000;
    public int DineroDisponible { get; set; } = 4_500_000;
    public bool TieneChefPersonal { get; set; } = false;
    public bool TieneZapatillasExclusivas { get; set; } = false;
    public bool TieneInversionStartup { get; set; } = false;

    // Recursos (0 - 100)
    public int Energia { get; set; } = 100;
    public int ConfianzaDT { get; set; } = 50;
    public int Quimica { get; set; } = 50;
    public int Hype { get; set; } = 30;

    // Temporada
    public int TemporadaActual { get; set; } = 1;
    public int Victorias { get; set; } = 0;
    public int Derrotas { get; set; } = 0;
    public int PartidosJugados => Victorias + Derrotas;
    public int DecisionesTomadasTemporada { get; set; } = 0;
    public int TotalDecisionesTemporada => RitmoElegido == RitmoSimulacion.Detallada ? 4 : 2;
    public int PuntosDesarrollo { get; set; } = 0;
    private bool allStarDisputado = false;

    // Portada de Revista
    public string TitularPortada { get; set; } = "";
    public string SubtituloPortada { get; set; } = "";
    public string ImagenPortada { get; set; } = "";

    // Playoffs
    public RondaPlayoffs RondaActualPlayoffs { get; set; } = RondaPlayoffs.PrimeraRonda;
    public int VictoriasSeriePlayoff { get; set; } = 0;
    public int DerrotasSeriePlayoff { get; set; } = 0;
    public string RivalPlayoffs { get; set; } = "Rival de Conferencia";

    // Box Score
    public int UltimosPuntos { get; set; } = 0;
    public int UltimosRebotes { get; set; } = 0;
    public int UltimasAsistencias { get; set; } = 0;
    public bool UltimoFueVictoria { get; set; } = false;

    // Palmarés
    public int AnillosCampeon { get; set; } = 0;
    public int PremiosMVP { get; set; } = 0;
    public bool GanadorROTY { get; set; } = false;
    public int PremiosDPOY { get; set; } = 0;
    public bool EnSalonDeLaFama { get; set; } = false;
    public List<string> PremiosGanadosTemporada { get; set; } = new();

    // Dilemas
    public Dilema? DilemaActual { get; set; }
    public string UltimaNoticia { get; set; } = "Comienza la temporada NBA 2026-2027.";
    private readonly List<Dilema> _mazoDilemas = new();
    private int _indiceDilema = 0;

    public event Action? OnStateChanged;

    public void IniciarCreacion()
    {
        JugadorActivo = null;
        FaseActual = FaseJuego.Creacion;
        AnillosCampeon = 0;
        PremiosMVP = 0;
        GanadorROTY = false;
        PremiosDPOY = 0;
        EnSalonDeLaFama = false;
        TemporadaActual = 1;
        DineroDisponible = 4_500_000;
        TieneChefPersonal = false;
        TieneZapatillasExclusivas = false;
        TieneInversionStartup = false;
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

    public void IniciarRutaAleatoria(Jugador jugador, string nombreEquipo, RitmoSimulacion ritmo)
    {
        JugadorActivo = jugador;
        JugadorActivo.EquipoActual = nombreEquipo;
        RitmoElegido = ritmo;
        PickAsignado = Random.Shared.Next(15, 45);
        IniciarTemporada();
    }

    public void FinalizarCombine(int puntos)
    {
        PuntosCombine = puntos;
        EquiposDisponiblesDraft = EquipoNBA.Franquicias.OrderBy(_ => Random.Shared.Next()).Take(3).ToList();

        if (puntos >= 75)
        {
            PickAsignado = Random.Shared.Next(1, 4);
            Hype = 80;
        }
        else if (puntos >= 50)
        {
            PickAsignado = Random.Shared.Next(4, 15);
            Hype = 60;
        }
        else
        {
            PickAsignado = Random.Shared.Next(15, 55);
            Hype = 35;
        }

        FaseActual = FaseJuego.EleccionEquipo;
        Notificar();
    }

    public void SeleccionarEquipoDraft(string equipo)
    {
        if (JugadorActivo != null) JugadorActivo.EquipoActual = equipo;
        IniciarTemporada();
    }

    private void IniciarTemporada()
    {
        var eq = EquipoNBA.ObtenerPorNombre(JugadorActivo?.EquipoActual ?? "");
        EstrellaCompanero = eq.EstrellaFranquicia;
        QuimicaEstrella = 50;

        var rivalEq = EquipoNBA.Franquicias.FirstOrDefault(f => f.Nombre != eq.Nombre) ?? EquipoNBA.Franquicias[1];
        EquipoRivalDraft = rivalEq.Nombre;
        NombreRivalDraft = JugadorActivo?.Posicion == Posicion.Base ? "Darius Cole" : "Trevor Vance";
        PpgRivalDraft = Random.Shared.Next(17, 24);

        Energia = 100;
        ConfianzaDT = 55;
        Quimica = 50;
        Victorias = 0;
        Derrotas = 0;
        DecisionesTomadasTemporada = 0;
        allStarDisputado = false;
        _indiceDilema = 0;
        FaseActual = FaseJuego.TemporadaRegular;
        CargarMazoTemporada();
        SiguienteDilema();
    }

    public void TomarOpcion(OpcionDilema opcion)
    {
        DecisionesTomadasTemporada++;

        int deltaE = opcion.DeltaEnergia;
        if (TieneChefPersonal && deltaE < 0) deltaE += 5;

        int deltaH = opcion.DeltaHype;
        if (TieneZapatillasExclusivas && deltaH > 0) deltaH = (int)(deltaH * 1.3);

        Energia = Math.Clamp(Energia + deltaE, 0, 100);
        ConfianzaDT = Math.Clamp(ConfianzaDT + opcion.DeltaDT, 0, 100);
        Quimica = Math.Clamp(Quimica + opcion.DeltaQuimica, 0, 100);
        Hype = Math.Clamp(Hype + deltaH, 0, 100);
        QuimicaEstrella = Math.Clamp(QuimicaEstrella + (opcion.DeltaQuimica / 2), 0, 100);
        UltimaNoticia = opcion.Retroalimentacion;

        // Concurso de Triples: Exige tiro estricto (+87) y Hype (+70)
                // Concurso de Triples: Exige tiro estricto (+87) y Hype (+70) con invitación opcional
        if (!allStarDisputado && DecisionesTomadasTemporada == 2)
        {
            allStarDisputado = true;
            bool califica = (JugadorActivo?.TiroExterior >= 87) && (Hype >= 70);

            if (califica)
            {
                FaseActual = FaseJuego.AllStarInvitacion;
                UltimaNoticia = "¡Invitación estelar recibida! La NBA te convoca al Concurso de Triples All-Star 2027.";
                Notificar();
                return;
            }
            else
            {
                UltimaNoticia = "All-Star Weekend: No alcanzaste el mínimo de +87 de tiro exterior para ser invitado al Concurso de Triples.";
            }
        }

        // Minijuegos regulares moderados (25% en momentos de partido)
        if (DilemaActual?.EsMomentoPartido == true && Random.Shared.Next(1, 100) <= 25)
        {
            int azar = Random.Shared.Next(0, 3);
            FaseActual = azar switch
            {
                0 => FaseJuego.MinijuegoTiro,
                1 => FaseJuego.MinijuegoLectura,
                _ => FaseJuego.MinijuegoDuelo
            };
            Notificar();
            return;
        }

        ContinuarBucleTemporada();
    }

        public void ResponderInvitacionAllStar(bool aceptar)
    {
        if (aceptar)
        {
            FaseActual = FaseJuego.AllStarTriples;
            UltimaNoticia = "¡Confirmaste tu participación en el Concurso de Triples de la NBA!";
        }
        else
        {
            // Decisión inteligente de descanso físico
            Energia = Math.Clamp(Energia + 25, 0, 100);
            FaseActual = FaseJuego.TemporadaRegular;
            UltimaNoticia = "Declinaste la invitación del All-Star. Aprovechaste la pausa para recuperar +25% de energía física.";
            ContinuarBucleTemporada();
        }
        Notificar();
    }

    public void FinalizarAllStarTriples(int puntos)
    {
        if (puntos >= 18)
        {
            Hype = Math.Clamp(Hype + 22, 0, 100);
            DineroDisponible += 750_000;
            LanzarPortada("CAMPEÓN DEL ALL-STAR", "Consagrado el mejor tirador de la NBA", "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80");
        }
        else
        {
            Hype = Math.Clamp(Hype + 6, 0, 100);
            FaseActual = FaseJuego.TemporadaRegular;
            ContinuarBucleTemporada();
        }
    }

    public void ResolverMinijuego(bool exitoso)
    {
        if (exitoso)
        {
            ConfianzaDT = Math.Clamp(ConfianzaDT + 10, 0, 100);
            Hype = Math.Clamp(Hype + 12, 0, 100);
            UltimaNoticia = "Intervención individual clave en la posesión final.";
        }
        else
        {
            ConfianzaDT = Math.Clamp(ConfianzaDT - 6, 0, 100);
            UltimaNoticia = "Fallo táctico en el cierre del juego.";
        }

        if (FaseActual == FaseJuego.Playoffs || FaseActual == FaseJuego.PlayoffBossBattle || FaseActual == FaseJuego.PlayoffTirosLibres)
        {
            ResolverPartidoPlayoff(exitoso);
        }
        else
        {
            FaseActual = FaseJuego.TemporadaRegular;
            ContinuarBucleTemporada();
        }
    }

    private void ContinuarBucleTemporada()
    {
        SimularBloquePartidos();

        if (DecisionesTomadasTemporada >= TotalDecisionesTemporada)
        {
            if (Victorias >= 3)
            {
                IniciarPlayoffs();
            }
            else
            {
                ProcesarFinDeTemporada();
            }
            return;
        }

        SiguienteDilema();
    }

    public bool ComprarChefPersonal()
    {
        if (DineroDisponible >= 1_200_000 && !TieneChefPersonal)
        {
            DineroDisponible -= 1_200_000;
            TieneChefPersonal = true;
            Energia = Math.Clamp(Energia + 20, 0, 100);
            Notificar();
            return true;
        }
        return false;
    }

    public bool ComprarZapatillasExclusivas()
    {
        if (DineroDisponible >= 2_000_000 && !TieneZapatillasExclusivas)
        {
            DineroDisponible -= 2_000_000;
            TieneZapatillasExclusivas = true;
            Hype = Math.Clamp(Hype + 20, 0, 100);
            Notificar();
            return true;
        }
        return false;
    }

    public bool ComprarInversionStartup()
    {
        if (DineroDisponible >= 1_500_000 && !TieneInversionStartup)
        {
            DineroDisponible -= 1_500_000;
            TieneInversionStartup = true;
            Notificar();
            return true;
        }
        return false;
    }

    private void IniciarPlayoffs()
    {
        FaseActual = FaseJuego.Playoffs;
        RondaActualPlayoffs = RondaPlayoffs.PrimeraRonda;
        VictoriasSeriePlayoff = 0;
        DerrotasSeriePlayoff = 0;
        RivalPlayoffs = ObtenerRivalAleatorio();
        UltimaNoticia = $"Postemporada: Primera Ronda contra {RivalPlayoffs}.";
        Notificar();
    }

        private int ultimoMinijuegoPlayoff = -1;

    public void JugarPartidoPlayoff(bool asumirLiderazgo)
    {
        if (asumirLiderazgo)
        {
            // Rotación inteligente entre los 4 minijuegos de Playoffs (sin repetir el anterior)
            int proximo;
            do
            {
                proximo = Random.Shared.Next(0, 4);
            } while (proximo == ultimoMinijuegoPlayoff);

            ultimoMinijuegoPlayoff = proximo;

            FaseActual = proximo switch
            {
                0 => FaseJuego.PlayoffBossBattle,
                1 => FaseJuego.PlayoffTirosLibres,
                2 => FaseJuego.PlayoffReboteBoxOut,
                _ => FaseJuego.PlayoffContraataqueDunk
            };
            Notificar();
        }
        else
        {
            int media = JugadorActivo != null ? (JugadorActivo.TiroExterior + JugadorActivo.Defensa) / 2 : 70;
            int dificultadRonda = (int)RondaActualPlayoffs * 9;
            int probBase = 42 - dificultadRonda;
            int probVictoria = probBase + (media / 5) + (Quimica / 5) + (QuimicaEstrella / 8);

            if (Energia < 50) probVictoria -= 25;

            bool ganado = Random.Shared.Next(1, 101) <= Math.Clamp(probVictoria, 8, 80);
            ResolverPartidoPlayoff(ganado);
        }
    }

    private void ResolverPartidoPlayoff(bool victoria)
    {
        FaseActual = FaseJuego.Playoffs;
        if (victoria) VictoriasSeriePlayoff++;
        else DerrotasSeriePlayoff++;

        Energia = Math.Clamp(Energia - 18, 0, 100);

        if (VictoriasSeriePlayoff >= 2)
        {
            if (RondaActualPlayoffs == RondaPlayoffs.FinalesNBA)
            {
                AnillosCampeon++;
                PremiosGanadosTemporada.Add("Anillo de Campeón de la NBA");
                LanzarPortada("¡CAMPEONES DEL MUNDO!", $"El trofeo Larry O'Brien viaja con {JugadorActivo?.EquipoActual}", "https://images.unsplash.com/photo-1519766304817-4f37bda74a29?w=700&auto=format&fit=crop&q=80");
                return;
            }

            RondaActualPlayoffs = (RondaPlayoffs)((int)RondaActualPlayoffs + 1);
            VictoriasSeriePlayoff = 0;
            DerrotasSeriePlayoff = 0;
            RivalPlayoffs = ObtenerRivalAleatorio();
            UltimaNoticia = $"Avanzaron a {NombreRonda(RondaActualPlayoffs)} frente a {RivalPlayoffs}.";
        }
        else if (DerrotasSeriePlayoff >= 2)
        {
            UltimaNoticia = $"Eliminados en {NombreRonda(RondaActualPlayoffs)} ante {RivalPlayoffs}. Temporada concluida.";
            ProcesarFinDeTemporada();
            return;
        }

        Notificar();
    }

    private void LanzarPortada(string titular, string subtitulo, string imagen)
    {
        TitularPortada = titular;
        SubtituloPortada = subtitulo;
        ImagenPortada = imagen;
        FaseActual = FaseJuego.PortadaHito;
        Notificar();
    }

    public void CerrarPortada()
    {
        if (AnillosCampeon > 0 && RondaActualPlayoffs == RondaPlayoffs.FinalesNBA)
        {
            ProcesarFinDeTemporada();
        }
        else
        {
            FaseActual = FaseJuego.TemporadaRegular;
            ContinuarBucleTemporada();
        }
    }

    public string NombreRonda(RondaPlayoffs ronda) => ronda switch
    {
        RondaPlayoffs.PrimeraRonda => "Primera Ronda de Conferencia",
        RondaPlayoffs.SemifinalesConferencia => "Semifinales de Conferencia",
        RondaPlayoffs.FinalesConferencia => "Finales de Conferencia",
        RondaPlayoffs.FinalesNBA => "Finales de la NBA",
        _ => "Postemporada"
    };

    private string ObtenerRivalAleatorio()
    {
        var rivals = EquipoNBA.Franquicias.Where(f => f.Nombre != JugadorActivo?.EquipoActual).ToList();
        return rivals[Random.Shared.Next(rivals.Count)].Nombre;
    }

    private void SimularBloquePartidos()
    {
        if (JugadorActivo == null) return;

        int minutos = Math.Clamp((ConfianzaDT * 38) / 100, 14, 38);
        int basePuntos = (JugadorActivo.TiroExterior + JugadorActivo.TiroMedia) / 8;
        UltimosPuntos = Random.Shared.Next(basePuntos - 3, basePuntos + 10) * (minutos / 20);
        UltimosPuntos = Math.Max(4, UltimosPuntos);

        UltimosRebotes = Random.Shared.Next(Math.Max(1, (JugadorActivo.Rebote / 10) - 2), (JugadorActivo.Rebote / 10) + 6);
        UltimasAsistencias = Random.Shared.Next(Math.Max(1, (JugadorActivo.Pase / 12) - 2), (JugadorActivo.Pase / 12) + 7);

        JugadorActivo.PuntosTotales += UltimosPuntos;
        JugadorActivo.RebotesTotales += UltimosRebotes;
        JugadorActivo.AsistenciasTotales += UltimasAsistencias;
        JugadorActivo.PartidosTotales++;

        int media = (JugadorActivo.TiroExterior + JugadorActivo.Pase + JugadorActivo.Defensa) / 3;
        int probVictoria = 30 + (media / 4) + (Quimica / 4) + (ConfianzaDT / 4);
        if (Energia < 30) probVictoria -= 25;

        UltimoFueVictoria = Random.Shared.Next(1, 101) <= probVictoria;
        if (UltimoFueVictoria)
        {
            Victorias++;
            JugadorActivo.VictoriasTotales++;
        }
        else
        {
            Derrotas++;
            JugadorActivo.DerrotasTotales++;
        }

        Energia = Math.Clamp(Energia - 10, 0, 100);
    }

    private void ProcesarFinDeTemporada()
    {
        FaseActual = FaseJuego.FinTemporada;
        PremiosGanadosTemporada.Clear();

        DineroDisponible += SalarioAnual;
        if (TieneInversionStartup) DineroDisponible += 800_000;

                // Entrega equilibrada: 4 base + 2 por récord ganador + 2 por títulos/premios
        int puntosGanados = 4;
        if (Victorias >= 3) puntosGanados += 2;
        if (PremiosGanadosTemporada.Count > 0) puntosGanados += 2;
        PuntosDesarrollo += puntosGanados;

        int media = JugadorActivo != null ? (JugadorActivo.TiroExterior + JugadorActivo.Pase + JugadorActivo.Defensa) / 3 : 70;
        bool cumpleMVP = Hype >= 88 && Victorias >= 3 && media >= 84;
        if (cumpleMVP)
        {
            PremiosMVP++;
            PremiosGanadosTemporada.Add("Jugador Más Valioso (MVP)");
        }

        bool cumpleROTY = TemporadaActual == 1 && PickAsignado <= 8 && Hype >= 70 && UltimosPuntos >= 18;
        if (!GanadorROTY && cumpleROTY)
        {
            GanadorROTY = true;
            PremiosGanadosTemporada.Add("Novato del Año (ROTY)");
        }

        if (JugadorActivo?.Defensa >= 87 && Quimica >= 75)
        {
            PremiosDPOY++;
            PremiosGanadosTemporada.Add("Defensor del Año (DPOY)");
        }

        Notificar();
    }

        public void MejorarAtributo(string atributo)
    {
        if (JugadorActivo == null || PuntosDesarrollo <= 0) return;
        switch (atributo)
        {
            case "TiroExterior": if (JugadorActivo.TiroExterior < 99) { JugadorActivo.TiroExterior = Math.Min(99, JugadorActivo.TiroExterior + 3); PuntosDesarrollo--; } break;
            case "TiroMedia": if (JugadorActivo.TiroMedia < 99) { JugadorActivo.TiroMedia = Math.Min(99, JugadorActivo.TiroMedia + 3); PuntosDesarrollo--; } break;
            case "Pase": if (JugadorActivo.Pase < 99) { JugadorActivo.Pase = Math.Min(99, JugadorActivo.Pase + 3); PuntosDesarrollo--; } break;
            case "Defensa": if (JugadorActivo.Defensa < 99) { JugadorActivo.Defensa = Math.Min(99, JugadorActivo.Defensa + 3); PuntosDesarrollo--; } break;
            case "Rebote": if (JugadorActivo.Rebote < 99) { JugadorActivo.Rebote = Math.Min(99, JugadorActivo.Rebote + 3); PuntosDesarrollo--; } break;
            case "Atletismo": if (JugadorActivo.Atletismo < 99) { JugadorActivo.Atletismo = Math.Min(99, JugadorActivo.Atletismo + 3); PuntosDesarrollo--; } break;
        }
        Notificar();
    }

    public void AvanzarSiguienteTemporada()
    {
        if (JugadorActivo == null) return;
        int saltoAnios = RitmoElegido == RitmoSimulacion.Expres ? 2 : 1;

        TemporadaActual += saltoAnios;
        JugadorActivo.Edad += saltoAnios;
        JugadorActivo.AniosEnLiga += saltoAnios;

        if (PremiosMVP > 0 || AnillosCampeon > 0) SalarioAnual = 38_000_000;
        else if (JugadorActivo.OVR >= 85) SalarioAnual = 22_000_000;

        if (JugadorActivo.Edad >= 31)
        {
            JugadorActivo.Atletismo = Math.Max(40, JugadorActivo.Atletismo - (saltoAnios * 2));
        }

        if (TemporadaActual % 4 == 0) FaseActual = FaseJuego.AgenciaLibre;
        else IniciarTemporada();

        Notificar();
    }

    public void FirmarAgenciaLibre(string nuevoEquipo)
    {
        if (JugadorActivo != null) JugadorActivo.EquipoActual = nuevoEquipo;
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

    public string ExportarEstadoJson() => JsonSerializer.Serialize(this);

    public void ImportarEstadoJson(string json)
    {
        var cargado = JsonSerializer.Deserialize<GameStateService>(json);
        if (cargado != null)
        {
            JugadorActivo = cargado.JugadorActivo;
            FaseActual = cargado.FaseActual;
            RitmoElegido = cargado.RitmoElegido;
            PuntosCombine = cargado.PuntosCombine;
            PickAsignado = cargado.PickAsignado;
            EquiposDisponiblesDraft = cargado.EquiposDisponiblesDraft;
            NombreRivalDraft = cargado.NombreRivalDraft;
            EquipoRivalDraft = cargado.EquipoRivalDraft;
            PpgRivalDraft = cargado.PpgRivalDraft;
            EstrellaCompanero = cargado.EstrellaCompanero;
            QuimicaEstrella = cargado.QuimicaEstrella;
            SalarioAnual = cargado.SalarioAnual;
            DineroDisponible = cargado.DineroDisponible;
            TieneChefPersonal = cargado.TieneChefPersonal;
            TieneZapatillasExclusivas = cargado.TieneZapatillasExclusivas;
            TieneInversionStartup = cargado.TieneInversionStartup;
            Energia = cargado.Energia;
            ConfianzaDT = cargado.ConfianzaDT;
            Quimica = cargado.Quimica;
            Hype = cargado.Hype;
            TemporadaActual = cargado.TemporadaActual;
            Victorias = cargado.Victorias;
            Derrotas = cargado.Derrotas;
            DecisionesTomadasTemporada = cargado.DecisionesTomadasTemporada;
            PuntosDesarrollo = cargado.PuntosDesarrollo;
            AnillosCampeon = cargado.AnillosCampeon;
            PremiosMVP = cargado.PremiosMVP;
            GanadorROTY = cargado.GanadorROTY;
            PremiosDPOY = cargado.PremiosDPOY;
            EnSalonDeLaFama = cargado.EnSalonDeLaFama;
            PremiosGanadosTemporada = cargado.PremiosGanadosTemporada;
            UltimosPuntos = cargado.UltimosPuntos;
            UltimosRebotes = cargado.UltimosRebotes;
            UltimasAsistencias = cargado.UltimasAsistencias;
            UltimoFueVictoria = cargado.UltimoFueVictoria;
            UltimaNoticia = cargado.UltimaNoticia;

            CargarMazoTemporada();
            SiguienteDilema();
            Notificar();
        }
    }

    private void SiguienteDilema()
    {
        if (_indiceDilema < _mazoDilemas.Count)
        {
            DilemaActual = _mazoDilemas[_indiceDilema];
            _indiceDilema++;
        }
        else
        {
            _indiceDilema = 0;
            _mazoDilemas.Clear();
            _mazoDilemas.AddRange(CatalogoDilemas.ObtenerMazoBarajado());
            DilemaActual = _mazoDilemas[0];
            _indiceDilema++;
        }
        Notificar();
    }

    private void CargarMazoTemporada()
    {
        _mazoDilemas.Clear();
        _mazoDilemas.AddRange(CatalogoDilemas.ObtenerMazoBarajado());
    }

    private void Notificar() => OnStateChanged?.Invoke();
}



