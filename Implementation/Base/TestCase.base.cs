using selenium_webtestframework.Implementation.Base.Driver;

namespace selenium_webtestframework.Implementation.Base;

[TestClass]
    public  abstract partial class TestCase : TestRun, ITestCase {
      public IWebdriver TestCaseWebDriver { get; set; } = null!;

      [TestInitialize]
      public void TestInitialize() {
          // Get free driver for this test case
          TestCaseWebDriver = WebDriver;
      }

      [TestCleanup]
      public void TestCleanup()
      {
          try
          {
              if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
              {
                  CaptureAndSaveScreenshot();
              }
          }
          finally
          {
              ResetDrivers();
          }
      }

      private void CaptureAndSaveScreenshot()
      {
          var screenshotFilename = Path.Combine(TestContext.TestRunResultsDirectory, $"{GetType().Name}_{DateTime.Now:HH.mm.ss}.png");
          TestContext.WriteLine($"Schreibe Screenshot-Datei {screenshotFilename}");
          var screenshotBytes = TestCaseWebDriver.GetScreenshot().AsByteArray;
          File.WriteAllBytes(screenshotFilename, screenshotBytes);

          var pageSourceFilename = Path.Combine(TestContext.TestRunResultsDirectory, $"{GetType().Name}_{DateTime.Now:HH.mm.ss}.html");
          try
          {
              File.WriteAllText(pageSourceFilename, TestCaseWebDriver.PageSource);
              TestContext.AddResultFile(pageSourceFilename);
          }
          catch { }
          TestContext.AddResultFile(screenshotFilename);
      }

      private void ResetDrivers()
      {
          TestCaseWebDriver.Url = TestCaseWebDriver.Configuration!.LoginUrl;
          WebDriver = TestCaseWebDriver;
      }
  }