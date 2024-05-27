using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asciiart
{
    internal class Program
    {
        private const double WIDHT_OFFSET = 1.5;
        private const int MAX_WIDHT = 350;

        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.WriteLine("press enter to start...\n");

            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    continue;
                }

                Console.Clear();

                var bitMap = new Bitmap(openFileDialog.FileName); // здесь мы будем разбивать картинку 
                bitMap = ResizeBitmap(bitMap);
                bitMap.ToGrayScale();

                var converter = new BitmapToASCIIConverter(bitMap);
                var rows = converter.Convert();

                foreach (var row in rows)
                {
                    Console.WriteLine(row);
                }
                Console.SetCursorPosition(0, 0);
            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var newHeight = bitmap.Height / WIDHT_OFFSET * MAX_WIDHT / bitmap.Width;
            if (bitmap.Height > MAX_WIDHT || bitmap.Height < MAX_WIDHT)
            {
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDHT, (int)newHeight));
            }
            return bitmap;
        }
    }
}
