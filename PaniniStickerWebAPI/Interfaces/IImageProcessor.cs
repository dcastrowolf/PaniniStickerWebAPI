using Newtonsoft.Json.Linq;
using PaniniWebAPI.Models;
using System.Drawing;

namespace PaniniStickerWebAPI.Interfaces
{
    public interface IImageProcessor
    {
        Image StickerImage { get; }
        JObject APIResult { get; }
        IImageProcessor GenerateOverlay(Image backImage, Image frontImage);
        IImageProcessor AddText(StickerRequest requestData, FontFamily fontFamily, Point position);
        IImageProcessor UploadImageProcessed();
    }
}
