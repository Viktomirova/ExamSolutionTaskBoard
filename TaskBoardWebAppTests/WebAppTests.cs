using NUnit.Framework;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Linq;
using System;

namespace ExamWebAppTests
{
    public class WebAppTests
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void ShutDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test1_GetFirstElementFromBoard()
        {
            // Arrange
            var page = new Construction(driver);
            page.Open();

            // Act
            var tasks = page.GetAllTasks();
            var boards = page.boardsList;

            // Assert
            Assert.That(tasks.Count() > 0);

            foreach (var board in boards)
            {
                var boardName = board.FindElement(By.CssSelector("h1")).Text;
                if (boardName == "Done")
                {
                    var taskTitle = board.FindElement(By.CssSelector("table > tbody > tr > td")).Text;
                    Assert.That(taskTitle, Is.EqualTo("Project skeleton"));
                    break;
                }
            }
        }

        [Test]
        public void Test2_FindTaskByKeyword()
        {
            // Arrange
            var page = new Construction(driver);
            page.Open();
            driver.FindElement(By.CssSelector("li:nth-of-type(4) > a")).Click();

            // Act
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("Home");
            driver.FindElement(By.CssSelector("button#search")).Click();


            // Assert
            var result = driver.FindElement(By.CssSelector(".title > td")).Text;
            Assert.That(result, Is.EqualTo("Home page"));
        }

        [Test]
        public void Test3_SearchForMissingTask()
        {
            // Arrange
            var page = new Construction(driver);
            page.Open();
            driver.FindElement(By.CssSelector("li:nth-of-type(4) > a")).Click();

            // Act
            var randnum = DateTime.Now.Ticks;
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("missing" + randnum);
            driver.FindElement(By.CssSelector("button#search")).Click();


            // Assert
            var result = driver.FindElement(By.CssSelector("div#searchResult")).Text;
            Assert.That(result, Is.EqualTo("No tasks found."));
        }

        [Test]
        public void Test4_TryToCreateInvalidTask()
        {
            // Arrange
            var page = new Construction(driver);
            page.Open();
            driver.FindElement(By.CssSelector("li:nth-of-type(3) > a")).Click();

            // Act
            driver.FindElement(By.CssSelector("button#create")).Click();


            // Assert
            var result = driver.FindElement(By.CssSelector(".err")).Text;
            Assert.That(result, Is.EqualTo("Error: Title cannot be empty!"));
        }

        [Test]
        public void Test5_CreateValidTask()
        {
            // Arrange
            var page = new Construction(driver);
            page.Open();
            driver.FindElement(By.CssSelector("li:nth-of-type(3) > a")).Click();

            // Act
            var titleField = driver.FindElement(By.CssSelector("input#title"));
            titleField.SendKeys("Whitesnake");
            var descriptionField = driver.FindElement(By.CssSelector("textarea#description"));
            descriptionField.SendKeys("With some description...");
            driver.FindElement(By.Id("create")).Click();

            // Assert
            driver.FindElement(By.CssSelector("li:nth-of-type(4) > a")).Click();
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("Whitesnake");
            driver.FindElement(By.CssSelector("button#search")).Click();

            var taskTitle = driver.FindElement(By.CssSelector(".title > td")).Text;
            Assert.That(taskTitle, Is.EqualTo("Whitesnake"));
        }
    }
}