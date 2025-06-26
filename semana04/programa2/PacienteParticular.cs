// Subclase que representa un paciente particular
public class PacienteParticular : Paciente
{
    private string motivo;

    public string Motivo
    {
        get => motivo;
        set => motivo = value;
    }

    // Constructor: llama al constructor base e inicializa el motivo
    public PacienteParticular(string nombre, string cedula, string turno, string motivo)
        : base(nombre, cedula, turno)
    {
        this.motivo = motivo;
    }

    // Implementación del método Mostrar
    public override void Mostrar()
    {
        Console.WriteLine($"{ "[Particular]",-15} | {Nombre,-20} | {Cedula,-10} | {Turno:dd/MM/yyyy HH:mm} | {Motivo,-15}");
    }
}
