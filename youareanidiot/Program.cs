﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youareanidiot
{

    internal static class Program
    {
        
        static SoundPlayer player;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Form1 f = new Form1();
            f.Show();

            while (true)
            {
                f.move();
                
                
                Application.DoEvents();
                Thread.Sleep(1);
            }
        }
        public static void playaudio()
        {
            
            player = new SoundPlayer(youareanidiot.Properties.Resources.you_are_an_idiot);

            player.PlayLooping();
        }
        public static void stopaudio()
        {
            player.Stop();
        }
        
    }
}
