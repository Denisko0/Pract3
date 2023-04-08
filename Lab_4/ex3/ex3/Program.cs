using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ImageProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string folderPath = @"C:\Images";

            
            var images = Directory.GetFiles(folderPath, "*.jpg").Select(p => new Bitmap(p)).ToList();

            // Операції обробки зображень, представлені делегатами Func<Bitmap, Bitmap>
            Func<Bitmap, Bitmap> grayscaleFilter = image =>
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        Color c = image.GetPixel(x, y);
                        byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);
                        image.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                    }
                }
                return image;
            };

            Func<Bitmap, Bitmap> sepiaFilter = image =>
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        Color c = image.GetPixel(x, y);
                        int r = (int)(.393 * c.R + .769 * c.G + .189 * c.B);
                        int g = (int)(.349 * c.R + .686 * c.G + .168 * c.B);
                        int b = (int)(.272 * c.R + .534 * c.G + .131 * c.B);
                        r = Math.Min(r, 255);
                        g = Math.Min(g, 255);
                        b = Math.Min(b, 255);
                        image.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                return image;
            };

            
            Action<Bitmap> displayImage = image =>
            {
                image.Save("output.jpg");
                Console.WriteLine($"Processed image: {image.Width}x{image.Height}");
            };

            // Обробка зображень
            foreach (var image in images)
            {
                
                var processedImage = grayscaleFilter(image);
                processedImage = sepiaFilter(processedImage);

                
                displayImage(processedImage);
            }
        }
    }
}