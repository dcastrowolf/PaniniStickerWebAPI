using Newtonsoft.Json.Linq;
using PaniniStickerWebAPI.Constants;
using PaniniStickerWebAPI.Helpers;
using PaniniStickerWebAPI.Interfaces;
using PaniniWebAPI.Models;
using RestSharp;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PaniniStickerWebAPI.ImageProcessors
{
    public class RestImageProcessor : IImageProcessor
    {
        public Image StickerImage { get; private set; }

        public JObject APIResult { get; private set; }

        private const string OVERLAY_PROCESSOR_API = "https://www.imgonline.com.ua/eng/impose-picture-on-another-picture-result.php";
        private const string OVERLAY_HTML_RESULT = "https://www.imgonline.com.ua/";

        public IImageProcessor AddText(StickerRequest requestData, FontFamily fontFamily, Point position)
        {
            if(StickerImage == null)
            {
                throw new InvalidOperationException("ERROR: this method should be called after GenerateOverlay. ");
            }

            //Define the font and its size for each seaction under fontFamily
            Font clubNameFont = new Font(fontFamily, 19);
            Font dateOfBirthFont = new Font(fontFamily, 15);
            Font fullNameFont = new Font(fontFamily, 24);

            Graphics graphics = Graphics.FromImage(StickerImage);

            graphics.DrawString(
                requestData.Club,
                clubNameFont,
                Brushes.Black,
                new Point(120, 703));
            graphics.DrawString(
                requestData.DateOfBirthday.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                dateOfBirthFont,
                Brushes.Black,
                new Point(189, 639));
            graphics.DrawString(
                requestData.FullName,
                fullNameFont,
                Brushes.Black,
                new Point(122, 671));

            return this;
        }

        public IImageProcessor GenerateOverlay(Image backImage, Image frontImage)
        {
            using (MemoryStream backUploadStream = new MemoryStream())
            using (MemoryStream frontUploadStream = new MemoryStream())
            {
                backImage.Save(backUploadStream, backImage.RawFormat);
                frontImage.Save(frontUploadStream, frontImage.RawFormat);

                var client = new RestClient(OVERLAY_PROCESSOR_API);
                var request = new RestRequest(Method.POST);
                request.AddFile("uploadfile", backUploadStream.ToArray(), MimeTypeHelper.GetMimeType(backImage.RawFormat))
                       .AddFile("uploadfile2", frontUploadStream.ToArray(), MimeTypeHelper.GetMimeType(frontImage.RawFormat))
                       .AddParameter("efset", "2")
                       .AddParameter("efset2", "50")
                       .AddParameter("efset3", "4")
                       .AddParameter("efset4", "1")
                       .AddParameter("efset5", "100")
                       .AddParameter("efset6", "5")
                       .AddParameter("efset7", "0")
                       .AddParameter("efset8", "0")
                       .AddParameter("efset9", "2")
                       .AddParameter("efset9", "2")
                       .AddParameter("jpegtype", "1")
                       .AddParameter("jpegqual", "92")
                       .AddParameter("outformat", "3")
                       .AddParameter("jpegmeta", "2");


                IRestResponse response = client.Execute(request);
                string htnlResponse = response.Content.Replace("1\">", "1\"/>").Replace("/favicon.ico\">", "/favicon.ico\"/>").Replace("design.css\">", "design.css\"/>").Replace("charset=utf-8\">", "charset=utf-8\"/>").Replace("<br>", "<br/>").Replace("&copy;", string.Empty);

                var hrefLink = XElement.Parse(htnlResponse)
                       .Descendants("a")
                       .Select(x => x.Attribute("href").Value);
                string imgResultUrl = hrefLink.ElementAt(8).Replace("..", OVERLAY_HTML_RESULT);
                string imgDownloadUrl = hrefLink.ElementAt(9).Replace("..", OVERLAY_HTML_RESULT);
                byte[] imgFileBytes = (new RestClient(imgDownloadUrl)).DownloadData(new RestRequest(Method.GET));
                using (MemoryStream imgFileStream = new MemoryStream(imgFileBytes))
                {
                    StickerImage = Image.FromStream(imgFileStream, true, true);
                }
                return this;
            }
        }

        

        public IImageProcessor UploadImageProcessed()
        {
            if(StickerImage == null)
            {
                throw new InvalidOperationException("ERROR: this method should be called after GenerateOverlay -> AddText. ");
            }
            string fileName = $"{DateTime.Now.ToString("ddMMyyyHHmmssfffffff")}_Sticker.png";
            using (MemoryStream source = new MemoryStream())
            {
                StickerImage.Save(source, StickerImage.RawFormat);

                var client = new RestClient(ImgBBConstants.IMGBB_ENDPOINT);
                var request = new RestRequest(Method.POST);
                request.AddCookie("PHPSESSID", "e1337f77d57c68fc747a8cb52e3325ca");
                request.AddFile("source", source.ToArray(), fileName, MimeTypeHelper.GetMimeType(StickerImage.RawFormat));
                request.AddParameter("type", "file");
                request.AddParameter("action", "upload");
                request.AddParameter("privacy", "undefined");
                request.AddParameter("timestamp", ImgBBConstants.TIME_STAMP);
                request.AddParameter("auth_token", ImgBBConstants.AUTH_TOKEN);
                request.AddParameter("nsfw", ImgBBConstants.NFSW);
                IRestResponse response = client.Execute(request);

                APIResult = JObject.Parse(response.Content);

                return this;
            }
        }
    }
}