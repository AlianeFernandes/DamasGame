using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamasGame
{
    public class Peca
    {
        public bool IsBranca { get; private set; }  // Propriedade que indica se a peça é branca (true) ou preta (false)
        public bool IsDama { get; set; }  // Propriedade que indica se a peça é uma dama

        // Construtor para inicializar a peça
        public Peca(bool isBranca)
        {
            IsBranca = isBranca;
            IsDama = false;  // Inicialmente, a peça não é uma dama
        }
    }
}