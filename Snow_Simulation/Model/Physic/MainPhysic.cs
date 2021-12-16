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
    public int MoveByX { get { return _offsetByX; } set { _offsetByX = value; } }
    public int MoveByY { get { return _offsetByY; } set { _offsetByY = Math.Abs(value); } }

    //?Списки это ссылочные типы или нет? нужно ли в методе указывать параметр ref???
    //?можно определить необязательные параметры как интерфейсы и указать явно объекты

    //!Необязательные параметры присваиваются по окончанию конструктора или при вызове???
    public MainPhysic(int width, int height,IDrawing draw,IFpsCheker fpsCheker = _fpsController,
      ISnowDrift drift = _snowDrift, ISnowGeneration generation = _snowGeneration, ISnowMoving moving = _snowMoving)
    {
      //ToDO: I... = null
      // if i... == null  присвоить значение 
      _draw = draw;
      _driftFunctional = new DriftFunctional();
      _fpsController = new FpsChecker();
      _height = height;
      _offsetByX = 0;
      _offsetByY = 0;
      _snow = new List<SnowFlake>();
      _snowDrift = new SnowDrift(_driftFunctional);
      _snowGeneration = new SnowGeneration(width);
      _snowMoving = new SnowMoving(height);
      _width = width;
      //ToDO:присвоить значения объектам интерфейсов и поменять реализацию ниже
    }

    private void Draw()
    {
      _draw.Draw(_snow, _snowDrift.flakes);
      _fpsController.Frame++;
    }

     public async void Simulate()
    {
      while(true)
      {
        await Task.Run(() => _fpsController.Calculate());
        await Task.Run(() => _snowGeneration.Generate(_snow));
        await Task.Run(() => _snowMoving.Move(_snow,_snowDrift));
        await Task.Run(() => Draw());
      }
    }
  }
}