using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLEnvironment
{
    class CommandLineCommand : Command
    {

        String command;

        public CommandLineCommand(String command)
        {
            this.command = command;
        }

        public string getCommand()
        {
            return command;
        }
        public void functionality()
        {
        }

        public void setCommand(string command)
        {
            this.command = command;
        }
    }
}
