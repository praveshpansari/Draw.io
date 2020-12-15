using System.Drawing;


namespace AssignmentASE
{
    /// <summary>
    /// Abstract class shape implementing the <see cref="IShapes"/> interface
    /// </summary>
    /// <remarks>
    /// Implements the draw and set methods
    /// </remarks>
    public abstract class Shape : IShapes
    {
        /// <summary>
        /// The x and y postion of the shape
        /// </summary>
        protected int x, y;

        /// <summary>
        /// Draws the shape on a graphics object using a given pen,fill information and brush
        /// </summary>
        /// <param name="g">The graphics object where the shape is to be drawn</param>
        /// <param name="fill">Whether the shape drawn is filled or not</param>
        /// <param name="p">The pen used to draw the shape</param>
        /// <param name="b">The brush used to draw the shape</param>
        public abstract void Draw(Graphics g, bool fill, Pen p, Brush b);

        /// <summary>
        /// Sets the size and position of the shape
        /// </summary>
        /// <param name="list">The list of integers containing postion and size of the shape</param>
        public virtual void Set(params int[] list)
        {
            this.x = list[0];
            this.y = list[1];
        }
    }
}


