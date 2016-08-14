using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;
using System.IO;
using System.Drawing.Imaging;
using System.Web.Mvc;

namespace QrCode.Helper
{
    public static class QRHelper
    {
        public static IHtmlString GenerarQr(this HtmlHelper html, string url, string alt = "Codigo Qr", int height = 50, int Widht = 50, int margin = 0)
        {
            var qrWrite = new BarcodeWriter();
            qrWrite.Format = BarcodeFormat.QR_CODE;
            qrWrite.Options = new EncodingOptions()
            {
                Height = height,
                Width = Widht,
                Margin = margin
            };
            using (var q = qrWrite.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    var img = new TagBuilder("img");
                    img.Attributes.Add("src", String.Format("data:image/png;base64,{0}",
                        Convert.ToBase64String(ms.ToArray())));
                    img.Attributes.Add("Alt", alt);
                    return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));

                }
            }
        }
        

        }

    }
