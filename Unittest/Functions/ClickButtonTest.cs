using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Moq;
using selenium_webtestframework.Implementation.Functions.Interactions;
using selenium_webtestframework.Implementation.Functions.Exception;
using selenium_webtestframework.Implementation.Base;
using selenium_webtestframework.Implementation.Base.Driver;

namespace selenium_webtestframework.Unittest.Functions
{
    [TestClass]
    public class ClickButtonTests
    {
        private Mock<ITestCase> _testcaseMock = null!;
        private Mock<IWebdriver> _webDriverMock = null!;
        private Mock<IWebElement> _webElementMock = null!;

        [TestInitialize]
        public void Setup()
        {
            _testcaseMock = new Mock<ITestCase>();
            _webDriverMock = new Mock<IWebdriver>();
            _testcaseMock.Setup(m => m.TestCaseWebDriver).Returns(_webDriverMock.Object);
            _webElementMock = new Mock<IWebElement>();
        }

        [TestMethod]
        public void FuncClickButton_ElementFound_ClicksElement()
        {
            // Arrange
            var btnTitle = "Submit";
            _webDriverMock.Setup(d => d.FindElements(By.XPath($"//button[text()='{btnTitle}']")))
                .Returns(new ReadOnlyCollection<IWebElement>(new[] { _webElementMock.Object }));

            // Act
            _webDriverMock.Object.ClickButton(btnTitle);

            // Assert
            _webElementMock.Verify(w => w.Click(), Times.Once);
        }

        [TestMethod]
        public void FuncClickButton_NoElementFound_ThrowsException()
        {
            // Arrange
            var btnTitle = "Submit";
            _webDriverMock.Setup(d => d.FindElements(By.XPath($"//button[text()='{btnTitle}']")))
                .Returns(new ReadOnlyCollection<IWebElement>(new IWebElement[] { }));

            // Act & Assert
            var equals = Assert.ThrowsException<NoElementFoundException>(() => _webDriverMock.Object.ClickButton(btnTitle)).Message
                .Equals("No element found with xpath //button[text()='Submit']. ");
            Assert.IsTrue(equals);

        }

        [TestMethod]
        public void FuncClickButton_MultipleElementsFound_ThrowsException()
        {
            // Arrange
            var btnTitle = "Submit";
            _webDriverMock.Setup(d => d.FindElements(By.XPath($"//button[text()='{btnTitle}']")))
                .Returns(new ReadOnlyCollection<IWebElement>(new[] { _webElementMock.Object, _webElementMock.Object }));

            // Act & Assert
            var equals = Assert.ThrowsException<MultipleElementsFoundException>(() => _webDriverMock.Object.ClickButton(btnTitle)).Message
                .Equals("Multiple elements found with xpath //button[text()='Submit'].  ");
            Assert.IsTrue(equals);
        }
    }
}
