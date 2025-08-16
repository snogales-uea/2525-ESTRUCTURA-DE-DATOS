using System;
using System.Collections.Generic;

class ProgramaConjunto
{
    static void Main(string[] args)
    {
        HashSet<int> miConjunto = new HashSet<int>();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\nOpciones:");
            Console.WriteLine("1. Crear conjunto vacío");
            Console.WriteLine("2. Crear conjunto con elementos iniciales");
            Console.WriteLine("3. Agregar elemento");
            Console.WriteLine("4. Verificar si contiene un elemento");
            Console.WriteLine("5. Eliminar elemento");
            Console.WriteLine("6. Mostrar todos los elementos");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    miConjunto = new HashSet<int>();
                    Console.WriteLine("Conjunto creado vacío.");
                    break;

                case "2":
                    miConjunto = new HashSet<int> { 1, 2, 3 };
                    Console.WriteLine("Conjunto inicializado con {1, 2, 3}.");
                    break;

                case "3": // Agregar elemento manualmente
                    Console.Write("Ingrese el número a agregar: ");
                    if (int.TryParse(Console.ReadLine(), out int numAgregar))
                    {
                        if (miConjunto.Add(numAgregar))
                        {
                            Console.WriteLine($"Elemento {numAgregar} agregado.");
                        }
                        else
                        {
                            Console.WriteLine($"El elemento {numAgregar} ya existe en el conjunto. No se agregó.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Valor no válido.");
                    }
                    break;

                case "4":
                    Console.Write("Ingrese el número a verificar: ");
                    if (int.TryParse(Console.ReadLine(), out int numBuscar))
                    {
                        bool contiene = miConjunto.Contains(numBuscar);
                        if (contiene)
                            Console.WriteLine($"El conjunto contiene el elemento {numBuscar}.");
                        else
                            Console.WriteLine($"El conjunto NO contiene el elemento {numBuscar}.");
                    }
                    else
                    {
                        Console.WriteLine("Valor no válido.");
                    }
                    break;

                case "5":
                    Console.Write("Ingrese el número a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int numEliminar))
                    {
                        if (miConjunto.Remove(numEliminar))
                            Console.WriteLine($"Elemento {numEliminar} eliminado.");
                        else
                            Console.WriteLine($"El elemento {numEliminar} no estaba en el conjunto.");
                    }
                    else
                    {
                        Console.WriteLine("Valor no válido.");
                    }
                    break;

                case "6":
                    // Mostrar conjunto ya está en MostrarConjunto()
                    break;

                case "7":
                    salir = true;
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    break;
            }

            // Mostrar siempre el contenido después de cada acción (excepto al salir)
            if (!salir)
            {
                MostrarConjunto(miConjunto);
            }
        }
    }

    static void MostrarConjunto(HashSet<int> conjunto)
    {
        Console.WriteLine("Contenido actual del conjunto:");
        if (conjunto.Count == 0)
        {
            Console.WriteLine("(Vacío)");
        }
        else
        {
            foreach (var item in conjunto)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
