using selenium_webtestframework.Implementation.Base;

namespace selenium_webtestframework.Webtest
{
    [TestClass]
    public class WebtestExample : TestCase
    {
        [TestMethod]
        public void TestMethod1()
        {
            ClickButton("Test");
            // Dieser Test sollte initial Fehlschlagen , da der Button nicht existiert auf der BaseUrl google.de    
        }
    }
}