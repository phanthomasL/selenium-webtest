# selenium-webtest
Extendable web test framework with Selenium

- Target framework: `.NET 8` (LTS)
- Test framework: `MSTest`
- Selenium WebDriver: `4.38.0`

## Configuration (appsettings.json)

```
{
    "BaseUrl": "https://www.google.de/",
    "BrowserSizeX": "1920",
    "BrowserSizeY": "1080",
    "BrowserType": "Chrome",
    "DefaultTimeoutSeconds": "5",
    "PageLoadTimeoutSeconds": "30"
}
```

## Highlights
- Driver pool for parallel test execution
- Clean TestCase API with partial classes and helpers
- Screenshots on failure (stored in MSTest results)
- Configurable timeouts and simple console logging

## Run tests

```
dotnet test
```

Only unit tests (no browser required):

```
dotnet test --filter FullyQualifiedName~selenium_webtestframework.Unittest.Functions
```

## Notes
- For E2E tests, ensure the browser and matching WebDriver are available in the environment (e.g., Chrome + chromedriver). Consider running headless in CI.