using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Snow_simulation.Model.Physic;

namespace SnowOnWPF
{
  public partial class MainWindow : Window
  {
    Drawing _draw;
    MainPhysic _physic;
    int _speed, _ofsetByX;
    bool _needChangePeriod;
    DispatcherTimer _timer;

    public MainWindow()
    {
      InitializeComponent();
      _draw = new Drawing(MainCanvas, (int)MainCanvas.Width, (int)MainCanvas.Height);
      _needChangePeriod = false;
      _ofsetByX = 0;
      _physic = new MainPhysic((int)MainCanvas.Width-19, (int)MainCanvas.Height, _draw);
      _speed = 1;
      _timer = new DispatcherTimer();

      _physic.GenerationSecond = 1;
      SpeedLabel.Content = _speed;
      _timer.Tick += new EventHandler(Timer_Tick);
      _timer.Interval = new TimeSpan(0, 0, 1);
    }

    private void Form_Loaded(object sender, RoutedEventArgs e)
    {
      _timer.Start();
      Application.Current.Dispatcher.Invoke(() => { _physic.Simulate(); });
    }
 
    private void Timer_Tick(object sender, EventArgs e)
    {
      int fps = Convert.ToInt32(_physic.FPS);
      fpsListener.Content = _physic.FPS;
      
      if(fps < 30)
      {
        fpsListener.Foreground = Brushes.Red;
      }
      else if (fps < 60)
      {
        fpsListener.Foreground = Brushes.Yellow;
      }
      else
      {
        fpsListener.Foreground = Brushes.Green;
      }
      _physic.MoveByX = _ofsetByX;
      _physic.MoveByY = _speed;
      if(_needChangePeriod)
      {
        _needChangePeriod = false;
        try
        {
          _physic.GenerationSecond = Convert.ToDouble(GenerationTimeTextbox.Text);
        }
        catch(Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
      }
      GC.Collect();
    }

    private void PlusSpeed_Click(object sender, RoutedEventArgs e)
    {
      _speed++;
      SpeedLabel.Content = _speed;
    }

    private void PlusXButton_Click(object sender, RoutedEventArgs e)
    {
      _ofsetByX++;
      MoveByX.Content = _ofsetByX;
    }

    private void MinusXButton_Click(object sender, RoutedEventArgs e)
    {
      _ofsetByX--;
      MoveByX.Content = _ofsetByX;
    }

    private void GenerationTimeTextbox_KeyDown(object sender, KeyEventArgs e)
    {
      if(GenerationTimeTextbox.Text.Length != 0)
      {
        if(e.Key == Key.Enter)
        {
          _needChangePeriod = true;
        }
      }
    }

    private void MinusSpead_Click(object sender, RoutedEventArgs e)
    {
      _speed--;
      SpeedLabel.Content = _speed;
    }

  }
}