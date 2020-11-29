using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    interface Shapes
    {
        void set(Color c, params int[] list);
        void draw(Graphics g, bool fill, Pen p, Brush b);
        double calcArea();
        double calcPerimeter();

    }
}
