using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Clientes.WebApi.Controllers
{
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Service is running");
        }
    }
}