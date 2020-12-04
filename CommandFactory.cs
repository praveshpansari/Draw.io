using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    /// <summary>
    /// This is the factory for the <see cref="Command"/>
    /// </summary>
    /// <remarks>
    /// Allows to create an object of commands below.
    /// <list type="bullet">
    /// <item>
    /// <term>Draw Line</term>
    /// <description><see cref="DrawLine"/></description>
    /// </item>
    /// <item>
    /// <term>Move To</term>
    /// <description><see cref="MoveTo"/></description>
    /// </item>
    /// <item>
    /// <term>Clear</term>
    /// <description><see cref="Clear"/></description>
    /// </item>
    /// <item>
    /// <term>Reset</term>
    /// <description><see cref="Reset"/></description>
    /// </item>
    /// </list>
    /// </remarks>
    class CommandFactory
    {
        /// <summary>
        /// Used to instantiate and get a specific command object
        /// </summary>
        /// <param name="shapeType">The type of the command to be generated</param>
        /// <returns>An instance of the inherited classes of <see cref="Command"/></returns>
        /// <exception cref="ArgumentException"></exception>
        public Command getCommand(String command)
        {
            // Tidy the command
            command = command.ToLower().Trim();


            if (command.Equals("drawto"))
            {
                return new DrawLine();

            }
            else if (command.Equals("moveto"))
            {
                return new MoveTo();

            }
            else if (command.Equals("clear"))
            {
                return new Clear();
            }
            else if (command.Equals("reset"))
            {
                return new Reset();
            }
            else
            {
                //if we get here then what has been passed in is inkown so throw an appropriate exception
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + command + " does not exist");
                throw argEx;
            }


        }
    }
}
