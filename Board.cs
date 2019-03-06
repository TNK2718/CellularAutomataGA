using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AboutGeneticAlgorithm
{
    class Board
    {
        private int BOARD_SIZE;
        public int CELL_STATE_SIZE = 8;
        private int MOORE_NEIGHBORHOOD = 9;
        public int[,] board;
        private int[,] board_buffer;
        private int[] rules;

        public Board(int[] rules_in, int boardsize)
        {
            BOARD_SIZE = boardsize;
            board = new int[BOARD_SIZE, BOARD_SIZE];
            board_buffer = new int[BOARD_SIZE, BOARD_SIZE];
            for (int x = 0; x < BOARD_SIZE; x++) {
                for (int y = 0; y < BOARD_SIZE; y++) {
                    SetCell(false, x, y, 0);
                }
            }
            rules = rules_in;
            Array.Sort(rules);
            ClearBoard();
        }

        public int GetCell(int[,] board_in, int x, int y)
        {
            if (x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE) {
                return board_in[x, y];
            } else {
                return 0;
            }
        }

        public void SetCell(bool is_board, int x, int y, int value)
        {
            if (x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE) {
                if (is_board == true) board[x, y] = value;
                else board_buffer[x, y] = value;
            } else {
                return;
            }
        }

        public int GetConditionsNo(int[,] board_in, int x, int y)
        {
            int result = 0;
            for (int i = 0; i < MOORE_NEIGHBORHOOD; i++) {
                result += FastPower(CELL_STATE_SIZE, i) * GetCell(board_in, x + i % 3 - 1, y + i / 3 - 1);
            }
            return result;
        }

        public void UpdateBoard()
        {
            Array.Copy(board, board_buffer, BOARD_SIZE * BOARD_SIZE);
            for (int x = 0; x < BOARD_SIZE; x++) {
                for (int y = 0; y < BOARD_SIZE; y++) {
                    int rule = -1;
                    for (int i = 0; i < CELL_STATE_SIZE; i++) {
                        int index = Array.BinarySearch(
                            rules, GetConditionsNo(board, x, y) + (CELL_STATE_SIZE - 1 - i) * FastPower(CELL_STATE_SIZE, MOORE_NEIGHBORHOOD));
                        if (index >= 0) rule = rules[index];
                        if (rule >= 0) break;
                    }
                    if (rule >= 0 && rule % FastPower(CELL_STATE_SIZE, MOORE_NEIGHBORHOOD) != 0) SetCell(false, x, y, rule / FastPower(CELL_STATE_SIZE, MOORE_NEIGHBORHOOD));
                }
            }
            Array.Copy(board_buffer, board, BOARD_SIZE * BOARD_SIZE);
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
            for (int y = 0; y < BOARD_SIZE; y++) {
                for (int x = 0; x < BOARD_SIZE; x++) {
                    Console.Write(GetCell(board, x, y));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void ClearBoard()
        {
            for (int x = 0; x < BOARD_SIZE; x++) {
                for (int y = 0; y < BOARD_SIZE; y++) {
                    SetCell(true, x, y, 0);
                }
            }
        }
    }
}
