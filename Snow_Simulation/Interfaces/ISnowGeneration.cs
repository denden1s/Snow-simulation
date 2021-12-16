using System.Diagnostics;

namespace Snow_Simulation.Interfaces
{
  public interface ISnowGeneration
  {
    public double GenerationSecond { set { GenerationSecond = (int)(Math.Round(value,3) * 1000); } }
    public void Generate(List<SnowFlake> snow);
  }
}