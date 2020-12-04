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
    interface CommandInterface
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <remarks>
        /// Performs the respective function of the command
        /// </remarks>
        void execute();

        /// <summary>  
        /// Sets the source and destination where the command is to be executed
        /// </summary>
        /// <param name="g">The graphics object where the command is executed</param>
        /// <param name="p">The pen to be used to execute the command</param>
        /// <param name="list">The list of integers containing source and destination</param>
        void set(Graphics g, Pen p, params int[] list);

        /// <summary>
        /// Provides information about what the command has done
        /// </summary>
        /// <returns>A string containing the lof of the action this command performed</returns>
        string getLog();
    }
}
