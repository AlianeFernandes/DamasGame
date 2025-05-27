using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DamasGame;

namespace DamasGame
{
    public class Jogo
    {
        public Peca[,] Tabuleiro { get; private set; }  // Tabuleiro de 8x8
        public bool TurnoBranco { get; private set; }   // Indica se é o turno das peças brancas

        // Construtor do Jogo, inicializando o tabuleiro
        public Jogo()
        {
            Tabuleiro = new Peca[8, 8];
            InicializarTabuleiro();
            TurnoBranco = true; // Começa com o turno das peças brancas
        }

        // Método que inicializa as peças no tabuleiro
        private void InicializarTabuleiro()
        {
            // Coloca as peças brancas nas 3 primeiras linhas
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 1)  // Coloca as peças apenas nas casas escuras
                    {
                        Tabuleiro[row, col] = new Peca(true);  // Peca branca
                    }
                }
            }

            // Coloca as peças pretas nas 3 últimas linhas
            for (int row = 5; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 1)  // Coloca as peças apenas nas casas escuras
                    {
                        Tabuleiro[row, col] = new Peca(false);  // Peca preta
                    }
                }
            }
        }

        // Método que move uma peça do tabuleiro, verificando se o movimento é válido
        public bool MoverPeca(int origemRow, int origemCol, int destinoRow, int destinoCol)
        {
            Peca peca = Tabuleiro[origemRow, origemCol];  // Pega a peça na posição de origem

            // Verifica se existe uma peça na posição de origem
            if (peca == null)
                return false;

            // Verifica se é o turno correto para a peça
            if (peca.IsBranca != TurnoBranco)
                return false;

            // Verifica se o movimento é válido
            if (!EhMovimentoValido(peca, origemRow, origemCol, destinoRow, destinoCol))
                return false;

            // Realiza o movimento
            Tabuleiro[destinoRow, destinoCol] = peca;
            Tabuleiro[origemRow, origemCol] = null;

            // Se a peça atingir a última linha, ela se torna uma dama
            if ((peca.IsBranca && destinoRow == 7) || (!peca.IsBranca && destinoRow == 0))
            {
                peca.IsDama = true;
            }   
            // Troca o turno
            TurnoBranco = !TurnoBranco;

            return true;
        }

        // Método que verifica se o movimento da peça é válido
        private bool EhMovimentoValido(Peca peca, int origemRow, int origemCol, int destinoRow, int destinoCol)
        {
            // Verifica se o destino está dentro dos limites do tabuleiro
            if (destinoRow < 0 || destinoRow >= 8 || destinoCol < 0 || destinoCol >= 8)
                return false;

            // Verifica se a casa de destino está livre
            if (Tabuleiro[destinoRow, destinoCol] != null)
                return false;
            // Calcula a direção do movimento
            int deltaRow = destinoRow - origemRow;
            int deltaCol = destinoCol - origemCol;

            // Verifica se a peça está se movendo para frente ou para trás (de acordo com a cor)
            if (peca.IsBranca && deltaRow <= 0 && !peca.IsDama)
            { // Peça branca só pode mover para baixo
                Console.WriteLine("Olá, mundo!: ", peca);
                return false;
            }

            if (!peca.IsBranca && deltaRow >= 0 && !peca.IsDama)  // Peça preta só pode mover para cima
                return false;

            

            // Verifica se o movimento é diagonal
            if (Math.Abs(deltaRow) != Math.Abs(deltaCol))
                return false;

            if (peca.IsDama) {
                int steps = Math.Max(Math.Abs(deltaRow), Math.Abs(deltaCol));
                int stepRow = (deltaRow == 0) ? 0 : deltaRow / Math.Abs(deltaRow);
                int stepCol = (deltaCol == 0) ? 0 : deltaCol / Math.Abs(deltaCol);
                int row = origemRow;
                int col = origemCol;

                for (int i = 0; i < steps; i++)
                {
                    row += stepRow;
                    col += stepCol;
                    if (Tabuleiro[row, col] != null)
                    {
                        if (Tabuleiro[row, col].IsBranca != peca.IsBranca)
                        { 
                            Tabuleiro[row, col] = null; // ou qualquer outra lógica para "apagar"
                        }
                    }
                }

                return true;
            }

            if (Math.Abs(deltaRow) > 2 || Math.Abs(deltaRow) == 0)
                return false;

            //quando tentando se mover uma diagonal de 2
            if (Math.Abs(deltaRow) == 2)
            {
                if (Tabuleiro[origemRow + (deltaRow / 2), origemCol + (deltaCol / 2)] == null)
                    return false;

                Peca alvo = Tabuleiro[origemRow + (deltaRow / 2), origemCol + (deltaCol / 2)];
                if ((peca.IsBranca && alvo.IsBranca) || (!peca.IsBranca && !alvo.IsBranca))
                    return false;
                Tabuleiro[origemRow + (deltaRow / 2), origemCol + (deltaCol / 2)] = null;
                return true;
            }

            // Se for uma peça comum, deve se mover apenas uma casa de cada vez
            return true;
        }
    }
}