// Subclase que representa un servicio de reparación (ej: reparación de motor)
public class Reparacion : Servicio
{
    private string descripcion;

    public string Descripcion
    {
        get => descripcion;
        set => descripcion = value;
    }

    // Constructor que llama al base e inicializa la descripción del problema
    public Reparacion(string cliente, string vehiculo, string fechaHora, string descripcion)
        : base(cliente, vehiculo, fechaHora)
    {
        this.descripcion = descripcion;
    }

    // Implementación del método Mostrar
    public override void Mostrar()
    {
        Console.WriteLine($"{ "Reparación",-16} | {Cliente,-20} | {Vehiculo,-15} | {FechaHora:dd/MM/yyyy HH:mm} | {Descripcion}");
    }
}
