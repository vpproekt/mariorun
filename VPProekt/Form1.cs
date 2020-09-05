using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPProekt
{
    public partial class Form1 : Form
    {
        int jumpSp, jumpVel, score, enemySp;
        bool jump;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();

            resetGame();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !jump && timer1.Enabled)
            {
                jump = true;
                mario.Image = Properties.Resources.mario_jump;
            }
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                resetGame();
            }

            if (jump && timer1.Enabled)
            {
                mario.Image = Properties.Resources.mario_run1;
                jump = false;
            }
        }
        private void gameEvent(object sender, EventArgs e)
        {
            mario.Top += jumpSp;
            label1.Text = "Points: " + score;

            if (jump && jumpVel < 0)
            {
                jump = false;
                mario.Image = Properties.Resources.mario_run1;
            }

            if (jump)
            {
                jumpSp = -12;
                jumpVel -= 1;
            }
            else
            {
                jumpSp = 12;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "enemy")
                {
                    x.Left -= enemySp;

                    if (x.Left + x.Width < -120)
                    {
                        x.Left = this.ClientSize.Width + rnd.Next(200, 800);
                        score++;
                    }

                    if (mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        timer1.Stop();
                        mario.Size = new System.Drawing.Size(32, 32);
                        mario.Image = Properties.Resources.mario_oops;
                        label1.Text = "Press R to Restart";
                    }

                }
            }

            if (mario.Top >= 300 && !jump)
            {
                jumpVel = 12;
                mario.Top = floor.Top - mario.Height;
                jumpSp = 0;
            }

            if (score >= 15)
            {
                enemySp = 15;
                if (score >= 30)
                {
                    enemySp = 20;
                }
            }

            if (score == 50)
            {
                timer1.Stop();
                label1.Text = "Congratulations!";
            }
        }

        private void resetGame()
        {
            jumpVel = 12;
            jumpSp = 0;
            score = 0;
            enemySp = 10;
            jump = false;
            label1.Text = "Points: " + score;

            mario.Top = floor.Top - mario.Height;
            mario.Size = new System.Drawing.Size(32, 56);
            mario.Image = Properties.Resources.mario_run1;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "enemy")
                {
                    int position = rnd.Next(600, 1000);
                    x.Left = 640 + (x.Left + position + x.Width * 3);
                }
            }

            timer1.Start();
        }
    }
}
