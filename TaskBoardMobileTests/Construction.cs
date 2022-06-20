using OpenQA.Selenium.Appium.Android;
using System.Collections.Generic;

namespace ExamMobileTests
{
    internal class Construction
    {
        private readonly AndroidDriver<AndroidElement> driver;

        public Construction(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public AndroidElement ConnectionField => driver.FindElementById("taskboard.androidclient:id/editTextApiUrl");
        public AndroidElement ConnectButton => driver.FindElementById("taskboard.androidclient:id/buttonConnect");
        public AndroidElement SearchField => driver.FindElementById("taskboard.androidclient:id/editTextKeyword");
        public AndroidElement SearchButton => driver.FindElementById("taskboard.androidclient:id/buttonSearch");
        public AndroidElement AddButton => driver.FindElementById("taskboard.androidclient:id/buttonAdd");
        public AndroidElement editTextField => driver.FindElementById("taskboard.androidclient:id/editTextTitle");
        public AndroidElement CreateButton => driver.FindElementById("taskboard.androidclient:id/buttonCreate");
        public IReadOnlyCollection<AndroidElement> TextViewTitle => driver.FindElementsById("taskboard.androidclient:id/textViewTitle");

        public void Connect()
        {
            ConnectionField.Clear();
            ConnectionField.SendKeys("https://taskboard.nakov.repl.co/api");
            ConnectButton.Click();
        }
        public void SearchTask(string text)
        {
            SearchField.Clear();
            SearchField.SendKeys(text);
            SearchButton.Click();
        }
        public void AddNewTask(string text)
        {
            AddButton.Click();
            editTextField.SendKeys(text);
            CreateButton.Click();
        }
    }
}
