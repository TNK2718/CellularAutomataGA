using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class CellAutomataGame
    {
        public int BOARD_SIZE = 20;
        public Board board1;
        public Board board2;

        enum GameState
        {
            INGAME, PLAYER1WIN, PLAYER2WIN
        }

        public CellAutomataGame(int[] rules_in1, int[] rules_in2)
        {
            board1 = new Board(rules_in1, BOARD_SIZE);
            board2 = new Board(rules_in2, BOARD_SIZE);
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
            for(int x = 0; x < BOARD_SIZE; x++) {
                for(int y = 0; y < BOARD_SIZE; y++) {
                    if(board1.GetCell(board1.board, x, y) != 0 && board2.GetCell(board2.board, x, y) != 0) {
                        board1.SetCell(true, x, y, 0);
                        board2.SetCell(true, x, y, 0);
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
            board1.SetCell(true, 1, 1, 1);
            board2.SetCell(true, BOARD_SIZE - 1, BOARD_SIZE - 1, 1);
        }

        public void Draw()
        {
            for (int y = 0; y < BOARD_SIZE; y++) {
                for (int x = 0; x < BOARD_SIZE; x++) {
                    if (board1.GetCell(board1.board, x, y) == 0) {
                        if (board2.GetCell(board2.board, x, y) == 0) Console.Write(" ");
                        Console.Write(-board2.GetCell(board2.board, x, y));
                    } else {
                        Console.Write(" ");
                        Console.Write(board1.GetCell(board1.board, x, y));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
