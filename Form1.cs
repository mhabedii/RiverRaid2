using Microsoft.VisualBasic.Devices;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace RiverRaid2
{
    public partial class Form1 : Form
    {
        bool goRight;
        bool goLeft;
        bool shooting;
        bool isGameOver;

        int score;
        int playerSpeed = 24;
        int enemySpeed;
        int bulletSpeed;

        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            showScore();
            moveEnemy();
            collisionPlayerAndEnemy();
            movePlayer();
            moveBullet();
            collisionBulletAndEnemy();
            resetEnemy();
            increaseSpeedEnemy();
        }

        private void showScore()
        {
            txtScore.Text = score.ToString();
        }


        private void moveEnemy()
        {
            enemyOne.Top += enemySpeed;
            enemyTwo.Top += enemySpeed;
            enemyThree.Top += enemySpeed;
        }

        private void collisionPlayerAndEnemy()
        {
            if (enemyOne.Bounds.IntersectsWith(player.Bounds))
            {
                gameOver();
            }
            if (enemyTwo.Bounds.IntersectsWith(player.Bounds))
            {
                gameOver();
            }
            if (enemyThree.Bounds.IntersectsWith(player.Bounds))
            {
                gameOver();
            }
        }
        private void movePlayer()
        {
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true && player.Left < 817)
            {
                player.Left += playerSpeed;
            }
        }

        private void moveBullet()
        {
            if (shooting == true)
            {
                bulletSpeed = 20;
                bullet.Top -= bulletSpeed;
            }
            else
            {
                bullet.Left = -300;
                bulletSpeed = 0;
            }

            if (bullet.Top < -30)
            {
                shooting = false;
            }
        }

        private void collisionBulletAndEnemy()
        {
            if (bullet.Bounds.IntersectsWith(enemyOne.Bounds))
            {
                score += 1;
                enemyOne.Top = -450;
                enemyOne.Left = rnd.Next(20, 600);
                shooting = false;
            }
            if (bullet.Bounds.IntersectsWith(enemyTwo.Bounds))
            {
                score += 1;
                enemyTwo.Top = -650;
                enemyTwo.Left = rnd.Next(20, 600);
                shooting = false;
            }
            if (bullet.Bounds.IntersectsWith(enemyThree.Bounds))
            {
                score += 1;
                enemyThree.Top = -750;
                enemyThree.Left = rnd.Next(20, 600);
                shooting = false;
            }
        }

        private void resetEnemy()
        {
            if (enemyOne.Top > 650)
            {
                enemyOne.Top = -450;
                enemyOne.Left = rnd.Next(20, 600);
                shooting = false;
            }
            if (enemyTwo.Top > 650)
            {
                enemyTwo.Top = -650;
                enemyTwo.Left = rnd.Next(20, 600);
                shooting = false;
            }
            if (enemyThree.Top > 650)
            {
                enemyThree.Top = -750;
                enemyThree.Left = rnd.Next(20, 600);
                shooting = false;
            }
        }

        private void increaseSpeedEnemy()
        {
            if (score == 5)
            {
                enemySpeed = 8;
            }
            if (score == 10)
            {
                enemySpeed = 10;
            }
            if (score == 15)
            {
                enemySpeed = 15;
            }
            if (score == 20)
            {
                enemySpeed = 20;
            }
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            /*            MessageBox.Show(e.KeyCode + "");
            */
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                goRight = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Space /*|| e.KeyCode == MouseButtons.Left*/)
            {
                if (shooting == false)
                {
                    shooting = true;
                    bullet.Top = player.Top + 30;
                    bullet.Left = player.Left + (player.Width / 2);
                }
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }
        }

        private void resetGame()
        {
            gameTimer.Start();
            enemySpeed = 6;

            enemyOne.Left = rnd.Next(20, 600);
            enemyTwo.Left = rnd.Next(20, 600);
            enemyThree.Left = rnd.Next(20, 600);

            enemyOne.Top = rnd.Next(0, 200) * -1;
            enemyTwo.Top = rnd.Next(0, 500) * -1;
            enemyThree.Top = rnd.Next(0, 900) * -1;

            score = 0;
            bulletSpeed = 0;
            bullet.Left = -300;
            shooting = false;

            txtScore.Text = score.ToString();
        }

        private void gameOver()
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text += Environment.NewLine + "Game Over !!" + Environment.NewLine + "Press Enter to Restart";
        }
    }
}
