using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lots_of_games_v3
{
    public partial class Form1 : Form
    {
        private Point pos;
        private bool dragging, lose = false;
        private int countCoins = 0;
        public Form1()
        {
            InitializeComponent();

            road.MouseDown += MouseClickDown;
            road.MouseUp += MouseClickUp;
            road.MouseMove += MouseClickMove;

            road2.MouseDown += MouseClickDown;
            road2.MouseUp += MouseClickUp;
            road2.MouseMove += MouseClickMove;

            labelLose.Visible = false;
            button_restart.Visible = false;

            KeyPreview = true;
        }

        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            pos.X = e.X;
            pos.Y = e.Y;
        }

        private void MouseClickUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currPoint = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(currPoint.X - pos.X, currPoint.Y - pos.Y + road.Top);
            }
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        // Основное тело игры (передвижение карты, соперников, монет)
        private void timer_Tick(object sender, EventArgs e)
        {
            int speed = 10;
            road.Top += speed;
            road2.Top += speed;

            int carSpeed = 7;
            machine_opponent1.Top += carSpeed;
            machine_opponent2.Top += carSpeed;
            machine_opponent3.Top += carSpeed;
            machine_opponent4.Top += carSpeed;
        
            coin.Top += speed-3;

            Coin_generation();

            if (road.Top >= 640)
            {
                road.Top = 0;
                road2.Top = -640;
            }

            if (machine_opponent1.Top >= 750)
            {
                machine_opponent1.Top = -161;
                machine_opponent1.Left = RandomNumber1();
                
            }

            if (machine_opponent2.Top >= 860)
            {
                machine_opponent2.Top = -501;
                machine_opponent2.Left = RandomNumber1();
            }

            if (machine_opponent3.Top >= 960)
            {
                machine_opponent3.Top = -150;
                machine_opponent3.Left = RandomNumber2();
            }

            if (machine_opponent4.Top >= 1060)
            {
                machine_opponent4.Top = -650;
                machine_opponent4.Left = RandomNumber2();   
            }

            // Условие проигрыша и вывод меню перезапуска
            if (player.Bounds.IntersectsWith(machine_opponent4.Bounds)
                || player.Bounds.IntersectsWith(machine_opponent3.Bounds)
                || player.Bounds.IntersectsWith(machine_opponent2.Bounds)
                || player.Bounds.IntersectsWith(machine_opponent1.Bounds))
            {
                timer.Enabled = false;
                labelLose.Visible = true;
                button_restart.Visible = true;
                lose = true;
            }

            if (player.Bounds.IntersectsWith(coin.Bounds))
            {
                countCoins += 1;
                labelCoins.Text = "Счет: " + countCoins.ToString();
                Coin_generation();
            }

        }

        private int RandomNumber1() 
        {
            List<int> numbers = new List<int> { 181, 307};

            Random random = new Random();
            int randomIndex = random.Next(0, numbers.Count);
            int randomNumber = numbers[randomIndex];

            return randomNumber;

        }

        private int RandomNumber2()
        {
            List<int> numbers2 = new List<int> { 437, 562 };

            Random random = new Random();
            int randomIndex2 = random.Next(0, numbers2.Count);
            int randomNumber2 = numbers2[randomIndex2];

            return randomNumber2;

        }

        private int RandomNumber3()
        {
            List<int> numbers2 = new List<int> { 181, 307,437, 562 };

            Random random = new Random();
            int randomIndex2 = random.Next(0, numbers2.Count);
            int randomNumber2 = numbers2[randomIndex2];

            return randomNumber2;

        }

        private void Coin_generation() 
        {
            if (coin.Top >= 640)
            {
                coin.Top = -300;
                coin.Left = RandomNumber3();
            }
        }

        // Обработка нажатия кнопок движения
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose) return;

            int speed = 50;

            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A)&& player.Left > 185)
            {
                player.Left -= speed; 
            }
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && player.Right < 660) 
            {
                player.Left += speed;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void machine_opponent3_Click(object sender, EventArgs e)
        {

        }

        private void button_restart_Click(object sender, EventArgs e)
        {
            machine_opponent4.Top = -650;
            machine_opponent3.Top = -501;
            machine_opponent2.Top = -361;
            machine_opponent1.Top = -161;
            labelLose.Visible= false;
            button_restart.Visible = false;
            timer.Enabled = true;
            lose = false;
            countCoins = 0;
            labelCoins.Text = "Счет: " + countCoins.ToString();
            coin.Top = -600;
        }
    }
}
