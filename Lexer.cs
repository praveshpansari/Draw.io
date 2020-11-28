using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace PLEnvironment
{
    /// <summary>
    /// This class takes in a string input and produces tokens based on provided enum type.
    /// </summary>
    class Lexer
    {
        // A variable for storing the current encountered identifier.
        String curText;
        // A variable for storing the current encountered number.
        double curNumber;

        /// <summary>
        /// An enum which contains all the required keywords and types for the language.
        /// </summary>
        public enum Type
        {
            IF = 0,
            ELSEIF = 1,
            ELSE = 2,
            WHILE = 3,
            FUNCTION = 4,
            IDENTIFIER = 5,
            OPERATOR = 6,
            BRACKET = 7,
            VARIABLE = 8,
            NUMBER = 9,
            COMPARE = 10,
            ERROR = 11
        }

        /// <summary>
        /// An enum for error type.
        /// </summary>
        public enum error
        {
            INVALID_COMMAND = 0x01
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

            /// <summary>
            /// A parameterized constructor for Token, where the value of token is a number.
            /// </summary>
            /// <param name="type">Type of the token</param>
            /// <param name="numerical">A string of the value</param>
            public Token(Type type, double numerical)
            {
                this.type = type;
                this.numerical = numerical;
            }

            /// <summary>
            /// This method coverts this class to a String in a specific format.
            /// </summary>
            /// <returns>A string displaying token and value</returns>
            public String toString()
            {
                if (keyword != null)
                    return type.ToString() + ": " + keyword;
                else
                    return type.ToString() + ": " + numerical;
            }
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
            string[] line = commandLine.ToLower().Trim().Split(' ');

            // A loop for each word in line
            foreach (string word in line)
            {
                // The last character in the word
                Char LastChar = ' ';
                // The current index in the word
                int num = -1;
                // Initialize the identifier text
                curText = "";
                // Initialize the number
                curNumber = 0;

                // A loop that runs till the end of the word
                while (num < word.Length - 1)
                {
                    // Increment index
                    num++;
                    // Set last char as the char at num index in word
                    LastChar = word[num];

                    // If a letter is encountered
                    if (Char.IsLetter(LastChar))
                    {
                        // Add that letter to the identifier variable
                        curText += LastChar;

                        // While we keep on encountering letters or are at the end of the word
                        while (Char.IsLetter(LastChar) && num < word.Length - 1)
                        {
                            // Increment index
                            num++;
                            // Set last char as the char at num index in word
                            LastChar = word[num];
                            // Add that letter to the identifier variable
                            curText += LastChar;
                        }

                        // Switch the current identifier
                        switch (curText)
                        {
                            // Identifier is "if"
                            case "if":
                                // Then a IF type token is added to the list
                                tokens.Add(new Token(Type.IF, "if"));
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
                            // Identifier is "var"
                            case "var":
                                // Then a VARIABLE type token is added to the list
                                tokens.Add(new Token(Type.VARIABLE, "var"));
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
                        while ((Char.IsDigit(LastChar) || LastChar == '.') && num < word.Length - 1)
                        {
                            // Add the current character to the buffer
                            NumStr += LastChar;
                            // Increment index
                            num++;
                            // Set last char as the char at num index in word
                            LastChar = word[num];

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
                        tokens.Add(new Token(Type.NUMBER, curNumber));
                    }

                    // If a symbol is encountered
                    if (Char.IsSymbol(LastChar))
                    {
                        // Add a OPERATOR type token to the list
                        tokens.Add(new Token(Type.OPERATOR, LastChar.ToString()));
                    }

                    // If a punctuation is encountered
                    if (Char.IsPunctuation(LastChar))
                    {
                        // Add a BRACKET type token to the list
                        tokens.Add(new Token(Type.BRACKET, LastChar.ToString()));
                    }
                }
            }
            // Return the list of tokens
            return tokens;
        }
    }
}
