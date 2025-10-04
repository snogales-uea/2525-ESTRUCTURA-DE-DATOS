using System;
using System.Collections.Generic;

class Graph
{
    private int V;
    private List<int>[] adj;

    public Graph(int vertices)
    {
        V = vertices;
        adj = new List<int>[V];
        for (int i = 0; i < V; i++)
            adj[i] = new List<int>();
    }

    public void AddEdge(int v, int w)
    {
        adj[v].Add(w);
    }

    // Imprimir nodos
    public void PrintNodes()
    {
        Console.WriteLine("=== Nodos ===");
        for (int i = 0; i < V; i++)
            Console.WriteLine(i);
    }

    // Imprimir aristas
    public void PrintEdges()
    {
        Console.WriteLine("\n=== Conexiones ===");
        for (int i = 0; i < V; i++)
        {
            foreach (var w in adj[i])
            {
                Console.WriteLine(i + " " + w);
            }
        }
    }

    // Recorrido en Anchura (BFS)
    public void BFS(int v)
    {
        Console.WriteLine($"\n=== BFS desde nodo {v} ===");
        Queue<int> queue = new Queue<int>();
        bool[] visited = new bool[V];

        queue.Enqueue(v);
        visited[v] = true;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            Console.Write(u + " "); // imprime el nodo visitado

            foreach (int w in adj[u])
            {
                if (!visited[w])
                {
                    queue.Enqueue(w);
                    visited[w] = true;
                }
            }
        }
        Console.WriteLine();
    }

    // Recorrido en Profundidad (DFS recursivo)
    public void DFS(int v)
    {
        Console.WriteLine($"\n=== DFS desde nodo {v} ===");
        bool[] visited = new bool[V];
        DFSUtil(v, visited);
        Console.WriteLine();
    }

    private void DFSUtil(int v, bool[] visited)
    {
        visited[v] = true;
        Console.Write(v + " ");

        foreach (int w in adj[v])
        {
            if (!visited[w])
            {
                DFSUtil(w, visited);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int numNodos = 20;
        int numConexiones = 40;

        Graph g = new Graph(numNodos);
        Random rnd = new Random();

        // Generar conexiones aleatorias
        for (int i = 0; i < numConexiones; i++)
        {
            int origen = rnd.Next(numNodos);
            int destino = rnd.Next(numNodos);
            g.AddEdge(origen, destino);
        }

        // Mostrar nodos y aristas
        g.PrintNodes();
        g.PrintEdges();

        // Recorridos desde nodo 0
        g.BFS(0);
        g.DFS(0);

        Console.ReadKey();
    }
}
