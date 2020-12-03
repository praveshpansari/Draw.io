using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    abstract class Command : CommandInterface
    {
        protected int x, y;
        protected Graphics g;
        protected Pen p;
        public Command()
        {

        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Command(int x, int y)
        {
            this.x = x; //its x pos
            this.y = y; //its y pos
        }

        public virtual void set(Graphics g, Pen p, params int[] list)
        {
            this.g = g;
            this.p = p;
            this.x = list[0];
            this.y = list[1];
        }

        public abstract void execute();

        public abstract string getLog();
    }
}
