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
            var timeout = 1; 
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
    ///     
    /// </summary>
    /// <param name="driver"></param>
    /// <returns></returns>
    private bool PageIsReady(IWebdriver driver)
    {
        return driver.ExecuteScript("return document.readyState").ToString()!.Equals("complete");
    }

}
