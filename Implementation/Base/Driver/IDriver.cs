using selenium_webtestframework.Implementation.Base.Driver.Util;

namespace selenium_webtestframework.Implementation.Base.Driver
{
    public interface IDriver : IDisposable
    {
        Configuration Configuration { get; set; }
    }
}