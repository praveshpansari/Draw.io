using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace AssignmentASE
{

    /// <summary>
    /// An enum which contains all the required keywords and types for the language.
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// The type for the if statement
        /// </summary>
        IF,
        /// <summary>
        /// The type for ending an if statement
        /// </summary>
        ENDIF,
        /// <summary>
        /// The type for a while loop
        /// </summary>
        WHILE,
        /// <summary>
        /// The type for ending a while loop
        /// </summary>
        ENDWHILE,
        /// <summary>
        /// The type for creating a method
        /// </summary>
        METHOD,
        /// <summary>
        /// The type for variable names or function names
        /// </summary>
        IDENTIFIER,
        /// <summary>
        /// The type for arithmetic and comparisn operators
        /// </summary>
        OPERATOR,
        /// <summary>
        /// The type for numericals
        /// </summary>
        NUMBER,
    }

    /// <summary>
    /// This class takes in a enum type and the corresponding value to the type.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The type of the token
        /// </summary>
        public Type type;
        /// <summary>
        /// The value of the token if it is a string
        /// </summary>
        public string keyword;
        /// <summary>
        /// THe value of the token if it is a number
        /// </summary>
        public double numerical;

        /// <summary>
        /// A parameterized constructor for Token, where the value of token is a string.
        /// </summary>
        /// <param name="type">Type of the token</param>
        /// <param name="keyword">A string of the value</param>
        public Token(Type type, string keyword)
        {
            this.type = type;
            this.keyword = keyword;
        }

        /// <summary>
        /// Get the value of the current token
        /// </summary>
        /// <returns>A string containing the value of the token</returns>
        public String GetValue()
        {
            return keyword;
        }

        /// <summary>
        /// Get the type of the token
        /// </summary>
        /// <returns>The Type<see cref="Type"/> of the token</returns>
        public Type GetTokenType()
        {
            return this.type;
        }

        /// <summary>
        /// This method coverts this class to a String in a specific format.
        /// </summary>
        /// <returns>A string displaying token and value</returns>
        public override String ToString()
        {
            return type.ToString() + ": " + keyword;
        }
    }


    /// <summary>
    /// This class takes in a string input and produces tokens based on provided enum type.
    /// </summary>
    public class Lexer
    {
        // A variable for storing the current encountered identifier.
        String curText;

        /// <summary>
        /// This function splits a given string on spaces and the runs a loop for each word, thereafter 
        /// a loop for each character. Adds required tokens to a list whenever whitespace is not encountered.
        /// </summary>
        /// <param name="commandLine">A string to be tokenised for the Parser.</param>
        /// <returns>A list of tokens containg tokens for the given string</returns>
        public List<Token> Advance(string commandLine)
        {
            // A list of tokens
            List<Token> tokens = new List<Token>();
            // Split the string after tidying up on spaces and store in line
            string line = commandLine.ToLower().Trim();

            // A loop for each word in line
            for (int num = 0; num < line.Length; num++)
            {
                // Initialize the identifier text
                curText = "";


                // The last character in the word
                // Increment index

                // Set last char as the char at num index in word
                char LastChar = line[num];

                // If a letter is encountered
                if (Char.IsLetter(LastChar))
                {
                    do
                    {
                        // Add that letter to the identifier variable
                        curText += LastChar;
                        if (num < line.Length - 1)
                        {
                            // Increment index
                            num++;
                            // Set last char as the char at num index in word
                            LastChar = line[num];
                        }
                        else break;
                    } while (Char.IsLetter(LastChar) && num < line.Length);


                    // Switch the current identifier
                    switch (curText)
                    {
                        // Identifier is "if"
                        case "if":
                            // Then a IF type token is added to the list
                            tokens.Add(new Token(Type.IF, "if"));
                            break;
                        // Identifier is "endif"
                        case "endif":
                            // Then a ENDIF type token is added to the list
                            tokens.Add(new Token(Type.ENDIF, "endif"));
                            break;
                        // Identifier is "function"
                        case "method":
                            // Then a FUNCTION type token is added to the list
                            tokens.Add(new Token(Type.METHOD, "method"));
                            break;
                        // Identifier is "while"
                        case "while":
                            // Then a WHILE type token is added to the list
                            tokens.Add(new Token(Type.WHILE, "while"));
                            break;
                        // Identifier is "endwhile"
                        case "endwhile":
                            // Then a WHILE type token is added to the list
                            tokens.Add(new Token(Type.ENDWHILE, "endwhile"));
                            break;
                        // Identifier is none of the above
                        default:
                            // Then the token must be an identifer, hence a IDENTIFIER type token is added to the list
                            tokens.Add(new Token(Type.IDENTIFIER, curText));
                            break;
                    }
                }

                // If a digit is encountered or a decimal point
                if (Char.IsDigit(LastChar) || LastChar == '.')
                {
                    // A temporary buffer string for storing all the numbers
                    string NumStr = "";

                    // While the character is digit or a decimal and are not at the end of the word
                    while ((Char.IsDigit(LastChar) || LastChar == '.') && num < line.Length - 1)
                    {
                        // Add the current character to the buffer
                        NumStr += LastChar;
                        // Increment index
                        num++;
                        // Set last char as the char at num index in word
                        LastChar = line[num];

                    }

                    // If the last char is digit or a decimal point
                    if ((Char.IsDigit(LastChar) || LastChar == '.'))
                    {
                        // Add the current character to the buffer
                        NumStr += LastChar;
                    }

                    // Add a NUMBER type token to the list
                    tokens.Add(new Token(Type.NUMBER, NumStr));
                }

                // If a symbol is encountered
                if (new Regex(@"[-%+/!*=<>]", RegexOptions.Compiled).IsMatch(LastChar.ToString()))
                {

                    // A temporary buffer string for storing all the symbols
                    string op = "";
                    do
                    {
                        // Add that symbol to the variable
                        op += LastChar;
                        if (num < line.Length - 1)
                        {
                            // Increment index
                            num++;
                            // Set last char as the char at num index in word
                            LastChar = line[num];
                        }
                        else { num++; break; }

                    }
                    // Do this until symbols encountered and stop at end
                    while (new Regex(@"[-%+/!*=<>]", RegexOptions.Compiled).IsMatch(LastChar.ToString()) && num < line.Length);

                    num--;

                    // Add a OPERATOR type token to the list
                    tokens.Add(new Token(Type.OPERATOR, op));
                }
            }
            // Return the list of tokens
            return tokens;
        }
    }
}
