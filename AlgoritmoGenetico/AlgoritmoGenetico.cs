using NeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeuralTreinamento.AlgoritmoGenetico
{
  /// <summary>
  /// Algoritmo Genético para treinamento das redes neurais e seleção da melhor rede neural
  /// </summary>
  public class AlgoritmoGenetico
  {
    private readonly Random rand = new Random();

    public NeuralNetworkClass Run(int inputSize, int hiddenSize, int outputSize, int populationSize, int generations, Func<NeuralNetworkClass, double> fitnessFunc, double mutationRate)
    {
      // Inicializar população
      List<NeuralNetworkClass> population = Enumerable.Range(0, populationSize)
          .Select(_ => new NeuralNetworkClass(inputSize, hiddenSize, outputSize)).ToList();

      for (int gen = 0; gen < generations; gen++)
      {
        // Ordena a população pela pontuação
        var scoredPopulation = population
            .Select(network => (network, score: fitnessFunc(network)))
            .OrderByDescending(pair => pair.score)
            .ToList();

        // Log de progresso
        Console.WriteLine($"Geração {gen + 1}: Melhor Score = {scoredPopulation.First().score}");

        // Seleção
        List<NeuralNetworkClass> selected = scoredPopulation
            .Take(populationSize / 2)
            .Select(pair => pair.network)
            .ToList();

        // Cruzamento e Mutação
        population = new List<NeuralNetworkClass>();
        while (population.Count < populationSize)
        {
          var parent1 = selected[rand.Next(selected.Count)];
          var parent2 = selected[rand.Next(selected.Count)];
          var child = Crossover(parent1, parent2);
          Mutate(child, mutationRate);
          population.Add(child);
        }
      }

      // Retornar a melhor rede
      return population.OrderByDescending(fitnessFunc).First();
    }

    /// <summary>
    /// Cruzamnento de indivíduos
    /// </summary>
    /// <param name="parent1"></param>
    /// <param name="parent2"></param>
    /// <returns></returns>
    private NeuralNetworkClass Crossover(NeuralNetworkClass parent1, NeuralNetworkClass parent2)
    {
      NeuralNetworkClass child = new NeuralNetworkClass(parent1.weightsInputHidden.Length, parent1.biasHidden.Length, parent1.biasOutput.Length);

      // Combinar pesos
      child.weightsInputHidden = CombineMatrices(parent1.weightsInputHidden, parent2.weightsInputHidden);
      child.weightsHiddenOutput = CombineMatrices(parent1.weightsHiddenOutput, parent2.weightsHiddenOutput);

      // Combinar vieses
      child.biasHidden = CombineArrays(parent1.biasHidden, parent2.biasHidden);
      child.biasOutput = CombineArrays(parent1.biasOutput, parent2.biasOutput);

      return child;
    }

    /// <summary>
    /// Mutação de indivíduo
    /// </summary>
    /// <param name="network"></param>
    /// <param name="mutationRate"></param>
    private void Mutate(NeuralNetworkClass network, double mutationRate)
    {
      MutateMatrix(network.weightsInputHidden, mutationRate);
      MutateMatrix(network.weightsHiddenOutput, mutationRate);
      MutateArray(network.biasHidden, mutationRate);
      MutateArray(network.biasOutput, mutationRate);
    }

    private void MutateMatrix(double[,] matrix, double mutationRate)
    {
      int rows = matrix.GetLength(0);
      int cols = matrix.GetLength(1);

      for (int i = 0; i < rows; i++)
      {
        for (int j = 0; j < cols; j++)
        {
          if (rand.NextDouble() < mutationRate)
          {
            // Adiciona uma pequena alteração aleatória no intervalo [-0.1, 0.1]
            matrix[i, j] += (rand.NextDouble() * 2 - 1) * 0.1;
          }
        }
      }
    }

    private void MutateArray(double[] array, double mutationRate)
    {
      for (int i = 0; i < array.Length; i++)
        if (rand.NextDouble() < mutationRate)
          array[i] += (rand.NextDouble() * 2 - 1) * 0.1; // Pequena alteração
    }

    private double[,] CombineMatrices(double[,] a, double[,] b)
    {
      // Validar se as dimensões das matrizes são iguais
      if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
        throw new ArgumentException("As matrizes devem ter as mesmas dimensões.");

      int rows = a.GetLength(0);
      int cols = a.GetLength(1);
      double[,] result = new double[rows, cols];

      // Combinar elemento por elemento
      for (int i = 0; i < rows; i++)
      {
        for (int j = 0; j < cols; j++)
        {
          result[i, j] = a[i, j] + b[i, j];
        }
      }

      return result;
    }

    private double[] CombineArrays(double[] a, double[] b)
    {
      return a.Zip(b, (x, y) => rand.NextDouble() < 0.5 ? x : y).ToArray();
    }
  }
}
