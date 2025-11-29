using System.Drawing;

namespace selenium_webtestframework.Implementation.Base.Driver.Util;

/// <summary>
/// Konfiguration f�r den Testtreiber
/// </summary>
public class Configuration(string applicationUrl, Size browserWindowSize, string browserType, string loginUrl, int defaultTimeoutSeconds = 5, int pageLoadTimeoutSeconds = 30)
{
    /// <summary>
    /// Url der Anwendung
    /// </summary>
    public string ApplicationUrl { get; } = applicationUrl;
    /// <summary>
    /// FensterGR��e
    /// </summary>
    public Size BrowserWindowSize { get; } = browserWindowSize;
    /// <summary>
    /// BrowserTyp z.B. Chrome, Firefox, Edge
    /// </summary>
    public string BrowserType { get; } = browserType;
    /// <summary>
    /// Login URL
    /// </summary>
    public string LoginUrl { get; } = loginUrl;

    /// <summary>
    /// Default wait timeout in seconds
    /// </summary>
    public int DefaultTimeoutSeconds { get; } = defaultTimeoutSeconds;

    /// <summary>
    /// Page load timeout in seconds
    /// </summary>
    public int PageLoadTimeoutSeconds { get; } = pageLoadTimeoutSeconds;
}