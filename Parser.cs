using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLEnvironment
{
    public class Parser
    {

        public void parse(string input, Painter p)
        {
            input = input.ToLower().Trim();
            string[] token = input.Split(' ');
            if (token.Length == 2)
            {
                string command = token[0];
                string[] parametersS = token[1].Split(',');


                int[] parameters = new int[parametersS.Length];
                for (int i = 0; i < parameters.Length; ++i)
                    try
                    {
                        parameters[i] = Int32.Parse(parametersS[i]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid parameter");
                    }

                if (command.Equals("drawto"))
                {
                    if (parameters.Length == 2)
                    {
                        p.DrawTo(parameters[0], parameters[1]);
                    }
                    else
                    {
                        throw new Exception("Command not found");
                    }
                }
            }


        }
    }
}
