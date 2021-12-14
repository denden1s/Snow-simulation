using System.Diagnostics;

namespace Snow_Simulation.Interfaces
{
  public interface ISnowGeneration
  {
    private int _generationPeriod;
    private Stopwatch _timer;
    private int _width;
    public double GenerationSecond { set { _generationPeriod = (int)(Math.Round(value,3) * 1000); } }
    public void Generate(List<SnowFlake> snow);
  }
}