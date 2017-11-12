using System;
using System.Collections;
using System.Collections.Generic;


namespace WindowsFormsApplication5
{
   

    public class Genome
    {
        public Genome()
        {
           
        }
        public Genome(int length)
        {
            m_length = length;
            m_genes = new double[length];
            CreateGenes();
        }
        public Genome(int length, bool createGenes)
        {
            m_length = length;
            m_genes = new double[length];
            if (createGenes)
                CreateGenes();
        }

        public Genome(ref double[] genes)
        {
            m_length = genes.Length;
            m_genes = new double[m_length];
            Array.Copy(genes, m_genes, m_length);
        }

        public Genome DeepCopy()
        {
            Genome g = new Genome(m_length, false);
            Array.Copy(m_genes, g.m_genes, m_length);
            return g;
        }

        private void CreateGenes()
        {
            for (int i = 0; i < m_genes.Length; i++)
                m_genes[i] = m_random.NextDouble();
        }

        public void Crossover(ref Genome genome2, out Genome child1, out Genome child2)
        {
            int pos = (int)(m_random.NextDouble() * (double)m_length);
            child1 = new Genome(m_length, false);
            child2 = new Genome(m_length, false);
            for (int i = 0; i < m_length; i++)
            {
                if (i < pos)
                {
                    child1.m_genes[i] = m_genes[i];
                    child2.m_genes[i] = genome2.m_genes[i];
                }
                else
                {
                    child1.m_genes[i] = genome2.m_genes[i];
                    child2.m_genes[i] = m_genes[i];
                }
            }
        }

        public void Mutate()
        {
            for (int pos = 0; pos < m_length; pos++)
            {
                if (m_random.NextDouble() < m_mutationRate)
                    m_genes[pos] = (m_genes[pos] + m_random.NextDouble()) / 2.0;
            }
        }

        public double[] Genes()
        {
            return m_genes;
        }

        public void Output()
        {
            foreach (double valeur in m_genes)
            {
                System.Console.WriteLine("{0:F4}", valeur);
            }
            System.Console.Write("------\n");
        }

        public void GetValues(ref double[] values)
        {
            for (int i = 0; i < m_length; i++)
                values[i] = m_genes[i];
        }

        public double[] m_genes;
        private int m_length;
        private double m_fitness;
        static Random m_random = new Random();

        private static double m_mutationRate;

        public double Fitness
        {
            get
            {
                return m_fitness;
            }
            set
            {
                m_fitness = value;
            }
        }

        public static double MutationRate
        {
            get
            {
                return m_mutationRate;
            }
            set
            {
                m_mutationRate = value;
            }
        }

        public int Length
        {
            get
            {
                return m_length;
            }
        }
    }
}
