using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TermMatching.Models;

namespace TermMatching.Controllers
{
    public class TermsController : ApiController
    {
        public ITermsRepository<string> Repository { get; set; }

        public TermsController()
        {
            Repository = new TermsRepository();
        }

        public TermsController(ITermsRepository<string> repository)
        {
            Repository = repository;
        }

        // GET: Terms
        [Route("Terms")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Repository.GetAll();
        }


        // POST: Terms
        [Route("Terms")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string value)
        {
            Repository.AddTerm(value);

            return Request.CreateErrorResponse(HttpStatusCode.OK, $"Term '{value}' added okay");
        }

        // POST/ Terms/Check
        [Route("Terms/Check")]
        [HttpPost]
        public HttpResponseMessage Check([FromBody]string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Text supplied");
            }
            else
            {
                TermsLogic logic = new TermsLogic(Repository);
                return Request.CreateResponse<List<ResponseMessage>>(HttpStatusCode.OK, logic.FindTerms(value));
            }
        }
    }
}
