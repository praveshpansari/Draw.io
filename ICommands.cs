using System.Drawing;

namespace AssignmentASE
{
    /// <summary>
    /// The interface for the shape classes
    /// </summary>
    public interface ICommands
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <remarks>
        /// Performs the respective function of the command
        /// </remarks>
        void Execute();

        /// <summary>  
        /// Sets the source and destination where the command is to be executed
        /// </summary>
        /// <param name="g">The graphics object where the command is executed</param>
        /// <param name="p">The pen to be used to execute the command</param>
        /// <param name="list">The list of integers containing source and destination</param>
        void Set(Graphics g, Pen p, params int[] list);

        /// <summary>
        /// Provides information about what the command has done
        /// </summary>
        /// <returns>A string containing the lof of the action this command performed</returns>
        string GetLog();
    }
}
