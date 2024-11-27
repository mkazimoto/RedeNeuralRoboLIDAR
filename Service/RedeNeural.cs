using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using System.CodeDom.Compiler;
using RedeNeuralTreinamento.Model;
using Newtonsoft.Json;
using System.IO;
using NeuralNetwork;

namespace RedeNeuralTreinamento.Service
{
  public class RedeNeural
  {
    public int inputSize { get; set; }

    public int hiddenSize { get; set; }

    public int outputSize { get; set; }

    public NeuralNetworkClass neuralNetworkClass { get; set; }

    public RedeNeural() 
    { 
    }

    public void Load(string filename)
    {
      neuralNetworkClass = NeuralNetworkClass.Load(filename);
    }

    public void Save(string filename)
    {
      neuralNetworkClass.Save(filename);
    }

    public void Treinamento(double[][] inputs, double[][] outputs)
    {
      //// Criação da rede neural com 1 camada oculta
      //network = new ActivationNetwork(
      //    function: new SigmoidFunction() { Alpha = 2 },  // Função de ativação Sigmóide
      //    inputsCount: inputs[0].Length,
      //    neuronsCount: new int[] { 10, outputs[0].Length }
      //);

      //// Inicialização dos pesos da rede
      //new NguyenWidrow(network).Randomize();

      //// Configuração do algoritmo de aprendizado
      //teacher = new BackPropagationLearning(network)
      //{
      //  // Taxa de aprendizado
      //  LearningRate = 0.9,

      //  //O valor determina a porção da atualização do peso anterior a ser usada na iteração atual.
      //  //Os valores de atualização do peso são calculados em cada iteração dependendo do erro do neurônio.
      //  //O momentum especifica a quantidade de atualização a ser usada da iteração anterior e
      //  //a quantidade de atualização a ser usada da iteração atual.
      //  //Se o valor for igual a 0, 1, por exemplo, então 0, 1 porção da atualização anterior
      //  //e 0, 9 porção da atualização atual são usadas para atualizar o valor do peso.
      //  Momentum = 0.1
      //};

      //// Treinamento da rede
      //int epochs = 1000;
      //for (int i = 0; i < epochs; i++)
      //{
      //  double error = teacher.RunEpoch(inputs, outputs);
      //  if (i % 100 == 0)
      //    Console.WriteLine($"Epoch {i}, Erro: {error:F4}");
      //}
    }

    public double[] Compute(double[] inputs)
    {
      var results = neuralNetworkClass.Forward(inputs);

      return results;
    }    

  }

  // Classe para representar o JSON
  public class NeuralNetworkJson
  {
    public double[][] weightsInputHidden { get; set; }
    public double[][] weightsHiddenOutput { get; set; }
    public double[] biasHidden { get; set; }
    public double[] biasOutput { get; set; }
  }
}
