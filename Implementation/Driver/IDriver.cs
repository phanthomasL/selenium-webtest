namespace TSystems.BFS.WebTest.NewWebTestApi.Implementation.Driver.TestDriver
{
    public interface IDriver : IDisposable
    {
        Configuration Configuration { get; set; }
    }
}