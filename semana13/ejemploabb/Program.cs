using System;
using System.Collections.Generic;

namespace BST_Con_Menu
{
    // Nodo del árbol
    class Nodo
    {
        public int Valor;
        public Nodo Izq;
        public Nodo Der;

        public Nodo(int valor) => Valor = valor;
    }

    // Árbol Binario de Búsqueda
    class ArbolBST
    {
        private Nodo _raiz;

        public bool Vacio => _raiz == null;

        // Insertar (ignora duplicados)
        public void Insertar(int valor) => _raiz = InsertarRec(_raiz, valor);

        private Nodo InsertarRec(Nodo actual, int valor)
        {
            if (actual == null) return new Nodo(valor);

            if (valor < actual.Valor)
                actual.Izq = InsertarRec(actual.Izq, valor);
            else if (valor > actual.Valor)
                actual.Der = InsertarRec(actual.Der, valor);
            // Si es igual, no se inserta (evitar duplicados)
            return actual;
        }

        // Buscar
        public bool Buscar(int valor) => BuscarRec(_raiz, valor);

        private bool BuscarRec(Nodo actual, int valor)
        {
            if (actual == null) return false;
            if (valor == actual.Valor) return true;
            return valor < actual.Valor
                ? BuscarRec(actual.Izq, valor)
                : BuscarRec(actual.Der, valor);
        }

        // Eliminar
        public void Eliminar(int valor) => _raiz = EliminarRec(_raiz, valor);

        private Nodo EliminarRec(Nodo actual, int valor)
        {
            if (actual == null) return null;

            if (valor < actual.Valor)
            {
                actual.Izq = EliminarRec(actual.Izq, valor);
            }
            else if (valor > actual.Valor)
            {
                actual.Der = EliminarRec(actual.Der, valor);
            }
            else
            {
                // Nodo encontrado: casos
                // 1) Sin hijos
                if (actual.Izq == null && actual.Der == null)
                    return null;

                // 2) Un hijo
                if (actual.Izq == null)
                    return actual.Der;
                if (actual.Der == null)
                    return actual.Izq;

                // 3) Dos hijos: reemplazar por el sucesor (mínimo del subárbol derecho)
                Nodo sucesor = Minimo(actual.Der);
                actual.Valor = sucesor.Valor;
                actual.Der = EliminarRec(actual.Der, sucesor.Valor);
            }
            return actual;
        }

        private Nodo Minimo(Nodo actual)
        {
            while (actual.Izq != null) actual = actual.Izq;
            return actual;
        }

        // Recorridos
        public List<int> Preorden()
        {
            var r = new List<int>();
            PreordenRec(_raiz, r);
            return r;
        }
        private void PreordenRec(Nodo n, List<int> r)
        {
            if (n == null) return;
            r.Add(n.Valor);
            PreordenRec(n.Izq, r);
            PreordenRec(n.Der, r);
        }

        public List<int> Inorden()
        {
            var r = new List<int>();
            InordenRec(_raiz, r);
            return r;
        }
        private void InordenRec(Nodo n, List<int> r)
        {
            if (n == null) return;
            InordenRec(n.Izq, r);
            r.Add(n.Valor);
            InordenRec(n.Der, r);
        }

        public List<int> Postorden()
        {
            var r = new List<int>();
            PostordenRec(_raiz, r);
            return r;
        }
        private void PostordenRec(Nodo n, List<int> r)
        {
            if (n == null) return;
            PostordenRec(n.Izq, r);
            PostordenRec(n.Der, r);
            r.Add(n.Valor);
        }

        // Utilidades extra (opcionales)
        public void Limpiar() => _raiz = null;

        public int? Min()
        {
            if (_raiz == null) return null;
            return Minimo(_raiz).Valor;
        }

        public int? Max()
        {
            if (_raiz == null) return null;
            var n = _raiz;
            while (n.Der != null) n = n.Der;
            return n.Valor;
        }

        public int Altura() => AlturaRec(_raiz);
        private int AlturaRec(Nodo n)
        {
            if (n == null) return -1; // altura de árbol vacío como -1 (convención)
            return 1 + Math.Max(AlturaRec(n.Izq), AlturaRec(n.Der));
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var arbol = new ArbolBST();

            while (true)
            {
                Console.WriteLine("\n=== ÁRBOL BINARIO DE BÚSQUEDA (BST) ===");
                Console.WriteLine("1) Insertar");
                Console.WriteLine("2) Eliminar");
                Console.WriteLine("3) Buscar");
                Console.WriteLine("4) Recorrido PREORDEN");
                Console.WriteLine("5) Recorrido INORDEN (ordenado)");
                Console.WriteLine("6) Recorrido POSTORDEN");
                Console.WriteLine("7) Mínimo / Máximo");
                Console.WriteLine("8) Altura");
                Console.WriteLine("9) Limpiar árbol");
                Console.WriteLine("0) Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1": // Insertar
                        if (LeerEntero("Ingrese el valor a insertar: ", out int vIns))
                        {
                            arbol.Insertar(vIns);
                            Console.WriteLine($"Insertado: {vIns}");
                        }
                        break;

                    case "2": // Eliminar
                        if (LeerEntero("Ingrese el valor a eliminar: ", out int vDel))
                        {
                            bool existia = arbol.Buscar(vDel);
                            arbol.Eliminar(vDel);
                            Console.WriteLine(existia ? $"Eliminado: {vDel}" : "⚠ No existe ese valor en el árbol.");
                        }
                        break;

                    case "3": // Buscar
                        if (LeerEntero("Ingrese el valor a buscar: ", out int vBus))
                        {
                            Console.WriteLine(arbol.Buscar(vBus)
                                ? $"✔ {vBus} SÍ está en el árbol."
                                : $"✖ {vBus} NO está en el árbol.");
                        }
                        break;

                    case "4": // Preorden
                        ImprimirLista("Preorden", arbol.Preorden());
                        break;

                    case "5": // Inorden
                        ImprimirLista("Inorden", arbol.Inorden());
                        break;

                    case "6": // Postorden
                        ImprimirLista("Postorden", arbol.Postorden());
                        break;

                    case "7": // Min/Max
                        var min = arbol.Min();
                        var max = arbol.Max();
                        Console.WriteLine(arbol.Vacio
                            ? "El árbol está vacío."
                            : $"Mínimo: {min}, Máximo: {max}");
                        break;

                    case "8": // Altura
                        Console.WriteLine(arbol.Vacio
                            ? "El árbol está vacío."
                            : $"Altura del árbol: {arbol.Altura()}");
                        break;

                    case "9": // Limpiar
                        arbol.Limpiar();
                        Console.WriteLine("Árbol limpiado.");
                        break;

                    case "0":
                        Console.WriteLine("¡Hasta luego!");
                        return;

                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            }
        }

        // Helpers de E/S
        static bool LeerEntero(string prompt, out int valor)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            if (!int.TryParse(s, out valor))
            {
                Console.WriteLine("Entrada inválida. Debe ser un número entero.");
                return false;
            }
            return true;
        }

        static void ImprimirLista(string titulo, List<int> datos)
        {
            if (datos.Count == 0)
            {
                Console.WriteLine($"[{titulo}] Árbol vacío.");
                return;
            }
            Console.WriteLine($"[{titulo}] {string.Join(" - ", datos)}");
        }
    }
}
