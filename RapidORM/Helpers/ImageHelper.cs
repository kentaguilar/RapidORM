using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;

namespace RapidORM.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// Generates by array from given image path
        /// </summary>
        /// <param name="imagePath"></param>
        public static byte[] GetByteArrayFromImage(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open);
            BufferedStream bf = new BufferedStream(fs);
            byte[] buffer = new byte[bf.Length];
            bf.Read(buffer, 0, buffer.Length);

            return buffer;
        }

        /// <summary>
        /// Converts bitmap image to byte array
        /// </summary>
        /// <param name="imageResource"></param>
        public static byte[] ConvertBitmapToByteArray(Bitmap imageResource)
        {
            ImageConverter converter = new ImageConverter();
            byte[] buffer = (byte[])converter.ConvertTo(imageResource, typeof(byte[]));

            return buffer;

        }

        /// <summary>
        /// Converts bitmap image to byte array
        /// </summary>
        /// <param name="resourceName"></param>
        public static void ExtractFromResource(string resourceName)
        {
            using (Stream sFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                byte[] buffer = new byte[sFile.Length];
                sFile.Read(buffer, 0, Convert.ToInt32(sFile.Length));

                byte[] buffer_new = buffer;

                Console.WriteLine(buffer_new);
            }
        }

        /// <summary>
        /// Converts byte array to image and save to certain location
        /// </summary>
        /// <param name="imageByte"></param>
        public static void ConvertByteArrayToImageAndSaveToLocation(byte[] imageByte)
        {
            using (Image image = Image.FromStream(new MemoryStream(imageByte)))
            {
                image.Save("output.jpg", ImageFormat.Jpeg);  
            }
        }

        /// <summary>
        /// Converts byte array to stream and save to certain location
        /// </summary>
        /// <param name="imageByte"></param>
        public static Image ConvertByteArrayToStreamAndSaveToLocation(byte[] imageByte)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageByte))
            {
                return Image.FromStream(memoryStream);
            }
        }

        /// <summary>
        /// Converts image type to byte array
        /// </summary>
        /// <param name="image"></param>
        public static byte[] ConvertImageToByte(Image image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }
    }
}
