namespace selenium_webtestframework.Implementation.Functions.Exception
{
    /// <summary>
    /// Exception thrown when no element is found with the given xpath
    /// </summary>
    /// <param name="xpath"></param>
    /// <param name="message"></param>
    public class NoElementFoundException(string xpath, string? message = null) : System.Exception($"No element found with xpath {xpath}. " + message)
    {
    }
}
