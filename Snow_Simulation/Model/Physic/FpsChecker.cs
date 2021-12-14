using System;
using System.Diagnostics;
using Snow_Simulation.Interfaces;

namespace Snow_Simulation.Model.Physic
{
  public class FpsChecker : IFpsChecker
  {
    private int _fps, _frames;
    private Stopwatch _timer;

    public int FPS {get{return _fps;}}
    public int Frame
    {
      get {return _frames;}
      set {_frames = (int)Math.Abs(value);}
    }

    public FpsChecker()
    {
      _fps = 0;
      _frames = 0;
      _timer = new Stopwatch();
    }
    public void Calculate()
    {
      _timer.Start();
      if(_timer.ElapsedMilliseconds >= 1000)
      {
        _fps = _frames;
        _frames = 0;
        _timer.Stop();
        _timer.Reset();
      }
    }
  }
}