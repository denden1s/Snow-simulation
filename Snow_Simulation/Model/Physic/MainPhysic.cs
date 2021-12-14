using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SnowSimulation.Interfaces;
using Snow_Simulation.Model;
using Snow_Simulation.Model.MainPhysic;
using Snow_Simulation.Model.SnowDrift;

namespace Snow_Simulation.Model.Physic
{
  public class MainPhysic
  {
    private int _width, _height, _offsetByY, _offsetByX;
    private IDrawing _draw;
    private DriftFunctional _driftFunctional;
    private FpsChecker _fpsController;
    private List<SnowFlake> _snow;
    private SnowDrift _snowDrift;
    private SnowGeneration _snowGeneration;
   
    public string FPS { get { return Convert.ToString(_fpsController.FPS); } }
    public double GenerationSecond { set { _snowGeneration.GenerationSecond = value;} }
    public int MoveByX { get { return _offsetByX; } set { _offsetByX = value; } }
    public int MoveByY { get { return _offsetByY; } set { _offsetByY = Math.Abs(value); } }

    //Списки это ссылочные типы или нет? нужно ли в методе указывать параметр ref???
    //можно определить необязательные параметры как интерфейсы и указать явно объекты
    public MainPhysic(IDrawing draw, int width, int height, int genTiming = 1000)
    {
      _draw = draw;
      _driftFunctional = new DriftFunctional();
      _fpsController = new FpsChecker();
      _height = height;
      _offsetByX = 0;
      _offsetByY = 0;
      _snow = new List<SnowFlake>();
      _snowDrift = new SnowDrift(_driftFunctional);
      _snowGeneration = new SnowGeneration(width, genTiming);
      _width = width;
    }

    private void Draw()
    {
      _draw.Draw(_snow, _snowDrift.flakes);
      _fpsController.Frame++;
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

    private void SnowMove()
    {
      List<SnowFlake> itemsToRemove = new List<SnowFlake>();
      foreach(SnowFlake sf in _snow)
      {
        if(_snowDrift.ContainsFlakeByX(sf.X))
        {
          //Situation when snow not drop on the floor
          if(sf.Y + sf.StepY < _snowDrift.Y(sf.X))
          {
            sf.StepY = MoveByY;
            sf.StepX = MoveByX;
            sf.MoveDown();
            MoveOnX(sf);
          }
          else
          {
            _snowDrift.ReplaceDots(sf);
            itemsToRemove.Add(sf);
          }
        }
        else
        {
          if(sf.Y + sf.StepY < _height)
          {
            sf.MoveDown();
            sf.StepY = MoveByY;
            sf.StepX = MoveByX;
            MoveOnX(sf);
          }
          else
          {
            _snowDrift.Add(sf.X, sf.Y);
            itemsToRemove.Add(sf);
          }
        }
      }
      foreach(SnowFlake i in itemsToRemove)
      {
        _snow.Remove(i);
      }
    }   

     public async void Simulate()
    {
      while(true)
      {
        await Task.Run(() => _fpsController.Calculate());
        await Task.Run(() => _snowGeneration.Generate(_snow));
        await Task.Run(() => SnowMove());
        await Task.Run(() => Draw());
      }
    }
  }
}