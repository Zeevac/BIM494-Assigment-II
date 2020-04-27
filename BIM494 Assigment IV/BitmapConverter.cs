using System.IO;
using Android.Graphics;

namespace BIM494_Assigment_IV
{
    public static class BitmapConverter
    {
        public static byte[] GetBytesFromBitmap(Bitmap bitmap)
        {
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
            return bitmapData;
        }
    }
}