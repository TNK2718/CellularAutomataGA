using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class CellAutomataGame
    {
        public int BOARDSIZE = 10;
        public Board board1;
        public Board board2;

        enum GameState
        {
            INGAME, PLAYER1WIN, PLAYER2WIN
        }

        public CellAutomataGame(int[] rules_in1, int[] rules_in2)
        {
            board1 = new Board(rules_in1, BOARDSIZE);
            board2 = new Board(rules_in2, BOARDSIZE);
        }

        public void UpdateGameBoard()
        {
            board1.UpdateBoard();
            board2.UpdateBoard();
            ApplyCellfunction();
            ApplyCollision();
        }

        private void ApplyCollision()
        {
            for(int x = 0; x < BOARDSIZE; x++) {
                for(int y = 0; y < BOARDSIZE; y++) {
                    if(board1.GetCell(x, y) != 0 && board2.GetCell(x, y) != 0) {
                        board1.SetCell(x, y, 0);
                        board2.SetCell(x, y, 0);
                    }
                }
            }
        }

        public void ApplyCellfunction()
        {

        }

        public void InitializeBoards()
        {
            board1.ClearBoard();
            board2.ClearBoard();
            board1.SetCell(0, 0, 1);
            board1.SetCell(2, 2, 1);
            board2.SetCell(BOARDSIZE - 1, BOARDSIZE - 1, 1);
            board2.SetCell(BOARDSIZE - 3, BOARDSIZE - 3, 1);
        }

        public void Draw()
        {
            for (int y = 0; y < BOARDSIZE; y++) {
                for (int x = 0; x < BOARDSIZE; x++) {
                    if (board1.GetCell(x, y) == 0) {
                        if (board2.GetCell(x, y) == 0) Console.Write(" ");
                        Console.Write(-board2.GetCell(x, y));
                    } else {
                        Console.Write(" ");
                        Console.Write(board1.GetCell(x, y));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
