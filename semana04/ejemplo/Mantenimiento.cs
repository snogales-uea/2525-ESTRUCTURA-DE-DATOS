// Subclase que representa un servicio de mantenimiento (ej: cambio de aceite)
public class Mantenimiento : Servicio
{
    private string tipoServicio;

    public string TipoServicio
    {
        get => tipoServicio;
        set => tipoServicio = value;
    }

    // Constructor que llama al constructor base y agrega el tipo de mantenimiento
    public Mantenimiento(string cliente, string vehiculo, string fechaHora, string tipoServicio)
        : base(cliente, vehiculo, fechaHora)
    {
        this.tipoServicio = tipoServicio;
    }

    // Implementación del método Mostrar, con formato personalizado
    public override void Mostrar()
    {
        Console.WriteLine($"{ "Mantenimiento",-16} | {Cliente,-20} | {Vehiculo,-15} | {FechaHora:dd/MM/yyyy HH:mm} | {TipoServicio}");
    }
}
