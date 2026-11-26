using TheNextPick.Engine;
using TheNextPick.Engine.Observer;
using TheNextPick.Engine.Strategies;
using TheNextPick.Models;
using System.Reflection;

int checks = 0;
void Check(bool value, string message) { if (!value) throw new Exception(message); checks++; }
Jugador Player() => new JugadorBuilder().ConDatosBasicos("Prueba", "Colombia", 11, Posicion.Base).AsignarAtributosPorPosicion().Build();
var strategies = new IEstrategiaArquetipo[] { new EstrategiaTirador(), new EstrategiaPlaymaker(), new EstrategiaAtleta(), new EstrategiaCerrojo() };
foreach (var position in Enum.GetValues<Posicion>())
foreach (var strategy in strategies)
{
    var p = new JugadorBuilder().ConDatosBasicos("Prueba", "Colombia", 11, position).AsignarAtributosPorPosicion().Build();
    int before = p.TiroExterior + p.TiroMedia + p.Pase + p.Defensa + p.Rebote + p.Atletismo;
    strategy.AplicarBonificaciones(p);
    int after = p.TiroExterior + p.TiroMedia + p.Pase + p.Defensa + p.Rebote + p.Atletismo;
    Check(after > before && p.OVR <= 99, $"Arquetipo {position}/{strategy.Clave}");
}
var shooter = Player(); new EstrategiaTirador().AplicarBonificaciones(shooter);
Check(shooter.OVR == 76 && shooter.TiroExterior == 90 && shooter.TiroMedia == 83, "Base tirador");
var g = new GameStateService(); var observer = new Observer();
g.Suscribir(observer); g.Suscribir(observer); g.Notificar("Prueba");
Check(g.CantidadObservadoresActivos == 1 && observer.Count == 1 && observer.Last == "Prueba", "Suscripción duplicada");
g.Desuscribir(observer); g.Notificar(); Check(observer.Count == 1, "Desuscripción");
try { g.IrADraftCombine(null!, RitmoSimulacion.Detallada); throw new Exception("No validó null"); } catch (ArgumentNullException) { checks++; }
try { g.ConfirmarDraftYComenzar(); throw new Exception("No validó jugador"); } catch (InvalidOperationException) { checks++; }
Check(new EstrategiaSimulacionDetallada().SimularBloque(shooter, 80, 100).DesgasteEnergia == 10, "Desgaste detallado");
Check(new EstrategiaSimulacionRapida().SimularBloque(shooter, 80, 100).DesgasteEnergia == 7, "Desgaste rápido");
foreach (var rhythm in Enum.GetValues<RitmoSimulacion>())
for (int run = 0; run < 10; run++)
{
    g = new(); var p = Player(); g.IrADraftCombine(p, rhythm); g.FinalizarCombine(90); g.ConfirmarDraftYComenzar();
    int[] expected = {14,28,41,55,69,82};
    for (int i=0;i<6;i++)
    {
        g.TomarDecision(new OpcionDecisionRPG { DeltaEnergia = 100 });
        Check(g.PartidosJugados == expected[i], "Calendario por bloques");
        Check(g.Energia == (rhythm == RitmoSimulacion.Detallada ? 90 : 93), "Desgaste una vez por bloque");
        if (i==2)
        {
            Check(g.FaseActual == FaseJuego.AllStarWeekend, "All-Star partido 41");
            var o = new Observer();g.Suscribir(o);
            g.RegistrarResultadoAllStar("Victoria",30,"Campeón de Triples");
            Check(g.HistorialPremios.Any(x=>x.Nombre=="Campeón de Triples") && o.Last=="ResultadoAllStar", "Premio y notificación All-Star");
            g.ContinuarTrasAllStar();
        }
    }
    Check(p.PartidosTotales == 82 && p.VictoriasTotales+p.DerrotasTotales==82, "Estadísticas de carrera");
    Check(p.RebotesTotales > 0 && p.AsistenciasTotales > 0, "Rebotes y asistencias");
    if(g.FaseActual==FaseJuego.Playoffs)g.AvanzarRondaPlayoffs(false);
    Check(g.FaseActual==FaseJuego.FinTemporada && g.TemporadaActual==1 && p.AniosEnLiga==1,"Fin de temporada");
    g.AvanzarSiguienteTemporada();
    Check(g.TemporadaActual==2 && g.PartidosJugados==0 && p.Edad==20,"Nueva temporada");
}
// A lesion removes only personal appearances, not team calendar results.
g=new();var injured=Player();g.IrADraftCombine(injured,RitmoSimulacion.Detallada);g.FinalizarCombine(90);g.ConfirmarDraftYComenzar();
injured.LesionActual=TipoLesion.DesgarroMuscular;injured.PartidosRestantesLesion=2;
g.TomarDecision(new OpcionDecisionRPG {DeltaEnergia=100});
Check(g.PartidosJugados==14 && injured.PartidosTotales==12 && !injured.EstaLesionado,"Lesión leve");
injured.LesionActual=TipoLesion.RoturaLigamentosACL;injured.PartidosRestantesLesion=99;g.FaseActual=FaseJuego.AlertaLesion;g.ContinuarTrasLesion();
Check(g.PartidosJugados==82 && injured.PartidosTotales==12 && g.FaseActual==FaseJuego.FinTemporada,"Lesión grave");
g.AvanzarSiguienteTemporada();Check(!injured.EstaLesionado,"Recuperación entre temporadas");
foreach(int score in new[]{0,29,30,49,50,69,70,87,88,100})
{
    g=new();g.IrADraftCombine(Player(),RitmoSimulacion.Detallada);g.FinalizarCombine(score);
    var range=score>=88?(1,3):score>=70?(4,10):score>=50?(11,25):score>=30?(26,30):(31,60);
    Check(g.PickAsignado>=range.Item1 && g.PickAsignado<=range.Item2,"Rango draft");
}
var cards=(List<DilemaRPG>)typeof(CatalogoDecisionesRPG).GetField("BaseDeCartas",BindingFlags.NonPublic|BindingFlags.Static)!.GetValue(null)!;
Check(cards.Count==52 && cards.Select(c=>c.Id).Distinct().Count()==52,"Catálogo 52 IDs");
foreach(var c in cards.Where(c=>c.Rareza==RarezaCarta.Normal)) Check(c.Opciones[0].DeltaDefensa==2 && c.Opciones[1].DeltaTiroExterior==2,"Recompensas normales");
Console.WriteLine($"PASS: {checks} comprobaciones; 20 temporadas completas; ambos ritmos, Observer, premios, lesiones, draft y recompensas.");
class Observer : IObservadorJuego {public int Count;public string Last="";public void Actualizar(string e){Count++;Last=e;}}
