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

        String command;

        public Environment()
        {
            InitializeComponent();
        }

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //TODO: execute command function
            }
        }
    }
}
