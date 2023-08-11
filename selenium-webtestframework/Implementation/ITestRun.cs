using selenium_webtestframework.Implementation.Driver;

namespace selenium_webtestframework.Implementation;

public interface ITestRun {
    TestContext TestContext { get; set; }
    IWebdriver WebDriver { get; }
       


}