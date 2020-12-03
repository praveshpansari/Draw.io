using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    class CommandFactory
    {
        public Command getCommand(String command)
        {
            command = command.ToLower().Trim(); //yoi could argue that you want a specific word string to create an object but I'm allowing any case combination


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
