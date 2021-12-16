using Snow_simulation.Model.Drift;

namespace Snow_simulation.Interfaces
{
  public interface ISnowDrift
  {
    public void SmoothDrift(List<SnowDrift> _drift);
    public void Sort(List<SnowDrift> _drift);
  }
}