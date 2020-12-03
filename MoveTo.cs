using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    class MoveTo : Command
    {
        int toX, toY;
        public MoveTo() : base()
        { }


        public override void execute()
        {
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
            return "[" + DateTime.Now.ToString("T") + "] " + "Cursor moved to (" + X + "," + Y + ").\r\n";
        }
    }
}
