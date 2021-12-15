using System;
using System.Collections.Generic;
using System.Drawing;

namespace TP_Kursovay
{
    //Класс, создающий и управляющий частицами
    public class Emitter
    {
        List<Particle> particles = new List<Particle>(); //Список частиц
        public float GravitationY = 1f; //Гравитация Y
        public int ParticlesCount = 1500;

        public int X;
        public int Y; 
        public int Direction = 0; //Направление в градусах куда сыпет эмиттер
        public int Spreading = 0; //Разброс частиц относительно Direction
        public int SpeedMin = 1;  // начальная минимальная скорость движения частицы
        public int SpeedMax = 10;  // начальная максимальная скорость движения частицы
        public int RadiusMin = 2;  // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 20;   // минимальное время жизни частицы
        public int LifeMax = 100;  // максимальное время жизни частицы
        public int ParticlesPerTick = 20; // добавил новое поле

        public Color ColorFrom = Color.Gold; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public AntiGravityPoint antiGravityPoint = new AntiGravityPoint();
        public List<AntiGravityPoint> antiGravityPoints = new List<AntiGravityPoint>();

        //Обновляет состояние частиц эммитера
        public void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.Life -= 0.5f; //Уменьшаем жизнь частицы
                if (particle.Life <= 0)
                {
                    ResetParticle(particle); //Перемещает частицы в точку эммитера
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

            //Генерируем за тик не более 10 частиц
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < ParticlesCount)
                {
                    var particle = CreateParticle(); 
                    ResetParticle(particle);
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
        }

        //Отрисовывает все частицы
        public void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }

        }

        //Перемещает частицу в точку эммитера
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

        //Создает частицу
        public Particle CreateParticle()
        {
            var particle = new Particle();
            particle.FromColor = ColorFrom;
            particle.ToColor = ColorTo;

            return particle;
        }
    }
}
