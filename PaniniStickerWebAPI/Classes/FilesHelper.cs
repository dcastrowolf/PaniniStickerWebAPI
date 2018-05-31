using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PaniniStickerWebAPI.Classes
{
    public class FilesHelper
    {
        public static string UploadPhoto(HttpPostedFile file)
        {
            string urlResponse = string.Empty;
            if (file != null)
            {
                MemoryStream source = new MemoryStream();

                byte[] buffer = new byte[16 * 1024];
                int read;
                while ((read = file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                    source.Write(buffer, 0, read);

                var client = new RestClient("https://imgbb.com/json");
                var request = new RestRequest(Method.POST);
                request.AddCookie("PHPSESSID", "e1337f77d57c68fc747a8cb52e3325ca");
                request.AddFile("source", source.ToArray(), file.FileName, file.ContentType);
                request.AddParameter("type", "file");
                request.AddParameter("action", "upload");
                request.AddParameter("privacy", "undefined");
                request.AddParameter("timestamp", "1527541040686");
                request.AddParameter("auth_token", "c08e59c27aeeb1cb0f126e3ed3856cf1fd675d06");
                request.AddParameter("nsfw", "0");
                IRestResponse response = client.Execute(request);

                JObject jObject = JObject.Parse(response.Content);
                urlResponse = (string)jObject["image"]["url"];
            }
            return urlResponse;
        }
    }
}