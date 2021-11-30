using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snow_simulation;

namespace Snow
{
  public partial class Form1 : Form
  {
    private MainPhysic _p;
    private Graphics _gr;
    private DrawingModels _draw;
    public Form1()
    {
      InitializeComponent();
      _gr = this.CreateGraphics();
      _draw = new DrawingModels(_gr);
      _p = new MainPhysic(_draw, Width, Height);

    }

    private async void Form1_Load(object sender, EventArgs e)
    {
      
      _p.Simulate();
       

    }

    private void Form1_DoubleClick(object sender, EventArgs e)
    {
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.Text = "FPS: "+_p.FPS;
    }
  }
}
