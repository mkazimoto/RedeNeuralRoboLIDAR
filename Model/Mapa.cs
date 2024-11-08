using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeuralTreinamento.Model
{
  public class Mapa
  {
    public int Width { get; set; }

    public int Height { get; set; } 

    public Bitmap Bitmap { get; set; }

    public Mapa() 
    { 
    }

    public void SetBitmap(Bitmap bitmap)
    {
      this.Width = bitmap.Width;
      this.Height = bitmap.Height;
      this.Bitmap = bitmap;
    }
  }
}
