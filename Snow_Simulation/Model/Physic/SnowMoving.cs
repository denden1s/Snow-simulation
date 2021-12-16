using Snow_Simulation.Interfaces;
namespace Snow_Simulation.Model.Physic
{
  public class SnowMoving : ISnowMoving
  {
    public int Height{get; private set;}
    public SnowMoving(int height)
    {
      Height = height;
    }

    private void MoveOnX(SnowFlake flake)
    {
      if(flake.StepX != 0)
      {
        if(flake.StepX > 0)
        {
          if(flake.X + flake.StepX < _width)
            flake.MoveByX();
        }
        else
        {
          if(flake.X + flake.StepX > 0)
            flake.MoveByX();
        }
      }
    }
    public void Move(List<SnowFlake> flakes,SnowDrift snowDrift)
    {
      List<SnowFlake> itemsToRemove = new List<SnowFlake>();
      foreach(SnowFlake sf in flakes)
      {
        if(_snowDrift.ContainsFlakeByX(sf.X))
        {
          //Situation when snow not drop on the floor
          if(sf.Y + sf.StepY < snowDrift.Y(sf.X))
          {
            sf.StepY = MoveByY;
            sf.StepX = MoveByX;
            sf.MoveDown();
            MoveOnX(sf);
          }
          else
          {
            snowDrift.ReplaceDots(sf);
            itemsToRemove.Add(sf);
          }
        }
        else
        {
          if(sf.Y + sf.StepY < Height)
          {
            sf.MoveDown();
            sf.StepY = MoveByY;
            sf.StepX = MoveByX;
            MoveOnX(sf);
          }
          else
          {
            snowDrift.Add(sf.X, sf.Y);
            itemsToRemove.Add(sf);
          }
        }
      }
      foreach(SnowFlake i in itemsToRemove)
      {
        flakes.Remove(i);
      }
    }
  }
}