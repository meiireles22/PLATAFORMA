using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Game_Tutorial_MOO_ICT
{
    public partial class Form2 : Form
    {

        // Declaração das variáveis de controlo de movimento e estado do jogo
        bool goLeft, goRight, jumping, isGameOver;
        // Velocidade do salto
        int jumpSpeed;
        // Força do salto
        int force;
        // Pontuação inicial
        int score = 0;
        // Velocidade do jogador
        int playerSpeed = 7;
        // Velocidade da plataforma horizontal
        int horizontalSpeed = 5;
        // Velocidade da plataforma vertical
        int verticalSpeed = 3;
        // Velocidade do enemyOne
        int enemyOneSpeed = 5;
        // Velocidade do enemyTwo
        int enemyTwoSpeed = 3;


        public Form2()
        {
            InitializeComponent();
        }

        // Lógica principal do jogo executada a cada tick do temporizador
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            // Atualiza o texto da pontuação na interface
            txtScore.Text = "Score: " + score;
            // Move o jogador verticalmente com base na velocidade do salto
            player.Top += jumpSpeed;
            // Lógica de movimento horizontal do jogador
            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }
            // Lógica do salto
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            // Verifica se o player bate nas plataformas, moedas e inimigos
            foreach (Control x in this.Controls)
            {
                // Lógica de encostar com plataformas
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        // Lógica do salto quando atinge uma plataforma
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            // Movimentação horizontal especial para a plataforma horizontal
                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }

                        // Traz a plataforma para a frente para evitar problemas de renderização
                        x.BringToFront();
                    }
                    // Lógica de apanhar as moedas
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    //Lógica de bater nos inimigos
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine + "You were killed in your journey!!";
                        }
                    }
                }
            }

            //Lógica de movimento das plataformas e inimigos
            horizontalPlatform.Left -= horizontalSpeed;
            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }
            verticalPlatform.Top -= verticalSpeed;
            if (verticalPlatform.Top < 195 || verticalPlatform.Top > 581)
            {
                verticalSpeed = -verticalSpeed;
            }
            enemyOne.Left += enemyOneSpeed;
            if (enemyOne.Left < pictureBox5.Left || enemyOne.Left + enemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }
            enemyTwo.Left += enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            //Verifica se o jogador caiu para fora da area determinada
            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death!";
            }

            // Verifica se o jogador atingiu a porta com pontuação suficiente
            if (player.Bounds.IntersectsWith(door.Bounds) && score == 26)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "Your quest is complete!";
            }
            else
            {
                txtScore.Text = "Score: " + score + Environment.NewLine + "Collect all the coins";
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Lógica para controlar as teclas pressionadas
            if (e.KeyCode == Keys.A)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.W && jumping == false)
            {
                jumping = true;
            }

        }

        //função de movimentação(defenir a movimentação)
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                goRight = false;
            }
            if (jumping == true && e.KeyCode == Keys.W)
            {
                jumping = false;
            }
            // Reneciar o jogo se pressionar Enter após o fim do jogo
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }
        //função para reneciar o jogo
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            //Torna visíveis todos os PictureBoxes que representam moedas
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }
            // Reseta as posições do jogador, plataformas e inimigos
            player.Left = 72;
            player.Top = 656;
            enemyOne.Left = 471;
            enemyTwo.Left = 360;
            horizontalPlatform.Left = 275;
            verticalPlatform.Top = 581;
            gameTimer.Start();
        }

        private void txtScore_Click(object sender, EventArgs e)
        {
            // Não há lógica associada ao clique na pontuação pois foi um erro no meu sistema em que se eu apagar este "private" 
            //fico sem o form1.
        }






        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
