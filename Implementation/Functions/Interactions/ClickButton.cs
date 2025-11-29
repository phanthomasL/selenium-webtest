using OpenQA.Selenium;
using selenium_webtestframework.Implementation.Base.Driver;
using selenium_webtestframework.Implementation.Functions.Exception;

namespace selenium_webtestframework.Implementation.Functions.Interactions
{
    internal static class FuncClickButton
    {

        public static void ClickButton(this IWebdriver webdriver, string btnTitle)
        {
            var driver = webdriver;
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
