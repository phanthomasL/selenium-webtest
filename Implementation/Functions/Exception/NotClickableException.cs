using System.Drawing;
using OpenQA.Selenium;

namespace selenium_webtestframework.Implementation.Functions.Exception
{
    public class NotClickableException : System.Exception
    {
        public string TagName { get; }
        public Point Location { get; }

        public NotClickableException(IWebElement element, string? message = null, System.Exception? innerException = null)
            : base($"The Element with tag {element.TagName} at Location: {element.Location} could not be clicked  " + message, innerException)
        {
            TagName = element.TagName;
            Location = element.Location;
        }
    }
}
