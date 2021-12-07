using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SnowSimulation.Interfaces;
using Snow_Simulation.Model;

namespace Snow_simulation
{
  public class MainPhysic
  {
    private int _width, _height, _fpsCounter, _fps, _generationPeriod, _offsetByY, _offsetByX;
    private IDrawing _draw;
    private Stopwatch _fpsTimer, _generationTimer;
    private List<SnowFlake> _snow;
    private SnowDrift _snowDrift;
   
    public string FPS { get { return Convert.ToString(_fps); } }
    public double GenerationSecond { set { _generationPeriod = (int)(Math.Round(value,3) * 1000); } }
    public int MoveByX { get { return _offsetByX; } set { _offsetByX = value; } }
    public int MoveByY { get { return _offsetByY; } set { _offsetByY = Math.Abs(value); } }

    public MainPhysic(IDrawing draw, int width, int height, int genTiming = 1000)
    {
      _draw = draw;
      _fps = 0;
      _fpsCounter = 0;
      _fpsTimer = new Stopwatch();
      _generationPeriod = genTiming;
      _generationTimer = new Stopwatch();
      _height = height;
      _offsetByX = 0;
      _offsetByY = 0;
      _snow = new List<SnowFlake>();
      _snowDrift = new SnowDrift();
      _width = width;
    }

    private void Draw()
    {
      _draw.Draw(_snow, _snowDrift.flakes);
      _fpsCounter++;
    }

    private void GenerateSnow()
    {
      _generationTimer.Start();
      Random randomNum = new Random();
      if(_generationTimer.ElapsedMilliseconds >= _generationPeriod)
      {
        SnowFlake generatedSnowFlake = new SnowFlake(randomNum.Next(0, _width), 0);
        _snow.Add(generatedSnowFlake);
        _generationTimer.Stop();
        _generationTimer.Reset();
      }
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

    private void SetFps()
    {
      _fpsTimer.Start();
      if(_fpsTimer.ElapsedMilliseconds >= 1000)
      {
        _fps = _fpsCounter;
        _fpsCounter = 0;
        _fpsTimer.Stop();
        _fpsTimer.Reset();
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
        await Task.Run(() => SetFps());
        await Task.Run(() => GenerateSnow());
        await Task.Run(() => SnowMove());
        await Task.Run(() => Draw());
      }
    }
  }
}