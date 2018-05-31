using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaniniStickerWebAPI.Models
{
    public class Sticker
    {
        
        public string PhotoUrl { get; set; }
        [Required]
        [Display(Name = "Photo")]
        public HttpPostedFile PhotoFile { get; set; }
        [Required]
        [Display(Name = "Team Frame")]
        public string Frame { get; set; }

        //public System.Web.Mvc Frame { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birthday")]
        public DateTime DateOfBirthday { get; set; }        
        public string Club { get; set; }        
        public string Country { get; set; }
        [Required]
        public short Debut { get; set; }
    }
}