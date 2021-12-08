using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP_Kursovay
{
    public partial class Form1 : Form
    {
        // собственно список, пока пустой
        Emitter emitter; // добавили эмиттер
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            // а тут теперь вручную создаем
            emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = 0,
                Y = 0,

                AntiGravityPoint = new AntiGravityPoint
                {
                    X = 0f,
                    Y = 0f
                }
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); // тут теперь обновляем эмиттер
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);

                emitter.Render(g); // а тут теперь рендерим через эмиттер
            }

            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            // а тут в эмиттер передаем положение мыфки
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;

            if (emitter.AntiGravityPoint != null) {
                emitter.AntiGravityPoint.X = e.X;
                emitter.AntiGravityPoint.Y = e.Y;
            }
        }
    }
}
