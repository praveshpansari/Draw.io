using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    /// <summary>
    /// The interface for the shape classes
    /// </summary>
    interface Shapes
    {
        /// <summary>
        /// Sets the size and position of the shape
        /// </summary>
        /// <param name="list">The list of integers containing postion and size of the shape</param>
        void set(params int[] list);

        /// <summary>
        /// Draws the shape on a graphics object using a given pen,fill information and brush
        /// </summary>
        /// <param name="g">The graphics object where the shape is to be drawn</param>
        /// <param name="fill">Whether the shape drawn is filled or not</param>
        /// <param name="p">The pen used to draw the shape</param>
        /// <param name="b">The brush used to draw the shape</param>
        void draw(Graphics g, bool fill, Pen p, Brush b);

    }
}
