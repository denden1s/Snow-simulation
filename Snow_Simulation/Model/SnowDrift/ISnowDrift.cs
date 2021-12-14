namespace Snow_Simulation.Model.SnowDrift
{
  public interface ISnowDrift
  {
    public void SmoothDrift(List<SnowDrift> _drift);
    public void Sort(List<SnowDrift> _drift);
  }
}