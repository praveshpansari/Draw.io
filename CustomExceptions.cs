using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentASE
{
    /// <summary>
    /// Custom Exception class for invalid parameters
    /// </summary>
    class InvalidParameterException : Exception
    {
        public InvalidParameterException(String message) : base(message)
        {

        }
    }

    /// <summary>
    /// Custom Exception class whena variable is not found
    /// </summary>
    class IdentifierNotDefinedException : Exception
    {
        public IdentifierNotDefinedException(String message) : base(message)
        {

        }

    }


}
