using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssignmentASE
{
    public partial class Environment : Form
    {

        Bitmap myBitmap;
        Graphics g;
        Parser parser;
        Painter painter;
        public Environment()
        {
            InitializeComponent();
            myBitmap = new Bitmap(outputWindow.Width, outputWindow.Height);
            parser = new Parser();
            g = outputWindow.CreateGraphics();
            painter = new Painter(g);
        }

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void outputWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics windowG = e.Graphics;
            windowG.DrawImageUnscaled(myBitmap, 0, 0);
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            
            g.DrawLine(new Pen(Color.Black), new Point(25, 20), new Point(50, 70));
            string input = commandLine.Text;
            parser.parse(input, painter);
        }
    }
}
