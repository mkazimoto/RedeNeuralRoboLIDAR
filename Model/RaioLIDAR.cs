using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeuralTreinamento.Model
{
  /// <summary>
  /// Raio do LIDAR
  /// </summary>
  public class RaioLIDAR
  {
    /// <summary>
    /// Angulo em graus do rotação
    /// </summary>
    public int Angle { get; set; }

    /// <summary>
    /// Angulo em radianos
    /// </summary>
    public double AngleRadians { get; set; }

    public Point Destiny { get; set; }

    public double Distance { get; set; }

    public RaioLIDAR() { }

    public override string ToString()
    {
      return $"Angle: {Angle} - Distance: {Distance}";
    }
  }
}
