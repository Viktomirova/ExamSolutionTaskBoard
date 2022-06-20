using OpenQA.Selenium;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamWebAppTests
{
    public class Construction
    {
        protected readonly IWebDriver driver;

        private const string url = "https://taskboard.nakov.repl.co/boards";
        public Construction(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public Board board { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }
        public IReadOnlyCollection<IWebElement> tasksList => driver.FindElements(By.CssSelector("body > main > div > div > table > tbody"));
        public IReadOnlyCollection<IWebElement> boardsList => driver.FindElements(By.CssSelector("body > main > div > div"));
                
        public List<string> GetAllTasks()
        {
            var tasks = tasksList.Select(t => t.Text).ToList();
            return tasks;
        }
        
        public List<string> GetAllBoards()
        {
            var boards = boardsList.Select(b => b.Text).Where(b => b != null).ToList();
            return boards;
        }

        public void Open()
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }
    }
}
