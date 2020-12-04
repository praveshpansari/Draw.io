using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    /// <summary>
    /// Draw Line Command class inherited from <see cref="Command"/>
    /// </summary>
    /// <remarks>
    /// Allows to Execute the drawto command and draw a line from
    /// current position to a given position
    /// </remarks>
    class DrawLine : Command
    {
        int toX, toY;

        public override void execute()
        {
            g.DrawLine(p, x, y, toX, toY);
            x = toX;
            y = toY;
        }

        public override void set(Graphics g, Pen p, params int[] list)
        {
            base.set(g, p, list[0], list[1]);
            this.toX = list[2];
            this.toY = list[3];
        }

        public override string getLog()
        {
            return "[" + DateTime.Now.ToString("T") + "] " + "Drew a line to (" + X + "," + Y + ").\r\n";
        }
    }
}
