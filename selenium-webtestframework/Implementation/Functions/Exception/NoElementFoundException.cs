using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selenium_webtestframework.Implementation.Functions.Exception
{
    public class NoElementFoundException : System.Exception
    {
        /// <summary>
        /// Exception thrown when no element is found with the given xpath
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="message"></param>
        /// <exception cref="System.Exception"></exception>
        public NoElementFoundException(string xpath, string? message = null) : base(message)
        {
            throw new System.Exception($"No element found with xpath {xpath}. " + message);
        }
    }
}
