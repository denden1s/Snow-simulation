using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Snow_simulation.Interfaces;
using Snow_simulation.Model.Physic;
using Snow_simulation.Model.Drift;
using Snow_simulation.Model;

namespace Snow_simulation.Model.Physic
{
  public class MainPhysic
  {
    //Interfaces
    private IFpsChecker _IFpsController;
    private ISnowDrift _ISnowDrift;
    private ISnowGeneration _ISnowGeneration;
    private ISnowMoving _ISnowMoving;

    private int _width, _height, _offsetByY, _offsetByX;
    private IDrawing _draw;
    private DriftFunctional _driftFunctional;
    private FpsChecker _fpsController;
    private List<SnowFlake> _snow;
    private SnowDrift _snowDrift;

    private SnowGeneration _snowGeneration;
    private SnowMoving _snowMoving;
   
    public string FPS { get { return Convert.ToString(_fpsController.FPS); } }
    public double GenerationSecond { set { _snowGeneration.GenerationSecond = value;} }
    public int MoveByX { 
      get { return _offsetByX; }
      set 
      {
        _offsetByX = value; 
        _ISnowMoving.MoveByX = _offsetByX;
        _snowMoving.MoveByY = _offsetByX;
      } 
    }
    public int MoveByY { 
      get { return _offsetByY; } 
      set
      { 
        _offsetByY = Math.Abs(value);
        _ISnowMoving.MoveByY = _offsetByY;
        _snowMoving.MoveByY = _offsetByY;
      } 
    }

    public MainPhysic(int width, int height,IDrawing draw,IFpsChecker fpsCheker = null,
      ISnowDrift drift = null, ISnowGeneration generation = null, ISnowMoving moving = null)
    {
      _draw = draw;
      _driftFunctional = new DriftFunctional();
      _fpsController = new FpsChecker();
      _height = height;
      _offsetByX = 0;
      _offsetByY = 0;
      _snow = new List<SnowFlake>();
      _snowDrift = new SnowDrift(_driftFunctional);
      _snowGeneration = new SnowGeneration(width);
      _snowMoving = new SnowMoving(height,width);
      _width = width;
      //ToDO:присвоить значения объектам интерфейсов и поменять реализацию ниже
      _IFpsController = fpsCheker == null ? (IFpsChecker)_fpsController : fpsCheker;
      _ISnowDrift = drift == null ? (ISnowDrift)_driftFunctional : drift;
      _ISnowGeneration = generation == null ? (ISnowGeneration)_snowGeneration : generation;
      _ISnowMoving = moving == null ? (ISnowMoving)_snowMoving : moving;
    }

    private void Draw()
    {
      while(true)
      {
        _draw.Draw(_snow, _snowDrift.flakes);
        _IFpsController.Frame++;
      }
    } 
     public void Simulate()
    {
      Task.Run(() => _IFpsController.Calculate());
      Task.Run(() => _ISnowGeneration.Generate(_snow));
      Task.Run(() => _ISnowMoving.Move(_snow,_snowDrift));
      Task.Run(() => Draw());
    }
  }
}