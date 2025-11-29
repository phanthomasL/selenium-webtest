using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using selenium_webtestframework.Implementation.Base.Driver.Util;

namespace selenium_webtestframework.Implementation.Base.Driver
{
    public class WebDriver : IWebdriver
    {
        private readonly Synchronisation _sync;
        public Configuration? Configuration { get; set; }
        public IWebdriver Driver { get; set; } = null!;
        private bool _disposed;

        public WebDriver(Configuration configuration)
        {
            Configuration = configuration;
            _sync = new Synchronisation(configuration);
            CreateInstance();
        }

        public string Url
        {
            get => Driver.Url;
            set => Driver.Url = value;
        }

        public string Title => Driver.Title;
        public string PageSource => Driver.PageSource;
        public string CurrentWindowHandle => Driver.CurrentWindowHandle;
        public ReadOnlyCollection<string> WindowHandles => Driver.WindowHandles;

        public void Close()
        {
            Driver.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                try { Driver?.Quit(); } catch { /* ignore */ }
                try { Driver?.Dispose(); } catch { /* ignore */ }
            }
            _disposed = true;
        }

        public void Quit()
        {
            Driver.Quit();
        }

        public Screenshot GetScreenshot()
        {
            return Driver.GetScreenshot();
        }

        public void ProofConsoleErrors(string message)
        {
            foreach (var log in Manage().Logs.GetLog(LogType.Browser))
            {
                Assert.IsFalse(log.Level == LogLevel.Severe, $"{message} " + log.Message);
            }
        }

        public IWebElement FindElement(By by)
        {
            _sync.WaitForPageAndNg(Driver);
            return Driver.FindElement(by);
        }

        public IWebElement FindElement(string xpath)
        {
            _sync.WaitForPageAndNg(Driver);
            return Driver.FindElement(By.XPath(xpath));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            _sync.WaitForPageAndNg(Driver);
            return Driver.FindElements(by);
        }

        public IOptions Manage()
        {
            return Driver.Manage();
        }

        public INavigation Navigate()
        {
            _sync.WaitForPageAndNg(Driver);
            return Driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            _sync.WaitForPageAndNg(Driver);
            return Driver.SwitchTo();
        }

        public object? ExecuteScript(string script, params object?[] args)
        {
            return Driver.ExecuteScript(script, args);
        }

        public object? ExecuteScript(PinnedScript script, params object?[] args)
        {
            return Driver.ExecuteScript(script, args);
        }

        public object? ExecuteAsyncScript(string script, params object?[] args)
        {
            return Driver.ExecuteAsyncScript(script, args);
        }

        private void CreateInstance()
        {
            IWebdriver driver;
            try
            {
                switch (Configuration!.BrowserType)
                {
                    case "Firefox":
                        var ffOptionsStd = new FirefoxOptions();
                        ffOptionsStd.AddArguments($"--width={Configuration!.BrowserWindowSize.Width}",
                            $"--height={Configuration!.BrowserWindowSize.Height}");
                        driver = new FireFoxDriver(ffOptionsStd);
                        break;

                    case "FirefoxHeadless":
                        var ffOptions = new FirefoxOptions();
                        ffOptions.AddArguments("-headless",
                            $"--width={Configuration!.BrowserWindowSize.Width}",
                            $"--height={Configuration!.BrowserWindowSize.Height}");
                        driver = new FireFoxDriver(ffOptions);
                        break;

                    case "Edge":
                        driver = new EdgeDriver();
                        break;

                    case "Chrome":
                        var chromeStd = new ChromeOptions();
                        chromeStd.AddArguments(
                            $"--window-size={Configuration!.BrowserWindowSize.Width},{Configuration!.BrowserWindowSize.Height}",
                            "--headless=new", "--disable-gpu", "--no-sandbox");
                        driver = new ChromeDriver(chromeStd);
                        break;

                    case "ChromeHeadless":
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments(
                            $"--window-size={Configuration!.BrowserWindowSize.Width},{Configuration!.BrowserWindowSize.Height}",
                            "--headless=new", "--disable-gpu", "--no-sandbox");
                        driver = new ChromeDriver(chromeOptions);
                        break;

                    default:
                        throw new Exception("No valid browser configured in appsettings (BrowserType)");
                }

                driver.Url = Configuration!.LoginUrl;
                // propagate configuration to inner driver types
                switch (driver)
                {
                    case EdgeDriver e:
                        e.Configuration = Configuration;
                        break;
                    case ChromeDriver c:
                        c.Configuration = Configuration;
                        break;
                    case FireFoxDriver f:
                        f.Configuration = Configuration;
                        break;
                }
            }
            catch (WebDriverException wde)
            {
                throw new Exception("Failed to create WebDriver session. Ensure browser and driver binaries are available and compatible.", wde);
            }

            Driver = driver;
        }


        private class EdgeDriver : OpenQA.Selenium.Edge.EdgeDriver, IWebdriver
        {
            public Configuration? Configuration { get; set; }
        }

        private class ChromeDriver : OpenQA.Selenium.Chrome.ChromeDriver, IWebdriver
        {
            public ChromeDriver()
            {
            }

            public ChromeDriver(ChromeOptions options) : base(options)
            {
            }

            public Configuration? Configuration { get; set; }
        }

        private class FireFoxDriver : FirefoxDriver, IWebdriver
        {
            public Configuration? Configuration { get; set; }

            public FireFoxDriver()
            {
            }

            public FireFoxDriver(FirefoxOptions options) : base(options)
            {
            }
        }
    }
}