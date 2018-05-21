using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
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
            PrivateFontCollection collection = new PrivateFontCollection();
            collection.AddFontFile(@"D:\aldov\Documents\Visual Studio 2017\Projects\PaniniStickerWebAPI\PaniniStickerWebAPI\fonts\Whitney-Semibld.ttf");
            FontFamily fontFamily = new FontFamily("Whitney Semibold", collection);
            Font font1 = new Font(fontFamily, 19);
            Font font2 = new Font(fontFamily, 15);


            string imgCanva = new Program().Overlay("D:\\aldov\\Pictures\\1779331_10152816745620303_3813166502861713682_n.jpg", "image/jpeg");

            var fileBytes = (new RestClient(imgCanva)).DownloadData(new RestRequest(Method.GET));
            //File.WriteAllBytes(Path.Combine("D:\\aldov\\Pictures\\", "monacanva.png"), fileBytes);

            Stream imgStream = new MemoryStream(fileBytes);
            Image imgTmp = Image.FromStream(imgStream);
            Bitmap bitmap = (Bitmap)imgTmp;
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.DrawString("ALDO FERNANDO VILARDY", font1, Brushes.Black, new Point(120, 703));
            graphics.DrawString("03-12-1986", font2, Brushes.Black, new Point(189, 640));

            bitmap.Save(@"D:\aldov\Pictures\mona.png");

            Console.ReadKey();
        }

        public string Overlay(string imgPath, string mimeType)
        {
            var client = new RestClient("https://www.imgonline.com.ua/eng/impose-picture-on-another-picture-result.php");
            var request = new RestRequest(Method.POST);
            //request.AddFile("uploadfile", "D:\\aldov\\Pictures\\1779331_10152816745620303_3813166502861713682_n.jpg", "image/jpeg");
            request.AddFile("uploadfile", imgPath, mimeType);
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
            string htnlResponse = response.Content.Replace("1\">", "1\"/>").Replace("/favicon.ico\">", "/favicon.ico\"/>").Replace("design.css\">", "design.css\"/>").Replace("charset=utf-8\">", "charset=utf-8\"/>").Replace("<br>", "<br/>").Replace("&copy;", string.Empty);

            var hrefLink = XElement.Parse(htnlResponse)
                   .Descendants("a")
                   .Select(x => x.Attribute("href").Value);
            string imgResultUrl = hrefLink.ElementAt(8);
            string imgDownloadUrl = hrefLink.ElementAt(9);
            return imgResultUrl;
        }
    }
}
