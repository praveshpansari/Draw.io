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
            /// A parameterized constructor for Token, where the value of token is a string.
            /// </summary>
            /// <param name="type"></param>
            /// <param name="numerical"></param>
            public Token(Type type, double numerical)
            {
                this.type = type;
                this.numerical = numerical;
            }

            public String toString()
            {
                if (keyword != null)
                    return type.ToString() + ": " + keyword;
                else
                    return type.ToString() + ": " + numerical;
            }
        }

        public List<Token> Advance(string commandLine)
        {
            List<Token> result = new List<Token>();
            string[] line = commandLine.ToLower().Trim().Split(' ');
            foreach (string word in line)
            {
                Char LastChar = ' ';
                int num = -1;
                curText = "";
                curNumber = 0;
                while (num < word.Length - 1)
                {
                    num++;
                    LastChar = word[num];

                    if (Char.IsLetter(LastChar))
                    {
                        curText += LastChar;
                        while (Char.IsLetter(LastChar) && num < word.Length - 1)
                        {
                            num++;
                            LastChar = word[num];
                            curText += LastChar;
                        }
                        switch (curText)
                        {
                            case "if":
                                result.Add(new Token(Type.IF, "if"));
                                break;
                            case "elseif":
                                result.Add(new Token(Type.ELSEIF, "elseif"));
                                break;
                            case "else":
                                result.Add(new Token(Type.ELSE, "else"));
                                break;
                            case "function":
                                result.Add(new Token(Type.FUNCTION, "function"));
                                break;
                            case "while":
                                result.Add(new Token(Type.WHILE, "while"));
                                break;
                            case "var":
                                result.Add(new Token(Type.VARIABLE, "var"));
                                break;
                            default:
                                result.Add(new Token(Type.IDENTIFIER, curText));
                                break;
                        }
                    }

                    if (Char.IsDigit(LastChar) || LastChar == '.')
                    {
                        string NumStr = "";
                        NumStr += LastChar;
                        while ((Char.IsDigit(LastChar) || LastChar == '.') && num < word.Length - 1)
                        {
                            num++;
                            LastChar = word[num];
                            NumStr += LastChar;
                        }
                        curNumber = Double.Parse(NumStr);
                        result.Add(new Token(Type.NUMBER, curNumber));
                    }

                    if (Char.IsSymbol(LastChar))
                    {
                        result.Add(new Token(Type.OPERATOR, LastChar.ToString()));
                    }

                    if (Char.IsPunctuation(LastChar))
                    {
                        result.Add(new Token(Type.BRACKET, LastChar.ToString()));
                    }
                }
            }
            return result;
        }
    }
}
