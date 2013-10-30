using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Controls.Development;
using EXIFPhotoEditor.Properties;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;


namespace EXIFPhotoEditor
{
    public partial class MainForm : Form
    {
        private IEnumerable<string> m_Files = null;
        MainSettings m_MainSettings = new MainSettings();
        public MainForm()
        {
            InitializeComponent();
            tb_timeZone.Text = m_MainSettings.LastTimeZone;
            tb_Minutes.Text = m_MainSettings.LastDeltaTime;
        }


        private void bChangeDateTime_Click(object sender, EventArgs e)
        {
            string errors;
            var countChangedFiles = new ChangeDataClass()
                .ChangeCreatingDateTimeForFiles(m_Files, int.Parse(tb_Minutes.Text), out errors);

            MessageBox.Show(String.Format(Resources.CountFilesWasProcessed,
                countChangedFiles, String.IsNullOrEmpty(errors) ? Resources.WithoutErrors : errors),
                Resources.Result, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void bChangeGeoData_Click(object sender, EventArgs e)
        {
            string errors;
            TaskFactory uiFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
            progressBar1.Maximum = m_Files.Count();
            this.Enabled = false;
            Task.Factory.StartNew(() =>
            {
                var countChangedFiles = new ChangeDataClass().ChangeGeoDataForFiles
                    (m_Files, tb_pathToGpxFile.Text, int.Parse(tb_timeZone.Text),
                        () =>uiFactory.StartNew(()=>progressBar1.PerformStep()),
                   out errors);

                
                uiFactory.StartNew(() => 
                {
                    this.Enabled = true;
                    MessageBox.Show(String.Format(Resources.CountFilesWasProcessed,
                        countChangedFiles, String.IsNullOrEmpty(errors) ? Resources.WithoutErrors : 
                         "\r\n" +Resources.Error+ errors),
                        Resources.Result, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value =1; 
                });
            });
        }

        private Bitmap getImageFromFile(string pathToFile, int width, int heigth)
        {
            using (var imgPhoto = Image.FromFile(pathToFile))
            {
                if (imgPhoto.Height == imgPhoto.Width)
                    width = heigth;
                if (imgPhoto.Height > imgPhoto.Width && imgPhoto.Height != 0)
                    width = (imgPhoto.Width * heigth) / imgPhoto.Height;
                if (imgPhoto.Height < imgPhoto.Width && imgPhoto.Width != 0)
                    heigth = (imgPhoto.Height * width) / imgPhoto.Width;

                // Создаем пустую канву. Измененная графика будет записана в эту канву.
                var bmPhoto = new Bitmap(width, heigth);
                bmPhoto.SetResolution(10, 10);
                using (var grPhoto = Graphics.FromImage(bmPhoto))
                {
                    grPhoto.SmoothingMode =
                         System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    grPhoto.InterpolationMode =
                         System.Drawing.Drawing2D.InterpolationMode.High;
                    grPhoto.PixelOffsetMode =
                         System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                    grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, width, heigth), 0, 0,
                        imgPhoto.Width, imgPhoto.Height, GraphicsUnit.Pixel);
                }
                return bmPhoto;
            }
        }
        private void b_LoadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogOpenFile = new OpenFileDialog())
            {
                dialogOpenFile.Multiselect = true;
                dialogOpenFile.Filter = "Image Files|*.jpg;*.jpeg";
                dialogOpenFile.CheckPathExists = true;
                dialogOpenFile.InitialDirectory = m_MainSettings.PathToLastImageDir;

                if (dialogOpenFile.ShowDialog() == DialogResult.OK)
                {
                    m_MainSettings.PathToLastImageDir = Path.GetDirectoryName(dialogOpenFile.FileName);
                    m_Files = new List<string>(dialogOpenFile.FileNames);
                    TaskFactory uiFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
                    progressBar1.Maximum = m_Files.Count();
                    this.Enabled = false;
                    Task.Factory.StartNew(() =>
                    {
                       Image[] smallImages = m_Files
                         .AsParallel()
                         .Select(filepath => 
                            {
                                var res = (Image)getImageFromFile(filepath, 24, 24);
                                 uiFactory.StartNew(() => progressBar1.PerformStep());
                                return res;
                            })
                        .ToArray();

                        uiFactory.StartNew(() =>
                        {
                            il_Photos.Images.AddRange(smallImages);
                            ilb_files.Items.Clear();
                            ilb_files.Items.AddRange(m_Files.Select((file, index) => new ImageListBoxItem(file, index)).ToArray());
                            progressBar1.Value = 1;
                            this.Enabled = true;
                        });
                    });
                }
            }
        }

        private void b_openTrackFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogOpenFile = new OpenFileDialog())
            {
                dialogOpenFile.Multiselect = true;
                dialogOpenFile.Filter = "GPX Files|*.gpx";
                dialogOpenFile.CheckPathExists = true;
                dialogOpenFile.InitialDirectory = m_MainSettings.PathToLastGPXDir;

                if (dialogOpenFile.ShowDialog() == DialogResult.OK)
                {
                    m_MainSettings.PathToLastGPXDir = Path.GetDirectoryName(dialogOpenFile.FileName);
                    tb_pathToGpxFile.Text = dialogOpenFile.FileName;
                }
            }
        }

        private void tb_timeZone_Validating(object sender, CancelEventArgs e)
        {
            int tz;
            if (!int.TryParse(tb_timeZone.Text, out tz) || tz < 0 || tz > 12)
            {
                e.Cancel = true;
                MessageBox.Show(Resources.NotCorrectTimeZone, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            m_MainSettings.LastTimeZone = tz.ToString();
        }

        private void tb_Minutes_Validating(object sender, CancelEventArgs e)
        {
            int tz;
            if (!int.TryParse(tb_Minutes.Text, out tz))
            {
                e.Cancel = true;
                MessageBox.Show(Resources.NotCorrectDeltaTime, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            m_MainSettings.LastDeltaTime = tz.ToString();
        }
    }
}
