using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaniniWebAPI.Models
{
    public class StickerRequest
    {
        [JsonProperty("PhotoUrl")]
        public string PhotoUrl { get; set; }
        [JsonProperty("Frame")]
        public string Frame { get; set; }
        [JsonProperty("FullName")]
        public string FullName { get; set; }
        [JsonProperty("Position")]
        public string Position { get; set; }
        [JsonProperty("DoB")]
        public DateTime DateOfBirthday { get; set; }
        [JsonProperty("Club")]
        public string Club { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Debut")]
        public short Debut { get; set; }
    }
}