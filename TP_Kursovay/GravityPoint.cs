using System;
using System.Drawing;
using System.Windows;

namespace TP_Kursovay
{
    public class AntiGravityPoint
    {
        public float X;
        public float Y;
        public int Power = 100; //Диаметр окружности
        public Color color = Color.Cyan; //Перекрашивает частицы в данный цвет при касании

        //Изменяет состояние частицы, в близи данной окружности
        public void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
            if (r + particle.Radius <= Power / 1.5) // если частица оказалось внутри окружности
            {
                // то притягиваем ее
                float r2 = (float)Math.Max(100, gX * gX + gY * gY);

                Vector vector1 = new Vector(gX, gY);
                Vector vector2 = new Vector(particle.SpeedX, particle.SpeedY);
                double angleBetween;

                angleBetween = Vector.AngleBetween(vector1, vector2);
                angleBetween = 180 - angleBetween;

                float cs = (float)Math.Cos(angleBetween / 180 * Math.PI);
                float sn = (float)Math.Sin(angleBetween / 180 * Math.PI);

                particle.SpeedX = particle.SpeedX * cs - particle.SpeedY * sn;
                particle.SpeedY = -particle.SpeedX * sn + particle.SpeedY * cs;
                particle.FromColor = color;
            }
        }

        public void Render(Graphics g)
        {
            // буду рисовать окружность с диаметром равным Power
            g.DrawEllipse(
                   new Pen(color),
                   X - Power / 2,
                   Y - Power / 2,
                   Power,
                   Power
               );
        }
    }
}
