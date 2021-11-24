using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            centr = new Point3(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);

            

            poly = Tetr(size);

            DrawPolyhedron();
        }

        Point3 lightSource = new Point3(0, 0, 1000);

        static Bitmap bmp = new Bitmap(800, 663);

        Graphics g = Graphics.FromImage(bmp);

        Pen myPen = new Pen(Color.Black);

        int size = 70;

        Point3 centr;

        Polyhedron poly = new Polyhedron();

        Bitmap Texture = new Bitmap(bmp.Width, bmp.Height);

        public class Point3
        {
            public double X;
            public double Y;
            public double Z;
            public double U;
            public double V;

            public Point3() { X = 0; Y = 0; Z = 0; }

            public Point3(double x, double y, double z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            public Point3(double x, double y, double z, double u, double v)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.U = u;
                this.V = v;
            }
        }

        public class Line
        {
            public Point3 p1;
            public Point3 p2;

            public Line()
            {
                p1 = new Point3();
                p2 = new Point3();
            }

            public Line(Point3 p1, Point3 p2)
            {
                this.p1 = p1;
                this.p2 = p2;
            }

        }

        public class Edge
        {
            public List<Point3> points;

            public Edge()
            {
                this.points = new List<Point3> { };
            }
            public Edge(List<Point3> p)
            {
                this.points = p;
            }
        }

        public class Polyhedron
        {
            public List<Edge> edges;
            
            public Point3 centr_figure;

            public Polyhedron()
            {
                this.edges = new List<Edge> { };
                this.centr_figure = new Point3(0, 0, 0);
            }
            public Polyhedron(List<Edge> e, Point3 point)
            {
                this.edges = e;
                this.centr_figure = point;
            }
        }

        public class Triangle
        {
            public Point A;
            public Point B;
            public Point C;
        }

        public Polyhedron Hex(int size)
        {
            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();
            // 1-2-3-4
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, -hc, -hc), // 3
                new Point3(-hc, -hc, -hc) // 4
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-2-6-5
            e.points = new List<Point3> {
                //new Point3(-hc, hc, -hc), // 1
                //new Point3(hc, hc, -hc), // 2
                //new Point3(hc, hc, hc), // 6 
                //new Point3(-hc, hc, hc) // 5
                new Point3(-hc, hc, -hc), // 1
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, hc, hc), // 6 
                new Point3(-hc, hc, hc) // 5
                
            };
            p.edges.Add(e);
            e = new Edge();

            // 5-6-7-8
            e.points = new List<Point3> {
                //new Point3(-hc, hc, hc), // 5
                //new Point3(hc, hc, hc), // 6 
                //new Point3(hc, -hc, hc), // 7
                //new Point3(-hc, -hc, hc) // 8
                new Point3(-hc, hc, hc), // 5
                new Point3(hc, hc, hc), // 6 
                new Point3(hc, -hc, hc), // 7
                new Point3(-hc, -hc, hc) // 8
            };
            p.edges.Add(e);
            e = new Edge();

            // 6-2-3-7
            e.points = new List<Point3> {
                new Point3(hc, hc, hc), // 6 
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, -hc, -hc), // 3
                new Point3(hc, -hc, hc) // 7
                
            };
            p.edges.Add(e);
            e = new Edge();

            // 5-1-4-8
            e.points = new List<Point3> {
                
                new Point3(-hc, hc, -hc), // 1
                new Point3(-hc, hc, hc), // 5
                new Point3(-hc, -hc, hc), // 8
                new Point3(-hc, -hc, -hc) // 4
                
            };
            p.edges.Add(e);
            e = new Edge();

            // 4-3-7-8
            e.points = new List<Point3> {
                new Point3(-hc, -hc, -hc), // 4
                new Point3(hc, -hc, -hc), // 3
                new Point3(hc, -hc, hc), // 7
                new Point3(-hc, -hc, hc) // 8
            };
            p.edges.Add(e);
            e = new Edge();

            return p;

        }

        public Polyhedron Tetr(int size)
        {
            //var tetr_centr = size / 2;
            //return new List<Line>
            //{
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(-tetr_centr, -tetr_centr, tetr_centr)), //1->2
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)), //1->4
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(tetr_centr, tetr_centr, tetr_centr)), //1->3
            //    new Line(new Point3(-tetr_centr, -tetr_centr, tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)), //2->4
            //    new Line(new Point3(-tetr_centr, -tetr_centr, tetr_centr), new Point3(tetr_centr, tetr_centr, tetr_centr)), //2->3
            //    new Line(new Point3(tetr_centr, tetr_centr, tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)) //3->4
            //};

            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();

            //1-2-3
            e.points = new List<Point3> {
                new Point3(-hc, hc, hc), // 1
                new Point3(-hc,-hc, -hc), // 2
                new Point3(hc, hc, -hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-4-2
            e.points = new List<Point3> {
                new Point3(-hc, hc, hc), // 1
                new Point3(-hc, hc, -hc), // 4
                new Point3(-hc,-hc, -hc), // 2
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-3-4
            e.points = new List<Point3> {
                new Point3(hc, hc, -hc), // 3
                new Point3(-hc, hc, -hc), // 4
                new Point3(-hc, hc, hc), // 1
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-2-4
            e.points = new List<Point3> {
                new Point3(-hc,-hc, -hc), // 2
                new Point3(-hc, hc, -hc), // 4
                new Point3(hc, hc, -hc), // 3
            };
            p.edges.Add(e);

            return p;
        }

        public Polyhedron Oct(int size)
        {
            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();

            // 1-5-4
            e.points = new List<Point3> {
                new Point3(0, 0, hc), // 1
                new Point3(0, -hc, 0), // 5
                new Point3(hc, 0, 0), // 4 
            };
            p.edges.Add(e);
            e = new Edge();

            // 2-5-1
            e.points = new List<Point3> {
                new Point3(-hc, 0, 0), // 2
                new Point3(0, -hc, 0), // 5
                new Point3(0, 0, hc), // 1
            };
            p.edges.Add(e);
            e = new Edge();

            // 2-5-3
            e.points = new List<Point3> {
                new Point3(0, 0, -hc), // 3
                new Point3(0, -hc, 0), // 5
                new Point3(-hc, 0, 0), // 2 
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-5-4
            e.points = new List<Point3> {
                new Point3(hc, 0, 0), // 4 
                new Point3(0, -hc, 0), // 5
                new Point3(0, 0, -hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();
            ////////
            // 1-6-4
            e.points = new List<Point3> {
                new Point3(hc, 0, 0), // 4 
                new Point3(0, hc, 0), // 6
                new Point3(0, 0, hc), // 1
            };
            p.edges.Add(e);
            e = new Edge();

            // 2-6-1
            e.points = new List<Point3> {
                new Point3(0, 0, hc), // 1
                new Point3(0, hc, 0), // 6
                new Point3(-hc, 0, 0), // 2
            };
            p.edges.Add(e);
            e = new Edge();

            // 2-6-3
            e.points = new List<Point3> {
                new Point3(-hc, 0, 0), // 2 
                new Point3(0, hc, 0), // 6
                new Point3(0, 0, -hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-6-4
            e.points = new List<Point3> {
                new Point3(0, 0, -hc), // 3
                new Point3(0, hc, 0), // 6
                new Point3(hc, 0, 0), // 4 
            };
            p.edges.Add(e);
            e = new Edge();

            return p;
        }

        Point Position2d(Point3 p)
        {
            return new Point((int)p.X + (int)centr.X, (int)p.Y + (int)centr.Y);
        }

        Point[] Position2d(Edge e)
        {
            List<Point> p2D = new List<Point> { };
            foreach (var p3 in e.points)
            {
                p2D.Add(new Point((int)p3.X + (int)centr.X, (int)p3.Y + (int)centr.Y));
            }
            return p2D.ToArray();
        }

        public static double[,] MultiplyMatrix(double[,] m1, double[,] m2)
        {
            double[,] m = new double[1, 4];

            for (int i = 0; i < 4; i++)
            {
                var temp = 0.0;
                for (int j = 0; j < 4; j++)
                {
                    temp += m1[0, j] * m2[j, i];
                }
                m[0, i] = temp;
            }
            return m;
        }

        public void DrawPolyhedron()
        {
            g.Clear(Color.White);
            foreach (var edg in poly.edges)
            {
                g.DrawPolygon(myPen, Position2d(edg));
                
            }
            
            pictureBox1.Image = bmp;
        }

        public void AphinOffset()
        {
            var posx = double.Parse(textBox1.Text);
            var posy = double.Parse(textBox2.Text);
            var posz = double.Parse(textBox3.Text);

            poly.centr_figure.X += posx;
            poly.centr_figure.Y -= posy;
            poly.centr_figure.Z += posz;
            List<Edge> newEdges = new List<Edge>();
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X;
                    m[0, 1] = point.Y;
                    m[0, 2] = point.Z;
                    m[0, 3] = 1;

                    double[,] matr = new double[4, 4]
                {   { 1, 0, 0, 0},
                    { 0, 1, 0, 0 },
                    {0, 0, 1, 0 },
                    { posx, -posy, posz, 1 } };

                    var final_matrix = MultiplyMatrix(m, matr);

                    newPoints.points.Add(new Point3(final_matrix[0, 0], final_matrix[0, 1], final_matrix[0, 2]));
                }
                newEdges.Add(newPoints);

            }
            poly.edges = newEdges;
        }

        public void AphinRotate()
        {
            List<Edge> newEdges = new List<Edge>();
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X - poly.centr_figure.X;
                    m[0, 1] = point.Y - poly.centr_figure.Y;
                    m[0, 2] = point.Z - poly.centr_figure.Z;
                    m[0, 3] = 1;

                    var angle = double.Parse(textBox4.Text) * Math.PI / 180;
                    double[,] matrx = new double[4, 4]
                {   { Math.Cos(angle), 0, Math.Sin(angle), 0},
                    { 0, 1, 0, 0 },
                    {-Math.Sin(angle), 0, Math.Cos(angle), 0 },
                    { 0, 0, 0, 1 } };

                    angle = double.Parse(textBox5.Text) * Math.PI / 180;
                    double[,] matry = new double[4, 4]
                    {  { 1, 0, 0, 0 },
                    { 0, Math.Cos(angle), -Math.Sin(angle), 0},
                    {0, Math.Sin(angle), Math.Cos(angle), 0 },
                    { 0, 0, 0, 1 } };

                    angle = double.Parse(textBox6.Text) * Math.PI / 180;
                    double[,] matrz = new double[4, 4]
                    {  { Math.Cos(angle), -Math.Sin(angle), 0, 0},
                    { Math.Sin(angle), Math.Cos(angle), 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 } };

                    var final_matrix = MultiplyMatrix(m, matrx);
                    final_matrix = MultiplyMatrix(final_matrix, matry);
                    final_matrix = MultiplyMatrix(final_matrix, matrz);

                    newPoints.points.Add(new Point3(final_matrix[0, 0] + poly.centr_figure.X, final_matrix[0, 1] + poly.centr_figure.Y, final_matrix[0, 2] + poly.centr_figure.Z));
                }
                newEdges.Add(newPoints);
            }
            poly.edges = newEdges;
        }

        public void AphinScale()
        {
            g.Clear(Color.White);
            List<Edge> newEdges = new List<Edge>();
            var posx = double.Parse(textBox7.Text);
            var posy = double.Parse(textBox8.Text);
            var posz = double.Parse(textBox9.Text);
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X - poly.centr_figure.X;
                    m[0, 1] = point.Y - poly.centr_figure.Y;
                    m[0, 2] = point.Z - poly.centr_figure.Z;
                    m[0, 3] = 1;

                    double[,] matr = new double[4, 4]
                {   { posx, 0, 0, 0 },
                    { 0, posy, 0, 0 },
                    { 0, 0, posz, 0 },
                    { 0, 0, 0, 1 } };

                    var final_matrix = MultiplyMatrix(m, matr);

                    newPoints.points.Add(new Point3(final_matrix[0, 0] + poly.centr_figure.X, final_matrix[0, 1] + poly.centr_figure.Y, final_matrix[0, 2] + poly.centr_figure.Z));
                }
                newEdges.Add(newPoints);

            }
            poly.edges = newEdges;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                poly = Tetr(size);
            DrawPolyhedron();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                poly = Hex(size);
            DrawPolyhedron();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                poly = Oct(size);
            DrawPolyhedron();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AphinOffset();
            if (radioButton5.Checked)
                Z_buffer();
            else if (radioButton4.Checked)
                Z_buffer_texture();
            else
                DrawPolyhedron();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AphinRotate();
            if (radioButton5.Checked)
                Z_buffer();
            else if (radioButton4.Checked)
                Z_buffer_texture();
            else
                DrawPolyhedron();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AphinScale();
            if (radioButton5.Checked)
                Z_buffer();
            else if (radioButton4.Checked)
                Z_buffer_texture();
            else
                DrawPolyhedron();
        }

        public bool InTriangle(Point3 p1, Point3 p2, Point3 p3, Point3 trypoint)
        {
            double x1 = p1.X;
            double x2 = p2.X;
            double x3 = p3.X;

            double y1 = p1.Y;
            double y2 = p2.Y;
            double y3 = p3.Y;

            double x0 = trypoint.X;
            double y0 = trypoint.Y;

            double a = (x1 - x0) * (y2 - y1) - (x2 - x1) * (y1 - y0);
            double b = (x2 - x0) * (y3 - y2) - (x3 - x2) * (y2 - y0);
            double c = (x3 - x0) * (y1 - y3) - (x1 - x3) * (y3 - y0);

            //if ((Math.Sign(t1) == Math.Sign(t2)) && (Math.Sign(t2) == Math.Sign(t3)) && (Math.Sign(t3) == Math.Sign(t1)) || t1 == 0 || t2 == 0 || t3 == 0)
            if (((int)a > 0 && (int)b > 0 && (int)c > 0) || ((int)a < 0 && (int)b < 0 && (int)c < 0))
                return true;
            return false;
        }

        public bool PointOnEdge(Point3 p1, Point3 p2, Point3 p3, Point3 trypoint)
        {
            double x1 = p1.X;
            double x2 = p2.X;
            double x3 = p3.X;

            double y1 = p1.Y;
            double y2 = p2.Y;
            double y3 = p3.Y;

            double x0 = trypoint.X;
            double y0 = trypoint.Y;

            //for (double i = x0 - 1; i < x0 + 1; i += 0.1)
            {
                double a = (x1 - x0) * (y2 - y1) - (x2 - x1) * (y1 - y0);
                double b = (x2 - x0) * (y3 - y2) - (x3 - x2) * (y2 - y0);
                double c = (x3 - x0) * (y1 - y3) - (x1 - x3) * (y3 - y0);

                //List<Point3> points = new List<Point3>();

                //if (((int)a >= 0 && (int)b >= 0 && (int)c >= 0) || ((int)a <= 0 && (int)b <= 0 && (int)c <= 0))
                //if (a > -1 || a < 1 || b > -1 || b < 1 || c > -1 || c < 1)
                if (a.Equals(0) || b.Equals(0) || c.Equals(0))
                    return true;
            }
            return false;
        }

        public double find_z_three_points(List<Point3> points, double x, double y)
        {
            double x1 = points[0].X;
            double y1 = points[0].Y;
            double z1 = points[0].Z;

            double x2 = points[1].X;
            double y2 = points[1].Y;
            double z2 = points[1].Z;

            double x3 = points[2].X;
            double y3 = points[2].Y;
            double z3 = points[2].Z;

            double a11 = x - x1;
            double a12 = x2 - x1;
            double a13 = x3 - x1;
            double a21 = y - y1;
            double a22 = y2 - y1;
            double a23 = y3 - y1;
            //double a31 =z1 - z1;
            double a32 = z2 - z1;
            double a33 = z3 - z1;

            double z = (a11 * a22 * a33 + a13 * a21 * a32 - a11 * a23 * a32 - a12 * a21 * a33) / (a13 * a22 - a12 * a23) + z1;
            return z;
        }

        public void Z_buffer()
        {
            lightSource = new Point3(double.Parse(textBox10.Text), double.Parse(textBox11.Text), double.Parse(textBox12.Text));

            double[,] buffer = new double[pictureBox1.Width, pictureBox1.Height];
            Color[,] color = new Color[pictureBox1.Width, pictureBox1.Height];
            for (int i = 0; i < pictureBox1.Width; i++)
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    buffer[i, j] = double.MinValue;
                    color[i, j] = Color.White;
                }

            Point3 normal1 = new Point3();
            Point3 normal2 = new Point3();
            Point3 normal3 = new Point3();
            Point3 normal4 = new Point3(0, 0, 0);

            var temp_edges = new List<Edge>();
            foreach (var edge in poly.edges)
            {
                Edge temp_edge = new Edge();
                List<Point3> oldList = edge.points;
                List<Point3> newList = new List<Point3>(oldList.Count);
                oldList.ForEach((item) =>
                {
                    newList.Add(new Point3(item.X, item.Y, item.Z));
                });
    
                temp_edge.points = newList;
                temp_edges.Add(temp_edge);
            }
            

            foreach (var edge in temp_edges)
            {
                g.Clear(Color.White);
                g.DrawPolygon(myPen, Position2d(edge));
                //pictureBox1.Image = bmp;

                Edge temp_edge = new Edge();
                List<Point3> oldList = edge.points;
                List<Point3> newList = new List<Point3>(oldList.Count);
                oldList.ForEach((item) =>
                {
                    newList.Add(new Point3(item.X, item.Y, item.Z));
                });

                temp_edge.points = newList;
                Point3 p1 = temp_edge.points[0];
                p1.X += centr.X;
                p1.Y += centr.Y;
                p1.Z += centr.Z;
                Point3 p2 = temp_edge.points[1];
                p2.X += centr.X;
                p2.Y += centr.Y;
                p2.Z += centr.Z;
                Point3 p3 = temp_edge.points[2];
                p3.X += centr.X;
                p3.Y += centr.Y;
                p3.Z += centr.Z;

                normal1 = FindVertexNormal(edge.points[0], temp_edges);
                normal2 = FindVertexNormal(edge.points[1], temp_edges);
                normal3 = FindVertexNormal(edge.points[2], temp_edges);

                Point3 p4 = temp_edge.points[2];
                if (edge.points.Count == 4)
                {
                    normal4 = FindVertexNormal(edge.points[3], temp_edges);
                    p4 = temp_edge.points[3];
                    p4.X += centr.X;
                    p4.Y += centr.Y;
                    p4.Z += centr.Z;
                }

                

                normal1 = new Point3(normal1.X + centr.X, normal1.Y + centr.Y, normal1.Z + centr.Z);
                normal2 = new Point3(normal2.X + centr.X, normal2.Y + centr.Y, normal2.Z + centr.Z);
                normal3 = new Point3(normal3.X + centr.X, normal3.Y + centr.Y, normal3.Z + centr.Z);
                normal4 = new Point3(normal4.X + centr.X, normal4.Y + centr.Y, normal4.Z + centr.Z);

                double x1 = p1.X, y1 = p1.Y, z1 = p1.Z;
                double x2 = p2.X, y2 = p2.Y, z2 = p2.Z;
                double x3 = p3.X, y3 = p3.Y, z3 = p3.Z;

                Point3 l1 = new Point3(p1.X + lightSource.X, p1.Y + lightSource.Y, p1.Z + lightSource.Z);
                Point3 l2 = new Point3(p2.X + lightSource.X, p2.Y + lightSource.Y, p2.Z + lightSource.Z);
                Point3 l3 = new Point3(p3.X + lightSource.X, p3.Y + lightSource.Y, p3.Z + lightSource.Z);
                Point3 l4 = new Point3(p4.X + lightSource.X, p4.Y + lightSource.Y, p4.Z + lightSource.Z);

                double intensive1 = Math.Max(0, CosBetweenVector(normal1, l1));
                double intensive2 = Math.Max(0, CosBetweenVector(normal2, l2));
                double intensive3 = Math.Max(0, CosBetweenVector(normal3, l3));
                double intensive4 = Math.Max(0, CosBetweenVector(normal4, l4));


                for (int x = 0; x < pictureBox1.Width; x++)
                    for (int y = 0; y < pictureBox1.Height; y++)
                    {
                        
                        {
                            if (InTriangle(p1, p2, p3, new Point3(x, y, 0)))
                            {
                                double z = find_z_three_points(temp_edge.points, x, y);
                                if (z > buffer[x, y])
                                {
                                    buffer[x, y] = z;

                                    double ip;

                                    double L1 = ((p2.Y - p3.Y) * (x - p3.X) + (p3.X - p2.X) * (y - p3.Y)) / ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
                                    double L2 = ((p3.Y - p1.Y) * (x - p3.X) + (p1.X - p3.X) * (y - p3.Y)) / ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
                                    double L3 = 1 - L1 - L2;
                                    if (L1 >= 0 && L2 >= 0 && L3 >= 0 && L1 <= 1 && L2 <= 1 && L3 <= 1)
                                    {
                                        ip = L1 * intensive1 + L2 * intensive2 + L3 * intensive3;
                                        Color newcol = bmp.GetPixel(x, y);
                                        int colR = (int)(newcol.R * ip);
                                        int colG = (int)(newcol.G * ip);
                                        int colB = (int)(newcol.B * ip);
                                        if (colR < 0)
                                            colR = 0;
                                        if (colR > 255)
                                            colR = 255;
                                        if (colG < 0)
                                            colG = 0;
                                        if (colG > 255)
                                            colG = 255;
                                        if (colB < 0)
                                            colB = 0;
                                        if (colB > 255)
                                            colB = 255;
                                        Color newcol2 = Color.FromArgb(colR, colG, colB);
                                        color[x, y] = newcol2;
                                    }
                                }
                            }
                        }
                        if (edge.points.Count == 4)
                        {
                            if (InTriangle(p1, p3, p4, new Point3(x, y, 0)))
                            {
                                double z = find_z_three_points(temp_edge.points, x, y);
                                if (z > buffer[x, y])
                                {
                                    buffer[x, y] = z;

                                    double ip;

                                    double L1 = ((p4.Y - p3.Y) * (x - p3.X) + (p3.X - p4.X) * (y - p3.Y)) / ((p4.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p4.X) * (p1.Y - p3.Y));
                                    double L2 = ((p3.Y - p1.Y) * (x - p3.X) + (p1.X - p3.X) * (y - p3.Y)) / ((p4.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p4.X) * (p1.Y - p3.Y));
                                    double L3 = 1 - L1 - L2;
                                    if (L1 >= 0 && L2 >= 0 && L3 >= 0 && L1 <= 1 && L2 <= 1 && L3 <= 1)
                                    {
                                        ip = L1 * intensive1 + L2 * intensive4 + L3 * intensive3;
                                        Color newcol = bmp.GetPixel(x, y);
                                        int colR = (int)(newcol.R * ip);
                                        int colG = (int)(newcol.G * ip);
                                        int colB = (int)(newcol.B * ip);
                                        if (colR < 0)
                                            colR = 0;
                                        if (colR > 255)
                                            colR = 255;
                                        if (colG < 0)
                                            colG = 0;
                                        if (colG > 255)
                                            colG = 255;
                                        if (colB < 0)
                                            colB = 0;
                                        if (colB > 255)
                                            colB = 255;
                                        Color newcol2 = Color.FromArgb(colR, colG, colB);
                                        color[x, y] = newcol2;
                                    }
                                }
                            }
                        }

                    }
            }
            g.Clear(Color.White);
            for (int x = 0; x < pictureBox1.Width; x++)
                for (int y = 0; y < pictureBox1.Height; y++)
                    bmp.SetPixel(x, y, (color[x, y]));
            
        }
        
        public void Z_buffer_texture()
        {
            double[,] buffer = new double[pictureBox1.Width, pictureBox1.Height];
            Color[,] color = new Color[pictureBox1.Width, pictureBox1.Height];
            for (int i = 0; i < pictureBox1.Width; i++)
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    buffer[i, j] = double.MinValue;
                    color[i, j] = Color.White;
                }
            
            var temp_edges = new List<Edge>();
            foreach (var edge in poly.edges)
            {
                Edge temp_edge = new Edge();
                List<Point3> oldList = edge.points;
                List<Point3> newList = new List<Point3>(oldList.Count);
                oldList.ForEach((item) =>
                {
                    newList.Add(new Point3(item.X, item.Y, item.Z));
                });

                temp_edge.points = newList;
                temp_edges.Add(temp_edge);
            }


            foreach (var edge in temp_edges)
            {
                g.Clear(Color.White);
                g.DrawPolygon(myPen, Position2d(edge));
                pictureBox1.Image = bmp;

                Edge temp_edge = new Edge();
                List<Point3> oldList = edge.points;
                List<Point3> newList = new List<Point3>(oldList.Count);
                oldList.ForEach((item) =>
                {
                    newList.Add(new Point3(item.X, item.Y, item.Z));
                });

                temp_edge.points = newList;
                Point3 p1 = temp_edge.points[0];
                p1.X += centr.X;
                p1.Y += centr.Y;
                p1.Z += centr.Z;
                p1.U = 0;
                p1.V = 0;
                Point3 p2 = temp_edge.points[1];
                p2.X += centr.X;
                p2.Y += centr.Y;
                p2.Z += centr.Z;
                p2.U = 1;
                p2.V = 0;
                Point3 p3 = temp_edge.points[2];
                p3.X += centr.X;
                p3.Y += centr.Y;
                p3.Z += centr.Z;
                p3.U = 1;
                p3.V = 1;

                Point3 p4 = temp_edge.points[2];
                if (edge.points.Count == 4)
                {
                    p4 = temp_edge.points[3];
                    p4.X += centr.X;
                    p4.Y += centr.Y;
                    p4.Z += centr.Z;
                    p4.U = 0;
                    p4.V = 1;
                }
                for (int x = 0; x < pictureBox1.Width; x++)
                    for (int y = 0; y < pictureBox1.Height; y++)
                    {
                        if (InTriangle(p1, p2, p3, new Point3(x, y, 0)))
                            {
                                double z = find_z_three_points(temp_edge.points, x, y);
                                if (z > buffer[x, y])
                                {
                                    buffer[x, y] = z;

                                    double pU;
                                    double pV;

                                    double L1 = ((p2.Y - p3.Y) * (x - p3.X) + (p3.X - p2.X) * (y - p3.Y)) / ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
                                    double L2 = ((p3.Y - p1.Y) * (x - p3.X) + (p1.X - p3.X) * (y - p3.Y)) / ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
                                    double L3 = 1 - L1 - L2;
                                    if (L1 >= 0 && L2 >= 0 && L3 >= 0 && L1 <= 1 && L2 <= 1 && L3 <= 1)
                                    {
                                        pU = L1 * p1.U + L2 * p2.U + L3 * p3.U;
                                        pV = L1 * p1.V + L2 * p2.V + L3 * p3.V;
                                        Color newcol = Texture.GetPixel((int)(Texture.Width - (pU * (Texture.Width - 1))), (int)(Texture.Height - (pV * (Texture.Height - 1))));
                                        color[x, y] = newcol;
                                    }
                                }
                            }
                        if (edge.points.Count == 4)
                        {
                            if (InTriangle(p1, p3, p4, new Point3(x, y, 0)))
                            {
                                double z = find_z_three_points(temp_edge.points, x, y);
                                if (z > buffer[x, y])
                                {
                                    buffer[x, y] = z;

                                    double pU;
                                    double pV;

                                    double L1 = ((p4.Y - p3.Y) * (x - p3.X) + (p3.X - p4.X) * (y - p3.Y)) / ((p4.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p4.X) * (p1.Y - p3.Y));
                                    double L2 = ((p3.Y - p1.Y) * (x - p3.X) + (p1.X - p3.X) * (y - p3.Y)) / ((p4.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p4.X) * (p1.Y - p3.Y));
                                    double L3 = 1 - L1 - L2;
                                    if (L1 >= 0 && L2 >= 0 && L3 >= 0 && L1 <= 1 && L2 <= 1 && L3 <= 1)
                                    {
                                        pU = L1 * p1.U + L2 * p4.U + L3 * p3.U;
                                        pV = L1 * p1.V + L2 * p4.V + L3 * p3.V;
                                        Color newcol = Texture.GetPixel((int)(Texture.Width - (pU * (Texture.Width - 1))), (int)(Texture.Height - (pV * (Texture.Height - 1))));
                                        color[x, y] = newcol;
                                    }
                                }
                            }
                        }

                    }
            }
            g.Clear(Color.White);
            for (int x = 0; x < pictureBox1.Width; x++)
                for (int y = 0; y < pictureBox1.Height; y++)
                    bmp.SetPixel(x, y, (color[x, y]));
        }

        public Point3 FindEdgeNormal(Edge pol)
        {
            

            Point3 v1 = new Point3(pol.points[0].X - pol.points[1].X, pol.points[0].Y - pol.points[1].Y, pol.points[0].Z - pol.points[1].Z);
            Point3 v2 = new Point3(pol.points[2].X - pol.points[1].X, pol.points[2].Y - pol.points[1].Y, pol.points[2].Z - pol.points[1].Z);
            Point3 normal = new Point3(v1.Z * v2.Y - v1.Y * v2.Z, v1.X * v2.Z - v1.Z * v2.X, v1.Y * v2.X - v1.X * v2.Y);

            Point3 pointcentr = new Point3(0, 0, 0);

            double length_v1 = Math.Sqrt(Math.Pow(pol.points[0].X - pol.points[1].X, 2) + Math.Pow(pol.points[0].Y - pol.points[1].Y, 2) + Math.Pow(pol.points[0].Z - pol.points[1].Z, 2));
            double length_v2 = Math.Sqrt(Math.Pow(pol.points[2].X - pol.points[1].X, 2) + Math.Pow(pol.points[2].Y - pol.points[1].Y, 2) + Math.Pow(pol.points[2].Z - pol.points[1].Z, 2));
            double length_v3 = Math.Sqrt(Math.Pow(pol.points[2].X - pol.points[0].X, 2) + Math.Pow(pol.points[2].Y - pol.points[0].Y, 2) + Math.Pow(pol.points[2].Z - pol.points[0].Z, 2));

            
            length_v1 = (int)(length_v1 * 100);
            length_v2 = (int)(length_v2 * 100);
            length_v3 = (int)(length_v3 * 100);

            if (!(length_v1 == length_v2 && length_v2 == length_v3 && length_v1 == length_v3))
            {
                pointcentr.X += (pol.points[2].X + pol.points[0].X) / 2;
                pointcentr.Y += (pol.points[2].Y + pol.points[0].Y) / 2;
                pointcentr.Z += (pol.points[2].Z + pol.points[0].Z) / 2;
            }
            else
            {
                for (int i = 0; i < pol.points.Count(); i++)
                {
                    pointcentr.X += pol.points[i].X;
                    pointcentr.Y += pol.points[i].Y;
                    pointcentr.Z += pol.points[i].Z;
                }
                pointcentr.X /= pol.points.Count();
                pointcentr.Y /= pol.points.Count();
                pointcentr.Z /= pol.points.Count();
            }

            double inv_length = 1 / Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y + normal.Z * normal.Z);
            normal.X *= inv_length;
            normal.Y *= inv_length;
            normal.Z *= inv_length;

            

            //g.DrawLine(myPen, Position2d(new Point3(pointcentr.X, pointcentr.Y, pointcentr.Z)), Position2d(new Point3(normal.X * 200, normal.Y * 200, normal.Z * 200)));
            pictureBox1.Image = bmp;

            //normal.X -= pointcentr.X;
            //normal.Y -= pointcentr.Y;
            //normal.Z -= pointcentr.Z;

            return normal;
        }

        public Point3 FindVertexNormal(Point3 vertex, List<Edge> temp)
        {
            Point3 normalvertex = new Point3(vertex.X, vertex.Y, vertex.Z);
            int Count = 0;
            foreach (var edg in temp)
            {
                if (edg.points.Contains(vertex))
                {
                    normalvertex.X += FindEdgeNormal(edg).X;
                    normalvertex.Y += FindEdgeNormal(edg).Y;
                    normalvertex.Z += FindEdgeNormal(edg).Z;
                    Count++;
                }
                else
                    continue;
            }
            normalvertex.X = normalvertex.X / Count;
            normalvertex.Y = normalvertex.Y / Count;
            normalvertex.Z = normalvertex.Z / Count;

            

            //g.DrawLine(myPen, Position2d(vertex), Position2d(normalvertex));

            return normalvertex;

        }

        public double CosBetweenVector(Point3 v1, Point3 v2)
        {
            double cos = (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z) / (Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y + v1.Z * v1.Z) * Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z));
            return cos;
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpeg;*.jpg;*.png";
                if (ofd.ShowDialog() == DialogResult.Cancel) return;
                Texture = new Bitmap(ofd.FileName);
                string filename = ofd.FileName;
                Regex r = new Regex(@"\b(\w+\.\w+)$");
                MatchCollection matches = r.Matches(filename);
                label18.Text = matches[0].Value + "\nРазмеры: " + Texture.Width + ":" + Texture.Height;
            }
        }
        
    }

    
}


