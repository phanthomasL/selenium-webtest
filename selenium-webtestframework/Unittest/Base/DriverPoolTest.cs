using Moq;
using OpenQA.Selenium.Chrome;
using selenium_webtestframework.Implementation.Base.Driver;
using selenium_webtestframework.Implementation.Base.Driver.Pool;
using selenium_webtestframework.Implementation.Base.Driver.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selenium_webtestframework.Unittest.Base
{
    //[TestClass]
    //public class DriverPoolTests
    //{
    //    private readonly Configuration Configuration = new(string.Empty,
    //        new System.Drawing.Size { Height = 1920, Width = 1080 }, string.Empty, string.Empty);

    //    [TestMethod]
    //    public void EnsureAddNewDriverAddsDriverCorrectly()
    //    {
    //        //Arrange
    //        var configuration = Configuration;
    //        var context = new Mock<TestContext>();
    //        // Use Moq to create a mock chrome driver object
    //        var mockDriver = new Mock<IWebdriver>(configuration);
    //        var driverPool = new DriverPool<ChromeDriver>(configuration, context.Object);

    //        //Act
    //        driverPool.AddNewDriver();

    //        //Assert
    //        Assert.IsTrue(DriverPool<object>.DriverInstances.Count == 1);
    //    }

    //    [TestMethod]
    //    public void EnsureGetFreeDriverReturnsFreeDriver()
    //    {
    //        //Arrange
    //        var configuration = Configuration;
    //        // Use Moq to create a mock chrome driver object
    //        var context = new Mock<TestContext>();
    //        var driverPool = new DriverPool<ChromeDriver>(configuration, context.Object);
    //        driverPool.AddNewDriver();

    //        //Act
    //        var driver = driverPool.GetFreeDriver();

    //        //Assert
    //        Assert.IsTrue(DriverPool<object>.DriverInstances[driver] == DriverStatus.InUse);
    //    }

    //    [TestMethod]
    //    public void EnsureCloseAllDriverInstancesClosesAllDrivers()
    //    {
    //        //Arrange
    //        var configuration = Configuration;
    //        var context = new Mock<TestContext>();
    //        // Use Moq to create a mock chrome driver object
    //        var mockDriver = new Mock<IWebdriver>(configuration);
    //        var driverPool = new DriverPool<ChromeDriver>(mockDriver.Object, context);
    //        driverPool.AddNewDriver();
    //        driverPool.AddNewDriver();

    //        //Act
    //        driverPool.CloseAllDriverInstances();

    //        //Assert
    //        Assert.IsTrue(DriverPool<object>.DriverInstances.Count == 0);
    //    }

    //    [TestMethod]
    //    public void EnsureReleaseDriverInstanceFreesDriver()
    //    {
    //        //Arrange
    //        var configuration = Configuration;
    //        var context = new Mock<TestContext>();
    //        // Use Moq to create a mock chrome driver object
    //        var mockDriver = new Mock<IWebdriver>(configuration);
    //        var driverPool = new DriverPool<ChromeDriver>(mockDriver.Object, context);
    //        driverPool.AddNewDriver();
    //        var driver = driverPool.GetFreeDriver();

    //        //Act
    //        driverPool.ReleaseDriverInstance(driver);

    //        //Assert
    //        Assert.IsTrue(DriverPool<object>.DriverInstances[driver] == DriverStatus.Free);
    //    }

    //}

}