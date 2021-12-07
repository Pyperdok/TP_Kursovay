using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Kursovay
{
    class Circle
    {
        public int Radius; // радуис 
        public float X; // X координата положения в пространстве
        public float Y; // Y координата положения в пространстве

        public Circle(float x, float y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public virtual void Render(Graphics g)
        {
            var b = new Pen(Brushes.Red);
            // остальное все так же
            g.DrawEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
        }
    }
}
