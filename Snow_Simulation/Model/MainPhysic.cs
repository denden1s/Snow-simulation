using System;
using System.Collections.Generic;
using Snow_simulation;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using SnowSimulation.Interfaces;
using Snow_Simulation.Model;

namespace Snow_simulation
{
  public class MainPhysic
  {
    private List<SnowFlake> _snow;
    private SnowDrift _snowDrift;
    private IDrawing _draw;
    private Stopwatch timerForGenFlake;
    private Stopwatch timer;
    private bool _stopSimulation;

    private int _width, _height, _fpsCounter, _fps, _generationPeriod;
    public string FPS { get { return Convert.ToString(_fps); } }
    public bool StopSimulation { set { _stopSimulation = value; } }
    public double GenerationSecond { set { _generationPeriod = (int)(Math.Round(value,3) * 1000); } }
    private void SetFps()
    {
      timer.Start();
      if(timer.ElapsedMilliseconds >= 1000)
      {
        _fps = _fpsCounter;
        _fpsCounter = 0;
        timer.Stop();
        timer.Reset();
      }
    }

    private void Draw()
    {
      _draw.Draw(_snow, _snowDrift.flakes);//зачем здесь передавать объект полностью, если можно передать
      //только список точек point
      _fpsCounter++;
    }

    private void GenerateSnowFromDrift()
    {
      //
    }
    private void GenerateSnow()
    {
      timerForGenFlake.Start();
      Random rand = new Random();
      if(timerForGenFlake.ElapsedMilliseconds >= _generationPeriod)
      {
        SnowFlake generatedSnowFlake = new SnowFlake(rand.Next(0, _width), 0);
        _snow.Add(generatedSnowFlake);
        timerForGenFlake.Stop();
        timerForGenFlake.Reset();
      }
    }

    //можно попробовать изменить направление движения снега по оси y, в таком случае нужно прекратить
    //генерацию снега и начать генерировать его из сугроба

    private void SnowMove()
    {
      List<SnowFlake> itemsToRemove = new List<SnowFlake>();
      foreach(SnowFlake sf in _snow)
      {
        if(_snowDrift.ContainsSnowByX(sf.X))
        {
          if(sf.Y < _snowDrift.MaxYPos(sf.X) - sf.StepY)
          {
            //
            sf.MoveDown();
          }
          else
          {
            _snowDrift.ReplaceDots(sf);
            itemsToRemove.Add(sf);
          }
        }
        else
        {
          if(sf.Y <= _height)
            sf.MoveDown();// движение в стороны
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

    public MainPhysic(IDrawing draw, int width, int height, int genTiming = 100)
    {
      _stopSimulation = false;
      _fpsCounter = 0;
      _fps = 0;
      _width = width;
      _height = height;
      _draw = draw;    
      _generationPeriod = genTiming;
      _snowDrift = new SnowDrift();
      timerForGenFlake = new Stopwatch();
      timer = new Stopwatch();
      _snow = new List<SnowFlake>();
    }

    public async void Simulate()
    {
      while(!_stopSimulation)
      {
        await Task.Run(() => SetFps());
        await Task.Run(() => GenerateSnow());
        await Task.Run(() => SnowMove());
        await Task.Run(() => Draw());
      }     
    }
  }
}