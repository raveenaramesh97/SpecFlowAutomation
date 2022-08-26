using BoDi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumBDDSpecflow.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SeleniumBDDSpecflow.Steps
{
    [Binding]
    public  class SampleTestStepDefinitions 
    {

        protected IWebDriver driver;

        
        private readonly FeatureContext featureContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public SampleTestStepDefinitions(FeatureContext _featureContext, ISpecFlowOutputHelper outputHelper)
        {
            featureContext = _featureContext;
            _specFlowOutputHelper = outputHelper;
            driver = new ChromeDriver();

        }

        [Given(@"Chrome Browser is Launched")]
        public void GivenChromeBrowserIsLaunched()
        {
            MainPage page = new MainPage(driver, featureContext);
            page.LaunchBrowser();
        }

        [Given(@"I go to PHPTravels Page")]
        [When(@"I go to PHPTravels Page")]
        public void GivenIGoToPHPTravelsPage()
        {
            MainPage page = new MainPage(driver, featureContext);

            page.OpenURL(driver);            
        }
       
        [Then(@"I see PHPTravels Page is Opened")]
        public void ThenISeePHPTravelsPageIsOpened()
        {
            MainPage page = new MainPage(driver, featureContext);

            var actualValue = page.GetAttribute(driver);
            Assert.AreEqual(actualValue, "phptravels");
        }

        [When(@"I Try to create an Instant Demo Request with '(.*)' '(.*)' '(.*)' '(.*)' '(.*)' '(.*)' '(.*)' '(.*)'")]
        public void WhenITrytoCreateAnInstantDemoRequestWith(string firstName, string firstElement, string LastName,string LastElement, string BusinessName, string BusinessElement, string email, string emailElement)
        {
            MainPage page = new MainPage(driver, featureContext);
            page.EnterTextWithNamw(firstElement, firstName);
            page.EnterTextWithNamw(LastElement, LastName);
            page.EnterTextWithNamw(BusinessElement, BusinessName);
            page.EnterTextWithNamw(emailElement, email);
        }

        [Then(@"I see Submit '(.*)' button is disabled")]
        public void ThenISeeSubmitButtonIsDisabled(string element)
        {
            MainPage page = new MainPage(driver, featureContext);
            bool value = page.isElementEnabled(element);
            Assert.IsFalse(value);
        }

        [When(@"I Collect all '(.*)' Links on the Page")]
        [Obsolete]
        public void WhenICollectAllLinksOnThePage(string element)
        {
            MainPage page = new MainPage(driver, featureContext);

            ReadOnlyCollection<IWebElement> webElements = page.collectAllLinksOnWebPage(element);
            ScenarioContext.Current.Add("list", webElements);
        }

        [Then(@"I filter '(.*)' and click on the link")]
        [Obsolete]
        public void ThenIFilterAndClickOnTheLink(string p0)
        {
            MainPage page = new MainPage(driver, featureContext);
            ReadOnlyCollection<IWebElement> links = ScenarioContext.Current.Get<ReadOnlyCollection<IWebElement>>("list");

            foreach (WebElement element in links)
            {
               string link= page.GetHref(element);
                if (link.Equals("https://phptravels.com/order"))
                {
                    page.ClickOnItemByLink(link);
                    page.SwitchToChildWindow();
                    break;
                }

            }
        }
        [Then(@"I see Plans and Prices page displayed")]
        public void ThenISeePlansAndPricesPageDisplayed()
        {
            MainPage page = new MainPage(driver, featureContext);
            bool value = page.isElementDisplayed();
            Assert.IsTrue(value);
        }

        [Then(@"I click on '(.*)' link on the child window")]
        public void WhenIClickOnLinkOnTheChildWindow(string element)
        {
            MainPage page = new MainPage(driver, featureContext);
            page.ClickOnItemByLink("https://docs.phptravels.com");
            page.SwitchToChildWindow();
        }
        [Then(@"I Close the browser")]
        public void ThenICloseTheBrowser()
        {
            MainPage page = new MainPage(driver, featureContext);
            page.CloseApp();
        }             
    }
}
