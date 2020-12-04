using System;
using System.Collections.Generic;

namespace AssignmentASE
{
    /// <summary>
    /// Responsible for parsing commands from command line and code editor
    /// </summary>
    public class Parser
    {
        // The painter object where apporopriate methods are called
        Painter p;
        String error;
        // A dictionary for stroring variables
        public Dictionary<string, int> Variables { get; set; }


        /// <summary>
        /// Parameterized Constructor to initialize the painter object
        /// </summary>
        /// <param name="p">The painter object which knows where to draw on</param>
        public Parser(Painter p)
        {
            this.p = p;
            error = "";
            Variables = new Dictionary<string, int>();
        }

        /// <summary>
        /// Parses a single string input
        /// </summary>
        /// <param name="input">The string to be parsed</param>
        /// <param name="lineNum">The current line number</param>
        /// <remarks>The line number is 0 when a single command is to be parsed</remarks>
        public void parseCommand(string input, int lineNum)
        {
            // Tidy the input string
            input = input.ToLower().Trim();

            // Split the input on a single space into tokens
            string[] token = input.Split(' ');

            // Extract the command from the tokens
            string command = token[0];

            // Create a new empty string array for parameters
            string[] parameters = new string[0];

            // If parameters are more than one, then add them to the array by splitting the rest of the token on ','
            if (token.Length > 1) { parameters = token[1].Split(','); }

            // If the command is tocenter
            if (command.Equals("tocenter"))
            {
                // Call the center method in painter
                p.Center();
            }

            // If the command is fill
            else if (command.Equals("fill"))
            {
                // Check if paramter count is correct
                if (parameters.Length == 1)
                {   // Try to set the fill
                    try { p.SetFill(parameters[0]); }
                    catch (ArgumentException c)
                    {
                        // If exception caught then display error for wrong parameter
                        error += "[" + DateTime.Now.ToString("T") + "] " + c.Message;
                        error += (lineNum != 0) ? " at line " + lineNum : "";
                        error += ".\r\n";
                    }
                }
                else
                {   // Display error for invalid number of parameters
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }

            // If command is pen
            else if (command.Equals("pen"))
            {
                // Check if paramter count is correct
                if (parameters.Length == 1)
                {
                    // Try to set the color
                    try { p.SetColor(parameters[0]); }
                    catch (ArgumentException c)
                    {
                        // If exception caught then display error for wrong parameter
                        error += "[" + DateTime.Now.ToString("T") + "] " + c.Message;
                        error += (lineNum != 0) ? " at line " + lineNum : "";
                        error += ".\r\n";
                    }
                }
                else
                {
                    // Display error for invalid number of parameters
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }
            // If command is drawto, moveto, clear, reset
            else if (command.Equals("drawto") || command.Equals("moveto") || command.Equals("clear") || command.Equals("reset"))
            {
                // initialize parameters list
                int[] parametersInt = new int[0];

                // If there are parmaters
                if (parameters.Length > 0)
                {
                    // Set the length of the parameter list
                    parametersInt = new int[parameters.Length];
                    try
                    {
                        // Try to parse all parameter to integer and store in list
                        for (int i = 0; i < parameters.Length; ++i)
                            parametersInt[i] = Int32.Parse(parameters[i]);

                    }
                    catch (FormatException)
                    {
                        // Catch Exception and display invalid parameters errpr
                        error += "[" + DateTime.Now.ToString("T") + "] " + "Parameters not valid for this command";
                        error += (lineNum != 0) ? " at line " + lineNum : "";
                        error += ".\r\n";
                    }

                }
                try
                {   // Try to execute the command
                    p.ExecuteCommand(command, parametersInt);
                }
                catch (IndexOutOfRangeException)
                {
                    // Catch Exception and display incorrect number of parameters error
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command.";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }

            // if the command is circle, square, rect, triangle
            else if (command.Equals("circle") || command.Equals("square") || command.Equals("rect") || command.Equals("triangle"))
            {
                // Set the length of the parameter list
                int[] parametersInt = new int[parameters.Length];
                try
                {   // Try to parse all parameter to integer and store in list
                    for (int i = 0; i < parameters.Length; ++i)
                        parametersInt[i] = Int32.Parse(parameters[i]);
                    p.DrawShape(command, parametersInt);
                }
                catch (FormatException)
                {
                    // Catch Exception and display invalid parameters errpr
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Parameters not valid for this command";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
                catch (IndexOutOfRangeException)
                {
                    // Catch Exception and display incorrect number of parameters error
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Incorrect number of parameters for this command.";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
            }
            else
            {
                // If the command is not recognized show error
                error += "[" + DateTime.Now.ToString("T") + "] " + "Command not recognized";
                error += (lineNum != 0) ? " at line " + lineNum : "";
                error += ".\r\n";
            }
        }

        /// <summary>
        /// Parses the text form the code editor
        /// </summary>
        /// <param name="input">The text from the code editor</param>
        /// <remarks>Uses <see cref="parseCommand(string, int)"/></remarks>
        public void parseEditor(string input)
        {
            // Split the input on new line into lines
            string[] lines = input.Split('\n');

            // For each line
            for (int lineNum = 0; lineNum < lines.Length; lineNum++)
            {
                // If the line is not blank or null
                if (!String.IsNullOrWhiteSpace(lines[lineNum]))
                {
                    // Call the parse command method passing the line , and the line num + 1
                    parseCommand(lines[lineNum], lineNum + 1);
                }
            }
        }

        /// <summary>
        /// Displays the errors encountered
        /// </summary>
        /// /// <remarks>Uses <see cref="Painter.WriteError(string)"/></remarks>
        public void displayError()
        {
            // IF there are errors
            if (error != "")
            {
                // Call the write error method
                p.WriteError(error); error = "";
            }
        }
    }
}