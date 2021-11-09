using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Snow_simulation;

namespace SnowSimulation.Interfaces
{
  public interface IDrawing
  {
    void Draw(List<SnowFlake> objects, List<SnowFlake> drift);
    //void DrawSnowDrift(List<SnowFlake> objects);
  }
}
