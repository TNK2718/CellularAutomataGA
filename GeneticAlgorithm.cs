using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class CellAutomataGA
    {
        private const int POPULATION = 10;
        private const int ELITEPOPULATION = 2;
        private const int CHROMOSOMESIZE = 200;
        private const int MOORENEIGHBORHOOD = 9;
        private const int CELLSTATESIZE = 2;
        private const double CROSSOVERRATE = 0.2;
        private const double MUTATIONRATE = 0.01;
        private const int EPISODESFOREVALATION = 100;
        private int chromosomeMaxNumber;

        private IntArrayChromosomes intArrayChromosomes;
        private double[] scores;
        private List<int> elitelist;

        public int[] rulesForEvalate;

        public CellAutomataGA()
        {
            chromosomeMaxNumber = FastPower(CELLSTATESIZE, MOORENEIGHBORHOOD + 1) - 1;
            intArrayChromosomes = new IntArrayChromosomes(POPULATION, CHROMOSOMESIZE, chromosomeMaxNumber);
            scores = new double[POPULATION];
            elitelist = new List<int>();

            rulesForEvalate = new int[2]{ ConvertToConditionNo(new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0 })
                , ConvertToConditionNo(new int[] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 }) };
        }

        public void Initialize()
        {
            intArrayChromosomes.Initialize(chromosomeMaxNumber);
        }

        public void Crossover(IntArrayChromosomes intChromosomes, double crossoverrate, List<int> _elitelist)
        {
            Random random = new Random();
            for(int i = 0; i < POPULATION - 1; i++) {
                double rnddbl = random.NextDouble();
                int rndindex = random.Next(i, POPULATION - 1);
                if (crossoverrate <= rnddbl && !_elitelist.Contains<int>(i) && !_elitelist.Contains<int>(rndindex)) {
                    int point1 = random.Next(0, CHROMOSOMESIZE);
                    int point2 = random.Next(0, CHROMOSOMESIZE);
                    if (point1 <= point2) intArrayChromosomes.TransChromosome(i, rndindex, point1, point2);
                    else intArrayChromosomes.TransChromosome(i, rndindex, point2, point1);
                }
            }
        }

        public void Evaluate()
        {
            for(int i = 0; i < POPULATION; i++) {
                CellAutomataGame cellAutomataGame = new CellAutomataGame(intArrayChromosomes.ReadChromosomeAsRule(i), rulesForEvalate);
                cellAutomataGame.InitializeBoards();
                for(int n = 0; n < EPISODESFOREVALATION; n++) {
                    cellAutomataGame.UpdateGameBoard();
                }
                scores[i] = EvaluateFunction(cellAutomataGame);
            }
        }

        private double EvaluateFunction(CellAutomataGame cellAutomataGame)
        {
            double score = 0;
            for(int x = 0; x < cellAutomataGame.BOARDSIZE; x++) {
                for(int y = 0; y < cellAutomataGame.BOARDSIZE; y++) {
                    score += cellAutomataGame.board2.GetCell(cellAutomataGame.board2.board, x, y);
                }
            }
            score = (cellAutomataGame.BOARDSIZE * cellAutomataGame.BOARDSIZE - score) / (cellAutomataGame.BOARDSIZE * cellAutomataGame.BOARDSIZE);
            score = score * score;
            return score;
        }

        public void Mutation(IntArrayChromosomes intChromosomes, double mutationrate, List<int> _elitelist, int chromosomeMaxNumber)
        {
            Random random = new Random();
            for(int i = 0; i < POPULATION; i++) {
                double rndnum = random.NextDouble();
                if(rndnum <= mutationrate && !_elitelist.Contains<int>(i)) {
                    intChromosomes.SetChromosome(i, random.Next(0, CHROMOSOMESIZE), random.Next(0, chromosomeMaxNumber));
                }
            }
        }

        public void SelectElite(int elitepopulation)
        {
            elitelist.Clear();

            double[] tmpscores = new double[POPULATION];//
            scores.CopyTo(tmpscores, 0);//
            Array.Sort(tmpscores);//sort
            Array.Reverse(tmpscores);

            double elitescore = tmpscores[elitepopulation - 1];
            for(int i = 0; i < POPULATION; i++) {
                if (scores[i] >= elitescore) elitelist.Add(i);
            }
        }

        public void NextGeneration()
        {
            Evaluate();
            SelectElite(ELITEPOPULATION);
            Crossover(intArrayChromosomes, CROSSOVERRATE, elitelist);
            Mutation(intArrayChromosomes, MUTATIONRATE, elitelist, chromosomeMaxNumber);
        }

        public int FastPower(int _base, int exponent)
        {
            if (exponent == 0) return 1;
            else if (exponent == 1) return _base;
            else if (exponent % 2 == 0) {
                int tmp = FastPower(_base, exponent / 2);
                return tmp * tmp;
            } else {
                int tmp = FastPower(_base, (exponent - 1) / 2);
                return tmp * tmp * _base;
            }
        }

        public int ConvertToConditionNo(int[] input)
        {
            int returnvalue = 0;
            for(int i = 0; i <= MOORENEIGHBORHOOD; i++) {
                returnvalue += FastPower(CELLSTATESIZE, i) * input[MOORENEIGHBORHOOD - i];
            }
            return returnvalue;
        }

        public void ShowScores()
        {
            Console.WriteLine("Scores:");
            for (int i = 0; i < POPULATION; i++) {
                Console.WriteLine(scores[i]);
            }
        }

        public void ShowEliteScores()
        {
            Console.WriteLine("EliteScores");
            foreach(int score in elitelist) {
                Console.WriteLine(score);
            }
        }

        public int[] EliteRule()
        {
            return intArrayChromosomes.ReadChromosomeAsRule(elitelist[0]);
        }
    }
}
