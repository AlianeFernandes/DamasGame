using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DamasGame;

namespace DamasGame
{
    static class Program
    {
        // Ponto de entrada principal para o aplicativo.
        [STAThread]
        static void Main()
        {
            // Configurações necessárias para o Windows Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializa o jogo com o Form1 (onde a interface gráfica estará)
            Application.Run(new Form1());
        }
    }
}