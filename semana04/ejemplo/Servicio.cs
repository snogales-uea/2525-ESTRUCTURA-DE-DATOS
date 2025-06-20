// Clase base abstracta que representa un servicio general en el taller
public abstract class Servicio
{
    // Encapsulamiento: atributos privados
    private string cliente;
    private string vehiculo;
    private string fechaHora;

    // Propiedades públicas para acceder a los atributos privados
    public string Cliente
    {
        get => cliente;
        set => cliente = value;
    }

    public string Vehiculo
    {
        get => vehiculo;
        set => vehiculo = value;
    }

    public string FechaHora
    {
        get => fechaHora;
        set => fechaHora = value;
    }

    // Constructor común para todos los servicios
    public Servicio(string cliente, string vehiculo, string fechaHora)
    {
        this.cliente = cliente;
        this.vehiculo = vehiculo;
        this.fechaHora = fechaHora;
    }

    // Método abstracto que será implementado por las subclases
    public abstract void Mostrar();
}
