using System;
using System.Drawing;
using System.Windows.Forms;
using DamasGame;

namespace DamasGame
{
    public partial class Form1 : Form
    {
        private int selectedRow = -1;
        private int selectedCol = -1;
        private Jogo jogo = new Jogo();  // Instância do jogo

        public Form1()
        {
            InitializeComponent();
        }

        // Método que desenha o tabuleiro no formulário
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int tileSize = 80;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    // Desenho dos quadrados do tabuleiro
                    bool isBlack = (row + col) % 2 == 1;
                    Brush tileBrush = isBlack ? Brushes.SaddleBrown : Brushes.Beige;
                    e.Graphics.FillRectangle(tileBrush, col * tileSize, row * tileSize, tileSize, tileSize);

                    // Desenho das peças no tabuleiro
                    Peca peca = jogo.Tabuleiro[row, col]; // Obtendo a peça no tabuleiro
                    if (peca != null)
                    {
                        Brush pecaBrush = peca.IsBranca ? Brushes.White : Brushes.Black;
                        e.Graphics.FillEllipse(pecaBrush, col * tileSize + 10, row * tileSize + 10, tileSize - 20, tileSize - 20);

                        // Se a peça for dama, desenha um 'D' em vermelho
                        if (peca.IsDama)
                        {
                            e.Graphics.DrawString("D", new Font("Arial", 16, FontStyle.Bold), Brushes.Red, col * tileSize + 30, row * tileSize + 30);
                        }
                    }
                }
            }
        }

        // Método para capturar o clique do mouse e realizar o movimento das peças
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int tileSize = 80;
            int row = e.Y / tileSize;  // Calculando a linha clicada
            int col = e.X / tileSize;  // Calculando a coluna clicada

            if (selectedRow == -1)
            {
                // Se nenhuma peça estiver selecionada, tenta selecionar a peça clicada
                if (jogo.Tabuleiro[row, col] != null)
                {
                    selectedRow = row;
                    selectedCol = col;
                }
            }
            else
            {
                // Tenta mover a peça selecionada para o novo local
                bool moveFeito = jogo.MoverPeca(selectedRow, selectedCol, row, col);
                if (moveFeito)
                {
                    selectedRow = selectedCol = -1;  // Reseta a seleção após o movimento
                    Invalidate();  // Redesenha o tabuleiro
                }
                else
                {
                    // Se o movimento for inválido, cancela a seleção
                    selectedRow = selectedCol = -1;
                }
            }
        }
    }
}