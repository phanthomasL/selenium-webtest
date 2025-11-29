using OpenQA.Selenium;

namespace selenium_webtestframework.Implementation.Base.Driver;

public interface IWebdriver : IWebDriver, IJavaScriptExecutor, ITakesScreenshot, IDriver
{

}