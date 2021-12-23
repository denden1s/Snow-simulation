using System.Collections.Generic;
using Snow_simulation.Interfaces;
using Snow_simulation.Model;
using System.Linq;

namespace Snow_simulation.Model.Drift
{
  //ToDo: may be make static class
  public class DriftFunctional : ISnowDrift
  {
    public DriftFunctional() {}
    public void SmoothDrift(List<SnowFlake> _drift)
    {
      for(int j = 0; j < 4; j++)
      {
        for(int i = 0; i < _drift.Count - 1; i++)
        {
          if(_drift[i].Y + 2 <= _drift[i + 1].Y)
          {
            _drift[i].Y++;
            _drift[i + 1].Y--;
          }
        }
      }  
    }
    public void Sort(List<SnowFlake> _drift)
    {
      List<SnowFlake> temp = _drift.OrderBy(i => i.X).ToList();
      _drift = temp;
    }
  }
}