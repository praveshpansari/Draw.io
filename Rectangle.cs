﻿using System.Drawing;

namespace AssignmentASE
{
    /// <summary>
    /// Rectangle Shape class inherited from <see cref="Shape"/>
    /// </summary>
    /// <remarks>
    /// This class allows to set and draw a rectangle of specified width and height
    /// </remarks>
    public class Rectangle : Shape
    {
        // The width and height of the rectangle
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
            // If fill is on
            if (fill)
                // Draw a filled rectangle
                g.FillRectangle(b, x - (width / 2), y - (height / 2), width, height);
            else
                //Else draw a outlined rectangle
                g.DrawRectangle(p, x - (width / 2), y - (height / 2), width, height);
        }
    }
}
