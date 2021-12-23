using System.Collections.Generic;
using Snow_simulation.Model;
using Snow_simulation.Model.Drift;

namespace Snow_simulation.Interfaces
{
  public interface ISnowDrift
  {
    void SmoothDrift(List<SnowFlake> _drift);
    void Sort(List<SnowFlake> _drift);
  }
}