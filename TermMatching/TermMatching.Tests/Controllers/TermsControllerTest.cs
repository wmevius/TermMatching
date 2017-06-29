using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TermMatching.Controllers;
using System.Net.Http;
using TermMatching.Models;
using System.Runtime.Caching;
using System.IO;

namespace TermMatching.Tests.Controllers
{
    /// <summary>
    /// Summary description for TermsControllerTest
    /// </summary>
    [TestClass]
    public class TermsControllerTest
    {

        private const string cacheKey = "testkey";
        private const string testDataFile = "testdata";

        private TermsController controller;

        [TestInitialize]
        public void Initialize()
        {
            TermsRepository repository = new TermsRepository(testDataFile, cacheKey);
            controller = new TermsController(repository);
            controller.Request = new HttpRequestMessage();
        }


        [TestMethod]
        public void TestPost()
        {
            HttpResponseMessage message =  controller.Post("value");

            Assert.AreEqual(System.Net.HttpStatusCode.OK, message.StatusCode);
        }

        [TestMethod]
        public void TestCheck()
        {
            HttpResponseMessage message = controller.Check("");

            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, message.StatusCode);
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(testDataFile))
            {
                File.Delete(testDataFile);
            }
        }
    }
}
