using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AssignmentASE
{
    /// <summary>
    /// Abstract class shape implementing the <see cref="Shapes"/> interface
    /// </summary>
    /// <remarks>
    /// Implements the draw and set methods
    /// </remarks>
    public abstract class Shape : Shapes
    {
        protected int x, y;

        //here we are passing on the obligation to implement them to the derived classes by declaring them as abstract
        public abstract void draw(Graphics g, bool fill, Pen p, Brush b);

        //set is declared as virtual so it can be overridden by a more specific child version
        //but is here so it can be called by that child version to do the generic stuff
        //note the use of the param keyword to provide a variable parameter list to cope with some shapes having more setup information than others
        public virtual void set(params int[] list)
        {
            this.x = list[0];
            this.y = list[1];
        }
    }
}


