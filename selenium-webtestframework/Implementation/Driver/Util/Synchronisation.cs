using OpenQA.Selenium;

namespace selenium_webtestframework.Implementation.Driver.Util;

internal class Synchronisation
{
    internal static int PollingThreshold = 10; // Schwelle 10 mal, wie oft soll es versucht werden 
    internal static int PollingTimeoutInMilliseconds = 100; // 100ms Sekunden

    /// <summary>
    /// 
    /// </summary>
    /// <param name="driver"></param>
    public void WaitForNg(IWebdriver driver)
    {

        try
        {
#if DEBUG
            var timeout = 1; // Solution Configurations im Visual Studio auf "DEBUG"
#else
            var timeout = 25; // Solution Configurations im Visual Studio auf "RELEASE", bzw.im Nightly Build
#endif
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(NgIsReady);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Warnung: WebDriverWait war zu spät dran, oder es gibt Folgefehler nach: {e.Message}, Timestamp: {DateTime.Now:HH:mm:ss.fff}");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="driver"></param>
    public void WaitForPage(IWebdriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(PageIsReady);
        }
        catch
        {
            throw new Exception("Timeout waiting for page to be ready");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="driver"></param>
    public void WaitForPageAndNg(IWebdriver driver)
    {
        WaitForNg(driver);
        WaitForPage(driver);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="driver"></param>
    /// <returns></returns>
    private bool NgIsReady(IWebdriver driver)
    {
        return driver.ExecuteAsyncScript(@"
            var callback = arguments[arguments.length - 1];
            if (document.readyState !== 'complete') 
            {
                callback('document not ready');
            } 
            else 
            {
                try 
                {
                    var testabilities = window.getAllAngularTestabilities();
                    var count = testabilities.length;
                    var decrement = function() 
                    {
                        count--;
                        if (count === 0) 
                        {
                            callback('complete');
                        }
                    };
                    testabilities.forEach(function(testability) 
                    {
                        testability.whenStable(decrement);
                    });
                } 
                catch (err) 
                {
                    callback(err.message);
                }
            }"
        ).ToString()!.Equals("complete");
    }

    /// <summary>
    ///     Fragt den Status der Seite ab und gibt true zurück, wenn sie bereit ist
    /// </summary>
    /// <param name="driver"></param>
    /// <returns></returns>
    private bool PageIsReady(IWebdriver driver)
    {
        return driver.ExecuteScript("return document.readyState").ToString()!.Equals("complete");
    }

    /// <summary>
    /// Prüft ob für das Element ob noch Änderungen am CSS/Style durch geführt werden (Animation => Veränderungen über die Zeit),
    /// beendet die Methode wenn innerhalb von 2 Zeiteinheiten keine Änderungen mehr festgestellt werden können
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xPath">Der XPath zum überprüfenden Element</param>
    /// <param name="failOnNoChange">Soll Fehler geworfen werden wenn das zu überprüfende Element nicht verändert wurde</param>
    public void WaitForAnimationEnd(IWebdriver webDriver, string xPath, bool failOnNoChange = false)
    {
        var i = 0; // poll counter
        var cumulative = 0; // the number of CSS/style checks that didn't find any changes
        IWebElement element = null;
        try { element = webDriver.FindElement(By.XPath(xPath)); }
        catch (Exception e) { Assert.Fail(e.ToString()); }
        var originalCss = element == null ? string.Empty : element.GetAttribute("class");
        var originalStyle = element == null ? string.Empty : element.GetAttribute("style");
        var prevCss = originalCss;
        var prevStyle = originalStyle;

        while (i < PollingThreshold) // Do polls until threshold is exceeded
        {
            element = webDriver.FindElement(By.XPath(xPath));
            if (element != null)
            {
                var css = element.GetAttribute("class");
                var style = element.GetAttribute("style");

                // if the previous CSS/Style is same - increase the counter
                if ((css == prevCss || style == prevStyle)) cumulative++; // wenn sich der Style und das CSS sich nicht geändert hat Zähler erhöhen
                if (cumulative > 2) return; // Zähler ist 2 scheinbar keine Veränderung mehr was als Fertig gewertet wird

                prevCss = css;
                prevStyle = style;
            }

            Thread.Sleep(PollingTimeoutInMilliseconds);
            i++;
        }

        if (element == null) throw new TimeoutException("Element not found "); // fail if no element
        if (failOnNoChange && (originalCss == prevCss) && (originalStyle == prevCss))
            throw new TimeoutException("Element was not changed"); //require element style/CSS change
    }
}
