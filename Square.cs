using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    /// <summary>
    /// Square Shape class inherited from <see cref="Rectangle"/>
    /// </summary>
    /// <remarks>
    /// This class allows to set and draw a rectangle of same size
    /// </remarks>
    class Square : Rectangle
    {

        public override void set(params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is size
            base.set(list[0], list[1], list[2], list[2]);
        }
    }
}
