namespace TSystems.BFS.WebTest.NewWebTestApi.Implementation.Driver.TestDriver.Web
{
    public class WebDriver : IWebdriver
    {
        private static Synchronisation _sync;
        public Configuration Configuration { get; set; }
        public IEosWebDriver Driver { get; set; }

        public WebDriver(Configuration configuration)
        {
            Configuration = configuration;
            CreateInstance();
            _sync = new Synchronisation();
        }

        public string Url { get => Driver.Url; set => Driver.Url = value; }
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
            Driver.Dispose();
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

        public object ExecuteScript(string script, params object[] args)
        {
            return Driver.ExecuteScript(script, args);
        }

        public object ExecuteScript(PinnedScript script, params object[] args)
        {
            return Driver.ExecuteScript(script, args);
        }

        public object ExecuteAsyncScript(string script, params object[] args)
        {
            return Driver.ExecuteAsyncScript(script, args);
        }

        private void CreateInstance()
        {
            IEosWebDriver driver;
            switch (Configuration.BrowserType)
            {
                case "Firefox":
                    driver = new FireFoxDriver();
                    break;


                case "FirefoxHeadless":
                    var ffOptions = new FirefoxOptions();
                    ffOptions.AddArguments("-headless", $"--width={Configuration.BrowserWindowSize.Width}",
                        $"--height={Configuration.BrowserWindowSize.Height}");
                    driver = new FireFoxDriver(ffOptions);
                    break;


                case "Edge":
                    driver = new EdgeDriver();
                    break;


                case "Chrome":
                    driver = new ChromeDriver();
                    break;


                case "ChromeHeadless":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments($"--window-size={Configuration.BrowserWindowSize.Width},{Configuration.BrowserWindowSize.Height}",
                        "--start-maximized", "--headless");
                    driver = new ChromeDriver(chromeOptions);
                    break;


                default:
                    throw new Exception("In der App.configuration wurde kein valider Browser hinterlegt");
            }

            driver.Url = Configuration.AnmeldeUrl;
            ResizeBrowserWindow(driver);
            Driver = driver;
        }

        private void ResizeBrowserWindow(IWebDriver driverInstance)
        {
#if !NET
            // Auf zweiten Monitor positionieren wenn vorhanden

            var monitor = Screen.FromPoint(new Point(Screen.PrimaryScreen.Bounds.Right + 1, Screen.PrimaryScreen.Bounds.Bottom));
            var browserWindowPosition = new Point(monitor.Bounds.X, monitor.Bounds.Y);
            driverInstance.Manage().Window.Position = browserWindowPosition;

            if (Configuration.BrowserWindowSize.Width != 0 && Configuration.BrowserWindowSize.Height != 0)
            {
                //var calculatedBrowserWindowsSize = new Size(monitor.Bounds.Width / 2, monitor.Bounds.Height / 2);
                driverInstance.Manage().Window.Size = Configuration.BrowserWindowSize;
            }
            else
            {
                driverInstance.Manage().Window.Maximize();
            }
#endif
        }

        private class EdgeDriver : OpenQA.Selenium.Edge.EdgeDriver, IEosWebDriver
        {
            public Configuration Configuration { get; set; }
        }

        private class ChromeDriver : OpenQA.Selenium.Chrome.ChromeDriver, IWebdriver
        {
            public ChromeDriver()
            {
            }

            public ChromeDriver(ChromeOptions options) : base(options)
            {
            }

            public Configuration Configuration { get; set; }
        }

        private class FireFoxDriver : FirefoxDriver, IWebdriver
        {
            public Configuration Configuration { get; set; }
            public FireFoxDriver()
            {
            }

            public FireFoxDriver(FirefoxOptions options) : base(options)
            {
            }
        }
    }