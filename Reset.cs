using System;

namespace AssignmentASE
{
    /// <summary>
    /// Reset Command class inherited from <see cref="Command"/>
    /// </summary>
    /// <remarks>
    /// Allows to Execute the reset command and place the cursor to top left
    /// </remarks>
    public class Reset : Command
    {
        public override void Execute()
        {
            x = 0;
            y = 0;
        }

        public override string GetLog()
        {
            return "[" + DateTime.Now.ToString("T") + "] " + "Cursor position reset to top left.\r\n";
        }
    }
}
