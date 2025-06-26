// Clase abstracta que representa a un paciente genérico
public abstract class Paciente
{
    // Encapsulamiento: campos privados
    private string nombre;
    private string cedula;
    private string turno;

    // Propiedades públicas para acceder a los campos privados
    public string Nombre
    {
        get => nombre;
        set => nombre = value;
    }

    public string Cedula
    {
        get => cedula;
        set => cedula = value;
    }

    public string Turno
    {
        get => turno;
        set => turno = value;
    }

    // Constructor común para todos los pacientes
    public Paciente(string nombre, string cedula, string turno)
    {
        this.nombre = nombre;
        this.cedula = cedula;
        this.turno = turno;
    }

    // Método abstracto que será implementado por las subclases
    public abstract void Mostrar();
}
