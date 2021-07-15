using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace InfectionSimulation
{
    class Person : GameObject
    {
        public bool Infected { get; set; }

        public override void UpdateOn(World world)
        {
            List<Person> near = world.ObjectsAt(Position);
            if (Infected)
            {
                Color = Color.Red;
                foreach (Person p in near)
                {
                    p.Infected = true;
                }
            }
            else
            {
                Color = Color.Blue;

                foreach (Person p in near)
                {
                    if (p.Infected)
                    {
                        Infected = true;
                        break;
                    }
                }                
            }

            Forward(world.Random(1, 2));
            Turn(world.Random(-25, 25));
        }
        
    }
}
