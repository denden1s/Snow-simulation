using System.Collections.Generic;
using System.Linq;
using Snow_simulation.Interfaces;
using  Snow_simulation.Model;

namespace Snow_simulation.Model.Drift
{
  public class SnowDrift
  {
    private ISnowDrift _driftFunctions;
    public List<SnowFlake> flakes { get; private set; }

    public SnowDrift(ISnowDrift functional)
    {
      flakes = new List<SnowFlake>();
      _driftFunctions = functional;
    }
    
    public void Add(int x, int y)
    {
      flakes.Add(new SnowFlake(x,y));
      _driftFunctions.Sort(flakes);
      _driftFunctions.SmoothDrift(flakes);
    }

    //? May be changed by SRP, problem: how i can update flakes list
    public bool ContainsFlakeByX(int x)
    {
      return flakes.Where(i => i.X == x).FirstOrDefault() != null ? true : false;
    }
    public void ReplaceDots(SnowFlake flake)
    {
      SnowFlake sf = flakes.Where(i => i.X == flake.X).FirstOrDefault();
      if(sf != null)
      {
        flakes.Remove(sf);
        flakes.Add(flake);
        _driftFunctions.Sort(flakes);
      }
    }

    //Need to find in drift dot by x
    public int Y(int x)
    {
      return (from sf in flakes
              where sf.X == x
              select sf.Y).FirstOrDefault();
    }
  }
}