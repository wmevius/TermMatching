using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Caching;
using System.Collections.Generic;
using TermMatching.Models;
using System.IO;

namespace TermMatching.Tests.Models
{
    [TestClass]
    public class TermsRepositoryTest
    {
        private const string cacheKey = "testkey";
        private const string testDataFile = "testdata";

        private List<string> testTerms;
        private TermsRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new TermsRepository(testDataFile, cacheKey);
            testTerms = new List<string>() { "term1", "term2" };

            MemoryCache cache = MemoryCache.Default;
            cache[cacheKey] = testTerms;
        }

        [TestMethod]
        public void TestGetAll()
        {
            List<string> allTerms = repository.GetAll();
            Assert.AreEqual(testTerms.Count, allTerms.Count);
        }

        [TestMethod]
        public void TestAdd()
        {
            repository.AddTerm("term3");

            List<string> allTerms = repository.GetAll();
            Assert.IsTrue(allTerms.Contains("term3"));
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
