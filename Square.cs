using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    class Square : Rectangle
    {
        private int size;

        public Square() : base()
        {

        }
        public Square(Color colour, int x, int y, int size) : base(colour, x, y, size, size)
        {
            this.size = size;
        }

        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is size
            base.set(colour, list[0], list[1], list[2], list[2]);
        }

        //no draw method here because it is provided by the parent class Rectangle
        public override void draw(Graphics g, bool fill, Pen p, Brush b)
        {
            base.draw(g, fill, p, b);
        }

    }
}
