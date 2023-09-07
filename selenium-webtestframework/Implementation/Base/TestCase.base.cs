using OpenQA.Selenium;
using selenium_webtestframework.Implementation.Base;
using selenium_webtestframework.Implementation.Base.Driver;

namespace selenium_webtestframework.Implementation;

[TestClass]
  public  abstract partial class Testcase : TestRun, ITestcase {
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
          TestCaseWebDriver.GetScreenshot().SaveAsFile(screenshotFilename, ScreenshotImageFormat.Png);
          TestContext.AddResultFile(screenshotFilename);
      }

      private void ResetDrivers()
      {
          TestCaseWebDriver.Url = TestCaseWebDriver.Configuration.AnmeldeUrl;
          WebDriver = TestCaseWebDriver;
      }
  }