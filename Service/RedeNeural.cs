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
      neuralNetworkClass = new NeuralNetworkClass(inputSize, hiddenSize, outputSize);
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
      neuralNetworkClass = new NeuralNetworkClass(inputSize, hiddenSize, outputSize);

      // Treinamento da rede
      int epochs = 1000;
      for (int i = 0; i < epochs; i++)
      {
        for (int j = 0; j < inputs.Length; j++)
        {
          neuralNetworkClass.Train(inputs[j], outputs[j], 0.5);          
        }
      }
    }

    public double[] Compute(double[] inputs)
    {
      var results = neuralNetworkClass.Forward(inputs);

      return results;
    }    
  }

}
