using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AssignmentASE
{
    /// <summary>
    /// Responsible for parsing commands from command line and code editor
    /// </summary>
    public class Parser
    {
        // The painter object where apporopriate methods are called
        readonly Painter p;

        String error;

        // The lexer for tokenizing input
        readonly Lexer lexer;

        /// <summary>
        /// Gets or sets the dictionary of variables.
        /// </summary>
        /// <value>
        /// Contains the name and the value of variables.
        /// </value>
        public Dictionary<string, string> Variables { get; set; }

        /// <summary>
        /// Parameterized Constructor to initialize the painter object
        /// </summary>
        /// <param name="p">The painter object which knows where to draw on</param>
        public Parser(Painter p)
        {
            this.p = p;
            this.lexer = new Lexer();
            error = "";
            Variables = new Dictionary<string, string>();
        }

        /// <summary>
        /// Parses a single string input
        /// </summary>
        /// <param name="input">The string to be parsed</param>
        /// <param name="lineNum">The current line number</param>
        /// <remarks>The line number is 0 when a single command is to be parsed</remarks>
        public void ParseCommand(string input, int lineNum)
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
                    catch (InvalidParameterException c)
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
                    catch (InvalidParameterException c)
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
                            if (Variables.ContainsKey(parameters[i]))
                                parametersInt[i] = Int32.Parse(Variables[parameters[i]]);
                            else
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
                    {
                        if (Variables.ContainsKey(parameters[i]))
                            parametersInt[i] = Int32.Parse(Variables[parameters[i]]);
                        else
                            parametersInt[i] = Int32.Parse(parameters[i]);
                    }
                    p.DrawShape(command, parametersInt);
                }
                catch (FormatException)
                {
                    // Catch Exception and display invalid parameters errpr
                    error += "[" + DateTime.Now.ToString("T") + "] " + "Parameters not valid for this command";
                    error += (lineNum != 0) ? " at line " + lineNum : "";
                    error += ".\r\n";
                }
                catch (InvalidParameterException)
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
        /// Parses a single expression statement
        /// </summary>
        /// <param name="input">The string to be parsed</param>
        /// <param name="lineNum">The current line number</param>
        /// <exception cref="IdentifierNotDefinedException"></exception>
        /// <exception cref="InvalidParameterException"></exception>
        /// <remarks>The line number is 0 when a single command is to be parsed</remarks>
        public void ParseUsingLexer(string input, int lineNum)
        {
            // Get tokens from lexer
            var tokens = lexer.Advance(input);
            // Initialize variable name
            string variable_name = "";

            try
            {
                // If tokens less than 3 means invalid assignemnt statement
                if (tokens.Count >= 3)
                {
                    // Loop for each tokens
                    for (int i = 0; i < tokens.Count; i++)
                    {
                        var t = tokens[i];
                        // A list for storing all numbers in an expression
                        List<int> numbersList = new List<int>();
                        // Queue for storing the operators
                        var operators = new Queue<string>();

                        // If the index is 0
                        if (i == 0)
                        {
                            // If the 1st token is identifier
                            if (t.GetTokenType() == Type.IDENTIFIER)
                                // Get the name of the variable
                                variable_name = t.GetValue();
                            else
                                // Else throw an exception as no variable in assignment
                                throw new InvalidParameterException("'" + t.GetValue() + "' cannot be defined,");
                        }

                        // If an exprssion has multiple operations
                        if (t.GetTokenType() == Type.OPERATOR && t.GetValue() == "=" && tokens.Count > 3)
                        {
                            // Loop all the tokens after the assignment operator
                            foreach (var _token in tokens.GetRange(2, tokens.Count - 2))
                            {
                                // If encountered a variable
                                if (_token.GetTokenType() == Type.IDENTIFIER)
                                {
                                    try
                                    {
                                        // Try to add the number from the Variables dictionary to the number list
                                        numbersList.Add(int.Parse(Variables[_token.GetValue()]));
                                    }
                                    catch (KeyNotFoundException)
                                    {
                                        // If the identifier is not recognized show error
                                        error += "[" + DateTime.Now.ToString("T") + "] '" + _token.GetValue() + "' is not defined.";
                                        error += (lineNum != 0) ? " at line " + lineNum : "";
                                        error += ".\r\n";
                                    }
                                }

                                // If encountered a numerical
                                if (_token.GetTokenType() == Type.NUMBER)
                                    // Add the number to the list
                                    numbersList.Add(int.Parse(_token.GetValue()));
                                // If encountered an operator
                                if (_token.GetTokenType() == Type.OPERATOR)
                                    // Add the operator to the queue
                                    operators.Enqueue(_token.GetValue());
                            }

                            // Initialize result as the first element
                            var result = numbersList[0];

                            // For each number in the list - 1
                            for (int j = 1; j < numbersList.Count; j++)
                            {
                                // Dequeue an operator from the queue and switch it
                                switch (operators.Dequeue())
                                {
                                    // Modulo
                                    case "%":
                                        result %= numbersList[j];
                                        break;
                                    // Addition
                                    case "+":
                                        result += numbersList[j];
                                        break;
                                    // Subtraction
                                    case "-":
                                        result -= numbersList[j];
                                        break;
                                    // Division
                                    case "/":
                                        result /= numbersList[j];
                                        break;
                                    // Multiplication
                                    case "*":
                                        result *= numbersList[j];
                                        break;
                                }
                            }

                            // If the variable does not already exists
                            if (!Variables.ContainsKey(variable_name))
                                // Create a new entry in the dictionary for this variable
                                Variables.Add(variable_name, result.ToString());
                            else
                                // Else reassign the variable
                                Variables[variable_name] = result.ToString();

                            break;
                        }

                        // If the last toke in identifier
                        if (t.GetTokenType() == Type.IDENTIFIER && i > 0)
                        {
                            // If the variable exists
                            if (!Variables.ContainsKey(variable_name))
                            {
                                try
                                {
                                    // Try to get and the value of the operand
                                    Variables.Add(variable_name, Variables[t.GetValue()]);
                                }
                                catch (KeyNotFoundException)
                                {
                                    // If the identifier is not recognized show error
                                    throw new IdentifierNotDefinedException("'" + t.GetValue() + "' is not defined,");
                                }
                            }
                            else
                            {
                                try
                                {
                                    // Reassign the variable if it is a new variable
                                    Variables[variable_name] = Variables[t.GetValue()];
                                }
                                catch (KeyNotFoundException)
                                {
                                    // If the identifier is not recognized show error
                                    throw new IdentifierNotDefinedException("'" + t.GetValue() + "' is not defined,");
                                }
                            }
                        }

                        // If the last token is number
                        if (t.GetTokenType() == Type.NUMBER && i > 0)
                        {
                            // If the variable doesn't exists
                            if (!Variables.ContainsKey(variable_name))
                            {
                                // Add a new entry to the dictionary
                                Variables.Add(variable_name, t.GetValue());
                            }
                            else
                            {
                                // REassign the existing variable
                                Variables[variable_name] = t.GetValue();
                            }
                        }
                    }
                }
                else
                {
                    // If token length is less than 3 throw an exception
                    throw new InvalidParameterException("Assignment statement format is invalid,");
                }
            }
            catch (Exception c)
            {
                // Catch each exception and display apt messages
                error += "[" + DateTime.Now.ToString("T") + "] " + c.Message;
                error += (lineNum != 0) ? " at line " + lineNum : "";
                error += ".\r\n";
            }
        }

        /// <summary>
        /// Parses a single if statement
        /// </summary>
        /// <param name="input">The string to be parsed</param>
        /// <param name="lineNum">The current line number</param>
        /// <returns>Boolean indicating wheter the statement is true or false</returns>
        /// <remarks>The line number is 0 when a single command is to be parsed</remarks>
        public bool ParseUsingIf(string input, int lineNum)
        {
            // Get all the tokens for the line
            var tokens = lexer.Advance(input);
            // Initialize result
            bool result = false;

            // Check if command is correct
            if (tokens[0].GetTokenType() != Type.IF && tokens[0].GetTokenType() != Type.WHILE)
            {
                // Log error as keyword not matched
                error += "[" + DateTime.Now.ToString("T") + "] Invalid command '" + tokens[0].GetValue() + "',";
                error += " at line " + (lineNum + 1);
                error += ".\r\n";
                return result;
            }

            // operation variable
            string op = "";
            // A list for storing operands
            var numbersList = new List<int>();

            try
            {
                // Try to loop tokens after the 'if' keyword upto count 3
                foreach (var _token in tokens.GetRange(1, 3))
                {
                    // If the token is identifier
                    if (_token.GetTokenType() == Type.IDENTIFIER)
                        // Add the value of that identifier to the number list
                        numbersList.Add(int.Parse(Variables[_token.GetValue()]));
                    // If the token in a number
                    if (_token.GetTokenType() == Type.NUMBER)
                        // Add the number to the number list
                        numbersList.Add(int.Parse(_token.GetValue()));
                    // If the token is an operator
                    if (_token.GetTokenType() == Type.OPERATOR)
                        // Assign the operator var
                        op = _token.GetValue();
                }

                // Left side of the condition
                int left = numbersList[0];
                // Right side of the condition
                int right = numbersList[1];

                // Switch the operation
                switch (op)
                {
                    // Less than
                    case "<":
                        result = left < right;
                        break;
                    // Greater than
                    case ">":
                        result = left > right;
                        break;
                    // Less than equals to
                    case "<=":
                        result = left <= right;
                        break;
                    // Greater than equals to
                    case ">=":
                        result = left >= right;
                        break;
                    // Is equal to
                    case "==":
                        result = left == right;
                        break;
                    // Not equal to
                    case "!=":
                        result = left != right;
                        break;
                    // Throw exception if not recognzed operation
                    default:
                        throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                // Show missing argument if invalid operation or numbers
                error += "[" + DateTime.Now.ToString("T") + "] Missing or invalid arguments given";
                error += (lineNum != 0) ? " at line " + lineNum : "";
                error += ".\r\n";
            }
            catch (KeyNotFoundException)
            {
                // If the identifier is not recognized show error
                error += "[" + DateTime.Now.ToString("T") + "] One or more variable are not defined.";
                error += (lineNum != 0) ? " at line " + lineNum : "";
                error += ".\r\n";
            }
            // Return the result
            return result;
        }

        /// <summary>
        ///  Parses a method definition statement
        /// </summary>
        /// <param name="lines">The text from the text editor split on return</param>
        /// <param name="lineNum">The line number where the method decleration has started</param>
        /// <returns>The line number where the function ends</returns>
        public int ParseUsingMethod(string[] lines, int lineNum)
        {
            // Get tokens for the line
            var tokens = lexer.Advance(lines[lineNum]);

            // Empty string for storing the formal params
            string formalParam = "";
            // Get the lineNum for the current function start
            int functionLineNum = lineNum;

            // Check if command is correct
            if (tokens[0].GetTokenType() != Type.METHOD)
            {
                // Log error as keyword not matched
                error += "[" + DateTime.Now.ToString("T") + "] Invalid command '" + tokens[0].GetValue() + "',";
                error += " at line " + (lineNum + 1);
                error += ".\r\n";
                return functionLineNum;
            }

            try
            {
                // Try to loop the tokens after the method keyword & name
                foreach (var _token in tokens.GetRange(2, tokens.Count - 2))
                {
                    // If type is identifier add to the string seperated by '|'
                    if (_token.GetTokenType() == Type.IDENTIFIER)
                    {
                        formalParam += _token.GetValue() + "|";
                    }
                }

                // Flag for method is ended
                bool isEndMethod = false;

                // For all the lines
                for (; functionLineNum < lines.Length; functionLineNum++)
                {
                    // If encountered an endmethod
                    if (lines[functionLineNum].Contains("endmethod"))
                    {
                        // Set flag to true
                        isEndMethod = true;
                        break;
                    }
                }

                // If no endmethod then throw exception
                if (!isEndMethod) throw new InvalidParameterException("Function not ended properly");

                // If no params remove the last '|'
                if (!formalParam.Equals(""))
                    formalParam = formalParam.Remove(formalParam.Length - 1);

                // If function name is a number throw exception
                if (tokens[1].GetTokenType() == Type.NUMBER) throw new InvalidParameterException("Function name cannot be a number");

                // Add the method to variable dictionary along with the linNum,endFunctionLine and the params
                Variables.Add(tokens[1].GetValue(), lineNum + "," + (functionLineNum - 1) + "," + formalParam);
            }
            catch (InvalidParameterException e)
            {
                // Catch exception and display relevant message
                error += "[" + DateTime.Now.ToString("T") + "] " + e.Message + ",";
                error += " at line " + (lineNum + 1);
                error += ".\r\n";
            }
            catch (ArgumentOutOfRangeException)
            {
                // If the method declaration is invalid
                error += "[" + DateTime.Now.ToString("T") + "] Invalid number of arguments for this command,";
                error += " at line " + (lineNum + 1);
                error += ".\r\n";
            }
            return functionLineNum;
        }

        /// <summary>
        /// Handles the end of a method
        /// </summary>
        /// <param name="currentFunction">The name of the current called function</param>
        /// <param name="cursor">Cursor for going back to line number after completing a function</param>
        /// <param name="lineNum">The lineNum where the endmethod is</param>
        /// <returns>The lineNum where the original function was called</returns>
        public int ParseEndMethod(string currentFunction, int cursor, int lineNum)
        {
            // If the cursor is not 0 and their is a currentfunction
            if (!currentFunction.Equals("") && cursor != 0)
            {
                // Get the parameters name from the function
                var formalParam = Variables[currentFunction].Split(',')[2].Split('|');
                // Loop for each paramerter
                foreach (var _parameter in formalParam)
                {
                    // Remove parameter from the variables table
                    Variables.Remove(_parameter);
                }
            }
            else
            {
                // Else if no method declaration before a function
                error += "[" + DateTime.Now.ToString("T") + "] No method found before ending a method";
                error += " at line " + (lineNum + 1);
                error += ".\r\n";
            }
            // Return cursor
            return cursor;
        }

        /// <summary>
        /// Parses a function call
        /// </summary>
        /// <param name="tokens">The tokens of the line where function is called</param>
        /// <param name="lineNum">The line number where the function is called</param>
        /// <param name="paramters">The parameters required by the function</param>
        /// <returns>The line number where the function call ends</returns>
        public int ParseMethodCall(List<Token> tokens, int lineNum, LinkedList<string> paramters)
        {
            try
            {
                // Get the function start line, end line and parameters from the dictionary
                var functionLines = Variables[tokens[0].GetValue()].Split(',');
                // Get each paramter after splitting on '|'
                var formalParam = functionLines[2].Split('|');

                // For each tokens after the name
                foreach (var _token in tokens.GetRange(1, tokens.Count - 1))
                {
                    // If argument is number store in paramters
                    if (_token.GetTokenType() == Type.NUMBER)
                        paramters.AddLast(_token.GetValue());
                    // Else if argument is a identfier store its value from variable in the parametrs
                    else if (_token.GetTokenType() == Type.IDENTIFIER)
                        paramters.AddLast(Variables[_token.GetValue()]);
                }

                // If the function call requires parameters
                if (formalParam.Length != 0 && !formalParam[0].Equals(""))
                {
                    // For the length of the paramters
                    for (int i = 0; i < formalParam.Length; i++)
                    {
                        // If the variable is already assigned reassign it
                        if (Variables.ContainsKey(formalParam[i]))
                        {
                            Variables[formalParam[i]] = paramters.ElementAt(i);
                        }
                        else
                            // Else store it as a new entry
                            Variables.Add(formalParam[i], paramters.ElementAt(i));
                    }
                }
                // Return the function start line
                return Int32.Parse(functionLines[0]);
            }
            catch (ArgumentOutOfRangeException)
            {
                // If the function call is not done correctly or parameters wrong
                error += "[" + DateTime.Now.ToString("T") + "] Invalid Number of arguments for this method,";
                error += " at line " + (lineNum + 1);
                error += ".\r\n";

            }
            catch (KeyNotFoundException)
            {
                // If the identifier is not recognized show error
                error += "[" + DateTime.Now.ToString("T") + "] Identifier not recognized or not defined,";
                error += " at line " + (lineNum + 1);
                error += ".\r\n";
            }
            // Return line number if no function found
            return lineNum;
        }

        /// <summary>
        /// Parses the text form the code editor
        /// </summary>
        /// <param name="input">The text from the code editor</param>
        /// <remarks>Uses <see cref="ParseCommand(string, int)"/>, <see cref="ParseUsingIf(string, int)"/>, <see cref="ParseUsingLexer(string, int)"/></remarks>
        public void ParseEditor(string input)
        {

            // Split the input on new line into lines
            string[] lines = input.Split('\n');

            // If a function is running store its name
            string currentFunction = "";

            // List for the current function parameters
            LinkedList<string> paramters = new LinkedList<string>();

            // Cursor for going back to line number after completing a function
            int cursor = 0;

            // For each line
            for (int lineNum = 0; lineNum < lines.Length; lineNum++)
            {
                // If the line is not blank or null
                if (!String.IsNullOrWhiteSpace(lines[lineNum]))
                {
                    // Temp var for the if line number
                    int ifLineNum;

                    // If the lines contains members of set ['=','if','endif','while','method','()']
                    if (lines[lineNum].Contains("=") || lines[lineNum].Contains("if") || lines[lineNum].Contains("endif") || lines[lineNum].Contains("while") || lines[lineNum].Contains("method") || Regex.Match(lines[lineNum], @"(\(.*\))").Success)
                    {
                        // If an endmnethod is encountered
                        if (lines[lineNum].Contains("endmethod"))
                        {
                            lineNum = ParseEndMethod(currentFunction, cursor, lineNum);

                            // Reset currentfunction & cursor
                            currentFunction = "";
                            cursor = 0;
                        }

                        // If there is a method definition
                        else if (lines[lineNum].Contains("method"))
                        {
                            // Set linenum to the end of the function
                            lineNum = ParseUsingMethod(lines, lineNum);
                        }

                        // If there is a method call
                        else if (Regex.Match(lines[lineNum], @"(\(.*\))").Success)
                        {
                            // Clear the current parametrs
                            paramters.Clear();
                            // Set  the cursor to the current linenum
                            cursor = lineNum;
                            // Get the tokens from the line
                            var tokens = lexer.Advance(lines[lineNum]);
                            // Set current function to the first token
                            currentFunction = tokens[0].GetValue();

                            lineNum = ParseMethodCall(tokens, lineNum, paramters);
                        }

                        // If there is a while loop
                        else if (lines[lineNum].Contains("while") && !lines[lineNum].Contains("endwhile"))
                        {
                            // temp line number for the while loop
                            int whileNum = lineNum;

                            // Check if command is correct
                            var temp = lexer.Advance(lines[lineNum]);
                            if (temp[0].GetTokenType() == Type.WHILE)
                            {
                                whileNum++;

                                // While the condition fo while is true
                                while (ParseUsingIf(lines[lineNum], lineNum + 1))
                                {
                                    // If the loop has end loop then set whilenum to linenum for looping
                                    if (lines[whileNum].Contains("endwhile"))
                                    {
                                        whileNum = lineNum;
                                    }
                                    else
                                    {
                                        // If an endmnethod is encountered
                                        if (lines[whileNum].Contains("endmethod"))
                                        {
                                            whileNum = ParseEndMethod(currentFunction, cursor, whileNum);
                                            // Reset currentfunction & cursor
                                            currentFunction = "";
                                            cursor = 0;
                                        }

                                        // If there is a method call
                                        else if (Regex.Match(lines[whileNum], @"(\(.*\))").Success && !lines[whileNum].Contains("method"))
                                        {
                                            // Clear the current parametrs
                                            paramters.Clear();
                                            // Set  the cursor to the current linenum
                                            cursor = whileNum;
                                            // Get the tokens from the line
                                            var tokens = lexer.Advance(lines[whileNum]);
                                            // Set current function to the first token
                                            currentFunction = tokens[0].GetValue();

                                            whileNum = ParseMethodCall(tokens, whileNum, paramters);
                                        }

                                        // If endif is encountered just continue
                                        else if (lines[whileNum].Contains("endif"))
                                        {
                                            continue;
                                        }

                                        // If 'if' is encountered
                                        else if (lines[whileNum].Contains("if"))
                                        {
                                            // If the condition is false
                                            if (!ParseUsingIf(lines[whileNum], lineNum + 1))
                                            {
                                                // A bool flag for 1 line
                                                bool flag = false;
                                                // Store the current linenum in temp var
                                                ifLineNum = whileNum;
                                                // Loop for all the lines
                                                for (; ifLineNum < lines.Length; ifLineNum++)
                                                {
                                                    // If a line contains endif break and set the flag to true
                                                    if (lines[ifLineNum].Contains("endif"))
                                                    {
                                                        flag = true;
                                                        break;
                                                    }
                                                }

                                                // If flag is not set only skip one line else skip till endif
                                                whileNum = flag ? ifLineNum : whileNum + 1;
                                            }
                                        }

                                        // If assignment statement is encountered
                                        else if (lines[whileNum].Contains("="))
                                        {
                                            // Parse using the expression method
                                            ParseUsingLexer(lines[whileNum], whileNum);
                                        }

                                        // Else parse using parsecommand
                                        else
                                            ParseCommand(lines[whileNum], whileNum + 1);

                                    }
                                    // Increment whilenum
                                    whileNum++;
                                }
                                // After condition false, set the linenum to whilenum
                                lineNum = whileNum;
                            }
                            else
                            {
                                // Log error as keyword not matched
                                error += "[" + DateTime.Now.ToString("T") + "] Invalid command '" + temp[0].GetValue() + "',";
                                error += " at line " + (lineNum + 1);
                                error += ".\r\n";
                            }
                        }

                        // If endif is encountered just continue
                        else if (lines[lineNum].Contains("endif"))
                        {
                            continue;
                        }

                        // If 'if' is encountered
                        else if (lines[lineNum].Contains("if"))
                        {
                            // If the condition is false
                            if (!ParseUsingIf(lines[lineNum], lineNum + 1))
                            {
                                // A bool flag for 1 line
                                bool flag = false;
                                // Store the current linenum in temp var
                                ifLineNum = lineNum;
                                // Loop for all the lines
                                for (; ifLineNum < lines.Length; ifLineNum++)
                                {
                                    // If a line contains endif break and set the flag to true
                                    if (lines[ifLineNum].Contains("endif"))
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                // If flag is not set only skip one line else skip till endif
                                lineNum = flag ? ifLineNum : lineNum + 1;
                            }
                        }

                        // If assignment statement is encountered
                        else if (lines[lineNum].Contains("="))
                        {
                            // Parse using the expression method
                            ParseUsingLexer(lines[lineNum], lineNum + 1);
                        }
                    }
                    // Else parse using parsecommand
                    else
                        // Call the parse command method passing the line , and the line num + 1
                        ParseCommand(lines[lineNum], lineNum + 1);
                }
            }
        }


        /// <summary>
        /// Displays the errors encountered
        /// </summary>
        /// /// <remarks>Uses <see cref="Painter.WriteError(string)"/></remarks>
        public void DisplayError()
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