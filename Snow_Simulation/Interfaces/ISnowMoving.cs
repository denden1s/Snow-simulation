using Snow_Simulation.Model;
namespace Snow_Simulation.Interfaces
{
  public interface ISnowMoving
  {
    public int Height{get; private set;}
    public void Move(List<SnowFlake> flakes,SnowDrift snowDrift);
  }
}