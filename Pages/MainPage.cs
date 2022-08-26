using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SeleniumBDDSpecflow.Pages
{
   public class MainPage : Page
    {
        IWebDriver driver = null;
        public ObjectContainer _objectContainer;
        public FeatureContext _featureContext;


        string URL = "https://www.google.com";
        string virtusaURL = "https://phptravels.com/demo/";
        private static string plansprices = "//div[@class='plan-type wow fadeIn animated']";
        public MainPage(IWebDriver driver, FeatureContext featureContext) : base(driver)
        {
            this.driver = driver;
            _featureContext = featureContext;

        }

        public void LaunchBrowser()
        {
            driver.Navigate().GoToUrl(URL);           
        }
        public void OpenURL(IWebDriver driver)
        {            
            driver.Navigate().GoToUrl(virtusaURL);
            driver.Manage().Window.Maximize();
        }
       
        public void ClickOnItem(string element)
        {
            IWebElement button = driver.FindElement(By.ClassName(element));
            button.Click();
        }

        public string GetAttribute(IWebDriver driver)
        {
           IWebElement element1= driver.FindElement(By.XPath("//a[@class='BS-logo nav-link']"));
            string actualValue = element1.GetAttribute("title");
            return actualValue;
        }

        public void EnterTextWithNamw(string element, string name)
        {
            IWebElement textbox = driver.FindElement(By.Name(element));
            textbox.SendKeys(name);
        }
        public string GetHref(WebElement element1)
        {
            string actualValue = element1.GetAttribute("href");
            return actualValue;
        }
        public void ClickOnItemByLink(string element)
        {
            var link = driver.FindElement(By.XPath("//a[@href='"+element+"']"));
            link.Click();
        }

        public bool isElementEnabled(string element)
        {
            IWebElement button = driver.FindElement(By.Id(element));
            bool value = button.Enabled;
            return value;
        }

        public ReadOnlyCollection<IWebElement> collectAllLinksOnWebPage(string element)
        {
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> webElements = driver.FindElements(By.TagName(element));
            return webElements;
        }
        public bool isElementDisplayed()
        {
            IWebElement button = driver.FindElement(By.XPath(plansprices));
            bool value = button.Displayed;
            return value;
        }
        public void SwitchToChildWindow()
        {

            string newWindowHandle = driver.WindowHandles.Last();
            var newWindow = driver.SwitchTo().Window(newWindowHandle);
            Console.WriteLine(newWindow);
        }

        public void CloseApp()
        {
            driver.Quit();
        }
    }
}
