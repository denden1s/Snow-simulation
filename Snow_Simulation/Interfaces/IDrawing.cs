using System.Collections.Generic;
using Snow_simulation;

namespace SnowSimulation.Interfaces
{
  public interface IDrawing
  {
    void Draw(List<SnowFlake> objects, List<SnowFlake> drift);
  }
}