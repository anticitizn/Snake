using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeForms
{
    public partial class Window : Form
    {
        private const int SIZE = 50;
        private const int WIDTH = 15;
        private const int HEIGHT = 15;

        private GameState game = null;
        private Bitmap gameField = null;
        private Graphics gameGraphics = null;

        public Window()
        {
            InitializeComponent();
            this.ClientSize = new Size(SIZE * WIDTH, SIZE * HEIGHT + 30);
            this.DoubleBuffered = true;

            gameField = new Bitmap(WIDTH * SIZE, HEIGHT * SIZE);
            gameGraphics = Graphics.FromImage(gameField);
            gameGraphics.PageUnit = GraphicsUnit.Pixel;

            game = new GameState(WIDTH, HEIGHT);

            timer.Start();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            // First draw on a Bitmap, then draw the Bitmap on the screen
            // This is done to prevent drawing over buttons and other GUI elements on the form
            Graphics g = gameGraphics;
            g.Clear(Color.White);

            List<List<Piece>> Map = game.Map;
            for (int y = 0; y < Map.Count; y++)
            {
                for (int x = 0; x < Map[y].Count; x++)
                {
                    Piece w = Map[y][x];
                    if (w.Type == PieceType.Empty)
                    {

                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(w.Color), w.X * SIZE, w.Y * SIZE, SIZE, SIZE);
                    }
                }
            }

            e.Graphics.DrawImage(gameField, 0, 35);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            scorePoints.Text = game.Score.ToString();
            this.Invalidate();

            game.OnTick();
            if (game.Running == false)
            {
                pauseButton.Enabled = false;
                timer.Stop();
            }
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            pauseButton.Enabled = true;
            game.Restart();
            timer.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            game.HandleInput(e);
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
                pauseButton.Text = "Unpause";
                this.KeyPreview = false;
            }
            else
            {
                timer.Start();
                pauseButton.Text = "Pause";
                this.KeyPreview = true;
            }
        }

        private void slowBtn_Click(object sender, EventArgs e)
        {
            this.timer.Interval = 100;
        }

        private void medBtn_Click(object sender, EventArgs e)
        {
            this.timer.Interval = 65;
        }

        private void fastBtn_Click(object sender, EventArgs e)
        {
            this.timer.Interval = 1;
        }

        private void playerSnakeBtn_Click(object sender, EventArgs e)
        {
            game.SwitchPlayer();
        }
    }
}
