using System.Drawing;

namespace selenium_webtestframework.Implementation.Base.Driver.Util;

/// <summary>
/// Konfiguration f�r den Testtreiber
/// </summary>
public class Configuration
{
    /// <summary>
    /// Url der Anwendung
    /// </summary>
    public string ApplicationUrl { get; }
    /// <summary>
    /// FensterGR��e
    /// </summary>
    public Size BrowserWindowSize { get; }
    /// <summary>
    /// BrowserTyp z.B. Chrome, Firefox, Edge
    /// </summary>
    public string BrowserType { get; }
    /// <summary>
    /// Login URL
    /// </summary>
    public string LoginUrl { get; }

    /// <summary>
    /// Default wait timeout in seconds
    /// </summary>
    public int DefaultTimeoutSeconds { get; }

    /// <summary>
    /// Page load timeout in seconds
    /// </summary>
    public int PageLoadTimeoutSeconds { get; }


    public Configuration(string applicationUrl, Size browserWindowSize, string browserType, string loginUrl, int defaultTimeoutSeconds = 5, int pageLoadTimeoutSeconds = 30)
    {
        ApplicationUrl = applicationUrl;
        BrowserWindowSize = browserWindowSize;
        BrowserType = browserType;
        LoginUrl = loginUrl;
        DefaultTimeoutSeconds = defaultTimeoutSeconds;
        PageLoadTimeoutSeconds = pageLoadTimeoutSeconds;
    }
}