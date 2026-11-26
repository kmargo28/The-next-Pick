namespace TheNextPick.Engine.Observer;

// INTERFAZ OBSERVER: Contrato del observador directo; Home actualiza sus componentes hijos
public interface IObservadorJuego
{
    void Actualizar(string eventoOrigen);
}

// INTERFAZ SUBJECT: Contrato para suscribir, desuscribir y notificar observadores en tiempo de ejecución
public interface ISujetoObservable
{
    void Suscribir(IObservadorJuego observador);
    void Desuscribir(IObservadorJuego observador);
    void Notificar(string eventoOrigen = "CambioDeEstado");
}
