using System;
using System.Collections.Generic;
using System.Drawing;

namespace TP_Kursovay
{
    public class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int MousePositionX;
        public int MousePositionY;
        public float GravitationY = 1f; // пусть гравитация будет силой один пиксель за такт, нам хватит
        public int ParticlesCount = 1500;

        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 0; // разброс частиц относительно Direction
        public int SpeedMin = 1; // начальная минимальная скорость движения частицы
        public int SpeedMax = 10; // начальная максимальная скорость движения частицы
        public int RadiusMin = 2; // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 20; // минимальное время жизни частицы
        public int LifeMax = 100; // максимальное время жизни частицы
        public int ParticlesPerTick = 20; // добавил новое поле

        public Color ColorFrom = Color.Gold; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public AntiGravityPoint antiGravityPoint = new AntiGravityPoint();
        public List<AntiGravityPoint> antiGravityPoints = new List<AntiGravityPoint>();

        public void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.Life -= 0.5f;  // не трогаем
                if (particle.Life <= 0)
                {
                    ResetParticle(particle); // заменили этот блок на вызов сброса частицы 
                }
                else
                {
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;

                    particle.SpeedY += GravitationY;

                    antiGravityPoint?.ImpactParticle(particle);
                    for (int i = 0; i < antiGravityPoints.Count; i++) {
                        antiGravityPoints[i]?.ImpactParticle(particle);
                    }
                }
            }

            // генерирую не более 10 штук за тик
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < ParticlesCount)
                {
                    var particle = CreateParticle(); // и собственно теперь тут его вызываем
                    ResetParticle(particle); // добавили вызов ResetParticle
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
        }

        public void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }

        }

        // добавил новый метод, виртуальным, чтобы переопределять можно было
        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = Particle.rand.Next(LifeMin, LifeMax);
            particle.FromColor = Color.Gold;
            particle.X = X;
            particle.Y = Y;

            var direction = Direction
                + (double)Particle.rand.Next(Spreading)
                - Spreading / 2;

            var speed = Particle.rand.Next(SpeedMin, SpeedMax);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = Particle.rand.Next(RadiusMin, RadiusMax);
        }

        /* добавил метод */
        public Particle CreateParticle()
        {
            var particle = new Particle();
            particle.FromColor = ColorFrom;
            particle.ToColor = ColorTo;

            return particle;
        }
    }
}
