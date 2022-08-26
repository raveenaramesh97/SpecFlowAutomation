using System;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Text;
using Allure.Commons;
using System.IO;
using TechTalk.SpecFlow.Infrastructure;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace SeleniumBDDSpecflow.Hooks
{
    [Binding]
    public sealed class hooks
    {
        static string configTheme = "standard";
        static string configReportPath = @$"E:\SeleniumC#SpecFlow\SeleniumBDDSpecflow\ExtentReports\ExtentReport.html";
        static string filepath = @$"E:\SeleniumC#SpecFlow\SeleniumBDDSpecflow\TestResults";
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        [ThreadStatic]
        private static ExtentTest feature;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static ExtentReports extentReport;
        private IWebDriver driver;
        private static readonly string base64ImageType = "base64";

        [BeforeTestRun]
        public static void InitializeReport()
        {
            //Initialize Extent report before test starts
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(configReportPath);

            switch (configTheme.ToLower())
            {
                case "dark":
                    htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                    break;
                case "standard":
                default:
                    htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
                    break;
            }

            //Attach report to reporter
            extentReport = new ExtentReports();
            extentReport.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extentReport.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            feature = extentReport.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void InitializeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            scenario = feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void CleanUp(ScenarioContext scenarioContext)
        {
            //to check if we missed to implement any step
            string resultOfImplementation = scenarioContext.ScenarioExecutionStatus.ToString();

            //Pending Status
            if (resultOfImplementation == "UndefinedStep")
            {
                // Log.StepNotDefined();
            }
        }
        public static string TakesScreenshot(IWebDriver driver, string FileName)
        {

            string pathProject = AppDomain.CurrentDomain.BaseDirectory;
            string pathScreen = pathProject.Replace("\\bin\\Debug", "");
            string path = pathScreen + "Images/";

            StringBuilder TimeAndDate = new StringBuilder(DateTime.Now.ToString());
            TimeAndDate.Replace("/", "_");
            TimeAndDate.Replace(":", "_");
            TimeAndDate.Replace(" ", "_");

            string imageName = FileName + TimeAndDate.ToString();

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path + "_" + imageName + "." + System.Drawing.Imaging.ImageFormat.Jpeg);

            return path + "_" + imageName + "." + "jpeg";
        }
        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepInfo = scenarioContext.StepContext.StepInfo.Text;


            //to check if we missed to implement steps inside method
            string resultOfImplementation = scenarioContext.ScenarioExecutionStatus.ToString();


            if (scenarioContext.TestError == null && resultOfImplementation == "OK")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo);
                else if (stepType == "And")
                    scenario.CreateNode<And>(stepInfo);
                else if (stepType == "But")
                    scenario.CreateNode<And>(stepInfo);
            }
            else if (scenarioContext.TestError != null)
            {
                Exception? innerException = scenarioContext.TestError.InnerException;
                string? testError = scenarioContext.TestError.Message;

                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo).Fail(innerException, MediaEntityBuilder.CreateScreenCaptureFromPath(filepath).Build());
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo).Fail(innerException, MediaEntityBuilder.CreateScreenCaptureFromPath(filepath).Build());
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo).Fail(testError, MediaEntityBuilder.CreateScreenCaptureFromPath(filepath).Build());
                else if (stepType == "And")
                    scenario.CreateNode<Then>(stepInfo).Fail(testError, MediaEntityBuilder.CreateScreenCaptureFromPath(filepath).Build());
                else if (stepType == "But")
                    scenario.CreateNode<Then>(stepInfo).Fail(testError, MediaEntityBuilder.CreateScreenCaptureFromPath(filepath).Build());
            }
            else if (resultOfImplementation == "StepDefinitionPending")
            {
                string errorMessage = "Step Definition is not implemented!";

                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo).Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo).Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo).Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "But")
                    scenario.CreateNode<Then>(stepInfo).Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());

            }

        }

    }
}