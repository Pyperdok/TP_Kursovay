using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP_Kursovay
{
    public partial class Form1 : Form
    {
        // собственно список, пока пустой
        Emitter emitter; // добавили эмиттер
        Circle circle;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            // а тут теперь вручную создаем
            emitter = new TopEmitter
            {
                Width = picDisplay.Width,
                GravitationY = 0.25f
            };

            circle = new Circle(picDisplay.Width / 2, picDisplay.Height / 2, 20);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); // тут теперь обновляем эмиттер
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);

                emitter.Render(g); // а тут теперь рендерим через эмиттер
                circle.Render(g);
            }

            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            // а тут в эмиттер передаем положение мыфки
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;
        }
    }
}
