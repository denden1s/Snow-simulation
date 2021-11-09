using System;

namespace Snow_simulation
{
  public class SnowFlake
  {
    private int _x, _y, _stepByX, _stepByY;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }

    public int StepX { 
      get { return _stepByX; }
      set { _stepByX = (int)Math.Abs(value); }
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
    public void MoveRight()
    {
      _x += _stepByX;
    }
    public void MoveLeft()
    {
      _x -= _stepByX;
    }
    public void MoveUp()
    {
      _y -= _stepByY;
    }
    public void MoveDown()
    {
      _y += _stepByY;
    }
  }
}
