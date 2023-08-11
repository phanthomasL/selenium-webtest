 public interface ITestcase {
        IWebdriver TestCaseWebDriver { get; set; }
    
        void TestInitialize();
        void TestCleanup();

    }