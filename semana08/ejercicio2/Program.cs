
/*
    Programación del botón retroceder de un navegador web
*/

Console.WriteLine("2525 - ESTRUCTURA DE DATOS - UEA / SEMANA 08");

Stack<string> historial = new Stack<string>();
bool salir = false;

while (!salir)
{
    Console.WriteLine("\n==== MENÚ NAVEGADOR WEB ====");
    Console.WriteLine("1. Navegar (ingresar nueva URL)");
    Console.WriteLine("2. Retroceder");
    Console.WriteLine("3. Ver página actual");
    Console.WriteLine("4. Salir");
    Console.Write("Seleccione una opción: ");

    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Console.Write("Ingrese la nueva URL: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                historial.Push(url);
                Console.WriteLine($"Navegado a: {url}");
            }
            else
            {
                Console.WriteLine("❌ URL inválida.");
            }
            break;

        case "2":
            if (historial.Count > 1)
            {
                string anterior = historial.Pop(); // Eliminamos la actual
                Console.WriteLine($"Retrocediendo desde: {anterior}");
                Console.WriteLine($"Ahora estás en: {historial.Peek()}");
            }
            else if (historial.Count == 1)
            {
                Console.WriteLine($"Eliminando: {historial.Pop()}");
                Console.WriteLine("Ya no hay más páginas a las que retroceder.");
            }
            else
            {
                Console.WriteLine("No hay páginas en el historial.");
            }
            break;

        case "3":
            if (historial.Count > 0)
            {
                Console.WriteLine($"Página actual: {historial.Peek()}");
            }
            else
            {
                Console.WriteLine("No hay páginas abiertas.");
            }
            break;

        case "4":
            salir = true;
            Console.WriteLine("Cerrando navegador...");
            break;

        default:
            Console.WriteLine("Opción no válida. Intente nuevamente.");
            break;
    }
}