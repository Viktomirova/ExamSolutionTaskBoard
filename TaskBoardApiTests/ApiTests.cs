using NUnit.Framework;

using RestSharp;
using System.Collections.Generic;
using System.Net;
using System;
using System.Text.Json;
using System.Linq;

namespace ExamApiTests
{
    public class ApiTests
    {
        private RestClient client;
        private RestRequest request;
        private const string url = "https://taskboard.nakov.repl.co/api/tasks";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(url);
        }

        [Test]
        public void Test1_GetAllTasks()
        {
            // Arrange
            request = new RestRequest(url);

            // Act
            var response = client.Execute(request);
            var tasks = JsonSerializer.Deserialize<List<Construction>>(response.Content);

            // Assert
            Assert.IsNotNull(response.Content);
            Assert.That(tasks.Count, Is.GreaterThan(0));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Test1_GetFirstTasksName()
        {
            // Arrange
            request = new RestRequest(url);

            // Act
            var response = client.Execute(request);
            var tasks = JsonSerializer.Deserialize<List<Construction>>(response.Content);

            // Assert
            Assert.That(tasks.First().title, Is.EqualTo("Project skeleton"));
        }

        [Test]
        public void Test2_SearchValidKeyword()
        {
            // Arrange
            request = new RestRequest(url + "/search/Home");

            // Act
            var response = client.Execute(request);
            var tasks = JsonSerializer.Deserialize<List<Construction>>(response.Content);

            // Assert
            Assert.IsNotNull(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(tasks.First().title, Is.EqualTo("Home page"));
        }

        [Test]
        public void Test3_SearchInvalidKeyword()
        {
            // Arrange
            var randnum = DateTime.Now.Ticks;
            request = new RestRequest(url + "/search/" + randnum);

            // Act
            var response = client.Execute(request);
            var tasks = JsonSerializer.Deserialize<List<Construction>>(response.Content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(tasks.Count == 0);
        }

        [Test]
        public void Test4_CreateInvalidTask()
        {
            // Arrange
            string title = String.Empty;
            string description = "CreateTask_InvalidData";
            string board = "Open";
            request = new RestRequest(url);
            request.AddJsonBody(new { title, description, board });

            // Act
            var response = client.Execute(request, Method.Post);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void Test5_CreateValidTask()
        {
            // Arrange
            string title = "New Title" + DateTime.Now.Ticks;
            string description = "CreateTask_ValidData";
            string board = "Open";
            request = new RestRequest(url);
            request.AddJsonBody(new { title, description, board });
            var response = client.Execute(request, Method.Post);

            // Act
            request = new RestRequest(url + "/search/" + title);
            var secondResponse = client.Execute(request);
            var tasks = JsonSerializer.Deserialize<List<Construction>>(secondResponse.Content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(tasks.First().title, Is.EqualTo(title));
        }
    }
}