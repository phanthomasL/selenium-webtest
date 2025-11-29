namespace selenium_webtestframework.Implementation.Functions.Exception
{
    public class NoElementFoundException : System.Exception
    {
        /// <summary>
        /// Exception thrown when no element is found with the given xpath
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="message"></param>
        public NoElementFoundException(string xpath, string? message = null)
            : base($"No element found with xpath {xpath}. " + message)
        { }
    }
}
