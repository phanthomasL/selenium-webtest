using OpenQA.Selenium;
using selenium_webtestframework.Implementation.Base;
using selenium_webtestframework.Implementation.Driver.Functions.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selenium_webtestframework.Implementation.Functions.Interactions
{
    internal static class ClickButton
    {

        internal static void FuncClickButton(this ITestcase testcase, string btnTitle)
        {
            var driver = testcase.TestCaseWebDriver;
            var xpath = $"//button[text()='{btnTitle}']";
            var elements = driver.FindElements(By.XPath(xpath));

            switch (elements.Count)
            {
                case 0:
                    throw new NoElementFoundException(xpath);
                case 1:
                    TryClick(elements.First());
                    break;
                default:
                    throw new MultipleElementsFoundException(xpath);
            }

        }

        private static void TryClick(IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (System.Exception e)
            {
                throw new NotClickableExcpetion(element, e.Message);
            }
        }
    }

}
