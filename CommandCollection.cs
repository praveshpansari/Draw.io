using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLEnvironment
{
    class CommandCollection
    {
        LinkedList<String> commands;

        public CommandCollection()
        {
            commands = new LinkedList<String>();
            commands.AddLast((new CommandLineCommand("moveTo")).getCommand());
            commands.AddLast(new CommandLineCommand("drawTo").getCommand());
            commands.AddLast(new CommandLineCommand("clear").getCommand());
            commands.AddLast(new CommandLineCommand("reset").getCommand());
            commands.AddLast(new CommandLineCommand("rectangle").getCommand());
            commands.AddLast(new CommandLineCommand("circle").getCommand());
            commands.AddLast(new CommandLineCommand("triangle").getCommand());
            commands.AddLast(new CommandLineCommand("pen").getCommand());
            commands.AddLast(new CommandLineCommand("fill").getCommand());
        }

        public void addCommand(CommandLineCommand command)
        {
            commands.AddLast(command.getCommand());
        }

        public Boolean containsCommand(String command)
        {
            return commands.Contains(command);
        }

    }
}
