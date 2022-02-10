using System.Collections.Generic;
using Snow_simulation.Model;
using  Snow_simulation.Model.Drift;

namespace Snow_simulation.Interfaces
{
  public interface ISnowMoving
  {
    public int Height{get;}
    public int MoveByX { get; set; }
    public int MoveByY { get; set; }
    public int Width{get;}
    public void Move(List<SnowFlake> flakes,SnowDrift snowDrift);
  }
}