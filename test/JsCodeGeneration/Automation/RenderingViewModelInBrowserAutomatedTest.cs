using EmbedIO;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Vitraux.Helpers;
using Vitraux.Test.Example;

namespace Vitraux.Test.JsCodeGeneration.Automation;

[CollectionDefinition("AutomationTests", DisableParallelization = true)]
public class AutomationTestsCollection { }

[Collection("AutomationTests")]
public class RenderingViewModelInBrowserAutomatedTest : IDisposable
{
    private const string BaseUrl = "http://localhost:7541/";
    private const string IndexPage = "index-for-automated-tests.html";
    private readonly WebServer _server;

    public RenderingViewModelInBrowserAutomatedTest()
    {
        _server = CreateTestServer(BaseUrl);
    }

    [Theory]
    [InlineData(QueryElementStrategy.OnlyOnceAtStart)]
    [InlineData(QueryElementStrategy.OnlyOnceOnDemand)]
    [InlineData(QueryElementStrategy.Always)]
    public void RenderedViewModelInChromeTest(QueryElementStrategy queryElementStrategy)
        => TestRenderedViewModelInBrowser(queryElementStrategy, CreateChromeDriver);

    [Theory]
    [InlineData(QueryElementStrategy.OnlyOnceAtStart)]
    [InlineData(QueryElementStrategy.OnlyOnceOnDemand)]
    [InlineData(QueryElementStrategy.Always)]
    public void RenderedViewModelInFirefoxTest(QueryElementStrategy queryElementStrategy)
        => TestRenderedViewModelInBrowser(queryElementStrategy, CreateFirefoxDriver);

    [Theory]
    [InlineData(QueryElementStrategy.OnlyOnceAtStart)]
    [InlineData(QueryElementStrategy.OnlyOnceOnDemand)]
    [InlineData(QueryElementStrategy.Always)]
    public void RenderedViewModelInEdgeTest(QueryElementStrategy queryElementStrategy)
        => TestRenderedViewModelInBrowser(queryElementStrategy, CreateEdgeDriver);

    private void TestRenderedViewModelInBrowser(QueryElementStrategy queryElementStrategy, Func<IWebDriver> driverFactory)
    {
        //Arrange
        StartTestServer();

        using var driver = driverFactory.Invoke();
        NavigatePage(driver);

        var rootJsGenerator = RootJsGeneratorFactory.Create();
        var petOwnerMappingData = GetConfiguredPetOwnerMapping();
        var generatedJsCode = rootJsGenerator.GenerateJs(petOwnerMappingData, queryElementStrategy);

        ExecuteScriptAsAwaitableFunction("initialize", [], [], generatedJsCode.InitializeViewJs, driver);

        //Act
        ExecuteScriptAsAwaitableFunction("updateView", ["vm"], [GetPetownerJson()], generatedJsCode.UpdateViewJs, driver);

        //Assert
        var renderedName = GetRenderedName(driver);
        var renderedAdress = GetRenderedAdress(driver);
        var renderedAdressData = GetRenderedAdressData(driver);
        var renderedPhoneNumber = GetRenderedPhoneNumber(driver);
        var renderedPhoneData = GetRenderedPhoneDataAttribute(driver);
        var renderedPets = GetRenderedPets(driver);

        Assert.Equal("John Doe", renderedName);
        Assert.Equal("123 Main St", renderedAdress);
        Assert.Equal("123 Main St", renderedAdressData);
        Assert.Equal("555-1234", renderedPhoneNumber);
        Assert.Equal(["555-1234", "555-1234"], renderedPhoneData);
        Assert.Collection(renderedPets, p => Assert.Equal(ExpectedRenderedPetTobby, p), p => Assert.Equal(ExpectedRenderedPetToulouse, p));
    }

