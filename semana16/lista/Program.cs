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
        for (int i = 0; i < V; i++)
            Console.WriteLine(i);
    }

    // Imprimir aristas
    public void PrintEdges()
    {
        for (int i = 0; i < V; i++)
        {
            foreach (var w in adj[i])
            {
                Console.WriteLine(i + " " + w);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph(4);
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 2);
        g.AddEdge(2, 0);
        g.AddEdge(2, 3);
        g.AddEdge(3, 3);

        Console.WriteLine("Lista de Adyacencia en C#");
        g.PrintNodes();
        g.PrintEdges();
    }
}
