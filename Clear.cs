using System;
using System.Drawing;

namespace AssignmentASE
{
    /// <summary>
    /// Clear Command class inherited from <see cref="Command"/>
    /// </summary>
    /// <remarks>
    /// Allows to Execute the clear command and clear the output window
    /// </remarks>
    public class Clear : Command
    {
        public override void Execute()
        {
            g.Clear(Color.FromKnownColor(KnownColor.Gainsboro));
        }

        public override string GetLog()
        {
            return "[" + DateTime.Now.ToString("T") + "] " + "Cleared the drawing area.\r\n";
        }
    }
}