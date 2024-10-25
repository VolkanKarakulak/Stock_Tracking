using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    public class InvalidOperationException : Exception
    {
        public InvalidOperationException() : base("Required data is missing or in an invalid state.")
        {

        }
    }
}
