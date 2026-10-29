namespace TheNextPick.Models;

public class EquipoNBA
{
    public string Nombre { get; set; } = string.Empty;
    public string Abreviatura { get; set; } = string.Empty;
    public string ColorPrimario { get; set; } = "#1D428A";
    public string ColorSecundario { get; set; } = "#FFC72C";
    public string EstrellaFranquicia { get; set; } = string.Empty;
    public string PosicionEstrella { get; set; } = string.Empty;
    public int MediaEstrella { get; set; } = 90;
    public string LogoUrl => $"https://a.espncdn.com/i/teamlogos/nba/500/{Abreviatura.ToLower()}.png";

    public static readonly List<EquipoNBA> Franquicias = new()
    {
        new() { Nombre = "San Antonio Spurs", Abreviatura = "SAS", ColorPrimario = "#000000", ColorSecundario = "#C4CED4", EstrellaFranquicia = "Victor Wembanyama", PosicionEstrella = "C", MediaEstrella = 97 },
        new() { Nombre = "Dallas Mavericks", Abreviatura = "DAL", ColorPrimario = "#00538C", ColorSecundario = "#002B5E", EstrellaFranquicia = "Luka Dončić", PosicionEstrella = "PG", MediaEstrella = 96 },
        new() { Nombre = "Oklahoma City Thunder", Abreviatura = "OKC", ColorPrimario = "#007AC1", ColorSecundario = "#EF3B24", EstrellaFranquicia = "Shai Gilgeous-Alexander", PosicionEstrella = "PG", MediaEstrella = 96 },
        new() { Nombre = "Minnesota Timberwolves", Abreviatura = "MIN", ColorPrimario = "#0C2340", ColorSecundario = "#78BE20", EstrellaFranquicia = "Anthony Edwards", PosicionEstrella = "SG", MediaEstrella = 95 },
        new() { Nombre = "Boston Celtics", Abreviatura = "BOS", ColorPrimario = "#007A33", ColorSecundario = "#BA9653", EstrellaFranquicia = "Jayson Tatum", PosicionEstrella = "SF", MediaEstrella = 95 },
        new() { Nombre = "Denver Nuggets", Abreviatura = "DEN", ColorPrimario = "#0E2240", ColorSecundario = "#FEC524", EstrellaFranquicia = "Nikola Jokić", PosicionEstrella = "C", MediaEstrella = 97 },
        new() { Nombre = "Milwaukee Bucks", Abreviatura = "MIL", ColorPrimario = "#00471B", ColorSecundario = "#EEE1C6", EstrellaFranquicia = "Giannis Antetokounmpo", PosicionEstrella = "PF", MediaEstrella = 96 },
        new() { Nombre = "New York Knicks", Abreviatura = "NYK", ColorPrimario = "#006BB6", ColorSecundario = "#F58426", EstrellaFranquicia = "Jalen Brunson", PosicionEstrella = "PG", MediaEstrella = 93 },
        new() { Nombre = "Orlando Magic", Abreviatura = "ORL", ColorPrimario = "#0077C0", ColorSecundario = "#C4CED4", EstrellaFranquicia = "Paolo Banchero", PosicionEstrella = "PF", MediaEstrella = 91 },
        new() { Nombre = "Detroit Pistons", Abreviatura = "DET", ColorPrimario = "#1D42BA", ColorSecundario = "#C8102E", EstrellaFranquicia = "Cade Cunningham", PosicionEstrella = "PG", MediaEstrella = 89 },
        new() { Nombre = "Houston Rockets", Abreviatura = "HOU", ColorPrimario = "#CE1141", ColorSecundario = "#000000", EstrellaFranquicia = "Alperen Şengün", PosicionEstrella = "C", MediaEstrella = 90 },
        new() { Nombre = "Miami Heat", Abreviatura = "MIA", ColorPrimario = "#98002E", ColorSecundario = "#F9A01B", EstrellaFranquicia = "Bam Adebayo", PosicionEstrella = "C", MediaEstrella = 89 },
        new() { Nombre = "Los Angeles Lakers", Abreviatura = "LAL", ColorPrimario = "#552583", ColorSecundario = "#FDB927", EstrellaFranquicia = "Anthony Davis", PosicionEstrella = "C", MediaEstrella = 94 },
        new() { Nombre = "Golden State Warriors", Abreviatura = "GSW", ColorPrimario = "#1D428A", ColorSecundario = "#FFC72C", EstrellaFranquicia = "Stephen Curry", PosicionEstrella = "PG", MediaEstrella = 92 }
    };

    public static EquipoNBA ObtenerPorNombre(string nombre)
    {
        return Franquicias.FirstOrDefault(f => f.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)) 
               ?? Franquicias[0];
    }
}
