using System.Diagnostics;
using Snow_simulation.Model;

namespace Snow_simulation.Interfaces
{
  public interface ISnowGeneration
  {
    public double GenerationSecond { set { GenerationSecond = (int)(Math.Round(value,3) * 1000); } }
    public void Generate(List<SnowFlake> snow);
  }
}