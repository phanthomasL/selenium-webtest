 using selenium_webtestframework.Implementation.Driver;

 namespace selenium_webtestframework.Implementation; 

 public interface ITestcase {
     IWebdriver TestCaseWebDriver { get; set; }
    
     void TestInitialize();
     void TestCleanup();

 }