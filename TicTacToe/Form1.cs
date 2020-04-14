using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public int flagMove { get; set; }
        public Form1()
        {
            InitializeComponent();
            flagMove = 0;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (flagMove == 0)
            {
                ((Button)(sender)).Text = "X";
                flagMove = 1;
            }
            else
            {
                ((Button)(sender)).Text = "O";
                flagMove = 0;
            }
        }
    }
}
