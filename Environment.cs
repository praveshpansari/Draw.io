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
        Parser parser;
        Painter painter;
        Bitmap outputImage;

        public Environment()
        {
            InitializeComponent();

            outputImage = new Bitmap(outputWindow.Width, outputWindow.Height);
            painter = new Painter(outputWindow, logBox);
            parser = new Parser(painter);
        }

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void outputWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(outputImage, 0, 0);
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            string input = commandLine.Text;

            if (!input.ToLower().Trim().Equals("run"))
                parser.parseCommand(input, 0);
            else parser.parseEditor(codeEditor.Text);

            outputWindow.Refresh();

            parser.displayError();
        }
    }
}