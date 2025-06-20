// Clase encargada de administrar todos los servicios agendados
public class AgendaServicios
{
    // Arreglo de servicios (uso de vectores)
    private Servicio[] servicios;

    // Contador para saber cuántos servicios se han registrado
    private int contador;

    // Constructor con capacidad máxima
    public AgendaServicios(int capacidad)
    {
        servicios = new Servicio[capacidad];
        contador = 0;
    }

    // Agrega un servicio a la agenda
    public void AgregarServicio(Servicio servicio)
    {
        if (contador < servicios.Length)
        {
            servicios[contador++] = servicio;
        }
        else
        {
            Console.WriteLine("Agenda llena.");
        }
    }

    // Muestra todos los servicios agendados
    public void MostrarServicios()
    {
        Console.WriteLine("\nAGENDA DE SERVICIOS:");
        Console.WriteLine("TIPO             | CLIENTE              | VEHICULO        | FECHA HORA       | DETALLE");
        for (int i = 0; i < contador; i++)
        {
            servicios[i].Mostrar();
        }
    }

    // Ordena los servicios por nombre del cliente
    public void OrdenarPorCliente()
    {
        Array.Sort(servicios, 0, contador, Comparer<Servicio>.Create(
            (a, b) => string.Compare(a.Cliente, b.Cliente, StringComparison.OrdinalIgnoreCase)
        ));

        Console.WriteLine("\nServicios ordenados por cliente.");
    }
}
