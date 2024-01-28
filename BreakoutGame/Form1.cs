using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakoutGame
{
    public partial class Breakout : Form
    {


        bool goLeft;
        bool goRight;

        int score;
        int ballx;
        int bally;

        int playerSpeed;

        Random rnd = new Random();

        public Breakout()
        {
            InitializeComponent();

            setupGame();
        }


        private void setupGame() 
        {
            score = 0;
            ballx = 5;
            bally = 5;

            playerSpeed = 10;
            gameTimer.Start();
        }



        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true && player.Left < 490)
            {
                player.Left += playerSpeed;

            }

            ball.Left += ballx;
            ball.Top += bally;



            // Check if Ball has touched bound
            if (ball.Left < 0 || ball.Left > 560)
            {
                ballx = -ballx;
            }

            if (ball.Top < 0)
            {
                bally = -bally;
            }




            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = rnd.Next(5, 12) * -1;

                if (ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;

                }
                else
                {

                    ballx = rnd.Next(5, 12);
                }

            }


            // Check if Ball has touched PictureBox 
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    
                    {

                        score += 1;

                        //make ball bounce on picturebox
                        bally = -bally;

                        this.Controls.Remove(x);

                    }
                }
            }


            // Show Win MessageBox after hitting all 15 boxes
            if (score == 15) 
            {

                gameTimer.Stop();
                MessageBox.Show("Win", "Win", MessageBoxButtons.OK);

            }


            // if ball falls of bounds display game over
            if (ball.Top > 510) 
            {
                //GameOver
                gameTimer.Stop();
                MessageBox.Show("Game Over",
                "Game Over", MessageBoxButtons.OK);

            }

        }

        // Check if Key is pressed
        private void keyisdown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) 
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                goRight = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                goRight = false;
            }

        }
    }
}
