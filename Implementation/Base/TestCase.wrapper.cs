using selenium_webtestframework.Implementation.Functions.Interactions;

namespace selenium_webtestframework.Implementation.Base;

/// <summary>
/// This file contains all wrapper driver methods which are called in the test cases
/// </summary>
public partial class Testcase
{
    public void ClickButton(string buttonTitle)
    {
        TestCaseWebDriver.ClickButton(buttonTitle);
    }
}