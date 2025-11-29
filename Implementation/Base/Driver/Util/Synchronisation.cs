namespace selenium_webtestframework.Implementation.Base.Driver.Util;

internal class Synchronisation(Configuration configuration)
{
    private readonly Configuration _configuration = configuration;

    /// <summary>
    /// Wait for the angular page to be ready
    /// </summary>
    /// <param name="driver"></param>
    public void WaitForNg(IWebdriver driver)
    {

        try
        {
            var timeout = _configuration.DefaultTimeoutSeconds;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(NgIsReady);
        }
        catch (Exception e)
        {
            throw new Exception("NG not ready", e);
        }
    }

    /// <summary>
    /// Wait for the page to be ready
    /// </summary>
    /// <param name="driver"></param>
    public void WaitForPage(IWebdriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(_configuration.DefaultTimeoutSeconds));
            wait.Until(PageIsReady);
        }
        catch (Exception e)
        {
            throw new Exception("Page not ready", e);
        }
    }

    /// <summary>
    /// Wait for the page and ng to be ready
    /// </summary>
    /// <param name="driver"></param>
    public void WaitForPageAndNg(IWebdriver driver)
    {
        // WaitForNg(driver); Angular is not used in this project so this is commented out
        WaitForPage(driver);
    }

    /// <summary>
    ///  Checks wheter all angular testabilities are ready
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
    /// Checks wheter ready state is complete
    /// </summary>
    /// <param name="driver"></param>
    /// <returns></returns>
    private bool PageIsReady(IWebdriver driver)
    {
        return driver.ExecuteScript("return document.readyState")?.ToString() == "complete";
    }

}
