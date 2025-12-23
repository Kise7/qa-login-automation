using OpenQA.Selenium;

namespace AutomationTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url;

        public LoginPage(IWebDriver driver, string url)
        {
            _driver = driver;
            _url = url;
        }

        // Locators
        private IWebElement UsernameInput => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton   => _driver.FindElement(By.Id("loginButton"));
        private IWebElement ErrorMessage  => _driver.FindElement(By.Id("errorMessage"));

        private IWebElement searchBar  => _driver.FindElement(By.XPath("//*[@title='Search']"));
        private IWebElement searchButton  => _driver.FindElement(By.XPath("//*[@value='Google Search']"));


        public void GoTo() => _driver.Navigate().GoToUrl(_url);

        public void Login(string user, string pass)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(user);
            PasswordInput.Clear();
            PasswordInput.SendKeys(pass);
            LoginButton.Click();
        }

        public void Search(string keyword)
        {
            searchBar.Clear();
            searchBar.SendKeys(keyword);
            searchButton.Click();
        }

        public string GetErrorText() => ErrorMessage.Text;
    }
}
