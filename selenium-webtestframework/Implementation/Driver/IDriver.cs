using selenium_webtestframework.Implementation.Driver.Util;

namespace selenium_webtestframework.Implementation.Driver
{
    public interface IDriver : IDisposable
    {
        Configuration Configuration { get; set; }
    }
}