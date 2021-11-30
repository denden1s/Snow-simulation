using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Snow_simulation;
using SnowSimulation.Interfaces;

namespace Snow
{
  public class DrawingModels : IDrawing
  {
    private Graphics _gr;

    public DrawingModels(Graphics gr)
    {
      _gr = gr;
    }
    public void Draw(List<SnowFlake> objects)
    {
      List<SnowFlake> temp = objects;
      Pen myPen = new Pen(Color.White);
      SolidBrush solidBrush = new SolidBrush(
      Color.White);
      _gr.Clear(Color.Black);
      foreach(SnowFlake sf in temp)
      {
        Rectangle rect = new Rectangle(sf.X, sf.Y, 5, 5);
        _gr.DrawEllipse(myPen, rect);
        _gr.FillEllipse(solidBrush, rect);
        GC.Collect();
      }
    }
  }
}
