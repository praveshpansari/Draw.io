using System;
using System.Drawing;

namespace AssignmentASE
{
    /// <summary>
    /// Triangle Shape class inherited from <see cref="Shape"/>
    /// </summary>
    /// <remarks>
    /// This class allows to set and draw a rectangle of specifice base length,
    /// and height
    /// </remarks>
    public class Triangle : Shape
    {
        // The base and height of the triangle
        int width, height;

        public override void Set(params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.Set(list[0], list[1]);
            this.width = list[2];
            this.height = list[3];
        }

        public override void Draw(Graphics g, bool fill, Pen p, Brush b)
        {
            //coords[0].X = (x - width);
            //coords[0].Y = (y + height);
            //coords[1].X = x + width;
            //coords[1].Y = (y + height);
            //coords[2].X = new Random().Next(x - width, x + width);
            //coords[2].Y = (y - height);

            if (fill)
                g.FillPolygon(b, new Point[] { new Point(x - width / 2, y + height / 2), new Point(x + width / 2, y + height / 2), new Point(new Random().Next(x - width / 2, x + width / 2), y - height / 2) });
            else
                g.DrawPolygon(p, new Point[] { new Point(x - width / 2, y + height / 2), new Point(x + width / 2, y + height / 2), new Point(new Random().Next(x - width / 2, x + width / 2), y - height / 2) });
        }
    }
}
