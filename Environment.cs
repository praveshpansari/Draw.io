using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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

            if (input == String.Empty)
            {
                if (codeEditor.Text != String.Empty)
                {
                    parser.parseEditor(codeEditor.Text);
                }

            }
            else
            {
                if (!input.ToLower().Trim().Equals("run"))
                    parser.parseCommand(input, 0);
                else parser.parseEditor(codeEditor.Text);
            }
            outputWindow.Refresh();

            parser.displayError();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to save button
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rich Text File | *.rtf";
            saveFileDialog.Title = "Save a Code Text File";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                this.codeEditor.SaveFile(saveFileDialog.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rich Text File | *.rtf";
            openFileDialog.Title = "Open a Code Text File";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                this.codeEditor.LoadFile(openFileDialog.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to save your code before closing it?", "New File", MessageBoxButtons.YesNoCancel);

            if (result != DialogResult.Cancel)
            {
                if (result == DialogResult.Yes)
                    this.saveToolStripMenuItem.PerformClick();
                this.codeEditor.Clear();
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }
    }
}
