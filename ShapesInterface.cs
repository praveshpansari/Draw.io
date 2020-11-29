using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesInheritanceExample
{
    interface Shapes
    {
        void set(Color c, params int[] list);
        void draw(Graphics g);
        double calcArea();
        double calcPerimeter();

    }
}
