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

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='All']")));
            IWebElement aiMode = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='R1QWuf']")));
            string actualText = textArea.GetAttribute("value");

            //Verify value on search bar
            Assert.That(actualText, Is.EqualTo("simplespace"));

            //Verify "AI Mode" tab
            Assert.That(aiMode.Text, Is.EqualTo("AI Mode"));
        }
    }

}