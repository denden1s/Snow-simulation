using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Snow_simulation;
using Snow_simulation.Interfaces;
using Snow_Simulation.Model;
using System.Linq;

namespace SnowOnWPF
{
  class Drawing : IDrawing
  {
    Canvas _canvas;
    List<Ellipse> _ellipsesPolygon;
    List<Ellipse> _flakes;
    List<Point> _points;
    List<Point> _driftPoints;
    Polygon _polygon;
    SolidColorBrush _solidColorBrush;
    double _width, _height;

    public Drawing(Canvas canvas, double width, double height)
    {
      _canvas = canvas;
      _ellipsesPolygon = new List<Ellipse>();
      _flakes = new List<Ellipse>();
      _height = height;
      _solidColorBrush = new SolidColorBrush();
      _points = new List<Point>();
      _driftPoints = new List<Point>();
      _polygon = new Polygon();
      _polygon.Fill = _solidColorBrush;
      _polygon.Stroke = Brushes.White;
      _polygon.StrokeThickness = 5;
      _solidColorBrush.Color = Colors.White;
      _width = width;
    }

    private void DrawDrift(List<SnowFlake> drift)
    {
      lock(drift)
      {
        _points.Clear();
        _driftPoints.Clear();
        _polygon.Points.Clear();
        _ellipsesPolygon.Clear();

        for(int i = 0; i < _width; i++)
        {
          SnowFlake sf = drift.Where(q => q.X == i).SingleOrDefault();
          if(sf != null)
            _points.Add(new Point(sf.X, sf.Y));
          else
            _points.Add(new Point(i, _height));

          _polygon.Points.Add(_points[i]);
        }
        GC.Collect();
        _polygon.Points.Add(new Point(_width, _height));
        _canvas.Children.Add(_polygon);
        if(_points.Where(i => i.Y - _height < 0).Count() > 0)
        {
          _driftPoints = _points.Where(i => i.Y - _height < 0).ToList();
          for(int i = 0; i < _driftPoints.Count; i++)
          {
            _ellipsesPolygon.Add(new Ellipse());
            SetEllipseParams(_ellipsesPolygon[i], 10);
            _canvas.Children.Add(_ellipsesPolygon[i]);
            _ellipsesPolygon[i].SetValue(Canvas.LeftProperty, (double)_driftPoints[i].X);
            _ellipsesPolygon[i].SetValue(Canvas.TopProperty, (double)_driftPoints[i].Y - 5);
          }
        }
        GC.Collect();
      }
    }

    private void DrawFlakes(List<SnowFlake> objects)
    {
      lock(objects)
      {
        _flakes.Clear();
        for(int i = 0; i < objects.Count; i++)
        {
          _flakes.Add(new Ellipse());
          SetEllipseParams(_flakes[i], 5);
          _canvas.Children.Add(_flakes[i]);
          _flakes[i].SetValue(Canvas.LeftProperty, (double)objects[i].X);
          _flakes[i].SetValue(Canvas.TopProperty, (double)objects[i].Y);
        }
        GC.Collect();
      }
    }

    private void SetEllipseParams(Ellipse element, int width)
    {
      element.Fill = _solidColorBrush;
      element.StrokeThickness = width;
      element.Stroke = Brushes.White;
      element.Width = width;
      element.Height = width;
    }

    public void Draw(List<SnowFlake> objects, List<SnowFlake> drift)
    {
      Application.Current.Dispatcher.Invoke(() =>
      {       
        _canvas.Children.Clear();
        DrawFlakes(objects);
        DrawDrift(drift);
      });
    }
  }
}