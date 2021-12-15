using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP_Kursovay
{
    //Основной класс, отвечающий за работу формы
    public partial class Form1 : Form
    {
        Emitter emitter;
        //Зеленая окружность
        AntiGravityPoint left = new AntiGravityPoint
        {
            X = 150f,
            Y = 400f,
            color = Color.Lime
        };

        //Фиолетовая окружность
        AntiGravityPoint right = new AntiGravityPoint
        {
            X = 150f,
            Y = 0f,
            color = Color.Purple
        };

        //Конструктор формы
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            picDisplay.MouseWheel += picDisplay_MouseWheel;
            emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 300,
                SpeedMin = 20,
                SpeedMax = 30,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red)
            };
            emitter.antiGravityPoints.Add(left);
            emitter.antiGravityPoints.Add(right);
        }

        //Таймер формы. Срабатывает каждые 40мс
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); //Обновление частиц эммитера
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);

                //Рендер всех объектов
                emitter.Render(g); 
                left.Render(g);
                right.Render(g);
                emitter.antiGravityPoint.Render(g);
            }

            picDisplay.Invalidate();
        }

        //Обработчик, срабатывающий при передвижении мыши по форме
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (emitter.antiGravityPoint != null) {
                emitter.antiGravityPoint.X = e.X;
                emitter.antiGravityPoint.Y = e.Y;
            }
        }

        //Обработчик, меняет размер окружности, прикрепленный к мышке
        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta >= 0 && emitter.antiGravityPoint.Power <= 200)
            {
                emitter.antiGravityPoint.Power += (int)(e.Delta / 10f);
            }
            else if(emitter.antiGravityPoint.Power >= 30 && e.Delta < 0)
            {
                emitter.antiGravityPoint.Power -= (int)(-e.Delta / 10f);
            }
        }
    }
}
