using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selenium_webtestframework.Implementation.Functions.Exception
{
    internal class NotClickableExcpetion : System.Exception
    {
        internal NotClickableExcpetion(IWebElement element, string? message = null) : base(message)
        {
            throw new System.Exception($"The Element with tag {element.TagName} at Location: {element.Location} could not be clicked  " + message);
        }
    }
}
