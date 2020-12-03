using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace AssignmentASE
{
    public class Parser
    {
        Painter p;
        String error;

        public Parser(Painter p)
        {
            this.p = p;
            error = "";
        }


        public void parseCommand(string input, int lineNum)
        {
            input = input.ToLower().Trim();
            string[] token = input.Split(' ');
            string command = token[0];
            string[] parameters = new string[0];
            if (token.Length > 1) { parameters = token[1].Split(','); }



            if (token[0].Equals("tocenter"))
            {
                p.Center();
            }

            else if (command.Equals("fill"))
            {
                if (parameters.Length == 1)
                {
                    try { p.SetFill(parameters[0]); }
                    catch (Exception c)
                    {
                        error += "[" + DateTime.Now.ToString("T") + "] " + c.Message;
                        error += (lineNum != 0) ? " at line " + lineNum : "";
                        error += ".\r\n";
                    }
                }
                else
                {
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }

            else if (command.Equals("pen"))
            {
                if (parameters.Length == 1)
                {
                    try { p.SetColor(parameters[0]); }
                    catch (Exception c)
                    {
                        error += "[" + DateTime.Now.ToString("T") + "] " + c.Message;
                        error += (lineNum != 0) ? " at line " + lineNum : "";
                        error += ".\r\n";
                    }
                }
                else
                {
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }

            else if (command.Equals("drawto") || command.Equals("moveto") || command.Equals("clear") || command.Equals("reset"))
            {
                int[] parametersInt = new int[0];
                if (parameters.Length > 0)
                {
                    parametersInt = new int[parameters.Length];
                    try
                    {
                        for (int i = 0; i < parameters.Length; ++i)
                            parametersInt[i] = Int32.Parse(parameters[i]);

                    }
                    catch (FormatException)
                    {
                        error += "[" + DateTime.Now.ToString("T") + "] " + "Parameters not valid for this command";
                        error += (lineNum != 0) ? " at line " + lineNum : "";
                        error += ".\r\n";
                    }

                }
                try
                {
                    p.ExecuteCommand(command, parametersInt);
                }
                catch (IndexOutOfRangeException)
                {
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command.";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }

            else if (command.Equals("circle") || command.Equals("square") || command.Equals("rect") || command.Equals("triangle"))
            {
                int[] parametersInt = new int[parameters.Length];
                try
                {
                    for (int i = 0; i < parameters.Length; ++i)
                        parametersInt[i] = Int32.Parse(parameters[i]);
                    p.DrawShape(command, parametersInt);
                }
                catch (FormatException)
                {
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Parameters not valid for this command";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
                catch (IndexOutOfRangeException)
                {
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command.";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }
            else
            {
                error += "[" + DateTime.Now.ToString("T") + "] " + "Command not recognized";
                error += (lineNum != 0) ? " at line " + lineNum : "";
                error += ".\r\n";
            }


        }

        public void parseEditor(string input)
        {
            string[] lines = input.Split('\n');
            for (int lineNum = 0; lineNum < lines.Length; lineNum++)
            {
                if (!String.IsNullOrWhiteSpace(lines[lineNum]))
                {
                    parseCommand(lines[lineNum], lineNum + 1);
                }
            }
        }

        public void displayError()
        {
            if (error != "")
            {
                p.WriteError(error); error = "";
            }
        }
    }
}