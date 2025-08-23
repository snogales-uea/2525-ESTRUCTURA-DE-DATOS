Random random = new Random();
int numeroSecreto = random.Next(1, 10); // Generar un número aleatorio entre 1 y 10
int intento = 0;
bool adivinado = false;
Console.WriteLine("Qué número estoy pensando entre 1 y 10.");
while (!adivinado)
{
    Console.Write("¿Qué número es?: ");
    string entrada = Console.ReadLine();
    if (int.TryParse(entrada, out intento)) // Convertir la entrada a un número
    {
        if (intento < numeroSecreto) // Comparar el intento con el número secreto
        {
            Console.WriteLine("El número secreto es mayor.");
        }
        else if (intento > numeroSecreto)
        {
            Console.WriteLine("El número secreto es menor.");
        }
        else
        {
            Console.WriteLine($"¡Felicidades! Adivinaste el número {numeroSecreto}.");
            adivinado = true;
        }
    }
    else
    {
        Console.WriteLine("Por favor, introduce un número válido.");
    }
}