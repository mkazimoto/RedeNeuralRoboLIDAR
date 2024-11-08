using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeuralTreinamento.Model
{
  /// <summary>
  /// Registro de entrada do sensor e movimento executado
  /// </summary>
  public class Registro
  {
    /// <summary>
    /// Valores do sensor LIDAR
    /// </summary>
    public double[] inputs { get; set; }

    /// <summary>
    /// Movimento executado pelo robô
    /// </summary>
    public double[] outputs { get; set; }

    public Registro() 
    { 
    }
  }
}
