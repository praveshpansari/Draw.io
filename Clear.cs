using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    /// <summary>
    /// Clear Command class inherited from <see cref="Command"/>
    /// </summary>
    /// <remarks>
    /// Allows to Execute the clear command and clear the output window
    /// </remarks>
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