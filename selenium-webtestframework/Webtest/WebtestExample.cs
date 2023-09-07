using selenium_webtestframework.Implementation;

namespace selenium_webtestframework.Webtest
{
    [TestClass]
    public class WebtestExample : Testcase
    {
        [TestMethod]
        public void TestMethod1()
        {
            ClickButton("Test");
            // Dieser Test sollte initial Fehlschlagen , da der Button nicht existiert auf der BaseUrl google.de    
        }
    }
}