using NUnit.Framework;

using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;

namespace ExamDesktopTests
{
    public class DesktopTests
    {
        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string url = "https://contactbook.nakov.repl.co/api";
        private const string appLocation = @"C:\TaskBoard.DesktopClient-v1.0\TaskBoard.DesktopClient.exe";

        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;

        [SetUp]
        public void StartApp()
        {
            options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability("app", appLocation);
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }

        [Test]
        public void Test1_VeryfyLogin()
        {
            var urlField = driver.FindElementByAccessibilityId("labelApiUrl");
            urlField.Clear();
            urlField.SendKeys(url);

            var connectButton = driver.FindElementByAccessibilityId("buttonConnect");
            connectButton.Click();

            var minimizeButton = driver.FindElementByXPath("/Window/TitleBar/Button[1]");
            var maximizeButton = driver.FindElementByXPath("/Window/TitleBar/Button[2]");
            var closeButton = driver.FindElementByXPath("/Window/TitleBar/Button[3]");
            Assert.That(minimizeButton.Text, Is.EqualTo("Minimize"));
            Assert.That(maximizeButton.Text, Is.EqualTo("Maximize"));
            Assert.That(closeButton.Text, Is.EqualTo("Close"));

        }

        [Test]
        public void Test2_VeryfyFirstTask()
        {
            var urlField = driver.FindElementByAccessibilityId("labelApiUrl");
            urlField.Clear();
            urlField.SendKeys(url);

            var connectButton = driver.FindElementByAccessibilityId("buttonConnect");
            connectButton.Click();

            var searchField = driver.FindElementByAccessibilityId("textBoxSearchText");
            searchField.SendKeys("Project skeleton");
            var searchButton = driver.FindElementByAccessibilityId("buttonSearch");
            searchButton.Click();

            var searchedItem = driver.FindElementByAccessibilityId("HeaderItem 0");
            Assert.That(searchedItem.Text, Is.EqualTo("Id"));
            var searchedItems = driver.FindElementsByAccessibilityId("HeaderItem 0");
            Assert.That(searchedItems.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test3_CreateNewValidTask()
        {
            var urlField = driver.FindElementByAccessibilityId("labelApiUrl");
            urlField.Clear();
            urlField.SendKeys(url);

            var connectButton = driver.FindElementByAccessibilityId("buttonConnect");
            connectButton.Click();

            var AddButton = driver.FindElementByAccessibilityId("buttonAdd");
            AddButton.Click();
            var titleField = driver.FindElementByAccessibilityId("textBoxTitle");
            titleField.SendKeys("This is the TASK!");
            var descriptionField = driver.FindElementByAccessibilityId("labelDescription");
            descriptionField.SendKeys("This test destroyed me!");
            var createButton = driver.FindElementByAccessibilityId("buttonCreate");
            createButton.Click();
            var searchField = driver.FindElementByAccessibilityId("textBoxSearchText");
            searchField.SendKeys("This is the TASK!");
            var searchButton = driver.FindElementByAccessibilityId("buttonSearch");
            searchButton.Click();
            var searchedItem = driver.FindElementByAccessibilityId("HeaderItem 0");
            Assert.That(searchedItem.Text, Is.EqualTo("Id"));
            var searchedItems = driver.FindElementsByAccessibilityId("HeaderItem 0");
            Assert.That(searchedItems.Count, Is.EqualTo(1));
        }
    }
}