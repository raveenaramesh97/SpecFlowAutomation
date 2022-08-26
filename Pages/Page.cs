using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace SeleniumBDDSpecflow.Pages
{
   public class Page
    {
        protected IWebDriver Driver;

        public Page(IWebDriver driver) 
        {
            Driver = driver;
        }
    }
}
