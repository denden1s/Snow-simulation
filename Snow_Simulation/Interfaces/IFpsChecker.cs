namespace Snow_simulation.Interfaces
{
  public interface IFpsChecker
  {
    public int FPS {get;}
    public int Frame {get; set;}
    public void Calculate();
  }
}