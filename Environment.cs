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

        CommandLineCommand command;

        public Environment()
        {
            InitializeComponent();
        }

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // TODO: A function that takes a command string and perform checks 
            }
        }
    }
}
