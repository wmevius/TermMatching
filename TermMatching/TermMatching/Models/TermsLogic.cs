using System.Collections.Generic;

namespace TermMatching.Models
{
    public class TermsLogic
    {
        public ITermsRepository<string> Repository { get; set; }

        public TermsLogic(ITermsRepository<string> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Returns the list of terms and whether they can be found in the input string.
        /// </summary>
        /// <param name="input">The string to look for terms in.</param>
        /// <returns>A list of all terms with for each a message to say whether it appears in the string.</returns>
        public List<ResponseMessage> FindTerms(string input)
        {
            List<ResponseMessage> responseList = new List<ResponseMessage>();

            foreach (string term in Repository.GetAll())
            {
                // start creating the return object
                ResponseMessage messageForTerm = new ResponseMessage() { Term = term };

                // we need to return the portion of the input string
                // while ignoring case when matching the term
                int locationOfTermInInput = input.ToLower().IndexOf(term.ToLower());
                if (locationOfTermInInput > -1)
                {
                    // there is a match, now add the matching part of the input string to make sure case is correct
                    string termAsInInput = input.Substring(locationOfTermInInput, term.Length);
                    messageForTerm.Message = $"Match found '{termAsInInput}'";
                }
                else
                {
                    // there is no match
                    messageForTerm.Message = "No Match";
                }
                responseList.Add(messageForTerm);
            }
            return responseList;
        }
    }
}
