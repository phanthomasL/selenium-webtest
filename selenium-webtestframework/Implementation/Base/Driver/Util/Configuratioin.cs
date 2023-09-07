using System.Drawing;

namespace selenium_webtestframework.Implementation.Base.Driver.Util;

/// <summary>
/// Konfiguration für den Testtreiber
/// </summary>
public class Configuration
{
    /// <summary>
    /// Url der Anwendung
    /// </summary>
    public string ApplicationUrl { get; }
    /// <summary>
    /// FensterGRöße
    /// </summary>
    public Size BrowserWindowSize { get; }
    /// <summary>
    /// BrowserTyp z.B. Chrome, Firefox, Edge
    /// </summary>
    public string BrowserType { get; }
    /// <summary>
    /// Url zum Anmelden
    /// </summary>
    public string AnmeldeUrl { get; }


    public Configuration(string applicationUrl, Size browserWindowSize, string browserType, string anmeldeUrl)
    {
        ApplicationUrl = applicationUrl;
        BrowserWindowSize = browserWindowSize;
        BrowserType = browserType;
        AnmeldeUrl = anmeldeUrl;
    }
}