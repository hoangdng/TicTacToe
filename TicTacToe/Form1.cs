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
        public string[,] board;
        public Form1()
        {
            InitializeComponent();
            board = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = "";
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "")
            {
                b.Text = "X";
                int x = (int)Char.GetNumericValue(b.Name[1]);
                int y = (int)Char.GetNumericValue(b.Name[2]);
                board[x, y] = "X";
                if (isEndGame(board) == 3) //Game is not end
                {
                    bestMove();
                }
                renderBoard();
                checkWinner(isEndGame(board));
            }  
        }
        public void checkWinner(int x)
        {
            if (x == 0)
            {
                MessageBox.Show("O win!");
            }
            if (x == 1)
            {
                MessageBox.Show("Tie...");
            }
            if (x == 2)
            {
                MessageBox.Show("X win!");
            }
        }
        public void renderBoard()
        {
            b00.Text = board[0, 0];
            b01.Text = board[0, 1];
            b02.Text = board[0, 2];
            b10.Text = board[1, 0];
            b11.Text = board[1, 1];
            b12.Text = board[1, 2];
            b20.Text = board[2, 0];
            b21.Text = board[2, 1];
            b22.Text = board[2, 2];
        }
        public bool equal3(string x, string y, string z)
        {
            return x == y && x == z && x != "";
        }
        public int isEndGame(string[,] currentBoard)
        //Return 0 if O win, 1 if tie, 2 if X win and 3 if game is not end
        {
            string winner = null;
            //horizontal
            for (int i = 0; i < 3; i++)
            {
                if (equal3(currentBoard[i, 0], currentBoard[i, 1], currentBoard[i, 2]))
                {
                    winner = currentBoard[i, 0];
                }
            }
            //vertical
            for (int i = 0; i < 3; i++)
            {
                if (equal3(currentBoard[0, i], currentBoard[1, i], currentBoard[2, i]))
                {
                    winner = currentBoard[0, i];
                }
            }
            //diagonal
            if (equal3(currentBoard[0, 0], currentBoard[1, 1], currentBoard[2, 2]))
            {
                winner = currentBoard[0, 0];
            }
            if (equal3(currentBoard[2, 0], currentBoard[1, 1], currentBoard[0, 2]))
            {
                winner = currentBoard[2, 0];
            }
            //check if board is occupied
            int blankSpot = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentBoard[i, j] == "")
                    {
                        blankSpot++;
                    }
                }
            }

            if (winner == null && blankSpot == 0)
            {
                return 1;
            }
            else if (winner != null)
            {
                return winner == "X" ? 2 : 0;
            }
            return 3;
        }

        public void bestMove()
        {
            // AI to make its turn
            int bestScore = -1000; //-Infinity
            int[] move = new int[2];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Is the spot available?
                    if (board[i, j] == "")
                    {
                        board[i, j] = "O";
                        int score = minimax(board, 0, false); //minimax(node, depth, maximizingPlayer)
                        board[i, j] = "";
                        if (score > bestScore)
                        {
                            bestScore = score;
                            move[0] = i;
                            move[1] = j;
                        }
                    }
                }
            }
            board[move[0], move[1]] = "O";
        }

        int[] scores = new int[3] { 1, 0, -1 };

        public int minimax(string[,] board, int depth, bool isMaximizing)
        {
            int result = isEndGame(board);
            if (result != 3)
            {
                return scores[result];
            }
            if (isMaximizing)
            {
                int bestScore = -1000; //-Infinity
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Is the spot available?
                        if (board[i, j] == "")
                        {
                            board[i, j] = "O";
                            int score = minimax(board, depth + 1, false);
                            board[i, j] = "";
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = 1000; //Infinity
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Is the spot available?
                        if (board[i, j] == "")
                        {
                            board[i, j] = "X";
                            int score = minimax(board, depth + 1, true);
                            board[i, j] = "";
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = "";
                }
            }
            renderBoard();
        }
    }
}
