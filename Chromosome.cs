using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class IntArrayChromosomes : ChromosomesBase<int>
    {
        public IntArrayChromosomes(int population, int chromosomesize, int chromosomeMaxNumber)
        {
            chromosomes = new int[population, chromosomesize];
            Initialize(chromosomeMaxNumber);
        }

        public override void Initialize(int chromosomeMaxNumber)
        {
            Random random = new Random();
            for(int i = 0; i < chromosomes.GetLength(0); i++) {
                for(int j = 0; j < chromosomes.GetLength(1); j++) {
                    chromosomes[i, j] = random.Next(0, chromosomeMaxNumber);
                }
            }
        }

        public override int ReadChromosome(int individual, int index)
        {
            if(individual >= 0 && individual < chromosomes.GetLength(0) && index >= 0 && index < chromosomes.GetLength(1)) {
                return chromosomes[individual, index];
            } else {
                return -1;
            }
        }

        public int[] ReadChromosomeAsRule(int individual)
        {
            int[] returnvalue = new int[chromosomes.GetLength(1)];
            for(int i = 0; i < chromosomes.GetLength(1); i++) {
                returnvalue[i] = chromosomes[individual, i];
            }
            return returnvalue;
        }

        public void SetChromosomeAsRule(int individual, int[] rule)
        {
            for(int i = 0; i < chromosomes.GetLength(1); i++) {
                SetChromosome(individual, i, rule[i]);
            }
        }

        public override void SetChromosome(int individual, int index, int element)
        {
            if (individual >= 0 && individual < chromosomes.GetLength(0) && index >= 0 && index < chromosomes.GetLength(1)) {
                chromosomes[individual, index] = element;
            } else {
                
            }
        }

        public void TransChromosome(int chromosome1,int chromosome2,int point1, int point2)
        {
            int[] tmp = new int[point2 - point1 + 1];
            for(int i = 0; i < tmp.Length; i++) {
                tmp[i] = ReadChromosome(chromosome1, i + point1);
            }
            for (int i = 0; i < tmp.Length; i++) {
                SetChromosome(chromosome1, i + point1, ReadChromosome(chromosome2, i + point1));
            }
            for (int i = 0; i < tmp.Length; i++) {
                SetChromosome(chromosome2, i + point1, tmp[i]);
            }
        }
    }

    public abstract class ChromosomesBase<T>
    {
        protected T[,] chromosomes;
        public abstract void Initialize(int chromosomeMaxNumber);
        public abstract void SetChromosome(int individual, int index, T element);
        public abstract T ReadChromosome(int individual, int index);
    }
}
