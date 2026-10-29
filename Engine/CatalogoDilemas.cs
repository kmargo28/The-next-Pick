using TheNextPick.Models;

namespace TheNextPick.Engine;

public static class CatalogoDilemas
{
    private static readonly List<Dilema> DilemasBase = new()
    {
        new Dilema
        {
            Id = 1,
            Categoria = "Preparacion",
            Titulo = "Sesion de Tiro Nocturna",
            Descripcion = "Las luces del estadio siguen encendidas tras el partido. Puedes quedarte a lanzar 500 triples extra o marcharte a descansar al hotel.",
            OpcionA = new OpcionDilema
            {
                Texto = "Quedarse a tirar triples",
                ImagenUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = -20, DeltaDT = +15, DeltaQuimica = +5, DeltaHype = +8,
                Retroalimentacion = "Tus horas extra en la duela pulieron tu mecanica de suspension."
            },
            OpcionB = new OpcionDilema
            {
                Texto = "Descanso y recuperacion muscular",
                ImagenUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = +18, DeltaDT = -4, DeltaQuimica = 0, DeltaHype = -2,
                Retroalimentacion = "Priorizaste las piernas frescas para el back-to-back de manana."
            }
        },
        new Dilema
        {
            Id = 2,
            Categoria = "Partido (Clutch)",
            Titulo = "Posesion Decisiva con 3 Segundos",
            Descripcion = "Marcador 106-107. Pides el balon en el saque de banda con doble marca encima.",
            EsMomentoPartido = true,
            OpcionA = new OpcionDilema
            {
                Texto = "Tiro forzado sobre la chicharra",
                ImagenUrl = "https://images.unsplash.com/photo-1519766304817-4f37bda74a29?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = -12, DeltaDT = -5, DeltaQuimica = -6, DeltaHype = +24,
                Retroalimentacion = "Tomaste el lanzamiento definitivo bajo maxima presion de la grada rival."
            },
            OpcionB = new OpcionDilema
            {
                Texto = "Asistencia al alero desmarcado",
                ImagenUrl = "https://images.unsplash.com/photo-1577471488278-16eec37ffcc2?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = -6, DeltaDT = +16, DeltaQuimica = +15, DeltaHype = +5,
                Retroalimentacion = "Pase de pique quirurgico que termino en canasta limpia ganadora."
            }
        },
        new Dilema
        {
            Id = 3,
            Categoria = "Prensa NBA",
            Titulo = "Rueda de Prensa Posterior",
            Descripcion = "Los cronistas insisten en tus bajos porcentajes de tiro de los ultimos dos cuartos.",
            OpcionA = new OpcionDilema
            {
                Texto = "Asumir autocritica y respaldar al DT",
                ImagenUrl = "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = 0, DeltaDT = +15, DeltaQuimica = +8, DeltaHype = -4,
                Retroalimentacion = "Respuesta corporativa de jugador franquicia; transmitiste liderazgo sereno."
            },
            OpcionB = new OpcionDilema
            {
                Texto = "Reclamar mayor protagonismo ofensivo",
                ImagenUrl = "https://images.unsplash.com/photo-1505666287802-9da132c02f41?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = 0, DeltaDT = -22, DeltaQuimica = -14, DeltaHype = +20,
                Retroalimentacion = "Tus quejas abrieron titulares de debate en First Take y ESPN."
            }
        },
        new Dilema
        {
            Id = 4,
            Categoria = "Patrocinios",
            Titulo = "Linea Exclusiva de Zapatillas (Signature Shoe)",
            Descripcion = "Una multinacional de indumentaria deportiva te presenta el prototipo de tus primeras zapatillas oficiales.",
            OpcionA = new OpcionDilema
            {
                Texto = "Firmar y lanzar campana comercial",
                ImagenUrl = "https://images.unsplash.com/photo-1518063319789-7217e6706b04?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = -14, DeltaDT = -8, DeltaQuimica = -6, DeltaHype = +30,
                Retroalimentacion = "Tus zapatillas rompieron record de ventas entre fanaticos del baloncesto."
            },
            OpcionB = new OpcionDilema
            {
                Texto = "Postergar el lanzamiento a postemporada",
                ImagenUrl = "https://images.unsplash.com/photo-1511556532299-8f662fc26c06?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = +6, DeltaDT = +10, DeltaQuimica = +5, DeltaHype = -5,
                Retroalimentacion = "Mantuviste tu enfoque prioritario al 100% sobre la tabla de posiciones."
            }
        },
        new Dilema
        {
            Id = 5,
            Categoria = "Vestuario",
            Titulo = "Reunion de Jugadores en Camerinos",
            Descripcion = "El plantel convoca un cónclave a puerta cerrada tras dos derrotas consecutivas de visita.",
            OpcionA = new OpcionDilema
            {
                Texto = "Tomar la palabra y exigir intensidad",
                ImagenUrl = "https://images.unsplash.com/photo-1526676037777-05a232554f77?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = -6, DeltaDT = +8, DeltaQuimica = +16, DeltaHype = +4,
                Retroalimentacion = "Los veteranos y jovenes aplaudieron que asumieras el mando vocal."
            },
            OpcionB = new OpcionDilema
            {
                Texto = "Escuchar en silencio sin buscar roces",
                ImagenUrl = "https://images.unsplash.com/photo-1515523110800-9415d13b84a8?w=700&auto=format&fit=crop&q=80",
                DeltaEnergia = 0, DeltaDT = 0, DeltaQuimica = +2, DeltaHype = -2,
                Retroalimentacion = "Guardaste compostura pero el compromiso del grupo apenas vario."
            }
        }
    };

    public static List<Dilema> ObtenerMazoBarajado()
    {
        return DilemasBase.OrderBy(_ => Random.Shared.Next()).ToList();
    }
}
