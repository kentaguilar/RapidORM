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
        public static byte[] GetByteArrayFromImage(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BufferedStream bf = new BufferedStream(fs);
            byte[] buffer = new byte[bf.Length];
            bf.Read(buffer, 0, buffer.Length);

            return buffer;
        }

        public static byte[] ConvertBitmapToByteArray(Bitmap targetImageResource)
        {
            ImageConverter converter = new ImageConverter();
            byte[] buffer = (byte[])converter.ConvertTo(targetImageResource, typeof(byte[]));

            return buffer;

        }

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

        public static void GetByteArrayFromDBAndSaveToLocation(byte[] rawImage)
        {
            using (Image image = Image.FromStream(new MemoryStream(rawImage)))
            {
                image.Save("output.jpg", ImageFormat.Jpeg);  // Or Png
            }
        }

        public static Image GetByteArrayAndInsertOnPictureBox(byte[] rawImage)
        {
            using (var ms = new MemoryStream(rawImage))
            {
                return Image.FromStream(ms);
            }
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
