using RedeNeuralTreinamento.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeuralTreinamento.Service
{
  public class SensorLIDAR
  {
    /// <summary>
    /// Lança raio e retorna ponto de colisão via algoritmo de Breseham
    /// </summary>
    public static System.Drawing.Point RayCast(int x1, int y1, int x2, int y2, Mapa mapa)
    {
      int dx = Math.Abs(x2 - x1);
      int dy = Math.Abs(y2 - y1);
      int sx = (x1 < x2) ? 1 : -1;
      int sy = (y1 < y2) ? 1 : -1;
      int err = dx - dy;

      var source = new System.Drawing.Point(x1, y1);

      while (true)
      {
        if (x1 < 0 || 
            y1 < 0 ||
            x1 >= mapa.Width ||
            y1 >= mapa.Height)
          return source;

        var pixel = mapa.Bitmap.GetPixel(x1, y1);

        // Houve colisão no pixel ?
        if (pixel.R != 255 ||
            pixel.G != 255 ||
            pixel.B != 255 
            )
        {
          return new Point(x1, y1);
        }

        if (x1 == x2 &&
            y1 == y2)
          return source;

        int e2 = err * 2;

        if (e2 > -dy)
        {
          err -= dy;
          x1 += sx;
        }

        if (e2 < dx)
        {
          err += dx;
          y1 += sy;
        }
      }
    }
  }
}
