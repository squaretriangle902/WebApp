using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public static class Common
    {
        public static byte[] ImageToBytes(HttpPostedFileBase image)
        {
            if (image is null)
            {
                return new byte[0];
            }
            var bytes = new byte[image.ContentLength];
            image.InputStream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        public static byte[] ResizeImage(byte[] bytes, int width, int height)
        {
            var myMemStream = new MemoryStream(bytes);
            var image = System.Drawing.Image.FromStream(myMemStream);
            return ResizeImage(image, width, height);
        }

        public static byte[] ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var resizedImage = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            var resultStream = new MemoryStream();
            resizedImage.Save(resultStream, System.Drawing.Imaging.ImageFormat.Png);
            return resultStream.ToArray();
        }
    }
}