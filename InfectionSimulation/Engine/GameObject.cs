using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfectionSimulation
{
    abstract class GameObject
    {
        private Point position = new Point(0,0);
        private double rotation = 0;
        private Color color = Color.Black;
        private long lastUpdate = 0;

        public virtual long UpdateInterval { get { return 10; } }

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }
        
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(Position, new Size(1, 1)); }
        }

        public void InternalUpdateOn(World world)
        {
            long now = Environment.TickCount;
            if (now - lastUpdate > UpdateInterval)
            {
                lastUpdate = now;
                UpdateOn(world);
            }
        }

        public virtual void UpdateOn(World world)
        {
            // Do nothing
        }

        public void Turn(int angle)
        {
            Rotation += Math.PI * angle / 180.0;
        }

        public void Forward(int dist)
        {
            //(direction degreeCos @ direction degreeSin) *distance + location
            Position = new Point((int)Math.Round(Math.Cos(rotation) * dist + Position.X),
                                 (int)Math.Round(Math.Sin(rotation) * dist + Position.Y));
        }

        public void LookTo(Point p)
        {
            Rotation = Math.Atan2(p.Y - Position.Y, p.X - Position.X);
        }
    }
}
