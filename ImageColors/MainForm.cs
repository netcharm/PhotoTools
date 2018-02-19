using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtensionMethods;
using System.IO;

namespace ImageColors
{
    public partial class MainForm : Form
    {
        Dictionary<string, Color> colors = new Dictionary<string, Color>();
        Dictionary<string, int> colorcount = new Dictionary<string, int>();

        /// <summary>
        /// Get the Filter string for all supported image types.
        /// This can be used directly to the FileDialog class Filter Property.
        /// https://www.codeproject.com/tips/255626/a-filedialog-filter-generator-for-all-supported-im
        /// </summary>
        /// <returns></returns>
        public string GetImageFilter()
        {
            StringBuilder allImageExtensions = new StringBuilder();
            string separator = "";
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            Dictionary<string, string> images = new Dictionary<string, string>();
            foreach (ImageCodecInfo codec in codecs)
            {
                allImageExtensions.Append(separator);
                allImageExtensions.Append(codec.FilenameExtension);
                separator = ";";
                images.Add(string.Format("{0} Files: ({1})", codec.FormatDescription, codec.FilenameExtension),
                           codec.FilenameExtension);
            }
            StringBuilder sb = new StringBuilder();
            if (allImageExtensions.Length > 0)
            {
                sb.AppendFormat("{0}|{1}", "All Images", allImageExtensions.ToString());
            }
            images.Add("All Files", "*.*");
            foreach (KeyValuePair<string, string> image in images)
            {
                sb.AppendFormat("|{0}|{1}", image.Key, image.Value);
            }
            return sb.ToString();
        }

        int CompareByColor(Color c1, Color c2)
        {
            return String.Compare(c1.ToHtml(), c2.ToHtml());
        }

        List<Color> FilterColors(int amount)
        {
            List<Color> filted = new List<Color>();

            foreach (var kv in colors)
            {
                if (colorcount[kv.Key] < amount) continue;
                filted.Add(kv.Value);
                //colorGrid.AddCustomColor(kv.Value);
            }
            filted.Sort(CompareByColor);
            return (filted);
        }

        public void CalcImageColor(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                colors.Clear();
                colorcount.Clear();

                LockBitmap lockbmp = new LockBitmap(bitmap);
                lockbmp.LockBits();
                double total = lockbmp.Width * lockbmp.Height;
                for (int x = 0; x < lockbmp.Width; x++)
                {
                    for (int y = 0; y < lockbmp.Height; y++)
                    {
                        var p = lockbmp.GetPixel(x, y);
                        string cn = p.ToHtml();
                        colors[cn] = p;
                        if (colorcount.ContainsKey(cn))
                            colorcount[cn]++;
                        else
                            colorcount[cn] = 1;
                    }
                    int per = Convert.ToInt32(x * 100.0 / lockbmp.Width);
                    bgWorker.ReportProgress(per);
                }
                lockbmp.UnlockBits();
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            imageBox.ZoomLevels = Cyotek.Windows.Forms.ZoomLevelCollection.Default;
            imageActions.ImageBox = imageBox;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = GetImageFilter();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = (Bitmap)Image.FromFile(dialog.FileName);
                imageBox.Image = bmp;
                imageBox.ZoomToFit();
                imageActions.Zoom = imageBox.Zoom;
                bgWorker.RunWorkerAsync();
                colorGrid.ShowCustomColors = false;
            }
        }

        private void colorAmount_ValueChanged(object sender, EventArgs e)
        {
            if (bgWorkerFilter.IsBusy) return;

            int amount = Convert.ToInt32(colorAmount.Value);
            List<Color> usagecolors = FilterColors(amount);
            lblColors.Text = $"{usagecolors.Count}/{colors.Count}";

            int vh = (int)((usagecolors.Count / 20.0 + 1) * (colorGrid.CellSize.Height + colorGrid.Spacing.Height));
            pnlColors.AutoScrollMinSize = new Size(pnlColors.AutoScrollMinSize.Width, vh);
            pnlColors.Update();
            //pnlColors.Refresh();

            colorGrid.Colors.Clear();
            bgWorkerFilter.RunWorkerAsync(usagecolors);
        }

        private void colorPicker_ColorChanged(object sender, EventArgs e)
        {
            int idx = colorGrid.Colors.Find(colorPicker.Color);
            colorGrid.Color = colorPicker.Color;
        }

        private void cmSaveToCSS_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSS File|*.css";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var c in colorGrid.Colors)
                {
                    var cstr = c.ToHtml().ToUpper();
                    var cn = cstr.Replace("#FF", "");
                    var cv = cstr.Replace("#FF", "#");
                    sb.AppendLine($".bs-callout-{cn} {{ border-color: {cv}; }}");
                    sb.AppendLine($".bs-callout-{cn} h4 {{ color: {cv}; }}");
                    sb.AppendLine($".bs-callout-bg-{cn} h4 {{ background-color: {cv}; }}");
                }
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }

        private void cmSaveToPal_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "GIMP Palette File|*.gpl";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("GIMP Palette");
                sb.AppendLine($"Name: {Path.GetFileNameWithoutExtension(dialog.FileName)}");
                sb.AppendLine("#");
                foreach (var c in colorGrid.Colors)
                {
                    sb.AppendLine($"{c.R:D03} {c.G:D03} {c.B:D03} {c.Name.ToUpper().Replace("FF", "")}");
                }
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            pbar.Minimum = 0;
            pbar.Maximum = 100;
            pbar.Style = ProgressBarStyle.Blocks;
            Bitmap bmp = (Bitmap)imageBox.Image.Clone();
            CalcImageColor(bmp);
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (0<= e.ProgressPercentage && e.ProgressPercentage <= 100)
            {
                pbar.Value = e.ProgressPercentage;
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbar.Value = 100;
            List<int> cc = colorcount.Values.ToList();
            //cc = cc.Union(cc).ToList();
            cc.Sort();
            cc.Reverse();
            colorAmount.Value = cc.Take(400).Last();
        }

        private void bgWorkerFilter_DoWork(object sender, DoWorkEventArgs e)
        {
            pbar.Minimum = 0;
            pbar.Maximum = 100;
            pbar.Style = ProgressBarStyle.Blocks;

            List<Color> usagecolors = (List<Color>)e.Argument;

            for (int i = 0; i < usagecolors.Count; i+=100)
            {
                bgWorkerFilter.ReportProgress(i, usagecolors);
            }
        }

        private void bgWorkerFilter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            List<Color> usagecolors = (List<Color>)e.UserState;
            int pv = (int)(100.0 * e.ProgressPercentage / usagecolors.Count);
            if (0 <= pv && pv <= 100) pbar.Value = pv;

            colorGrid.Colors.AddRange(usagecolors.Skip(e.ProgressPercentage).Take(100));
            colorGrid.Update();
            //colorGrid.Refresh();
        }

        private void bgWorkerFilter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbar.Value = 100;
        }
    }
}
