// Subclase que representa un paciente con obra social
public class PacienteObraSocial : Paciente
{
    private string obraSocial;

    public string ObraSocial
    {
        get => obraSocial;
        set => obraSocial = value;
    }

    // Constructor: llama al constructor base e inicializa la obra social
    public PacienteObraSocial(string nombre, string cedula, string turno, string obraSocial)
        : base(nombre, cedula, turno)
    {
        this.obraSocial = obraSocial;
    }

    // Implementación del método Mostrar
    public override void Mostrar()
    {
        // Formato con columnas fijas
        Console.WriteLine($"{ "[Obra Social]",-15} | {Nombre,-20} | {Cedula,-10} | {Turno:dd/MM/yyyy HH:mm} | {ObraSocial,-15}");
    }
}
