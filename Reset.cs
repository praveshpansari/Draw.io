using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    /// <summary>
    /// Reset Command class inherited from <see cref="Command"/>
    /// </summary>
    /// <remarks>
    /// Allows to Execute the reset command and place the cursor to top left
    /// </remarks>
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
