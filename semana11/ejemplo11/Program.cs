
string texto = "Este es un ejemplo de texto. Este texto es simple.";
Dictionary<string, int> contadorPalabras = new Dictionary<string, int>();
string[] palabras = texto.Split(' ');
foreach (string palabra in palabras)
{
    if (contadorPalabras.ContainsKey(palabra))
    {
        //contadorPalabras[palabra]++;
        contadorPalabras[palabra] = contadorPalabras[palabra] + 1;
    }
    else
    {
        //contadorPalabras[palabra] = 1;
        contadorPalabras.Add(palabra, 1);
    }
}
Console.WriteLine($"Total, palabras únicas {contadorPalabras.Count}");
foreach (string clave in contadorPalabras.Keys)
{
    Console.WriteLine($"Palabra: {clave}, Contador: {contadorPalabras[clave]}");
}