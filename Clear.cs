using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    class Clear : Command
    {
        public override void execute()
        {
            g.Clear(Color.FromKnownColor(KnownColor.Gainsboro));
        }

        public override string getLog()
        {
            return "[" + DateTime.Now.ToString("T") + "] " + "Cleared the drawing area.\r\n";
        }
    }
}