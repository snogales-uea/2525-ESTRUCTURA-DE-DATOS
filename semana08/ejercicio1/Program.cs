
/*
Asignación de 30 asientos en orden de llegada, una vez que todos los
asientos son vendidos.
- Descripción:  Simula una línea de espera para una atracción en un
                parque de diversiones.
- Objetivo:     Asegurarse de que cada persona en la cola suba a la
                atracción en el orden correcto.
*/

Console.WriteLine("2525 - ESTRUCTURA DE DATOS - UEA / SEMANA 08");

const int capacidadMaxima = 30;
Queue<string> filaEspera = new Queue<string>();
List<string> asientosAsignados = new List<string>();

Console.Write("Ingrese el nombre de la atracción: ");
string nombreAtraccion = Console.ReadLine();

Console.Write("Ingrese el número total de personas que desean ingresar: ");
int totalPersonas;
while (!int.TryParse(Console.ReadLine(), out totalPersonas) || totalPersonas <= 0)
{
    Console.Write("Ingrese un número válido: ");
}

// Generar nombres automáticos
for (int i = 1; i <= totalPersonas; i++)
{
    string persona = $"Persona {i}";

    if (asientosAsignados.Count < capacidadMaxima)
    {
        asientosAsignados.Add(persona);
    }
    else
    {
        filaEspera.Enqueue(persona);
    }
}

// Si hay menos de 30 personas, completar manualmente los faltantes
while (asientosAsignados.Count < capacidadMaxima)
{
    Console.Write($"Faltan {capacidadMaxima - asientosAsignados.Count} asiento(s). Ingrese nombre adicional: ");
    string nombreExtra = Console.ReadLine();
    asientosAsignados.Add(nombreExtra);
}

// Mostrar resultados
Console.WriteLine($"\n Personas que subieron a la atracción '{nombreAtraccion}':");
int asiento = 1;
foreach (var persona in asientosAsignados)
{
    Console.WriteLine($"Asiento {asiento++}: {persona}");
}

Console.WriteLine("\nPersonas en cola de espera:");
if (filaEspera.Count == 0)
{
    Console.WriteLine("No hay personas en espera.");
}
else
{
    int pos = 1;
    foreach (var persona in filaEspera)
    {
        Console.WriteLine($"Posición {pos++}: {persona}");
    }
}