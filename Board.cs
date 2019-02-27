using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Board
    {
        private int BOARDSIZE;
        private int CELLSTATESIZE = 2;
        private int MOORENEIGHBORHOOD = 9;
        public int[,] board;
        private int[,] board_buffer;
        private int[] rules;

        public Board(int[] rules_in, int boardsize)
        {
            BOARDSIZE = boardsize;
            board = new int[BOARDSIZE, BOARDSIZE];
            rules = rules_in;
            Array.Sort(rules);
            /*Random random = new Random();
            for(int i = 0; i < BOARDSIZE; i++) {
                for(int j = 0; j < BOARDSIZE; j++) {
                    if (random.Next(0, 9) >= 7) {
                        SetCell(i, j, 1);
                    }
                }
            }*/
            ClearBoard();
        }

        public int GetCell(int[,] board_in, int x, int y)
        {
            if (x >= 0 && x < BOARDSIZE && y >= 0 && y < BOARDSIZE) {
                return board_in[x, y];
            } else {
                return 0;
            }
        }

        public void SetCell(int[,] board_in, int x, int y, int value)
        {
            if (x >= 0 && x < BOARDSIZE && y >= 0 && y < BOARDSIZE) {
                board_in[x, y] = value;
            } else {
                return;
            }
        }

        public int GetConditionsNo(int[,] board_in, int x, int y)
        {
            int result = 0;
            for(int i = 0; i < MOORENEIGHBORHOOD; i++) {
                    result += FastPower(CELLSTATESIZE, i) * GetCell(board_in, x + i % 3 - 1, y + i / 3 - 1);
            }
            return result;
        }

        public void UpdateBoard()
        {
            board.CopyTo(board_buffer, 0);
            for (int x = 0; x < BOARDSIZE; x++) {
                for (int y = 0; y < BOARDSIZE; y++) {
                    int rule = -1;
                    for(int i = 0; i < CELLSTATESIZE; i++) {
                        int index = Array.BinarySearch(
                            rules, GetConditionsNo(board_buffer, x, y) + i * FastPower(CELLSTATESIZE, MOORENEIGHBORHOOD));
                        if (index >= 0) rule = rules[index];
                        if (rule >= 0) break;
                    }
                    if (rule >= 0) SetCell(board_buffer, x, y, rule / FastPower(CELLSTATESIZE, MOORENEIGHBORHOOD));
                }
            }
            board_buffer.CopyTo(board, 0);
        }

        public int FastPower(int number, int power)
        {
            if (power == 0) return 1;
            else if (power == 1) return number;
            else if (power % 2 == 0) {
                int tmp = FastPower(number, power / 2);
                return tmp * tmp;
            } else {
                int tmp = FastPower(number, (power - 1) / 2);
                return tmp * tmp * number;
            }
        }

        public void Draw()
        {
            for (int y = 0; y < BOARDSIZE; y++) {
                for (int x = 0; x < BOARDSIZE; x++) {
                    Console.Write(board, GetCell(x, y));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void ClearBoard()
        {
            for(int x = 0; x < BOARDSIZE; x++) {
                for(int y = 0; y < BOARDSIZE; y++) {
                    SetCell(board, x, y, 0);
                }
            }
        }
    }
}
