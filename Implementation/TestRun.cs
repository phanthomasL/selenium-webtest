 [TestClass]
    public class TestRun : ITestRun
    {

        public TestContext TestContext { get; set; }
        private static Configuration DriverConfiguration { get; set; }
        private static IDriverPool<WebDriver> WebDriverPool { get; set; }

        public IEosWebDriver WebDriver { get => WebDriverPool.GetFreeDriver(); set => WebDriverPool.ReleaseDriverInstance(value as WebDriver); }
       
        /// <summary>
        /// Creates on assembly initialize the 3 driver pools
        /// </summary>
        /// <param name="context">Test context</param>
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            DriverConfiguration = GetDriverConfiguration();
            WebDriverPool = new DriverPool<WebDriver>(DriverConfiguration, context);
        }

        /// <summary>
        /// On tear down close all driver in the pool
        /// </summary>
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            WebDriverPool.CloseAllDriverInstances();
        }

        private static Configuration GetDriverConfiguration()
        {

#if NET
            var builder = new ConfigurationBuilder();
            var appSettings = builder
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
#else
            var appSettings = ConfigurationManager.AppSettings;
#endif
            var messageQueuePrefix = appSettings["MessageQueuePrefix"] == "local" ? Dns.GetHostName() : appSettings["MessageQueuePrefix"];
            var baseUrl = appSettings["BaseUrl"] ?? throw new ArgumentException("Fehlende Konfiguration der baseUrl in den AppSettings");
            var applicationUrl = baseUrl.Contains("localhost") ? baseUrl + "BFS/" : baseUrl;
            var webApiUrl = baseUrl + "api/";
            var publicWebApiUrl = baseUrl + "eos-api/";
            var anmeldeUrl = applicationUrl + "Anmelden?manuell=";
            var publicWebApiToken = appSettings["PublicWebApiToken"] ?? throw new Exception("Kein PublicWebApiToken in Configuration gefunden");
            var browserWindowSize = new Size(int.Parse(appSettings["BrowserSizeX"] ?? throw new Exception("Kein Browsergröße X in Configuration gefunden")),
                int.Parse(appSettings["BrowserSizeY"] ?? throw new Exception("Kein Browsergröße y in Configuration gefunden")));
            var browserType = appSettings["BrowserTyp"] ?? throw new Exception("Kein Browsertyp in Configuration gefunden");
            var messageAutomateSvcUrl = appSettings["MessageAutomateSvcUrl"] ??
                                        "http://he104402.emea1.cds.t-internal.com/MessageAutomate/Services/RuleEngineSvc.asmx";

            return new Configuration(messageQueuePrefix, applicationUrl, webApiUrl, browserWindowSize, browserType, anmeldeUrl, publicWebApiToken,
                publicWebApiUrl, messageAutomateSvcUrl);
        }