using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    class Triangle : Shape
    {
        int width, height;
        public Triangle() : base()
        {
            width = 100;
            height = 100;
        }
        public Triangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {

            this.width = width; //the only thingthat is different from shape
            this.height = height;
        }

        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.set(colour, list[0], list[1]);
            this.width = list[2];
            this.height = list[3];

        }

        public override void draw(Graphics g, bool fill, Pen p, Brush b)
        {
            if (fill)
                g.FillPolygon(b, new Point[] {
                new Point(x, y),
                new Point(x + width, y),
                new Point(new Random().Next(x, x + width), y + height)
                });
            else
                g.DrawPolygon(p, new Point[] {
                new Point(x, y),
                new Point(x + width, y),
                new Point(new Random().Next(x, x + width), y + height)
                });
        }

        public override double calcArea()
        {
            return width * height / 2;
        }

        public override double calcPerimeter()
        {
            return 2 * width + 2 * height;
        }
    }
}
