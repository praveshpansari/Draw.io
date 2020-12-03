using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    class Reset : Command
    {
        public override void execute()
        {
            x = 0;
            y = 0;
        }

        public override string getLog()
        {
            return "[" + DateTime.Now.ToString("T") + "] " + "Cursor position reset to top left.\r\n";
        }
    }
}
