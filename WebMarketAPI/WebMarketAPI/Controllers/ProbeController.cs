using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebMarketAPI.Controllers
{
    public class ProbeController : ApiController
    {
        [Route("~/")]
        public IHttpActionResult Get() 
        {
            return Ok("Ok");
        }
    }
}
