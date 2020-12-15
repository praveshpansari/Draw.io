using System;
using System.Drawing;

namespace AssignmentASE
{
    /// <summary>
    /// Draw Line Command class inherited from <see cref="Command"/>
    /// </summary>
    /// <remarks>
    /// Allows to Execute the drawto command and draw a line from
    /// current position to a given position
    /// </remarks>
    public class DrawLine : Command
    {
        int toX, toY;

        public override void Execute()
        {
            g.DrawLine(p, x, y, toX, toY);
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
            return "[" + DateTime.Now.ToString("T") + "] " + "Drew a line to (" + X + "," + Y + ").\r\n";
        }
    }
}
