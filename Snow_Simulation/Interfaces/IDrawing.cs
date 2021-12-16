using System.Collections.Generic;
using Snow_simulation;

namespace Snow_simulation.Interfaces
{
  public interface IDrawing
  {
    void Draw(List<SnowFlake> objects, List<SnowFlake> drift);
  }
}