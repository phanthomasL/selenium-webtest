namespace selenium_webtestframework.Implementation.Functions.Exception
{
    /// <summary>
    /// Exception thrown when multiple elements are found with the given xpath
    /// </summary>
    /// <param name="xpath"></param>
    /// <param name="message"></param>
    public class MultipleElementsFoundException(string xpath, string? message = null) : System.Exception($"Multiple elements found with xpath {xpath}.  " + message)
    {
    }
}
