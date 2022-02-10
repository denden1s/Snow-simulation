using System.Collections.Generic;
using Snow_simulation.Interfaces;
using  Snow_simulation.Model.Drift;
using Snow_simulation.Model;
using System;
using System.Threading.Tasks;




namespace Snow_simulation.Model.Physic
{
  public class SnowMoving : ISnowMoving
  {
    public int Height{get; private set;}
    public int MoveByX { get; set; }
    public int MoveByY { get; set; } = 1;
    public int Width{get; private set;}
    
    public SnowMoving(int width, int height)
    {
      Height = height;
      Width = width;
    }

    private void MoveOnX(SnowFlake flake)
    {
      if(flake.StepX != 0)
      {
        if(flake.StepX > 0)
        {
          if(flake.X + flake.StepX < Width)
            flake.MoveByX();
        }
        else
        {
          if(flake.X + flake.StepX > 0)
            flake.MoveByX();
        }
      }
    }

    private void SingleMove(SnowFlake flake, SnowDrift snowDrift, List<SnowFlake> itemsToRemove)
    {
      Task.Run(()=>
      {

        if(snowDrift.ContainsFlakeByX(flake.X))
        {
          //Situation when snow not drop on the floor
          if(flake.Y + flake.StepY < snowDrift.Y(flake.X))
          {
            flake.StepY = MoveByY;
            flake.StepX = MoveByX;
            flake.MoveDown();
            MoveOnX(flake);
          }
          else
          {
            snowDrift.ReplaceDots(flake);
            itemsToRemove.Add(flake);
          }
        }
        else
        {
          if(flake.Y + flake.StepY < Height)
          {
            flake.MoveDown();
            flake.StepY = MoveByY;
            flake.StepX = MoveByX;
            MoveOnX(flake);

          }
          else
          {
            snowDrift.Add(flake.X, flake.Y);
            itemsToRemove.Add(flake);
          }
        }
      });
    }


    public void Move(List<SnowFlake> flakes,SnowDrift snowDrift)
    {
      List<SnowFlake> itemsToRemove = new List<SnowFlake>();
      foreach(SnowFlake sf in flakes)
        SingleMove(sf, snowDrift, itemsToRemove);
      Task.Run(()=>
      {
        while(true)
          if(itemsToRemove.Count > 0)
          {
            foreach(SnowFlake i in itemsToRemove)
              flakes.Remove(i);
          }
      });
    }
  }
}