using selenium_webtestframework.Implementation.Base.Driver;

namespace selenium_webtestframework.Implementation.Base;

public interface ITestRun
{
    TestContext TestContext { get; set; }
    IWebdriver WebDriver { get; }



}