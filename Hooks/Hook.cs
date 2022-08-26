//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using TechTalk.SpecFlow;
//using BoDi;
//using SeleniumBDDSpecflow.Steps;
//using SeleniumBDDSpecflow.Pages;
//using Allure.Commons;
//using TechTalk.SpecFlow.Infrastructure;

//namespace SeleniumBDDSpecflow.Hooks
//{
//     [Binding]
//    public class Hook
//    {

//        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
//        private readonly ScenarioContext _scenarioContext;
//        private readonly FeatureContext _featureContext;
//        private const int SlightWaitTimeout = 3000;


//        public Hook(ISpecFlowOutputHelper outputHelper, ScenarioContext scenarioContext, FeatureContext featureContext)
//        {
//            _specFlowOutputHelper = outputHelper;
//            _scenarioContext = scenarioContext;
//            _featureContext = featureContext;
//        }

//        [AfterStep]
//        public void RecordScreenFailure(ScenarioContext scenarioContext)
//        {
//            if (scenarioContext.TestError != null)
//            {
//                AllureLifecycle.Instance.UpdateTestCase(testResult =>
//                {
//                    StepResult stepResult =
//                        testResult.steps.Find(stepItem => stepItem.name.Contains(_scenarioContext.StepContext.StepInfo.Text));
//                    if (stepResult != null)
//                    {
//                        string stepName = _scenarioContext.ScenarioInfo.Title;
//                        CaptureImage captureImage = Capture.Screen();
//                        Attachment att = stepResult?.attachments.Find(attachment => attachment.name.Equals(stepName));
//                        if (att != null) stepResult?.attachments.Remove(att);
//                        string filePath = Path.GetFullPath(Environment.CurrentDirectory + @"\..\..\Screenshots\Failures\" + stepName + ".jpg");
//                        captureImage.ToFile(filePath);
//                        _specFlowOutputHelper.AddAttachment(filePath);
//                        stepResult?.attachments.Add(new Attachment()
//                        {
//                            name = stepName,
//                            source = filePath,
//                            type = "image/png"
//                        });
//                    }
//                });
//            }
//        }       
//    }
//}
