using selenium_webtestframework.Implementation.Base.Driver;

namespace selenium_webtestframework.Implementation.Base;

public interface ITestCase
{
    IWebdriver TestCaseWebDriver { get; set; }

    void TestInitialize();
    void TestCleanup();

}