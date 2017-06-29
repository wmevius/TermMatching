using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TermMatching.Models;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.IO;

namespace TermMatching.Tests.Models
{
    [TestClass]
    public class TermsLogicTest
    {
        private const string cacheKey = "testkey";
        private const string testDataFile = "testdata";

        private List<string> testTerms;
        private TermsRepository repository;

        private TermsLogic logic;

        [TestInitialize]
        public void Initialize()
        {
            repository = new TermsRepository(testDataFile, cacheKey);
            testTerms = new List<string>() { "term1", "term2" };

            MemoryCache cache = MemoryCache.Default;
            cache[cacheKey] = testTerms;

            logic = new TermsLogic(repository);
        }

        [TestMethod]
        public void TestFindTerms()
        {        
            List<ResponseMessage> findTermResults = logic.FindTerms("Term1 is there");

            Assert.AreEqual(testTerms.Count, findTermResults.Count);

            Assert.IsTrue(findTermResults.Find(t => t.Term == "term1").Message == "Match found 'Term1'");
            Assert.IsTrue(findTermResults.Find(t => t.Term == "term2").Message == "No Match");
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
