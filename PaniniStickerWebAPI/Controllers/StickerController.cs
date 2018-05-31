using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaniniStickerWebAPI.Controllers
{
    public class StickerController : ApiController
    {
        // GET: api/Sticker
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sticker/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sticker
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Sticker/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sticker/5
        public void Delete(int id)
        {
        }
    }
}
