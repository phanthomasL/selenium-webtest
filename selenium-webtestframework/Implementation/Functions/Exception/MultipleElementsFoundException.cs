using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selenium_webtestframework.Implementation.Functions.Exception
{
    public class MultipleElementsFoundException : System.Exception
    {
        /// <summary>
        /// Exception thrown when multiple elements are found with the given xpath
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="message"></param>
        /// <exception cref="System.Exception"></exception>
        public MultipleElementsFoundException(string xpath, string? message = null) : base(message)
        {
            throw new System.Exception($"Multiple elements found with xpath {xpath}.  " + message);
        }
    }
}
