using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Snow_simulation;

namespace SnowOnWPF
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    MainPhysic _p;
    Stopwatch timer = new Stopwatch();
    Drawing _draw;
    DispatcherTimer _timer;
    int _speed, _xMove;
    public MainWindow()
    {
      InitializeComponent();
      _speed = 1;
      _xMove = 0;
      _draw = new Drawing(MainCanvas, (int)MainCanvas.Width, (int)MainCanvas.Height);
      _p = new MainPhysic(_draw, (int)MainCanvas.Width, (int)MainCanvas.Height);
      _p.GenerationSecond = 0.100;
      _timer = new DispatcherTimer();
      _timer.Tick += new EventHandler(timer_Tick);
      _timer.Interval = new TimeSpan(0, 0, 1);
      SpeedLabel.Content = _speed;
    }


    private void form_Loaded(object sender, RoutedEventArgs e)
    {
      _timer.Start();
      Application.Current.Dispatcher.Invoke(() =>
      {
        _p.Simulate();
      });
    }
 
    private void timer_Tick(object sender, EventArgs e)
    {
      fpsListener.Content = _p.FPS;
      int fps = Convert.ToInt32(_p.FPS);
      _p.MoveByY = _speed;
      _p.MoveByX = _xMove;

      GC.Collect();
    }

    private void PlusSpeed_Click(object sender, RoutedEventArgs e)
    {
      _speed++;
      SpeedLabel.Content = _speed;
    }


    private void PlusXButton_Click(object sender, RoutedEventArgs e)
    {
      _xMove++;
      MoveByX.Content = _xMove;
    }

    private void MinusXButton_Click(object sender, RoutedEventArgs e)
    {
      _xMove--;
      MoveByX.Content = _xMove;
    }

    private void MinusSpead_Click(object sender, RoutedEventArgs e)
    {
      _speed--;
      SpeedLabel.Content = _speed;
    }
  }
}
