using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            CellAutomataGA cellAutomataGA = new CellAutomataGA();
            for(int i = 0; i < 20; i++) {
                cellAutomataGA.NextGeneration();
                Console.Write("Episode:");
                Console.WriteLine(i);
                cellAutomataGA.ShowScores();
                cellAutomataGA.ShowEliteScores();
            }
            Console.ReadLine();
            CellAutomataGame cellAutomataGame = new CellAutomataGame(cellAutomataGA.EliteRule(), cellAutomataGA.rulesForEvalate);
            cellAutomataGame.InitializeBoards();
            for(int i = 0; i < 100; i++) {
                cellAutomataGame.Draw();
                cellAutomataGame.UpdateGameBoard();
            }
            Console.Read();
        }

        static int[] MakeLifeGameRule()
        {
            return null;
        }
    }
}
