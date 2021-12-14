namespace Snow_Simulation.Model.MainPhysic
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
        _generationTimer.Start();
        Random randomNum = new Random();
        if(_generationTimer.ElapsedMilliseconds >= _generationPeriod)
        {
          SnowFlake generatedSnowFlake = new SnowFlake(randomNum.Next(0, _width), 0);
          _snow.Add(generatedSnowFlake);
          _timer.Stop();
          _timer.Reset();
        }
      }
    }
}