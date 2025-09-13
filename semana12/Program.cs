using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Team
    {
        public string Name { get; set; }
        public HashSet<string> Players { get; } = new HashSet<string>();
        public int Points { get; set; }
        public int GF { get; set; }
        public int GA { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GD => GF - GA;
        public Team(string name) => Name = name;
    }

    class Match
    {
        public Team A { get; }
        public Team B { get; }
        public int GoalsA { get; private set; }
        public int GoalsB { get; private set; }
        public bool Played { get; private set; }

        public Match(Team a, Team b) { A = a; B = b; }

        public void Play(Random rnd)
        {
            if (Played) return;
            GoalsA = rnd.Next(0, 6);
            GoalsB = rnd.Next(0, 6);
            Played = true;

            A.GF += GoalsA; A.GA += GoalsB;
            B.GF += GoalsB; B.GA += GoalsA;

            if (GoalsA > GoalsB)
            {
                A.Points += 3; A.Wins++; B.Losses++;
            }
            else if (GoalsB > GoalsA)
            {
                B.Points += 3; B.Wins++; A.Losses++;
            }
            else
            {
                A.Points++; B.Points++;
                A.Draws++; B.Draws++;
            }
        }

        public override string ToString() =>
            Played ? $"{A.Name} {GoalsA} - {GoalsB} {B.Name}" : $"{A.Name} vs {B.Name} (pendiente)";
    }

    static List<Team> Teams = new List<Team>();
    static List<Match> Matches = new List<Match>();
    static bool Generated = false;
    static bool Simulated = false;
    static Random Rnd = new Random();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== MENÚ =====");
            Console.WriteLine("1. Ingresar cantidad de equipos y generar jugadores");
            Console.WriteLine("2. Simular todos los partidos");
            Console.WriteLine("3. Ver tabla de posiciones");
            Console.WriteLine("4. Reportería");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");

            string op = Console.ReadLine();
            switch (op)
            {
                case "1": Generar(); break;
                case "2": Simular(); break;
                case "3": Tabla(); break;
                case "4": Reporteria(); break;
                case "0": return;
                default: Console.WriteLine("Opción inválida"); break;
            }
        }
    }

    static void Generar()
    {
        Console.Write("Ingrese cantidad de equipos (≥2): ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n < 2)
        {
            Console.WriteLine("Número inválido.");
            return;
        }

        Teams.Clear();
        Matches.Clear();
        Generated = false;
        Simulated = false;

        // Crear equipos
        for (int i = 1; i <= n; i++)
            Teams.Add(new Team($"Equipo{i}"));

        // Crear jugadores secuenciales
        int totalJugadores = n * 11;
        List<string> jugadores = new List<string>();
        for (int i = 1; i <= totalJugadores; i++)
            jugadores.Add($"Jugador{i}");

        // Asignar aleatoriamente a equipos
        jugadores = jugadores.OrderBy(x => Rnd.Next()).ToList();
        int idx = 0;
        foreach (var team in Teams)
        {
            for (int j = 0; j < 11; j++)
                team.Players.Add(jugadores[idx++]);
        }

        // Programar partidos (round-robin)
        for (int i = 0; i < Teams.Count; i++)
            for (int j = i + 1; j < Teams.Count; j++)
                Matches.Add(new Match(Teams[i], Teams[j]));

        Generated = true;
        Console.WriteLine($"Se generaron {n} equipos y {totalJugadores} jugadores.");
    }

    static void Simular()
    {
        if (!Generated) { Console.WriteLine("Primero genere equipos."); return; }
        if (Simulated) { Console.WriteLine("Los partidos ya fueron simulados."); return; }

        foreach (var m in Matches) m.Play(Rnd);
        Simulated = true;
        Console.WriteLine("Todos los partidos simulados.");
    }

    static void Tabla()
    {
        if (!Generated) { Console.WriteLine("Primero genere equipos."); return; }

        var tabla = Teams
            .OrderByDescending(t => t.Points)
            .ThenByDescending(t => t.GD)
            .ThenByDescending(t => t.GF)
            .ToList();

        Console.WriteLine("\n===== TABLA DE POSICIONES =====");
        Console.WriteLine("Pos | Equipo   | Pts | G | E | P | GF | GA | DG");
        int pos = 1;
        foreach (var t in tabla)
        {
            Console.WriteLine($"{pos,3} | {t.Name,-8} | {t.Points,3} | {t.Wins,1} | {t.Draws,1} | {t.Losses,1} | {t.GF,2} | {t.GA,2} | {t.GD,2}");
            pos++;
        }
    }

    static void Reporteria()
    {
        if (!Generated) { Console.WriteLine("Primero genere equipos."); return; }

        Console.WriteLine("\n--- Reportería ---");
        Console.WriteLine($"Equipos: {Teams.Count}");
        Console.WriteLine($"Jugadores totales: {Teams.Sum(t => t.Players.Count)}");
        Console.WriteLine($"Partidos programados: {Matches.Count}");
        Console.WriteLine($"Partidos jugados: {Matches.Count(m => m.Played)}");

        Console.WriteLine("\nDetalle de equipos:");
        foreach (var t in Teams)
        {
            Console.WriteLine($"{t.Name}\n: {string.Join("\n", t.Players)}");
        }
    }
}
