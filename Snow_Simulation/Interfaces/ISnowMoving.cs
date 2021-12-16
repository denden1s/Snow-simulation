using Snow_simulation.Model;
using  Snow_simulation.Model.Drift;

namespace Snow_simulation.Interfaces
{
  public interface ISnowMoving
  {
    public int Height{get; private set;}
    public void Move(List<SnowFlake> flakes,SnowDrift snowDrift);
  }
}