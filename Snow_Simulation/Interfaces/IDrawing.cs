using System.Collections.Generic;
using Snow_simulation;
using Snow_simulation.Model;

namespace Snow_simulation.Interfaces
{
  public interface IDrawing
  {
    void Draw(List<SnowFlake> objects, List<SnowFlake> drift);
  }
}