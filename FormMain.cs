using Accord.Neuro.Learning;
using Accord.Neuro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RedeNeuralTreinamento.Model;
using RedeNeuralTreinamento.Service;
using System.Xml;
using System.Threading;
using System.IO;

namespace RedeNeuralTreinamento
{
  public enum Status { Parado, Gravando, Treinamento, ExecutandoRedeNeural };

  public partial class FormMain : Form
  {
    public Status status { get; set; } = Status.Parado;

    public Robo robo { get; set; }

    public Mapa mapa { get; set; }

    public List<Registro> listRegistros { get; set; } = new List<Registro>();

    public int contador { get; set; }

    public int contadorParado { get; set; }

    public RedeNeural redeNeural { get; set; }

    public FormMain()
    {
      InitializeComponent();

      var bitmap = (Bitmap)pictureBox1.Image;

      mapa = new Mapa()
      {
        Width = bitmap.Width,
        Height = bitmap.Height,
        Bitmap = bitmap,
      };

      robo = new Robo(mapa)
      {
        X = 100,
        Y = 100,
        Diameter = 60,
        Rotation = Math.PI / 2,
        Speed = 2,
        NumberLasers = 32,
        DistanceMax = 2000,
      };

      redeNeural = new RedeNeural()
      {
        inputSize = robo.NumberLasers, // Distancia dos lasers
        hiddenSize = 8,
        outputSize = 3, // Up, Right, Left
      };

      redeNeural.Init();

      // Carrega a rede neural do arquivo
      if (File.Exists(@"RedeNeural\RedeNeural.dat"))
      {
        CarregarArquivoRedeNeural();
      }
    }


    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
      robo.Draw(e.Graphics);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      switch (status)
      {
        case Status.Gravando:
          GravarRegistro();
          break;
        case Status.ExecutandoRedeNeural:
          ExecutarRedeNeural();
          break;
      }

      robo.Run();

      pictureBox1.Refresh();
    }

    private void ExecutarRedeNeural()
    {
      // Pega as distancias do LIDAR
      var inputs = robo.GetInputs();

      // Executa rede neural
      var outputs = redeNeural.Compute(inputs);

      double maxValue = 0;
      int index = 0;
      for (int i = 0; i < outputs.Length; i++)
      {
        if (outputs[i] > maxValue)
        {
          maxValue = outputs[i];
          index = i;
        }
      }

      // O robo está parado ?
      if (robo.LastX == robo.X &&
          robo.LastY == robo.Y &&
          robo.lastKey != robo.key)
      {
        contadorParado++;
        if (contadorParado > 1)
        {
          contadorParado = 0;
          index = 0;
        }
      }

      lblSaidaUp.Text = outputs[0].ToString("F4");
      lblSaidaRight.Text = outputs[1].ToString("F4");
      lblSaidaLeft.Text = outputs[2].ToString("F4");

      robo.lastKey = robo.key;
      switch (index)
      {
        case 0:
          robo.key = Keys.Up;
          break;
        case 1:
          robo.key = Keys.Right;
          break;
        case 2:
          robo.key = Keys.Left;
          break;
        case 3:
          robo.key = Keys.Down;
          break;
      }

      // Pisca o status
      if (contador > 10)
      {
        if (pbStatus.BackColor == Color.Lime)
          pbStatus.BackColor = Color.Black;
        else
          pbStatus.BackColor = Color.Lime;

        contador = 0;
      }
      contador++;

    }

    private void GravarRegistro()
    {
      // Pisca o status
      if (contador > 10)
      {
        if (pbStatus.BackColor == Color.Red)
          pbStatus.BackColor = Color.Black;
        else
          pbStatus.BackColor = Color.Red;

        contador = 0;
      }
      contador++;

      if (robo.key != Keys.Pause)
      {
        // Registra os dados do sensor LIDAR e o movimento do robô
        listRegistros.Add(new Registro()
        {
          inputs = robo.GetInputs(),
          outputs = new double[]
          {
            (robo.key == Keys.Up) ? 1 : 0,
            (robo.key == Keys.Right) ? 1 : 0,
            (robo.key == Keys.Left) ? 1 : 0
          }
        });
      }
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Up ||
          e.KeyCode == Keys.Down ||
          e.KeyCode == Keys.Left ||
          e.KeyCode == Keys.Right 
          )
      {
        robo.key = e.KeyCode;
        e.Handled = true;
      }

      if (e.KeyCode == Keys.D1)
      {
        listRegistros.Clear();
        status = Status.Gravando;
        pbStatus.BackColor = Color.Red;
        lblStatus.Text = "Gravando... utilize as setas para controlar o robô...";
      }

      if (e.KeyCode == Keys.D2)
      {
        status = Status.Treinamento;
        pbStatus.BackColor = Color.Yellow;
        lblStatus.Text = "Treinando rede neural...";

        Application.DoEvents();

        redeNeural.Treinamento(listRegistros.Select(p => p.inputs).ToArray(), 
                               listRegistros.Select(p => p.outputs).ToArray());


        status = Status.ExecutandoRedeNeural;
        pbStatus.BackColor = Color.Green;
        lblStatus.Text = "Executando rede neural...";
      }


      if (e.KeyCode == Keys.D3)
      {
        CarregarArquivoRedeNeural();
      }

      if (e.KeyCode == Keys.D4)
      {
        if (timer1.Interval - 10 > 0) 
          timer1.Interval -= 10;
      }

      if (e.KeyCode == Keys.D5)
      {
        if (timer1.Interval + 10 < 1000)
          timer1.Interval += 10;
      }
    }

    private void CarregarArquivoRedeNeural()
    {
      redeNeural = new RedeNeural();
      redeNeural.Load(@"RedeNeural\RedeNeural.dat");

      status = Status.ExecutandoRedeNeural;
      pbStatus.BackColor = Color.Green;
      lblStatus.Text = "Executando rede neural...";      
    }

    private void Form1_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Up ||
          e.KeyCode == Keys.Down ||
          e.KeyCode == Keys.Left ||
          e.KeyCode == Keys.Right
          )
      {
        robo.key = Keys.Pause;
        e.Handled = true;
      }
    }
  }
}


