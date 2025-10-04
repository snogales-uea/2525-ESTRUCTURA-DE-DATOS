// GraficaArboles.cs
// Aplicación WinForms: carga dos tipos de archivos de texto y dibuja dos ejemplos de árboles:
// 1) Binary (nivel-order): una línea con valores separados por comas. Use "null" para nodos vacíos.
//    Ejemplo: 1,2,3,4,5,null,7
// 2) General tree (parejas padre->hijo por línea, separadas por espacio o coma):
//    Ejemplo:
//    A
//    A B
//    A C
//    B D
//    B E
//    C F
//
// Uso: compilar con dotnet (Windows) y ejecutar. Presiona "Cargar archivo" y elige el tipo.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GraficaArboles
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private Button btnLoadBinary;
        private Button btnLoadGeneral;
        private Panel drawingPanel;
        private string lastMessage = "";

        private TreeNode<string> rootBinary;
        private TreeNode<string> rootGeneral;

        public MainForm()
        {
            Text = "Gráfica de Árboles - Ejemplos";
            Width = 1000;
            Height = 700;

            btnLoadBinary = new Button { Text = "Cargar Binary (nivel-order)", Left = 10, Top = 10, Width = 220 };
            btnLoadGeneral = new Button { Text = "Cargar General (parent child)", Left = 240, Top = 10, Width = 220 };

            btnLoadBinary.Click += BtnLoadBinary_Click;
            btnLoadGeneral.Click += BtnLoadGeneral_Click;

            drawingPanel = new Panel { Left = 10, Top = 50, Width = 960, Height = 600, BorderStyle = BorderStyle.FixedSingle, AutoScroll = true };
            drawingPanel.Paint += DrawingPanel_Paint;

            Controls.Add(btnLoadBinary);
            Controls.Add(btnLoadGeneral);
            Controls.Add(drawingPanel);
        }

        private void BtnLoadBinary_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text files|*.txt|All files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var text = File.ReadAllText(ofd.FileName).Trim();
                        rootBinary = ParseBinaryLevelOrder(text);
                        lastMessage = "Binary cargado desde: " + Path.GetFileName(ofd.FileName);
                        drawingPanel.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar binary: " + ex.Message);
                    }
                }
            }
        }

        private void BtnLoadGeneral_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text files|*.txt|All files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lines = File.ReadAllLines(ofd.FileName).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
                        rootGeneral = ParseGeneralTree(lines);
                        lastMessage = "General cargado desde: " + Path.GetFileName(ofd.FileName);
                        drawingPanel.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar general: " + ex.Message);
                    }
                }
            }
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            int margin = 20;

            if (rootBinary != null)
            {
                // dibujar binary en la mitad superior
                Rectangle areaB = new Rectangle(margin, margin, drawingPanel.ClientSize.Width - 2 * margin, (drawingPanel.ClientSize.Height - 2 * margin) / 2 - 10);
                DrawBinaryTree(g, rootBinary, areaB);
            }

            if (rootGeneral != null)
            {
                // dibujar general en la mitad inferior
                Rectangle areaG = new Rectangle(margin, drawingPanel.ClientSize.Height / 2 + 5, drawingPanel.ClientSize.Width - 2 * margin, (drawingPanel.ClientSize.Height - 2 * margin) / 2 - 10);
                DrawGeneralTree(g, rootGeneral, areaG);
            }

            // mensaje
            using (var f = new Font("Segoe UI", 9))
            using (var b = new SolidBrush(Color.Black))
            {
                g.DrawString(lastMessage, f, b, new PointF(10, drawingPanel.ClientSize.Height - 18));
            }
        }

        // ---------------- Parsing ----------------
        // Binary: nivel-order separados por comas. "null" = nodo ausente
        private TreeNode<string> ParseBinaryLevelOrder(string csv)
        {
            if (string.IsNullOrWhiteSpace(csv)) throw new ArgumentException("Archivo vacío");
            var tokens = csv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();
            if (tokens.Length == 0) throw new ArgumentException("Sin tokens");

            var nodes = new List<TreeNode<string>>();
            foreach (var t in tokens)
            {
                if (t.Equals("null", StringComparison.OrdinalIgnoreCase)) nodes.Add(null);
                else nodes.Add(new TreeNode<string>(t));
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == null) continue;
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                if (left < nodes.Count) nodes[i].Left = nodes[left];
                if (right < nodes.Count) nodes[i].Right = nodes[right];
            }

            return nodes[0];
        }

        // General tree: líneas "parent child" o una sola raíz en línea
        private TreeNode<string> ParseGeneralTree(string[] lines)
        {
            var dict = new Dictionary<string, TreeNode<string>>();
            var hasParent = new HashSet<string>();

            foreach (var raw in lines)
            {
                var line = raw.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                var parts = line.Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 1)
                {
                    var name = parts[0];
                    if (!dict.ContainsKey(name)) dict[name] = new TreeNode<string>(name);
                }
                else if (parts.Length >= 2)
                {
                    var parent = parts[0];
                    var child = parts[1];
                    if (!dict.ContainsKey(parent)) dict[parent] = new TreeNode<string>(parent);
                    if (!dict.ContainsKey(child)) dict[child] = new TreeNode<string>(child);
                    dict[parent].Children.Add(dict[child]);
                    hasParent.Add(child);
                }
            }

            // root: aquel que no figura como hijo
            var root = dict.Keys.FirstOrDefault(k => !hasParent.Contains(k));
            if (root == null) throw new Exception("No se pudo determinar la raíz (revise el archivo)");
            return dict[root];
        }

        // ---------------- Drawing helpers ----------------
        private void DrawBinaryTree(Graphics g, TreeNode<string> root, Rectangle area)
        {
            if (root == null) return;
            int levelHeight = Math.Max(60, area.Height / (GetBinaryDepth(root) + 1));

            // compute positions using recursive width-based algorithm
            var positions = new Dictionary<TreeNode<string>, PointF>();
            ComputeBinaryPositions(root, 0, area.Left, area.Right, area.Top + 10, levelHeight, positions);

            // adjust panel scroll size if needed
            UpdatePanelScrollSize(positions.Values.Select(p => p).ToList());

            // draw edges
            using (var pen = new Pen(Color.DarkSlateGray, 2))
            {
                foreach (var kv in positions)
                {
                    var node = kv.Key;
                    var p = kv.Value;
                    if (node.Left != null && positions.ContainsKey(node.Left))
                        g.DrawLine(pen, p, positions[node.Left]);
                    if (node.Right != null && positions.ContainsKey(node.Right))
                        g.DrawLine(pen, p, positions[node.Right]);
                }
            }

            // draw nodes
            foreach (var kv in positions)
            {
                DrawNode(g, kv.Value, kv.Key.Value);
            }

            // title
            using (var f = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var b = new SolidBrush(Color.Black))
            {
                g.DrawString("Binary tree (nivel-order)", f, b, new PointF(area.Left, area.Top));
            }
        }

        private void ComputeBinaryPositions(TreeNode<string> node, int depth, int leftBound, int rightBound, int yStart, int levelHeight, Dictionary<TreeNode<string>, PointF> positions)
        {
            if (node == null) return;
            int midX = (leftBound + rightBound) / 2;
            float x = midX;
            float y = yStart + depth * levelHeight;
            positions[node] = new PointF(x, y);

            int span = Math.Max(40, (rightBound - leftBound) / 4);
            if (node.Left != null)
                ComputeBinaryPositions(node.Left, depth + 1, leftBound, Math.Max(leftBound + 20, midX - span), yStart, levelHeight, positions);
            if (node.Right != null)
                ComputeBinaryPositions(node.Right, depth + 1, Math.Min(rightBound - 20, midX + span), rightBound, yStart, levelHeight, positions);
        }

        private int GetBinaryDepth(TreeNode<string> node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(GetBinaryDepth(node.Left), GetBinaryDepth(node.Right));
        }

        private void DrawGeneralTree(Graphics g, TreeNode<string> root, Rectangle area)
        {
            if (root == null) return;
            int levelHeight = 80;
            // compute subtree widths and positions
            var positions = new Dictionary<TreeNode<string>, PointF>();
            float currentX = area.Left + 20;
            ComputeGeneralPositions(root, 0, ref currentX, area.Top + 20, levelHeight, positions);

            UpdatePanelScrollSize(positions.Values.Select(p => p).ToList());

            using (var pen = new Pen(Color.DarkGreen, 2))
            {
                foreach (var kv in positions)
                {
                    var node = kv.Key;
                    var p = kv.Value;
                    foreach (var child in node.Children)
                    {
                        if (positions.ContainsKey(child))
                            g.DrawLine(pen, p, positions[child]);
                    }
                }
            }

            foreach (var kv in positions)
                DrawNode(g, kv.Value, kv.Key.Value);

            using (var f = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var b = new SolidBrush(Color.Black))
            {
                g.DrawString("General tree (parent child)", f, b, new PointF(area.Left, area.Top));
            }
        }

        // Simple layout: post-order assign x positions sequentially; children centered under parent
        private void ComputeGeneralPositions(TreeNode<string> node, int depth, ref float currentX, int yStart, int levelHeight, Dictionary<TreeNode<string>, PointF> positions)
        {
            if (node == null) return;
            if (node.Children.Count == 0)
            {
                // leaf: place at currentX
                positions[node] = new PointF(currentX, yStart + depth * levelHeight);
                currentX += 100; // spacing
            }
            else
            {
                float firstX = currentX;
                foreach (var c in node.Children)
                {
                    ComputeGeneralPositions(c, depth + 1, ref currentX, yStart, levelHeight, positions);
                }
                // center parent above children
                var childXs = node.Children.Where(c => positions.ContainsKey(c)).Select(c => positions[c].X).ToList();
                float centerX = childXs.Count > 0 ? childXs.Average() : firstX;
                positions[node] = new PointF(centerX, yStart + depth * levelHeight);
            }
        }

        private void DrawNode(Graphics g, PointF center, string text)
        {
            int r = 20;
            RectangleF rect = new RectangleF(center.X - r, center.Y - r, r * 2, r * 2);
            using (var brush = new SolidBrush(Color.LightSteelBlue))
            using (var pen = new Pen(Color.DimGray))
            {
                g.FillEllipse(brush, rect);
                g.DrawEllipse(pen, rect);
            }
            using (var f = new Font("Segoe UI", 8))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var b = new SolidBrush(Color.Black))
            {
                g.DrawString(text, f, b, rect, sf);
            }
        }

        private void UpdatePanelScrollSize(List<PointF> points)
        {
            if (points == null || points.Count == 0) return;
            float maxX = points.Max(p => p.X) + 100;
            float maxY = points.Max(p => p.Y) + 100;
            drawingPanel.AutoScrollMinSize = new Size((int)Math.Ceiling(maxX), (int)Math.Ceiling(maxY));
        }
    }

    // Simple tree node classes
    public class TreeNode<T>
    {
        public T Value { get; set; }
        // For binary
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        // For general
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            Value = value;
        }
    }
}

/*
Sample archivos de prueba:

1) binary_example.txt
1,2,3,4,5,null,7

2) general_example.txt
A
A B
A C
B D
B E
C F

Compilar (dotnet):
- Crear proyecto de WinForms: dotnet new winforms -n GraficaArboles
- Reemplazar Program.cs con este archivo (o agregarlo) y compilar: dotnet run

Notas:
- Este código es un punto de partida. Para árboles grandes o diseños profesionales conviene usar algoritmos de layout más robustos (Reingold-Tilford, Buchheim, etc.).
- Puedes modificar tamaños, fuentes y espaciados en las constantes del código.
*/
