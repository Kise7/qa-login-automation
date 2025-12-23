using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers; // Required for ExpectedConditions
using AutomationTests.Pages;
using System.Runtime.CompilerServices;

namespace AutomationTests
{
    public class Tests
    {
        private IWebDriver _driver;
        private const string LoginUrl = "https://www.google.com/"; 

        private IWebElement textArea  => _driver.FindElement(By.XPath("//*[@name='q']"));
        //private IWebElement allTab  => _driver.FindElement(By.XPath("//span[text()='All']"));
        
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            _driver = new ChromeDriver(options);
        }

        [TearDown]
        public void Teardown()
        {
           _driver?.Dispose();
        }

        [Test]
        public void Search_Redirects()
        {
            var page = new LoginPage(_driver, LoginUrl);
            page.GoTo();

            page.Search("simplespace");
            
            string actualText = textArea.GetAttribute("value");
            //string verifyAllTab = "All";
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement allTab = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='All']")));

            Assert.That(actualText, Is.EqualTo("simplespace"));
            Assert.That(allTab.Text, Is.EqualTo("All"));

            //Assert.That(currentURL, Does.Contain("/searchhh"), "User should be redirected to list.");
        }
        
        // public void ValidLogin_RedirectsToDashboard()
        // {
        //     var page = new LoginPage(_driver, LoginUrl);
        //     page.GoTo();

        //     page.Login("testuser", "password123");

        //     Assert.That(_driver.Url, Does.Contain("/dashboard"), "User should be redirected to dashboard.");
        // }

        // [Test]
        // public void InvalidLogin_ShowsErrorMessage()
        // {
        //     var page = new LoginPage(_driver, LoginUrl);
        //     page.GoTo();

        //     page.Login("wronguser", "wrongpass");

        //     Assert.That(page.GetErrorText(), Is.EqualTo("Invalid credentials"), "Error message should be visible.");
        // }

        // [Test]
        // public void EmptyFields_ShowsValidation()
        // {
        //     var page = new LoginPage(_driver, LoginUrl);
        //     page.GoTo();

        //     page.Login("", "");

        //     // Example: check client-side validation
        //     var userAria = _driver.FindElement(By.Id("username")).GetAttribute("aria-invalid");
        //     var passAria = _driver.FindElement(By.Id("password")).GetAttribute("aria-invalid");

        //     Assert.That(userAria, Is.EqualTo("true"));
        //     Assert.That(passAria, Is.EqualTo("true"));
        // }
    }

}

// public class Tests
// {
//     [SetUp]
//     public void Setup()
//     {
//     }

//     [Test]
//     public void Test1()
//     {
//         Assert.Pass();
//     }
// }
