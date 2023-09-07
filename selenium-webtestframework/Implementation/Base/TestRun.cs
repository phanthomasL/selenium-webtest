using System.Drawing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using selenium_webtestframework.Implementation.Base.Driver;
using selenium_webtestframework.Implementation.Base.Driver.Pool;
using selenium_webtestframework.Implementation.Base.Driver.Util;

namespace selenium_webtestframework.Implementation.Base;

/// <summary>
/// Test run class for the selenium webtestframework
/// </summary>
[TestClass]
public class TestRun : ITestRun
{
    /// <summary>
    /// Test context for the test run
    /// </summary>
    public TestContext TestContext { get; set; } = null!;
    /// <summary>
    /// Driver configuration with the browser type, the application url and the login url
    /// </summary>
    private static Configuration DriverConfiguration { get; set; } = null!;
    /// <summary>
    /// Driver pool for the webdrivers to get free drivers and release them
    /// </summary>
    private static IDriverPool<WebDriver> WebDriverPool { get; set; } = null!;

    public IWebdriver WebDriver
    {
        get => WebDriverPool.GetFreeDriver();
        set => WebDriverPool.ReleaseDriverInstance((value as WebDriver)!);
    }

    /// <summary>
    /// Initializes the driver pool on test setup
    /// </summary>
    /// <param name="context">Test context</param>
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        DriverConfiguration = GetDriverConfiguration();
        WebDriverPool = new DriverPool<WebDriver>(DriverConfiguration, context);
    }

    /// <summary>
    /// Closes all driver instances on test teardown
    /// </summary>
    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        WebDriverPool.CloseAllDriverInstances();
    }

    /// <summary>
    /// Gets the driver configuration from the appsettings.json
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"> </exception>
    /// <exception cref="Exception"></exception>
    private static Configuration GetDriverConfiguration()
    {

        var builder = new ConfigurationBuilder();
        var appSettings = builder.Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
            .Build();

        var baseUrl = appSettings["BaseUrl"] ??
                      throw new ArgumentException("Fehlende Konfiguration der baseUrl in den AppSettings");
        var applicationUrl = baseUrl;
        var anmeldeUrl = applicationUrl; // loginurl, falls vorhanden
        var browserWindowSize = new Size(
            int.Parse(appSettings["BrowserSizeX"] ??
                      throw new Exception("Kein Browsergröße X in Configuration gefunden")),
            int.Parse(appSettings["BrowserSizeY"] ??
                      throw new Exception("Kein Browsergröße y in Configuration gefunden")));
        var browserType = appSettings["BrowserTyp"] ?? throw new Exception("Kein Browsertyp in Configuration gefunden");
        return new Configuration(applicationUrl, browserWindowSize, browserType, anmeldeUrl);
    }
}