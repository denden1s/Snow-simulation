using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Snow_simulation;
using SnowSimulation.Interfaces;
using System.Linq;

namespace SnowOnWPF
{
  class Drawing : IDrawing
  {
    PathGeometry _pathGeom;
    Path _p;
    Canvas _cv;
    double _width, _height;

    public Drawing(Canvas cv, double width, double height)
    {
      _p = new Path();
      _pathGeom = new PathGeometry();
      _cv = cv;
      _width = width;
      _height = height;
    }
    public void Draw(List<SnowFlake> objects, List<SnowFlake> drift)
    {
      Application.Current.Dispatcher.Invoke(() =>
      {
        Random rand = new Random();
        List<Ellipse> figures = new List<Ellipse>();
        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
        mySolidColorBrush.Color = Colors.White;
        for(int i = 0; i < objects.Count; i++)
        {

          figures.Add(new Ellipse());


          figures[i].Fill = mySolidColorBrush;
          figures[i].StrokeThickness = 5;
          figures[i].Stroke = Brushes.White;
          figures[i].Width = 5;
          figures[i].Height = 5;
          figures[i].Name = "n" + Convert.ToString(rand.Next()) + "n";
        }
        

        _cv.Children.Clear();
        for(int i = 0; i < objects.Count; i++)
        {
          _cv.Children.Add(figures[i]);

          figures[i].SetValue(Canvas.LeftProperty, (double)objects[i].X);
          figures[i].SetValue(Canvas.TopProperty, (double)objects[i].Y);
        }

        List<Point> points = new List<Point>();
        Polygon polygon = new Polygon();
        for(int i = 1; i < _width-1; i++)
        {
          points.Add(new Point(i, _height));
        }
        foreach(SnowFlake sf in drift)
        {
          if(points.Contains(points.Where(i => i.X == sf.X).FirstOrDefault()))
          {
            Point p = points.Where(i => i.X == sf.X).FirstOrDefault();
            if(p != null)
            {
              int index = points.IndexOf(p);
              points[index] = new Point(sf.X, sf.Y);
            }
          }
        }

        polygon.Points.Add(new Point(-1, _height + 1));
        foreach(Point p in points)
        {
          polygon.Points.Add(p);
        }
        polygon.Points.Add(new Point(_width + 1, _height + 1));
        polygon.Fill = mySolidColorBrush;
        polygon.Stroke = Brushes.White;
        polygon.StrokeThickness = 5;
        _cv.Children.Add(polygon);
        GC.Collect();
      });

    }

    public void DrawSnowDrift(List<SnowFlake> objects)
    {
      Application.Current.Dispatcher.Invoke(() =>
      {
        List<Point> points = new List<Point>();
        Polygon polygon = new Polygon();
        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
        mySolidColorBrush.Color = Colors.White;
        for(int i = 0; i < _width; i++)
        {
          points.Add(new Point(i, _height));
        }
        foreach(SnowFlake sf in objects)
        {
          Point p = points.Where(i => i.X == sf.X).First();
          if(p != null)
          {
            int index = points.IndexOf(p);
            points[index] = new Point(sf.X, sf.Y);
          }
        }

        polygon.Points.Add(new Point(-1, _height+1));
        foreach(Point p in points)
        {
          polygon.Points.Add(p);
        }
        polygon.Points.Add(new Point(_width+1, _height+1));
        polygon.Fill = mySolidColorBrush;
        polygon.Stroke = Brushes.White;
        polygon.StrokeThickness = 1;
        //_cv.Children.Clear();
        _cv.Children.Add(polygon);
      });
    }
  }
}
