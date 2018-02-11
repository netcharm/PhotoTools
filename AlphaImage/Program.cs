using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaImage
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var s = args[0].Split('x');
                var w = Convert.ToInt32(s[0]);
                var h = Convert.ToInt32(s[1]);
                var cv = string.Empty;
                var bg = Color.FromArgb(0, default(Color));

                if (args.Length>1)
                {
                    cv = args[1].Trim().Trim(new char[] { ' ', ',', '.', '_', '-', '~' });
                    var cn = Color.FromName(cv);

                    if (cv.StartsWith("trans", StringComparison.CurrentCultureIgnoreCase))
                        bg = Color.FromArgb(255, 0, 0, 0);
                    else if (cv.Equals("nblue", StringComparison.CurrentCultureIgnoreCase))
                        bg = Color.FromArgb(255, Convert.ToInt32(0.09 * 255), Convert.ToInt32(0.28 * 255), Convert.ToInt32(0.95 * 255));
                    else if (cv.Equals("ngreen", StringComparison.CurrentCultureIgnoreCase))
                        bg = Color.FromArgb(255, Convert.ToInt32(0.16 * 255), Convert.ToInt32(0.96 * 255), Convert.ToInt32(0.09 * 255));
                    else if (cn.A != bg.A || cn.R != bg.R || cn.G != bg.G || cn.B != bg.B)
                        bg = cn;
                    else
                    {
                        if (cv.Length == 6 || cv.Length == 8)
                        {
                            bg = ColorTranslator.FromHtml($"#{cv}");
                        }
                        else if ((cv.Length == 7 || cv.Length == 9) && cv.StartsWith("#"))
                        {
                            bg = ColorTranslator.FromHtml($"{cv}");
                        }
                    }
                }
                cv = string.IsNullOrEmpty(cv) ? cv : '_' + cv;
                var fo = $"{w}x{h}{cv}.png";
                Console.WriteLine($"Image Size: {w}x{h}, Background {bg}, Filename: {fo}");

                Bitmap target = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(target))
                {
                    g.FillRectangle(new SolidBrush(bg), 0, 0, w, h);
                }
                target.Save(fo, ImageFormat.Png);
                target.Dispose();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
