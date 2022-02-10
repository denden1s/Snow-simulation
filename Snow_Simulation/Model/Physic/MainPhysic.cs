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
    private ISnowDrift _ISnowDrift;
    private ISnowGeneration _ISnowGeneration;
    private ISnowMoving _ISnowMoving;

    private int _width, _height, _offsetByY, _offsetByX;
    private DriftFunctional _driftFunctional;
    private FpsChecker _fpsController;
    private List<SnowFlake> _snow;
    private SnowDrift _snowDrift;

    private SnowGeneration _snowGeneration;
    private SnowMoving _snowMoving;
   
    public double GenerationSecond { set { _snowGeneration.GenerationSecond = value;} }
    public int MoveByX { get { return _offsetByX; }
    set 
    {
      _offsetByX = value; 
      _ISnowMoving.MoveByX = _offsetByX;
      _snowMoving.MoveByY = _offsetByX;
    } }
    public int MoveByY { get { return _offsetByY; } 
    set
    { 
      _offsetByY = Math.Abs(value);
      _ISnowMoving.MoveByY = _offsetByY;
      _snowMoving.MoveByY = _offsetByY;
    } }

    public MainPhysic(int width, int height,
      ISnowDrift drift = null, ISnowGeneration generation = null, ISnowMoving moving = null)
    {
      _driftFunctional = new DriftFunctional();
      _fpsController = new FpsChecker();
      _height = height;
      _offsetByX = 0;
      _offsetByY = 0;
      _snow = new List<SnowFlake>();
      _snowDrift = new SnowDrift(_driftFunctional);
      _snowGeneration = new SnowGeneration(width);
      _snowMoving = new SnowMoving(width, height);
      _width = width;
      //ToDO:присвоить значения объектам интерфейсов и поменять реализацию ниже
      _ISnowDrift = drift == null ? (ISnowDrift)_driftFunctional : drift;
      _ISnowGeneration = generation == null ? (ISnowGeneration)_snowGeneration : generation;
      _ISnowMoving = moving == null ? (ISnowMoving) _snowMoving : moving;
    }

    public List<SnowFlake> GetSnowFlakes()
    {
      return _snow;
    }
    public SnowDrift GetSnowDrift()
    {
      return _snowDrift;
    }
     public async void Simulate()
     {
       while(true)
      {
        await Task.Run(() => _ISnowGeneration.Generate(_snow));
        await Task.Run(() => _ISnowMoving.Move(_snow,_snowDrift));
      }
    }
  }
}