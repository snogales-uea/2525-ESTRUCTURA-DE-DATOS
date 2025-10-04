using System;

class GraphMatrix
{
    private int[,] adjMatrix;
    private int V;

    public GraphMatrix(int vertices)
    {
        V = vertices;
        adjMatrix = new int[vertices, vertices];
    }

    public void AddEdge(int v, int w)
    {
        adjMatrix[v, w] = 1;
    }

    // Imprimir nodos
    public void PrintNodes()
    {
        Console.WriteLine("=== Nodos ===");
        for (int i = 0; i < V; i++)
            Console.WriteLine(i);
    }

    // Imprimir conexiones
    public void PrintEdges()
    {
        Console.WriteLine("\n=== Conexiones ===");
        for (int i = 0; i < V; i++)
        {
            for (int j = 0; j < V; j++)
            {
                if (adjMatrix[i, j] == 1)
                {
                    Console.WriteLine(i + " " + j);
                }
            }
        }
    }

    // Imprimir la matriz de adyacencia
    public void PrintMatrix()
    {
        Console.WriteLine("\n=== Matriz de Adyacencia ===");
        Console.Write("    ");
        for (int j = 0; j < V; j++)
            Console.Write(j + " ");
        Console.WriteLine();

        for (int i = 0; i < V; i++)
        {
            Console.Write(i + " | ");
            for (int j = 0; j < V; j++)
            {
                Console.Write(adjMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GraphMatrix g = new GraphMatrix(4);

        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 2);
        g.AddEdge(2, 0);
        g.AddEdge(2, 3);
        g.AddEdge(3, 3);

        g.PrintNodes();
        g.PrintEdges();
        g.PrintMatrix();

        Console.ReadKey();
    }
}
