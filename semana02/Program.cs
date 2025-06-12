Console.WriteLine("2525 - ESTRUCTURA DE DATOS - UEA / SEMANA 02");
Console.WriteLine("\n");

// Crear un rectangulo con base 5 y Altura 10
Rectangulo rectangulo = new Rectangulo(5, 10.5);

// Mostrar la base y Altura
Console.WriteLine($"{"Rectangulo",-16}|{"Valor",8}");
Console.WriteLine("----------------|--------");
Console.WriteLine($"{"Base",-16}|{rectangulo.Base,8}");
Console.WriteLine($"{"Altura",-16}|{rectangulo.Altura,8}");
Console.WriteLine($"{"Área",-16}|{rectangulo.CalcularArea(),8}");
Console.WriteLine($"{"Perímetro",-16}|{rectangulo.CalcularPerimetro(),8}");

Console.WriteLine("\n");

// Crear un rectangulo con base 5 y Altura 10
Circulo circulo = new Circulo(8);

// Mostrar la base y Altura
Console.WriteLine($"{"Circulo",-16}|{"Valor",8}");
Console.WriteLine("----------------|--------");
Console.WriteLine($"{"Radio",-16}|{circulo.radio,8}");
Console.WriteLine($"{"Area",-16}|{circulo.CalcularArea(),8}");
Console.WriteLine($"{"Perímetro",-16}|{circulo.CalcularPerimetro(),8}");