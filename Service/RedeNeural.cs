using Accord.Neuro.Learning;
using Accord.Neuro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using System.CodeDom.Compiler;
using RedeNeuralTreinamento.Model;

namespace RedeNeuralTreinamento.Service
{
  public class RedeNeural
  {
    public int inputSize { get; set; }

    public int hiddenSize { get; set; }

    public int outputSize { get; set; }

    public ActivationNetwork network { get; set; }

    public BackPropagationLearning teacher { get; set; }

    public RedeNeural() 
    { 
    }

    public void Load(string filename)
    {
      network = (ActivationNetwork)ActivationNetwork.Load(filename);
    }

    public void Init()
    {
      // Criação da rede neural com 1 camada oculta
      network = new ActivationNetwork(
          function: new SigmoidFunction() { Alpha = 2 },  // Função de ativação Sigmóide
          inputsCount: inputSize,
          neuronsCount: new int[] { hiddenSize, outputSize }
      );

    }

    public void Treinamento(double[][] inputs, double[][] outputs)
    {
      // Inicialização dos pesos da rede
      new NguyenWidrow(network).Randomize();

      foreach (var layer in network.Layers)
        foreach (var neuronio in layer.Neurons)
        {
          for (int i = 0; i < neuronio.Weights.Length; i++)
          {
            neuronio.Weights[i] = 1.0;
          }
        }

      // Configuração do algoritmo de aprendizado
      teacher = new BackPropagationLearning(network)
      {
        LearningRate = 0.1,

        //O valor determina a porção da atualização do peso anterior a ser usada na iteração atual.
        //Os valores de atualização do peso são calculados em cada iteração dependendo do erro do neurônio.
        //O momentum especifica a quantidade de atualização a ser usada da iteração anterior e
        //a quantidade de atualização a ser usada da iteração atual.
        //Se o valor for igual a 0, 1, por exemplo, então 0, 1 porção da atualização anterior
        //e 0, 9 porção da atualização atual são usadas para atualizar o valor do peso.
        Momentum = 0.5
      };

      // Treinamento da rede
      int epochs = 1000;
      for (int i = 0; i < epochs; i++)
      {
        double error = teacher.RunEpoch(inputs, outputs);
        if (i % 100 == 0)
          Console.WriteLine($"Epoch {i}, Erro: {error:F4}");
      }
    }

    public double[] Compute(double[] inputs)
    {
      var results = network.Compute(inputs);

      return results;
    }

  }
}
