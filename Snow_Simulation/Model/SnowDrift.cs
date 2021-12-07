using System.Collections.Generic;
using Snow_simulation;
using System.Linq;

namespace Snow_Simulation.Model
{
  class SnowDrift
  {
    public List<SnowFlake> flakes { get; private set; }

    public SnowDrift()
    {
      flakes = new List<SnowFlake>();
    }

    private void SmoothDrift()
    {
      for(int j = 0; j < 4; j++)
      {
        for(int i = 0; i < flakes.Count - 1; i++)
        {
          if(flakes[i].Y + 2 <= flakes[i + 1].Y)
          {
            flakes[i].Y++;
            flakes[i + 1].Y--;
          }
        }
      }      
    }
    private void Sort()
    {
      List<SnowFlake> temp = flakes.OrderBy(i => i.X).ToList();
      flakes = temp;
    }
    
    public void Add(int x, int y)
    {
      flakes.Add(new SnowFlake(x,y));
      Sort();
      SmoothDrift();
    }

    public bool ContainsFlakeByX(int x)
    {
      return flakes.Where(i => i.X == x).FirstOrDefault() != null ? true : false;
    }

    //Need to swap dots in polygon (optimize)
    public void ReplaceDots(SnowFlake flake)
    {
      SnowFlake sf = flakes.Where(i => i.X == flake.X).FirstOrDefault();
      if(sf != null)
      {
        flakes.Remove(sf);
        flakes.Add(flake);
        Sort();
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