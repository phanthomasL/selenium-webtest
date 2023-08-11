using OpenQA.Selenium;

namespace selenium_webtestframework.Implementation.Driver;

public interface IWebdriver : IWebDriver, IJavaScriptExecutor, ITakesScreenshot, IDriver
{
    
}