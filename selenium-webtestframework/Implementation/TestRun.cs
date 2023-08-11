using System.Drawing;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using selenium_webtestframework.Implementation.Driver;
using selenium_webtestframework.Implementation.Driver.Pool;
using selenium_webtestframework.Implementation.Driver.Util;

namespace selenium_webtestframework.Implementation;

[TestClass]
public class TestRun : ITestRun
{

    public TestContext TestContext { get; set; } = null!;
    private static Configuration DriverConfiguration { get; set; } = null!;
    private static IDriverPool<WebDriver> WebDriverPool { get; set; } = null!;

    public IWebdriver WebDriver { get => WebDriverPool.GetFreeDriver(); set => WebDriverPool.ReleaseDriverInstance((value as WebDriver)!); }
       
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

        var builder = new ConfigurationBuilder();
        var appSettings = builder.Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false })
            .Build();

        var baseUrl = appSettings["BaseUrl"] ?? throw new ArgumentException("Fehlende Konfiguration der baseUrl in den AppSettings");
        var applicationUrl = baseUrl.Contains("localhost") ? baseUrl + "BFS/" : baseUrl; 
        var anmeldeUrl = applicationUrl + "Anmelden?manuell=";
        var publicWebApiToken = appSettings["PublicWebApiToken"] ?? throw new Exception("Kein PublicWebApiToken in Configuration gefunden");
        var browserWindowSize = new Size(int.Parse((string)((appSettings["BrowserSizeX"]) ?? throw new Exception("Kein Browsergröße X in Configuration gefunden"))),
            int.Parse((string)(appSettings["BrowserSizeY"] ?? throw new Exception("Kein Browsergröße y in Configuration gefunden"))));
        var browserType = appSettings["BrowserTyp"] ?? throw new Exception("Kein Browsertyp in Configuration gefunden"); 
        return new Configuration(applicationUrl,  browserWindowSize, browserType, anmeldeUrl);
    }