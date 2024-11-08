﻿using RedeNeuralTreinamento.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedeNeuralTreinamento.Model
{
  /// <summary>
  /// Robo com sensor LIDAR
  /// </summary>
  public class Robo
  {
    public Mapa mapa { get; set; }

    public double X { get; set; }

    public double Y { get; set; }


    public double LastX { get; set; }

    public double LastY { get; set; }

    /// <summary>
    /// Diametro do robo
    /// </summary>
    public int Diameter { get; set; }

    /// <summary>
    /// Angulo de rotação em radianos
    /// </summary>
    public double Rotation { get; set; }

    /// <summary>
    /// Velocidade
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Numero de lasers do LIDAR
    /// </summary>
    public int NumberLasers
    {
      get { return _numberLasers; }
      set
      {
        LidarColision.Clear();
        _numberLasers = value;

        double angle = -90;
        for (int i = 0; i < _numberLasers; i++) 
        {
          LidarColision.Add(new RaioLIDAR()
          {
            Angle = (int)angle,
            AngleRadians = angle * Math.PI / 180f,
          });

          angle += (double)180 / ((double)_numberLasers - (double)1);
        }
      }
    }
    private int _numberLasers;

    public List<RaioLIDAR> LidarColision { get; set; } = new List<RaioLIDAR>();

    /// <summary>
    /// Distancia máxima do laser
    /// </summary>
    public double DistanceMax { get; set; }

    public Keys key { get; set; }
    public Keys lastKey { get; set; }

    public Robo(Mapa mapa)
    {
      this.mapa = mapa; 
    }

    public void Run()
    {
      LastX = X;
      LastY = Y;

      // Movimenta o robô
      switch (key)
      {
        case Keys.Left:
          Rotation -= Math.PI / 360;
          break;
        case Keys.Right:
          Rotation += Math.PI / 360;
          break;
        case Keys.Down:
          X -= Math.Cos(Rotation) * Speed;
          Y -= Math.Sin(Rotation) * Speed;
          break;
        case Keys.Up:
          X += Math.Cos(Rotation) * Speed;
          Y += Math.Sin(Rotation) * Speed;
          break;
      }

      // Verifica a colisão do sensor LIDAR
      foreach (var ray in LidarColision)
      {
        var pontoColisao = SensorLIDAR.RayCast((int)X, 
                                               (int)Y, 
                                               (int)(X + Math.Cos(Rotation + ray.AngleRadians) * DistanceMax), 
                                               (int)(Y + Math.Sin(Rotation + ray.AngleRadians) * DistanceMax), mapa);

        ray.Destiny = pontoColisao;

        // Calcula a distancia 
        ray.Distance = Math.Sqrt(Math.Pow(Math.Abs(ray.Destiny.X - X), 2) + Math.Pow(Math.Abs(ray.Destiny.Y - Y), 2));      
      }

    }

    public void Draw(System.Drawing.Graphics g)
    {
      // Desenha a colisão sensor Lidar
      foreach (var pontoColisao in LidarColision)
      {
        g.DrawLine(Pens.Gray, (int)X, (int)Y, pontoColisao.Destiny.X, pontoColisao.Destiny.Y);
        g.DrawEllipse(Pens.Red, pontoColisao.Destiny.X - 10, pontoColisao.Destiny.Y - 10, 20, 20);
      }

      var raio = Diameter / 2;

      // Desenha robo
      g.FillEllipse(Brushes.Blue, (int)X - raio, (int)Y - raio, Diameter, Diameter);
      g.DrawEllipse(Pens.Black, (int)X - raio, (int)Y - raio, Diameter, Diameter);
      g.DrawLine(Pens.Black, (int)X, (int)Y, (int)(X + Math.Cos(Rotation) * raio), (int)(Y + Math.Sin(Rotation) * raio));  
    }

    public double[] GetInputs()
    {
      return LidarColision.Select(p => p.Distance / 2000).Select(p => (p > 1.0) ? 1.0 : p).ToArray();
    }
  }
}