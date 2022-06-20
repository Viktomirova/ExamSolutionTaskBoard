using Castle.Core.Configuration;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;

using System.Threading;

using System;
using System.Linq;

namespace ExamMobileTests
{
    public class MobileTests
    {
        private const string AppiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private const string AppPath = @"C:\taskboard-androidclient.apk";
        protected AndroidDriver<AndroidElement> driver;

        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", AppPath);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }

        [Test]
        public void Test1_GetFirstTask()
        {
            var configuration = new Construction(driver);
            configuration.Connect();
            Thread.Sleep(10000);
            var firstElementTitle = configuration.TextViewTitle.First().Text;
            Assert.That(firstElementTitle, Is.EqualTo("Project skeleton"));
        }

        [Test]
        public void Test2_CreateNewTask()
        {
            var configuration = new Construction(driver);
            configuration.Connect();
            Thread.Sleep(10000);
            var title = "Configuration failed" + DateTime.Now.Ticks;

            configuration.AddNewTask(title);
            configuration.SearchTask(title);
            Thread.Sleep(10000);
            var firstElement = configuration.TextViewTitle.First().Text;
            Assert.That(firstElement, Is.EqualTo(title));
        }
    }
}