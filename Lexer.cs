using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace PLEnvironment
{

    class Lexer
    {
        String commandLine;

        enum keys
        {
            DRAWTO = 0,
            RECT = 1,
            METHOD = 2,
            IF = 3,

        }
        LinkedList<String> keywords = new LinkedList<String>();
        LinkedList<String> shapes = new LinkedList<string>();

        public Lexer(string commandLine)
        {
            this.commandLine = commandLine.Trim();

            this.keywords.AddLast("if");
            this.keywords.AddLast("else");

            this.shapes.AddLast("drawto");
            this.shapes.AddLast("rect");
        }

        public void Advance()
        {
            string[] line = this.commandLine.Split(' ');
            string currentWord = "";
            for (int word = 0; word < line.Length; word++)
            {
                currentWord = line[word];
                if (Regex.Match(currentWord, "[a-zA-Z][a-zA-Z0-9]").Success)
                {
                    
                }
            }
        }
    }
}
