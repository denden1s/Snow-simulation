using System.Collections.Generic;
using Snow_simulation.Interfaces;
using  Snow_simulation.Model.Drift;
using Snow_simulation.Model;
using System;
using System.Threading.Channels;

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
    public void Move(List<SnowFlake> flakes,SnowDrift snowDrift)
    {
      List<SnowFlake> itemsToRemove = new List<SnowFlake>();
      foreach(SnowFlake sf in flakes)
      {
        
        if(snowDrift.ContainsFlakeByX(sf.X))
        {
          //Situation when snow droped on the floor
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
            sf.StepY = MoveByY;
            sf.MoveDown();
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
        flakes.Remove(i);
    }
  }
}