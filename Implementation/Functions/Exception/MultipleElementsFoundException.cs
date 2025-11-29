namespace selenium_webtestframework.Implementation.Functions.Exception
{
    public class MultipleElementsFoundException : System.Exception
    {
        /// <summary>
        /// Exception thrown when multiple elements are found with the given xpath
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="message"></param>
        public MultipleElementsFoundException(string xpath, string? message = null)
            : base($"Multiple elements found with xpath {xpath}.  " + message)
        { }
    }
}
