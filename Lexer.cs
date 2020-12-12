using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace AssignmentASE
{

    /// <summary>
    /// An enum which contains all the required keywords and types for the language.
    /// </summary>
    public enum Type
    {
        IF,
        ELSEIF,
        ELSE,
        ENDIF,
        WHILE,
        ENDWHILE,
        FUNCTION,
        IDENTIFIER,
        OPERATOR,
        BRACKET,
        NUMBER,
        COMPARE,
        ERROR
    }

    /// <summary>
    /// This class takes in a enum type and the corresponding value to the type.
    /// </summary>
    public class Token
    {
        // The type of the token
        public Type type;
        // If it is a String
        public string keyword;
        // If it is a number
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

        public String getValue()
        {
            return keyword;
        }

        public Type getType()
        {
            return this.type;
        }

        /// <summary>
        /// This method coverts this class to a String in a specific format.
        /// </summary>
        /// <returns>A string displaying token and value</returns>
        public String toString()
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
        // A variable for storing the current encountered number.
        double curNumber;

        /// <summary>
        /// An enum for error type.
        /// </summary>
        public enum error
        {
            INVALID_COMMAND = 0x01
        }

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
                // The last character in the word
                Char LastChar = ' ';
                // Initialize the identifier text
                curText = "";
                // Initialize the number
                curNumber = 0;


                // Increment index

                // Set last char as the char at num index in word
                LastChar = line[num];

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
                        // Identifier is "elseif"
                        case "elseif":
                            // Then a ELSEIF type token is added to the list
                            tokens.Add(new Token(Type.ELSEIF, "elseif"));
                            break;
                        // Identifier is "else"
                        case "else":
                            // Then a ELSE type token is added to the list
                            tokens.Add(new Token(Type.ELSE, "else"));
                            break;
                        // Identifier is "function"
                        case "function":
                            // Then a FUNCTION type token is added to the list
                            tokens.Add(new Token(Type.FUNCTION, "function"));
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

                    // Parse the buffer to double and store in number variable
                    curNumber = Double.Parse(NumStr);
                    // Add a NUMBER type token to the list
                    tokens.Add(new Token(Type.NUMBER, NumStr));
                }

                // If a symbol is encountered
                if (new Regex(@"[-+/*=<>]", RegexOptions.Compiled).IsMatch(LastChar.ToString()))
                {

                    // A temporary buffer string for storing all the numbers
                    string op = "";
                    do
                    {
                        // Add that letter to the identifier variable
                        op += LastChar;
                        if (num < line.Length - 1)
                        {
                            // Increment index
                            num++;
                            // Set last char as the char at num index in word
                            LastChar = line[num];
                        }
                        else break;

                    } while (new Regex(@"[-+/*=<>]", RegexOptions.Compiled).IsMatch(LastChar.ToString()) && num < line.Length);
                    num--;
                    // Add a OPERATOR type token to the list
                    tokens.Add(new Token(Type.OPERATOR, op));
                }

                // If a punctuation is encountered
                if (new Regex(@"[()]", RegexOptions.Compiled).IsMatch(LastChar.ToString()))
                {
                    // Add a BRACKET type token to the list
                    tokens.Add(new Token(Type.BRACKET, LastChar.ToString()));
                }
            }
            // Return the list of tokens
            return tokens;
        }
    }
}
