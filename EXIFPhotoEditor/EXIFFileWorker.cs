using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using log4net.Core;
using log4net;
using System.Linq;
using System.Linq.Expressions;
using EXIFPhotoEditor.Properties;
using EXIFPhotoEditor.Helpers;

namespace EXIFPhotoEditor
{
    /// <summary>
    ///  Class allow edit EXIF info in files
    /// </summary>
    public class EXIFFileWorker:IDisposable
    {
        IFileManager m_FileManager;
        private ILog m_Log = log4net.LogManager.GetLogger(typeof(EXIFFileWorker));
        private string m_pathToFile;
        private JpegBitmapDecoder m_Decoder;
        private BitmapMetadata m_Exif;
        Stream  m_jpegStreamIn ;
        private EXIFFileWorker()
        {
            m_FileManager = StubFactory.FileManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="AgrumentException">Can't open file</exception>
        public EXIFFileWorker(string pathToFile):this()
        {
            m_pathToFile = pathToFile;
            if (String.IsNullOrEmpty(pathToFile))
                throw new ArgumentNullException("pathToFile");
            if (!m_FileManager.Exists(pathToFile))
                throw new ArgumentException("File not found", "pathToFile");
            try
            {
                if (m_jpegStreamIn  != null)
                    m_jpegStreamIn .Close();
                m_jpegStreamIn  = m_FileManager.Open(pathToFile, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Can't open file", "pathToFile", e);
            }
            // unpack photo and make decoder
            m_Decoder = new JpegBitmapDecoder(m_jpegStreamIn, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
            m_Exif = (BitmapMetadata)m_Decoder.Frames[0].Metadata.Clone();

        }

        public void SaveToOutputStream(string pathToOutputFile)
        {
            if (String.IsNullOrEmpty(pathToOutputFile))
                throw new ArgumentNullException("pathToOutputFile");
            if (!m_FileManager.DirectoryExists(Path.GetDirectoryName(pathToOutputFile)))
                throw new ArgumentException("Directory not exists", "pathToOutputFile");
            try
            {
                using (var jpegStreamOut = m_FileManager.Open(pathToOutputFile, FileMode.Create, FileAccess.Write))
                {
                    // save new dateTime
                    var encoder = new JpegBitmapEncoder(); // create new encoder for jpg
                    encoder.Frames.Add(BitmapFrame.Create(m_Decoder.Frames[0], m_Decoder.Frames[0].Thumbnail,
                        m_Exif, m_Decoder.Frames[0].ColorContexts)); //create new frame 

                    encoder.Save(jpegStreamOut);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Can't save into file", "pathToOutputFile", e);
            }
        }
        public DateTime GetDateTimeTaken()
        {
            return Convert.ToDateTime(m_Exif.DateTaken);
        }
        /// <summary>
        /// Change creating photo time 
        /// </summary>
        /// <param name="minutes">minutes for change (plus or minus)</param>
        /// <param name="errors">list of errors</param>
        /// <returns>true - save new file is ok, false - save take save errors</returns>
        public EXIFFileWorker ChangeTime(int minutes)
        {
            var creationTime = GetDateTimeTaken();

            // change datetime
            m_Exif.DateTaken = creationTime.AddMinutes(minutes).ToString("MM/dd/yyyy HH:mm:ss");

            return this;
        }
        /// <summary>
        /// Change geo coordinates
        /// </summary>
        /// <param name="minutes">minutes for change (plus or minus)</param>
        /// <param name="errors">list of errors</param>
        /// <returns>true - save new file is ok, false - save take save errors</returns>
        public EXIFFileWorker ChangeGeoData(double latitude, double longitude, double altitude)
        {
            m_Exif.SetQuery("/app1/ifd/gps/{ushort=1}", "N");
            m_Exif.SetQuery("/app1/ifd/gps/{ushort=3}", "E");
            m_Exif.SetQuery("/app1/ifd/gps/{ushort=0}", "2.2.0.0");
            m_Exif.SetQuery("/app1/ifd/gps/{ushort=2}", rational(altitude));
            m_Exif.SetQuery("/app1/ifd/gps/{ushort=2}", getExifGeoCoordsView(latitude));
            m_Exif.SetQuery("/app1/ifd/gps/{ushort=4}", getExifGeoCoordsView(longitude));
            return this;
        }
       
        private ulong[] getExifGeoCoordsView(double coord)
        {
            return new ulong[] { rational(new GeoFormatConvertor().GetDegreeFromFullForm(coord)), 
                            rational(new GeoFormatConvertor().GetMinutesFromFullForm(coord)),
                            rational(new GeoFormatConvertor().GetSecondsFromFullForm(coord)) };
        }
        private ulong rational(double a)
        {
            uint denom = 1000;
            uint num = (uint)(a * denom);
            ulong tmp;
            tmp = (ulong)denom << 32;
            tmp |= (ulong)num;
            return tmp;
        }


        public void Dispose()
        {
            if (m_jpegStreamIn != null)
                m_jpegStreamIn.Close();
        }
    }
}
