using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaniniWebAPI.Controllers
{
    public class StickerGeneratorController : ApiController
    {
        public HttpResponseMessage Post(Models.StickerRequest stickerRequest)
        {

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
