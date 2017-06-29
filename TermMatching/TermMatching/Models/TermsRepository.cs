using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;

namespace TermMatching.Models
{
    /// <summary>
    /// Repository class to allow access to our data
    /// </summary>
    public class TermsRepository : ITermsRepository<string>
    {

        private const string defaultDataFileName = "Terms.dat";
        private const string defaultCacheKey = "Terms";

        public string DataFileName { get; set; }
        public string CacheKey { get; set; }

        public TermsRepository()
        {
            DataFileName = defaultDataFileName;
            CacheKey = defaultCacheKey;
        }

        public TermsRepository(string dataFileName, string cacheKey)
        {
            DataFileName = dataFileName;
            CacheKey = cacheKey;
        }

        /// <summary>
        /// Returns all terms
        /// </summary>
        /// <returns>a list of terms</returns>
        public List<string> GetAll()
        {
            MemoryCache cache = MemoryCache.Default;

            List<string> terms = (List<string>)cache.Get(CacheKey);

            if (terms == null)
            {
                terms = new List<string>();

                try
                {
                    using (StreamReader reader = new StreamReader(DataFileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            terms.Add(reader.ReadLine());
                        }
                    }
                }
                catch
                {
                    // no need to worry: most likely file didn't exist yet
                }
            }
            return terms;
        }

        /// <summary>
        /// Add a term to the list if it isn't in it yet
        /// </summary>
        /// <param name="newTerm">The term to add</param>
        public void AddTerm(string newTerm)
        {
            List<string> terms = GetAll();
            if (!terms.Contains(newTerm))
            {
                terms.Add(newTerm);

                using (StreamWriter writer = new StreamWriter(DataFileName))
                {
                    foreach (string term in terms)
                    {
                        writer.WriteLine(term);
                    }
                }
                MemoryCache cache = MemoryCache.Default;
                cache[CacheKey] = terms;
            }
        }
    }
}