using System.Drawing;
using OpenQA.Selenium;

namespace selenium_webtestframework.Implementation.Functions.Exception
{
    public class NotClickableException(IWebElement element, string? message = null, System.Exception? innerException = null) : System.Exception($"The Element with tag {element.TagName} at Location: {element.Location} could not be clicked  " + message, innerException)
    {
        public string TagName { get; } = element.TagName;
        public Point Location { get; } = element.Location;
    }
}
