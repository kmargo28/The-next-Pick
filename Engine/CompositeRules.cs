namespace TheNextPick.Engine;

// Componente base del patrón Composite
public interface ICondicionLogro
{
    bool Cumple(GameStateService estado);
}

// Condición simple (Hoja)
public class CondicionCriterio : ICondicionLogro
{
    private readonly Func<GameStateService, bool> _criterio;
    public CondicionCriterio(Func<GameStateService, bool> criterio) => _criterio = criterio;
    public bool Cumple(GameStateService estado) => _criterio(estado);
}

// Condición compuesta Y (Composite AND)
public class CondicionAnd : ICondicionLogro
{
    private readonly List<ICondicionLogro> _hijos = new();

    public CondicionAnd(params ICondicionLogro[] hijos)
    {
        _hijos.AddRange(hijos);
    }

    public bool Cumple(GameStateService estado) => _hijos.All(h => h.Cumple(estado));
}

// Condición compuesta O (Composite OR)
public class CondicionOr : ICondicionLogro
{
    private readonly List<ICondicionLogro> _hijos = new();

    public CondicionOr(params ICondicionLogro[] hijos)
    {
        _hijos.AddRange(hijos);
    }

    public bool Cumple(GameStateService estado) => _hijos.Any(h => h.Cumple(estado));
}
