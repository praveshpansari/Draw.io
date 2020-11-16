using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLEnvironment
{
    interface Command
    {
        void setCommand(String command);
        string getCommand();
        void functionality();
    }
}
