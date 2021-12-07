using System;

namespace Snow_simulation
{
  public class SnowFlake
  {
    private int _x, _y, _stepByX, _stepByY;

    public int X { 
      get { return _x; }
      set { _x = (int)Math.Abs(value); }
    }
    public int Y { 
      get { return _y; }
      set { _y = (int)Math.Abs(value); }
    }

    public int StepX { 
      get { return _stepByX; }
      set { _stepByX = value; }
    }
    public int StepY { 
      get { return _stepByY; }
      set { _stepByY = (int)Math.Abs(value); }
    }

    public SnowFlake(int x, int y, int stepX = 0, int stepY = 1)
    {
      _x = x;
      _y = y;
      _stepByX = stepX;
      _stepByY = stepY;
    }
    public void MoveByX()
    {
      _x += _stepByX;
    }
    public void MoveDown()
    {
      _y += _stepByY;
    }
  }
}