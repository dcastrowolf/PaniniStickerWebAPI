using System.Drawing.Imaging;
using System.Linq;

namespace PaniniStickerWebAPI.Helpers
{
    public class MimeTypeHelper
    {
        public static string GetMimeType(ImageFormat imageFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }
    }
}