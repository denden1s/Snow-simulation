using System.Collections.Generic;
using System.Diagnostics;
using Snow_simulation.Interfaces;
using Snow_simulation.Model;
using System;

namespace Snow_simulation.Model.Physic
{
    public class SnowGeneration : ISnowGeneration
    {
      private int _generationPeriod;
      private Stopwatch _timer;
      private int _width;

      public double GenerationSecond { set { _generationPeriod = (int)(Math.Round(value,3) * 1000); } }
     
      public SnowGeneration(int width,int period = 1000)
      {
        _generationPeriod = period;
        _timer = new Stopwatch();
        _width = width;
      }
      public void Generate(List<SnowFlake> snow)
      {
        _timer.Start();
        Random randomNum = new Random();
        if(_timer.ElapsedMilliseconds >= _generationPeriod)
        {
          SnowFlake generatedSnowFlake = new SnowFlake(randomNum.Next(0, _width), 0);
          snow.Add(generatedSnowFlake);
          _timer.Stop();
          _timer.Reset();
        }
      }
    }
}