    private static IWebDriver CreateChromeDriver()
    {
        var options = new ChromeOptions() { PageLoadStrategy = PageLoadStrategy.Eager };
        options.AddArgument("--headless=new");
        options.AddArgument("--enable-gpu");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--ignore-certificate-errors");

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver()
    {
        var options = new FirefoxOptions() { PageLoadStrategy = PageLoadStrategy.Eager };
        options.AddArgument("--headless");
        options.SetLoggingPreference(LogType.Browser, LogLevel.All);

        var service = FirefoxDriverService.CreateDefaultService();
        service.HideCommandPromptWindow = true;
        service.LogLevel = FirefoxDriverLogLevel.Trace;

        return new FirefoxDriver(service, options);
    }

    private static IWebDriver CreateEdgeDriver()
    {
        var options = new EdgeOptions() { PageLoadStrategy = PageLoadStrategy.Eager };
        options.AddArgument("--headless=new");
        options.AddArgument("--enable-gpu");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--ignore-certificate-errors");

        return new EdgeDriver(options);
    }

    private void StartTestServer()
        => _server.Start();

    private static void NavigatePage(IWebDriver driver)
    {
        var fullUrlPage = new Uri(new Uri(BaseUrl), IndexPage);
        driver.Navigate().GoToUrl(fullUrlPage);
    }

    private static ModelMappingData GetConfiguredPetOwnerMapping()
    {
        var petownerConfig = new PetOwnerConfiguration(new DataUriConverter());
        var modelMapper = new ModelMapper<PetOwner>();
        var modelMappingData = petownerConfig.ConfigureMapping(modelMapper);

        return modelMappingData;
    }

    private static void ExecuteScriptAsAwaitableFunction(string functionName, string[] parameters, string[] args, string jsCode, IWebDriver driver)
    {
        var jsExecutor = (IJavaScriptExecutor)driver;

        var jsCodeFunction = $$""""
                            (async function {{functionName}}({{string.Join(',', parameters)}}) {
                                {{jsCode}}
                            })({{string.Join(',', args)}});
                            """";
        _ = jsExecutor.ExecuteScript(jsCodeFunction);
    }

    private static string GetRenderedName(IWebDriver driver)
        => driver
            .FindElement(By.Id("petowner-name"))
            .Text;

    private static string GetRenderedAdress(IWebDriver driver)
        => driver
            .FindElement(By.Id("petowner-address-parent"))
            .GetShadowRoot()
            .FindElement(By.CssSelector(".petowner-address-target"))
            .Text;

    private static string? GetRenderedAdressData(IWebDriver driver)
        => driver
            .FindElement(By.CssSelector(".petwoner-address > div"))
            .GetAttribute("data-petowner-address");

    private static string GetRenderedPhoneNumber(IWebDriver driver)
        => driver
            .FindElement(By.Id("petowner-phonenumber-id"))
            .Text;

    private static IEnumerable<string> GetRenderedPhoneDataAttribute(IWebDriver driver)
        => driver
            .FindElement(By.CssSelector(".petowner-phonenumber"))
            .GetShadowRoot()
            .FindElements(By.CssSelector(".petowner-phonenumber-target"))
            .Select(e => e.GetAttribute("data-phonenumber")!)
            .ToArray()
            .ToComparableEnumerable();

    private static IEnumerable<RenderedPet> GetRenderedPets(IWebDriver driver)
        => driver
            .FindElements(By.CssSelector("#pets-table > tbody > tr"))
            .Select(tr => new RenderedPet(
                tr.FindElement(By.CssSelector(".cell-pet-name")).Text,
                GetAnchorCellPetNames(tr),
                tr.FindElement(By.CssSelector(".pet-photo")).GetAttribute("src"),
                GetRenderedVaccines(tr),
                GetRenderedAntiparasitics(tr)))
            .ToArray()
            .ToComparableEnumerable();

    private static IEnumerable<string?> GetAnchorCellPetNames(IWebElement tr)
    {
        var anchorCellPetNames = tr.FindElements(By.CssSelector(".anchor-cell-pet-name")).Select(e => e.GetDomAttribute("href")).ToArray().ToComparableEnumerable();
        var anotherAnchorCellPetName = tr.FindElement(By.CssSelector(".another-anchor-cell-pet-name")).GetDomAttribute("href");

        return anchorCellPetNames.Append(anotherAnchorCellPetName).ToArray().ToComparableEnumerable();
    }

    private static IEnumerable<RenderedVaccine> GetRenderedVaccines(IWebElement tr)
        => tr
            .FindElements(By.CssSelector(".inner-table-vaccines > tbody > tr"))
            .Select(trv => new RenderedVaccine(
                trv.FindElement(By.CssSelector(".div-vaccine")).Text,
                trv.FindElement(By.CssSelector(".span-vaccine-date")).Text,
                trv.FindElements(By.CssSelector(".ingredients-list > li")).Select(li => li.Text).ToArray().ToComparableEnumerable()))
            .ToArray()
            .ToComparableEnumerable();

    private static IEnumerable<RenderedAntiparasitic> GetRenderedAntiparasitics(IWebElement tr)
        => tr
            .FindElement(By.CssSelector(".inner-nav-antiparasitics"))
            .GetShadowRoot()
            .FindElements(By.CssSelector(":host > div"))
            .Select(tra => new RenderedAntiparasitic(
                tra.FindElement(By.CssSelector(".div-antiparasitics")).Text,
                tra.FindElement(By.CssSelector(".span-antiparasitics-date")).Text))
            .ToArray()
            .ToComparableEnumerable();

    private static string GetPetownerJson()
        => """
          {
              v0: "John Doe",
              v1: "123 Main St",
              v2: "555-1234",
              c0:
                  [
                      {
                          v0: "Tobby",
                          v1: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAAAAFzUkdCAK7OHOkAAAAEZ0FNQQAAsY8L/GEFAAAACXBIWXMAAADdAAAA3QFwU6IHAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAA0VJREFUWEftls+LTlEcxu+MRIxpKCaUaJoiaiaNKCWSSWzEYv6DYSFZSZGFUZQpS2VhobFHmbDGIFmYYjFTZDEs/EioIYznczz3dt/3vefOe1ny1KdzznPO9/u97/1x3pP882pxW0V7xT7RbdCEuSZuYjSrsgtYLo6LFeKLuCcGRZ8o02NxSWwVC8WUOCdei6Y0X5wUn8VMAePiqNgplhn6eMwVxZCLnOQuFXfkhiBoWgyJ0x7/EOfFvJmImPMa1jImlhzkYkzu0seeFnspup2UZ0t7LFRpQqx1zITHvCvkpE+NQu0XPwXPu9eBpwTtAzEHrxmx1jEhh71eQW5qUKtGG8QnwcIBB7QLArid6/GqiBjHkqPd3oCgpRY1Mz0STJwN0ZL6vPG0t2xVFrHOMWgr1LBHzaBNAuO5aPU6xmP2D9iqLGKdY8wW/VZBLfrUTi57cMhr6K+190bMtV1ZxDpHyGk71LJH7eSVB52ep3/G3rCtII35jnk0G21lkrdFHBELbAVpPCxCTlv0O+1RO7nrQZcn+U5f2OsJEZbGB+1P2cokj52ONruTSOMe++Rssddlj9rJRQ+eCN7MHR6Phww5yWMbfiuu2MqE57k+W5nkpTskualBLcbUThaJ+zbyhO+3XvKzF7VesTlyOWcealI7iG+eLfSpSBesc/xfi1y5vNSgFjUbtFqwaNKxhdI8O90ascvQL90pNT8paKkR1TbBolHHNUhz/eKd1+XB6/eyBmlu1OuokYlNIa9VbvnjiOmCWPK7WyM85mJKc6Y1guovYKXb8H1G1Cb4JfXCYy6mNGdaI6j+Aj66LXxBrOuCvYKTTyr6eMzFlOZMaxRqj+CXjPjRNUhzHeKZ1/FPB/TxOrysQZob8TpqRMUbyv81L1TZyYdbzbmPPtBv83SDNMdJiZzkLv0KEKdagg47Piqt4ag17WFU5HJOcs+q7YIr/UrfOQql+VkvgBzORU76TYkTLMHfBQeIxc5XI/nRCyDGseRgTM5KOiG4coK/iduCW7lb8A+3VIQLcB+POdbcEcQQSw5y/ZE4110V/MuRrArEEEuOqErP6DmxX2wWfEIcPNj1uMWAPpj3gtMw2+5DwXP/rxIlyS8JhwIQeBm/8wAAAABJRU5ErkJggg==",
                          c0: [
                              { v0: "Tobby Rabies", v1: "2023-01-01", c0: [{ v0: "Ingredient1" }, { v0: "Ingredient2" }] },
                              { v0: "Tobby Distemper", v1: "2023-02-01", c0: [{ v0: "Ingredient3" }] }
                          ],
                          c1: [{ v0: "Antiparasitic1", v1: "2024-12-01" }, { v0: "Antiparasitic2", v1: "2024-12-02" }]
                      },
                      {
                          v0: "Toulouse",
                          v1: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAAsQAAALEBxi1JjQAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAVESURBVFiFrZdbbBRVGMd/30y325ZbS0u5CUJbMEpDooBBAbl4QeXFKEEpJt5iookxEF98EF9IvBCi4OWhDyZULg01QAIiIVGMCApIKmAFkWIhQGlLC7SlO7uzs/P5MLPd2bLbVuS87Dnn+873/53vnPlmFvpp+uuWVfrtBkeP1j6f1ef4jqm694uo/lzzTX+xsjWjX6ttr8C2TTo6avTY7oKMPu0d32NZYSzr0TsPEI+PBcCywsRubOhr1kO1S+m6McHztYfdDkBOv1ZVs7ff3v6a7v2yimmVjYTDcU6emMr1toKAr9wOQNYM6LGtJcRiJb0TCUewIgWITAdmELWGYdspwHjc1MNbH79jAFjxOuyYmTZnCIRyvX443IdYobu75o4AaP32u7nWsSAVW71OwVBIJnpo6sh77d1dY/Xwplf+NwA93Z+TSAjAmr3HeX3LIU+kqCjl4/c/2neSVzcdJMlAJPrebQNodXVIVYWbNx9Lzl25YdHSZZEQcEcW0/lnPddPHMXJDUE4j+bOCG3dUeKu6y3o6irTX+pGanV1CECb6sZoY+3L2lQ7KRNA2s3V7WvXYBhvAYXJLV3pjNBjO1Q8UEk0L5fmPXWAUjJ7IcNLx9NytJ6uqM3U0hGpQAUFnfREDjBy5rNMaDkDlAEdYM6WimWNWQEA9LvPwjhuBCeeyk5hIdxXCSJ0NtQT62ildP5Tnq3pH2i+nB6kdNwimbviRz1bNwpJtAUstVKxvCromn4EOz+chBXbT0H+Tc8qMG58rzhAbsko8kaPTy2aXAaTy8H0H5i8PIe25mrdsW6uTFl2FfRUQGKpXqzLD2qmFyI3LwbOBwwfdpOioj2MGj2k7+OWP2YC+WMmpO943DgoLYX2qw6W/S6Xzv+EKY6/x42ga33PEJbOAg4kl2atXtp6qBzMj4Hnsvn0aUdQVsqY2YfT4pyuKSaU2wx4BUR0lZRXrU/asxYiGT3nHKK/DVIchPN9xQHk3pc6gP0pIikO2vt/GSGLBw2gPKFaZ2a0iewKjAoHBaAtJ4ag8vCgAaCI1omzMgNoQ2A0ImjqJwORR0HD2e0ZhZ7MOK9YgYEGTdkBRBajCXCdgYXVBTdO1iNTgo/N9cEBKItwImB39plXQCG4j5TfLL16MNOHSWVAMq0SZn4bNh8rQbgHMQA3INQD9jWI94Ad2IgmQEwAEzfnoQwhF6S68vuAAJjxRwABwwveK5SE0UA/Od9bUuambebs5ruA+f6wnYulRwYGEJnn/ZrpQr15F9IygwuGmTTNS49lvAF4RtGvZeHCtEuVGUCZ7S++NQNi+POBS5A6AlAeTNYD/XtLGcgq3yuC637aV+oWAFU1SF4aIwfcYKrVW9L3brgBACigfXKFnq4pxjC2Af6Hq74vU168NCAAbT9MAob2jkVSu01mgH4yAGC1L8G+eh6Y6c/spHz5J7doZQSIJaaTiAU8Qv4zDuALGQaof5TqAyTvgCYgfn01rj0UNwGwjZj5goikFaDsACT+wGoFx0oBqA/gOt5YclKXU+PeUSHg2mC1QtzK8y6qrKP8TJVMW2ZnEocMf0xk4tPn9MKuBqyWSi+w4Qnm+ADqQCLhQzgegAI9lz0AIxTFvhHHHPKmTF2xMZtwVgAAJkZncGHINtzYEtxYiIQB7jBAIdLikxqeoBPzjkgMl1D+KayOr2T6O+szxs3QBvw7pRd3LyDWs49wUYh4dxNm3mlEDRy7GDN8PwnLRcJvMym6USR7qm8bAEAbt61GdCWGO0cmV/0FoGc3D0dC9RjSIGXLnvmvwsn2L7Q6LEzKRwz0AAAAAElFTkSuQmCC",
                          c0: [
                              { v0: "Toulouse Rabies", v1: "2023-01-02", c0: [{ v0: "Ingredient1a" }, { v0: "Ingredient2a" }] },
                              { v0: "Toulouse Distemper", v1: "2023-02-02", c0: [{ v0: "Ingredient3a" }] }
                          ],
                          c1: [{ v0: "Antiparasitic1a", v1: "2023-11-01" }, { v0: "Antiparasitic2a", v1: "2023-11-02" }]
                      }
                  ]
          }
          """;

    private static WebServer CreateTestServer(string urlBase)
        => new WebServer(o => o
            .WithUrlPrefix(urlBase)
            .WithMode(HttpListenerMode.EmbedIO))
            .WithStaticFolder("/", Path.Combine(Directory.GetCurrentDirectory(), "GeneratedVMFunctionTest"), true);

    private static RenderedPet ExpectedRenderedPetTobby
        => new(TobbyName, new ComparableEnumerable<string>([TobbyName, TobbyName, TobbyName]), TobbyPhoto, ExpectedRenderedVaccinesForTobby, ExpectedRenderedAntiparasiticsForTobby);

    private static string TobbyName
        => "Tobby";

    private static string TobbyPhoto
        => "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAAAAFzUkdCAK7OHOkAAAAEZ0FNQQAAsY8L/GEFAAAACXBIWXMAAADdAAAA3QFwU6IHAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAA0VJREFUWEftls+LTlEcxu+MRIxpKCaUaJoiaiaNKCWSSWzEYv6DYSFZSZGFUZQpS2VhobFHmbDGIFmYYjFTZDEs/EioIYznczz3dt/3vefOe1ny1KdzznPO9/u97/1x3pP882pxW0V7xT7RbdCEuSZuYjSrsgtYLo6LFeKLuCcGRZ8o02NxSWwVC8WUOCdei6Y0X5wUn8VMAePiqNgplhn6eMwVxZCLnOQuFXfkhiBoWgyJ0x7/EOfFvJmImPMa1jImlhzkYkzu0seeFnspup2UZ0t7LFRpQqx1zITHvCvkpE+NQu0XPwXPu9eBpwTtAzEHrxmx1jEhh71eQW5qUKtGG8QnwcIBB7QLArid6/GqiBjHkqPd3oCgpRY1Mz0STJwN0ZL6vPG0t2xVFrHOMWgr1LBHzaBNAuO5aPU6xmP2D9iqLGKdY8wW/VZBLfrUTi57cMhr6K+190bMtV1ZxDpHyGk71LJH7eSVB52ep3/G3rCtII35jnk0G21lkrdFHBELbAVpPCxCTlv0O+1RO7nrQZcn+U5f2OsJEZbGB+1P2cokj52ONruTSOMe++Rssddlj9rJRQ+eCN7MHR6Phww5yWMbfiuu2MqE57k+W5nkpTskualBLcbUThaJ+zbyhO+3XvKzF7VesTlyOWcealI7iG+eLfSpSBesc/xfi1y5vNSgFjUbtFqwaNKxhdI8O90ascvQL90pNT8paKkR1TbBolHHNUhz/eKd1+XB6/eyBmlu1OuokYlNIa9VbvnjiOmCWPK7WyM85mJKc6Y1guovYKXb8H1G1Cb4JfXCYy6mNGdaI6j+Aj66LXxBrOuCvYKTTyr6eMzFlOZMaxRqj+CXjPjRNUhzHeKZ1/FPB/TxOrysQZob8TpqRMUbyv81L1TZyYdbzbmPPtBv83SDNMdJiZzkLv0KEKdagg47Piqt4ag17WFU5HJOcs+q7YIr/UrfOQql+VkvgBzORU76TYkTLMHfBQeIxc5XI/nRCyDGseRgTM5KOiG4coK/iduCW7lb8A+3VIQLcB+POdbcEcQQSw5y/ZE4110V/MuRrArEEEuOqErP6DmxX2wWfEIcPNj1uMWAPpj3gtMw2+5DwXP/rxIlyS8JhwIQeBm/8wAAAABJRU5ErkJggg==";

    private static IEnumerable<RenderedVaccine> ExpectedRenderedVaccinesForTobby
        => new ComparableEnumerable<RenderedVaccine>([
                new RenderedVaccine("Tobby Rabies", "2023-01-01",new ComparableEnumerable<string>( ["Ingredient1", "Ingredient2"])),
                new RenderedVaccine("Tobby Distemper", "2023-02-01", new ComparableEnumerable<string>(["Ingredient3"]))
        ]);

    private static IEnumerable<RenderedAntiparasitic> ExpectedRenderedAntiparasiticsForTobby
        => new ComparableEnumerable<RenderedAntiparasitic>([
                new RenderedAntiparasitic("Antiparasitic1", "2024-12-01"),
                new RenderedAntiparasitic("Antiparasitic2", "2024-12-02")
        ]);

    private static RenderedPet ExpectedRenderedPetToulouse
        => new(ToulouseName, new ComparableEnumerable<string>([ToulouseName, ToulouseName, ToulouseName]), ToulousePhoto, ExpectedRenderedVaccinesForToulouse, ExpectedRenderedAntiparasiticsForToulouse);

    private static string ToulouseName
        => "Toulouse";

    private static string ToulousePhoto
        => "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAAsQAAALEBxi1JjQAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAVESURBVFiFrZdbbBRVGMd/30y325ZbS0u5CUJbMEpDooBBAbl4QeXFKEEpJt5iookxEF98EF9IvBCi4OWhDyZULg01QAIiIVGMCApIKmAFkWIhQGlLC7SlO7uzs/P5MLPd2bLbVuS87Dnn+873/53vnPlmFvpp+uuWVfrtBkeP1j6f1ef4jqm694uo/lzzTX+xsjWjX6ttr8C2TTo6avTY7oKMPu0d32NZYSzr0TsPEI+PBcCywsRubOhr1kO1S+m6McHztYfdDkBOv1ZVs7ff3v6a7v2yimmVjYTDcU6emMr1toKAr9wOQNYM6LGtJcRiJb0TCUewIgWITAdmELWGYdspwHjc1MNbH79jAFjxOuyYmTZnCIRyvX443IdYobu75o4AaP32u7nWsSAVW71OwVBIJnpo6sh77d1dY/Xwplf+NwA93Z+TSAjAmr3HeX3LIU+kqCjl4/c/2neSVzcdJMlAJPrebQNodXVIVYWbNx9Lzl25YdHSZZEQcEcW0/lnPddPHMXJDUE4j+bOCG3dUeKu6y3o6irTX+pGanV1CECb6sZoY+3L2lQ7KRNA2s3V7WvXYBhvAYXJLV3pjNBjO1Q8UEk0L5fmPXWAUjJ7IcNLx9NytJ6uqM3U0hGpQAUFnfREDjBy5rNMaDkDlAEdYM6WimWNWQEA9LvPwjhuBCeeyk5hIdxXCSJ0NtQT62ildP5Tnq3pH2i+nB6kdNwimbviRz1bNwpJtAUstVKxvCromn4EOz+chBXbT0H+Tc8qMG58rzhAbsko8kaPTy2aXAaTy8H0H5i8PIe25mrdsW6uTFl2FfRUQGKpXqzLD2qmFyI3LwbOBwwfdpOioj2MGj2k7+OWP2YC+WMmpO943DgoLYX2qw6W/S6Xzv+EKY6/x42ga33PEJbOAg4kl2atXtp6qBzMj4Hnsvn0aUdQVsqY2YfT4pyuKSaU2wx4BUR0lZRXrU/asxYiGT3nHKK/DVIchPN9xQHk3pc6gP0pIikO2vt/GSGLBw2gPKFaZ2a0iewKjAoHBaAtJ4ag8vCgAaCI1omzMgNoQ2A0ImjqJwORR0HD2e0ZhZ7MOK9YgYEGTdkBRBajCXCdgYXVBTdO1iNTgo/N9cEBKItwImB39plXQCG4j5TfLL16MNOHSWVAMq0SZn4bNh8rQbgHMQA3INQD9jWI94Ad2IgmQEwAEzfnoQwhF6S68vuAAJjxRwABwwveK5SE0UA/Od9bUuambebs5ruA+f6wnYulRwYGEJnn/ZrpQr15F9IygwuGmTTNS49lvAF4RtGvZeHCtEuVGUCZ7S++NQNi+POBS5A6AlAeTNYD/XtLGcgq3yuC637aV+oWAFU1SF4aIwfcYKrVW9L3brgBACigfXKFnq4pxjC2Af6Hq74vU168NCAAbT9MAob2jkVSu01mgH4yAGC1L8G+eh6Y6c/spHz5J7doZQSIJaaTiAU8Qv4zDuALGQaof5TqAyTvgCYgfn01rj0UNwGwjZj5goikFaDsACT+wGoFx0oBqA/gOt5YclKXU+PeUSHg2mC1QtzK8y6qrKP8TJVMW2ZnEocMf0xk4tPn9MKuBqyWSi+w4Qnm+ADqQCLhQzgegAI9lz0AIxTFvhHHHPKmTF2xMZtwVgAAJkZncGHINtzYEtxYiIQB7jBAIdLikxqeoBPzjkgMl1D+KayOr2T6O+szxs3QBvw7pRd3LyDWs49wUYh4dxNm3mlEDRy7GDN8PwnLRcJvMym6USR7qm8bAEAbt61GdCWGO0cmV/0FoGc3D0dC9RjSIGXLnvmvwsn2L7Q6LEzKRwz0AAAAAElFTkSuQmCC";

    private static IEnumerable<RenderedVaccine> ExpectedRenderedVaccinesForToulouse
        => new ComparableEnumerable<RenderedVaccine>([
                new RenderedVaccine("Toulouse Rabies", "2023-01-02", new ComparableEnumerable<string>(["Ingredient1a", "Ingredient2a"])),
                new RenderedVaccine("Toulouse Distemper", "2023-02-02", new ComparableEnumerable<string>(["Ingredient3a"]))
        ]);

    private static IEnumerable<RenderedAntiparasitic> ExpectedRenderedAntiparasiticsForToulouse
        => new ComparableEnumerable<RenderedAntiparasitic>([
                new RenderedAntiparasitic("Antiparasitic1a", "2023-11-01"),
                new RenderedAntiparasitic("Antiparasitic2a", "2023-11-02")
        ]);

    public void Dispose()
        => _server.Dispose();
}
