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

            if (token.Length == 1)
            {
                if (token[0].Equals("reset")) { p.Reset(); }
                else if (token[0].Equals("clear")) p.Clear();
                else if (token[0].Equals("tocenter")) { p.Center(); }
                else
                {
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Command not recognized";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                    return;
                }
            }
            else if (token.Length > 1)
            {
                string command = token[0];
                string[] parameters = token[1].Split(',');

                if (command.Equals("fill"))
                {
                    if (parameters.Length == 1)
                    {
                        p.SetFill(parameters[0]);
                    }
                    else
                    {
                        error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command";
                        error += (lineNum != 0) ? " at line " + lineNum : "";
                        error += ".\r\n";
                    }
                }

                else if (command.Equals("color"))
                {
                    if (parameters.Length == 1)
                    {
                        p.SetColor(parameters[0]);
                    }
                    else
                    {
                        error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command";
                        error += (lineNum != 0) ? " at line " + lineNum : "";
                        error += ".\r\n";
                    }
                }

                else if (command.Equals("moveto"))
                {
                    if (parameters.Length == 2)
                    {
                        int[] parametersInt = new int[parameters.Length];

                        try
                        {
                            for (int i = 0; i < parameters.Length; ++i)
                                parametersInt[i] = Int32.Parse(parameters[i]);
                            p.MoveTo(parametersInt[0], parametersInt[1]);
                        }
                        catch (FormatException)
                        {
                            error += "[" + DateTime.Now.ToString("T") + "] " + "Parameters not valid for this command";
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

                else if (command.Equals("drawto"))
                {
                    if (parameters.Length == 2)
                    {
                        int[] parametersInt = new int[parameters.Length];

                        try
                        {
                            for (int i = 0; i < parameters.Length; ++i)
                                parametersInt[i] = Int32.Parse(parameters[i]);
                            p.DrawTo(parametersInt[0], parametersInt[1]);
                        }
                        catch (FormatException)
                        {
                            error += "[" + DateTime.Now.ToString("T") + "] " + "Parameters not valid for this command";
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
                }
                else
                {
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Command not recognized";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }
            else
            {
                error += "[" + DateTime.Now.ToString("T") + "] " + "Invalid command and parameters format";
                error += (lineNum != 0) ? " at line " + lineNum : "";
                error += ".\r\n";
            }
            p.DrawCursor();
        }

        public void parseEditor(string input)
        {
            p.Clear();
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