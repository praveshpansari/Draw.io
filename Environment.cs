using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLEnvironment
{
    public partial class Environment : Form
    {

        Lexer lexer;
        Bitmap myBitmap = new Bitmap(640, 480);
        Graphics g;

        public Environment()
        {
            InitializeComponent();
            lexer = new Lexer();
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
            g = Graphics.FromImage(myBitmap);
            Pen p = new Pen(Color.Black, 2);
            for (int i = 0; i < lexer.Advance(commandLine.Text).Count(); i++)
            {
                Console.WriteLine(lexer.Advance(commandLine.Text)[i].toString());
                outputWindow.Image = myBitmap;
            }
        }
    }
}
