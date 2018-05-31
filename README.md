# PaniniStickerWebAPI
[![license](https://img.shields.io/github/license/aldovilardy/PaniniStickerWebAPI.svg)](https://github.com/aldovilardy/PaniniStickerWebAPI/blob/master/LICENSE)

![PaniniStickerWebAPILogo](/PaniniStickerWebAPI/Content/images/Logos/AppLogo.png)

Service to make your own Fifa Russia 2018 panini stickers.

## Using API REST
  
| Basic Concepts |  |
| --- | --- |
| URL | http://paninistickerwebapi.azurewebsites.net/api/ |
| Request | *POST* HTTP format JSON (application/json) |
| Responses | *JSON format* HTTP 200 code JSON (application/json) |

| Request Sticker Generator |  |
| --- | --- |
| URL | http://paninistickerwebapi.azurewebsites.net/api/StickerGenerator |
| Method | POST |

### Request JSON format

| JSON Key | Example Value | Description | 
| --- | --- | --- |
| photoUrl | "http://i.somethingawful.com/inserts/articlepics/photoshop/08-06-04-terminators/CapnDeviance.jpg" | The url of the source image |
| frame | "NON" | The Country Futbol Team using uppercase like Colombia "COL" or Russia "RUS" if you don't want a team you can select None "NON"  |
| fullName | "John Doe" | Name and Lastname of the person or character in the photo |
| position | "None" | You can select the player position like "Goalkeeper", "Defender", "Midfilder", "Forward" or "None"  |
| DoB | "0001-01-01T00:00:00" | The date/time of birthday in date time fortmat yyyy-MM-ddThh:mm:ss  |
| club | "Example Futbol Club" | The club, home town or nickname that you like to write  |
| country | "NON" | The Country using uppercase like Colombia "COL" or Russia "RUS" if you don't want a team you can select None "NON"  |
| debut | 1986 | The year of debut player |

**Here a JSON example of request:**
```json
{
  "photoUrl": "http://i.somethingawful.com/inserts/articlepics/photoshop/08-06-04-terminators/CapnDeviance.jpg",
  "frame": "NON",
  "fullName": "John Doe",
  "position": "None",
  "DoB": "1985-04-10T00:00:00",
  "club": "Example Futbol Club",
  "country": "NON",
  "debut": 1986
}
```

