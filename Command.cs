using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLEnvironment
{
    abstract class Command
    {

        protected String command;
        public void setCommand(String command)
        {
            this.command = command;
        }
        public string getCommand()
        {
            return this.command;
        }
        public abstract void functionality();



    }
}
