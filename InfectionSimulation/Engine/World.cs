using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfectionSimulation
{
    class World
    {
        private Random rnd = new Random();

        private const int width = 300;
        private const int height = 300;
        private Size size = new Size(width, height);
        private List<GameObject> objects = new List<GameObject>();
        private List<Person>[,] personas = new List<Person>[width, height];

        public IEnumerable<GameObject> GameObjects {
            get
            {
                return objects.ToArray();
            }
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Point Center { get { return new Point(width / 2, height / 2); } }

        public bool IsInside(Point p)
        {
            return p.X >= 0 && p.X < width
                && p.Y >= 0 && p.Y < height;
        }
        
        public Point RandomPoint()
        {
            return new Point(rnd.Next(width), rnd.Next(height));
        }

        public float Random()
        {
            return (float)rnd.NextDouble();
        }

        public int Random(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public void Add(GameObject obj, bool isAdded = false)
        {
            if (!isAdded)
                objects.Add(obj);
            
            if (personas[obj.Position.X, obj.Position.Y] is null)
            {
                personas[obj.Position.X, obj.Position.Y] = new List<Person>();
            }

            personas[obj.Position.X, obj.Position.Y].Add((Person)obj);
        }

        public void Remove(GameObject obj)
        {
            personas[obj.Position.X, obj.Position.Y].Remove((Person)obj);
        }

        public void Update()
        {
            foreach (GameObject obj in GameObjects)
            {
                Remove(obj);
                obj.InternalUpdateOn(this);
                obj.Position = Mod(obj.Position, size);
                Add(obj, true);
            }
        }

        public void DrawOn(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Black, 0, 0, width, height);

            foreach (GameObject obj in GameObjects)
            {
                graphics.FillRectangle(new Pen(obj.Color).Brush, obj.Bounds);
            }
        }

        public double Dist(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public double Dist(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        // http://stackoverflow.com/a/10065670/4357302
        private static int Mod(int a, int n)
        {
            int result = a % n;
            if ((a < 0 && n > 0) || (a > 0 && n < 0))
                result += n;
            return result;
        }
        private static Point Mod(Point p, Size s)
        {
            return new Point(Mod(p.X, s.Width), Mod(p.Y, s.Height));
        }

        public List<Person> ObjectsAt(Point pos)
        {
            return personas[pos.X, pos.Y];
        }

    }
}
