using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBookStore;

namespace MyBookStoreTests
{
    public class BookIntegrationTests
    {
        private readonly HttpClient _client;

        public BookIntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public void CustomerGetAllTest()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/books/");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
