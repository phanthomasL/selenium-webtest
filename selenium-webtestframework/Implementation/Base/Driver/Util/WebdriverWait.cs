using OpenQA.Selenium.Support.UI;
using selenium_webtestframework.Implementation.Base.Driver;

namespace selenium_webtestframework.Implementation.Base.Driver.Util;

internal class WebDriverWait : DefaultWait<IWebdriver>
{
    private static TimeSpan DefaultSleepTimeout => TimeSpan.FromMilliseconds(1);
    public WebDriverWait(IWebdriver webDriver, TimeSpan timeout) : this(new SystemClock(), webDriver, timeout, DefaultSleepTimeout) { }
    public WebDriverWait(IClock clock, IWebdriver webDriver, TimeSpan timeout, TimeSpan sleepInterval) : base(webDriver, clock)
    {
        Timeout = timeout;
        PollingInterval = sleepInterval;
    }

}