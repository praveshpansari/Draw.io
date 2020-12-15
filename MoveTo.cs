using System;
using System.Drawing;

namespace AssignmentASE
{
    /// <summary>
    /// MoveTo Command class inherited from <see cref="Command"/>
    /// </summary>
    /// <remarks>
    /// Allows to Execute the moveto command and move the cursor to given position
    /// </remarks>
    public class MoveTo : Command
    {
        int toX, toY;

        public override void Execute()
        {
            x = toX;
            y = toY;
        }

        public override void Set(Graphics g, Pen p, params int[] list)
        {
            base.Set(g, p, list[0], list[1]);
            this.toX = list[2];
            this.toY = list[3];
        }

        public override string GetLog()
        {
            return "[" + DateTime.Now.ToString("T") + "] " + "Cursor moved to (" + X + "," + Y + ").\r\n";
        }
    }
}
