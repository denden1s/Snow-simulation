using System;
using System.Collections.Generic;
using System.Text;
using Snow_simulation;
using System.Linq;

namespace Snow_Simulation.Model
{
  class SnowDrift
  {
    public List<SnowFlake> flakes { get; private set; }

    private void Sort()
    {
      List<SnowFlake> temp = flakes.OrderBy(i => i.X).ToList();
      flakes = temp;
    }
    public SnowDrift()
    {
      flakes = new List<SnowFlake>();
    }

    public void Add(int x, int y)
    {
      // зачем передавать объект полностью
      flakes.Add(new SnowFlake(x,y));
      Sort();
    }

    public void Remove(int x)
    {
      //перенос точки по оси y на 1 деление вверх -1
    }

    public bool ContainsSnowByX(int x)
    {
      //SnowFlake sf = flakes.Where(i => i.X == flake.X).FirstOrDefault();
      //if(sf != null)
      //{
      //  return true;
      //}
      //return false;
      return flakes.Where(i => i.X == x).FirstOrDefault() != null ? true : false;
    }

    public int MaxYPos(int x)
    {
      return (from sf in flakes
              where sf.X == x
              select sf.Y).FirstOrDefault();
    }

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
  }
}