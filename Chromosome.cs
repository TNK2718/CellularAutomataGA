using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace AboutGeneticAlgorithm
{
    public class IntArrayChromosomes : ChromosomesBase<int>
    {
        private int chrmosomesize;

        public IntArrayChromosomes(int population, int _chromosomesize, int chromosomeMaxNumber)
        {
            chrmosomesize = _chromosomesize;
            chromosomes = new List<Chromosome<int>>();
            for (int i = 0; i < population; i++) {
                chromosomes.Add(new Chromosome<int>(_chromosomesize));
                for (int j = 0; j < _chromosomesize; j++) {
                    chromosomes[i].value[j] = 0;
                }
            }
            SetUp(chromosomeMaxNumber);
        }

        public override void SetUp(int chromosomeMaxNumber)
        {
            var random = new System.Random();
            for (int i = 0; i < chromosomes.Count(); i++) {
                for (int j = 0; j < chromosomes[i].value.Length; j++) {
                    chromosomes[i].value[j] = random.Next(0, chromosomeMaxNumber);
                }
            }
        }

        public override int ReadChromosome(int individual, int index)
        {
            if (individual >= 0 && individual < chromosomes.Count() && index >= 0 && index < chromosomes[individual].value.Length) {
                return chromosomes[individual].value[index];
            } else {
                return -1;
            }
        }

        public int[] ReadChromosomeAsRule(int individual)
        {
            int[] returnvalue = new int[chromosomes[individual].value.Length];
            for (int i = 0; i < chromosomes[individual].value.Length; i++) {
                returnvalue[i] = chromosomes[individual].value[i];
            }
            return returnvalue;
        }

        public void SetChromosomeAsRule(int individual, int[] rule)
        {
            for (int i = 0; i < chromosomes[individual].value.Length; i++) {
                SetChromosome(individual, i, rule[i]);
            }
        }

        public override void SetChromosome(int individual, int index, int value)
        {
            if (individual >= 0 && individual < chromosomes.Count() && index >= 0 && index < chromosomes[individual].value.Length) {
                chromosomes[individual].value[index] = value;
            } else {

            }
        }

        public override void AddChromosome(int[] _value, double _score)
        {
            Chromosome<int> chromosome = new Chromosome<int>(chrmosomesize);
            chromosome.value = _value;
            chromosome.score = _score;
            chromosomes.Add(chromosome);
        }

        public override void RemoveChromosome(int individual)
        {
            chromosomes.RemoveAt(individual);
        }

        public void CrossOverChromosomes(int index1, int index2, int point1, int point2)
        {
            int tmppoint = point1;
            if (point1 > point2) {
                point1 = point2;
                point2 = tmppoint;
            }
            var chromosome1 = chromosomes[index1];
            var chromosome2 = chromosomes[index2];
            int[] tmp = new int[point2 - point1 + 1];
            for (int i = 0; i < tmp.Length; i++) {
                tmp[i] = ReadChromosome(index1, i + point1);
            }
            for (int i = 0; i < tmp.Length; i++) {
                SetChromosome(index1, i + point1, ReadChromosome(index2, i + point1));
            }
            for (int i = 0; i < tmp.Length; i++) {
                SetChromosome(index2, i + point1, tmp[i]);
            }
            chromosomes.Add(chromosome1);
            chromosomes.Add(chromosome2);
        }

        public void SortChromosomes()
        {
            chromosomes.Sort((a, b) => Math.Sign(b.score - a.score));
        }

        public int GetPopulation()
        {
            return chromosomes.Count();
        }
    }
}

