namespace selenium_webtestframework.Implementation.Base.Driver.Pool;

public interface IDriverPool<T>
{

    void CloseAllDriverInstances();
    void ReleaseDriverInstance(T driver);
    T GetFreeDriver();
    void AddNewDriver();
}