using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Snow_simulation.Model;
using Snow_simulation.Model.Physic;
using System;

namespace CrossplatformSnow
{
    public partial class MainWindow : Window
    {
        
        Drawing _draw;
        Canvas _mainCanvas;
        MainPhysic _physic;
        int _speed, _ofsetByX;
        bool _needChangePeriod;
        DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            
            _draw = new Drawing(_mainCanvas, (int)_mainCanvas.Width, (int)_mainCanvas.Height);
            _needChangePeriod = false;
            _ofsetByX = 0;
            _physic = new MainPhysic((int)_mainCanvas.Width, (int)_mainCanvas.Height,_draw);
            _speed = 1;
           // _timer = new DispatcherTimer();

            _physic.GenerationSecond = 1;
            //SpeedLabel.Content = _speed;
            //_timer.Tick += new EventHandler(Timer_Tick);
            //_timer.Interval = new TimeSpan(0, 0, 1);
             _physic.Simulate();
            Console.WriteLine("Start simulation");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _mainCanvas = this.FindControl<Canvas>("MainCanvas");
        }
        
    }
}