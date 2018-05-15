using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://www.imgonline.com.ua/eng/impose-picture-on-another-picture-result.php");
            var request = new RestRequest(Method.POST);
            request.AddFile("uploadfile", "D:\\aldov\\Pictures\\1779331_10152816745620303_3813166502861713682_n.jpg", "image/jpeg");
            request.AddFile("uploadfile2", "D:\\aldov\\Pictures\\panini\\Frame\\7025.png", "image/png");
            request.AddParameter("efset", "2");
            request.AddParameter("efset2", "50");
            request.AddParameter("efset3", "4");
            request.AddParameter("efset4", "1");
            request.AddParameter("efset5", "100");
            request.AddParameter("efset6", "5");
            request.AddParameter("efset7", "0");
            request.AddParameter("efset8", "0");
            request.AddParameter("efset9", "2");
            request.AddParameter("efset9", "2");
            request.AddParameter("jpegtype", "1");
            request.AddParameter("jpegqual", "92");
            request.AddParameter("outformat", "3");
            request.AddParameter("jpegmeta", "2");


            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            //string htnlResponse = response.Content.Replace("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">", "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>").Replace("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1\">", "<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1\"/>");
            string htnlResponse = response.Content.Replace("1\">", "1\"/>").Replace("/favicon.ico\">", "/favicon.ico\"/>").Replace("design.css\">", "design.css\"/>").Replace("charset=utf-8\">", "charset=utf-8\"/>").Replace("<br>", "<br/>").Replace("&copy;", string.Empty);

            var hrefLink = XElement.Parse(htnlResponse)
                   .Descendants("a")
                   .Select(x => x.Attribute("href").Value);
            string imgResultUrl = hrefLink.ElementAt(8);
            string imgDownloadUrl = hrefLink.ElementAt(9);
            foreach (var item in hrefLink)
                Console.WriteLine(item);


            Console.ReadKey();
        }
    }
}
