using System.Drawing;

namespace AssignmentASE
{
    /// <summary>
    /// Abstract class command implementing the <see cref="ICommands"/> interface
    /// </summary>
    /// <remarks>
    /// Implements the execute, set and log method
    /// </remarks>
    public abstract class Command : ICommands
    {
        // The current x and y coordinate in the graphics object
        protected int x, y;
        // The graphics object where the command is executed
        protected Graphics g;
        // The pen to be used when the command is executed
        protected Pen p;

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public virtual void Set(Graphics g, Pen p, params int[] list)
        {
            this.g = g;
            this.p = p;
            this.x = list[0];
            this.y = list[1];
        }

        public abstract void Execute();

        public abstract string GetLog();
    }
}
