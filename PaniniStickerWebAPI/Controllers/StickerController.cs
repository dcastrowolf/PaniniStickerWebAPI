using PaniniWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaniniStickerWebAPI.Controllers
{
    [RoutePrefix("api/v2/StickerGenerator")]
    public class StickerController : ApiController
    {

        public IHttpActionResult Post(StickerRequest stickerRequest)
        {
            //TODO: Implement the facade of this http resource
            throw new NotImplementedException();
        }

    }
}
