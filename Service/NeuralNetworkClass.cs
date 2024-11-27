using Newtonsoft.Json;
using System;
using System.IO;

namespace NeuralNetwork
{
  public class NeuralNetworkClass
  {
    private int inputSize;
    private int hiddenSize;
    private int outputSize;

    private double[,] weightsInputHidden;
    private double[,] weightsHiddenOutput;
    private double[] biasHidden;
    private double[] biasOutput;

    public NeuralNetworkClass(int inputSize, int hiddenSize, int outputSize)
    {
      this.inputSize = inputSize;
      this.hiddenSize = hiddenSize;
      this.outputSize = outputSize;

      // Inicializa pesos e bias com valores aleatórios
      Random rand = new Random();
      weightsInputHidden = InitializeMatrix(inputSize, hiddenSize, rand);
      weightsHiddenOutput = InitializeMatrix(hiddenSize, outputSize, rand);
      biasHidden = InitializeVector(hiddenSize, rand);
      biasOutput = InitializeVector(outputSize, rand);
    }

    // Método para inicializar matriz
    private double[,] InitializeMatrix(int rows, int cols, Random rand)
    {
      double[,] matrix = new double[rows, cols];
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
          matrix[i, j] = rand.NextDouble() * 2 - 1; // Valores entre -1 e 1
      return matrix;
    }

    // Método para inicializar vetor
    private double[] InitializeVector(int size, Random rand)
    {
      double[] vector = new double[size];
      for (int i = 0; i < size; i++)
        vector[i] = rand.NextDouble() * 2 - 1;
      return vector;
    }

    // Função de ativação sigmoid
    private double Sigmoid(double x) => 1 / (1 + Math.Exp(-x));

    // Derivada da função sigmoid
    private double SigmoidDerivative(double x) => x * (1 - x);

    // Forward Pass
    public double[] Forward(double[] input)
    {
      // Calcula a ativação da camada oculta
      double[] hiddenLayer = new double[hiddenSize];
      for (int j = 0; j < hiddenSize; j++)
      {
        double sum = biasHidden[j];
        for (int i = 0; i < inputSize; i++)
          sum += input[i] * weightsInputHidden[j, i];
        hiddenLayer[j] = Sigmoid(sum);
      }

      // Calcula a saída
      double[] outputLayer = new double[outputSize];
      for (int k = 0; k < outputSize; k++)
      {
        double sum = biasOutput[k];
        for (int j = 0; j < hiddenSize; j++)
          sum += hiddenLayer[j] * weightsHiddenOutput[k, j];
        outputLayer[k] = sum; // Ativação linear
      }

      return outputLayer;
    }

    // Método de treinamento com gradiente descendente
    public void Train(double[] input, double[] target, double learningRate)
    {
      // Forward Pass
      double[] hiddenLayer = new double[hiddenSize];
      for (int j = 0; j < hiddenSize; j++)
      {
        double sum = biasHidden[j];
        for (int i = 0; i < inputSize; i++)
          sum += input[i] * weightsInputHidden[i, j];
        hiddenLayer[j] = Sigmoid(sum);
      }

      double[] outputLayer = new double[outputSize];
      for (int k = 0; k < outputSize; k++)
      {
        double sum = biasOutput[k];
        for (int j = 0; j < hiddenSize; j++)
          sum += hiddenLayer[j] * weightsHiddenOutput[j, k];
        outputLayer[k] = sum;
      }

      // Backpropagation
      double[] outputError = new double[outputSize];
      for (int k = 0; k < outputSize; k++)
        outputError[k] = target[k] - outputLayer[k];

      double[] hiddenError = new double[hiddenSize];
      for (int j = 0; j < hiddenSize; j++)
      {
        double sum = 0;
        for (int k = 0; k < outputSize; k++)
          sum += outputError[k] * weightsHiddenOutput[j, k];
        hiddenError[j] = sum * SigmoidDerivative(hiddenLayer[j]);
      }

      // Atualiza pesos e bias da saída para camada oculta
      for (int k = 0; k < outputSize; k++)
      {
        biasOutput[k] += learningRate * outputError[k];
        for (int j = 0; j < hiddenSize; j++)
          weightsHiddenOutput[j, k] += learningRate * outputError[k] * hiddenLayer[j];
      }

      // Atualiza pesos e bias da camada oculta para entrada
      for (int j = 0; j < hiddenSize; j++)
      {
        biasHidden[j] += learningRate * hiddenError[j];
        for (int i = 0; i < inputSize; i++)
          weightsInputHidden[i, j] += learningRate * hiddenError[j] * input[i];
      }
    }

    // Salvar a rede neural em JSON
    public void Save(string filePath)
    {
      var data = new
      {
        inputSize,
        hiddenSize,
        outputSize,
        weightsInputHidden,
        weightsHiddenOutput,
        biasHidden,
        biasOutput
      };

      string json = JsonConvert.SerializeObject(data, Formatting.Indented);
      File.WriteAllText(filePath, json);
    }

    // Carregar a rede neural de um arquivo JSON
    public static NeuralNetworkClass Load(string filePath)
    {
      string json = File.ReadAllText(filePath);
      var data = JsonConvert.DeserializeObject<dynamic>(json);

      int inputSize = data.inputSize;
      int hiddenSize = data.hiddenSize;
      int outputSize = data.outputSize;

      var nn = new NeuralNetworkClass(inputSize, hiddenSize, outputSize);

      nn.weightsInputHidden = DeserializeMatrix(data.weightsInputHidden.ToString());
      nn.weightsHiddenOutput = DeserializeMatrix(data.weightsHiddenOutput.ToString());
      nn.biasHidden = JsonConvert.DeserializeObject<double[]>(data.biasHidden.ToString());
      nn.biasOutput = JsonConvert.DeserializeObject<double[]>(data.biasOutput.ToString());

      return nn;
    }

    private static double[,] DeserializeMatrix(string json)
    {
      double[][] array = JsonConvert.DeserializeObject<double[][]>(json);
      int rows = array.Length;
      int cols = array[0].Length;
      double[,] matrix = new double[rows, cols];
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
          matrix[i, j] = array[i][j];
      return matrix;
    }
  }

}
