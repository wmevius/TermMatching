
using System.Collections.Generic;

namespace TermMatching.Models
{
    /// <summary>
    /// Interface for the repository class, to facilitate mocking if required
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITermsRepository<T> where T : class
    {
        List<T> GetAll();
        void AddTerm(T t);
    }
}
