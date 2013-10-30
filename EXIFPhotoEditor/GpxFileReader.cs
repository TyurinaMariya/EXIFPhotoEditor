using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using EXIFPhotoEditor.Helpers;

namespace EXIFPhotoEditor
{
    public struct GeoData
    {
        public double Latitude;
        public double Longitude;
        public DateTime CreatingDate;
        public double Altitude;
        public override string ToString()
        {
            return String.Format("{0} Latitude = {1} Longitude = {2} Altitude  = {3}",
                CreatingDate, Latitude, Longitude, Altitude);
        }
    }
    /// <summary>
    ///  Class for reading .gpx files
    /// </summary>
    public class GpxFileReader : IDisposable
    {
        private IFileManager m_ClassFileManager;
        Stream m_File;
        private XDocument m_GpxDoc;
        private GpxFileReader() 
        {
            m_ClassFileManager = StubFactory.FileManager;
        }

        /// <summary>
        /// open file and load xml document
        /// </summary>
        /// <param name="pathToFile">path to file</param>
        /// <exception cref="ArgumentNullException">path to file is null or empty</exception>
        /// <exception cref="ArgumentException">File not found/ file can't open to read</exception>
        /// <exception cref="XmlException">Error open xml in file</exception>
        public GpxFileReader(string pathToFile):this()
        {
            openFile(pathToFile);
        }
       
        private void openFile(string pathToFile)
        {
            if (String.IsNullOrEmpty(pathToFile))
                throw new ArgumentNullException("pathToFile");
            if (!m_ClassFileManager.Exists(pathToFile))
                throw new ArgumentException("File not found", "pathToFile");
            try
            {
                if (m_File != null)
                    m_File.Close();
                m_File = m_ClassFileManager.Open(pathToFile, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Can't open file", "pathToFile", e);
            }
            m_GpxDoc = XDocument.Load(m_File);
        }
        /// <summary>
        /// Form object view of track from file
        /// </summary>
        /// <returns>object view of track</returns>
        public IEnumerable<GeoData> GetData()
        {
            var mainNode = m_GpxDoc.FirstNode;
            return (mainNode as XContainer)
                .Nodes()
                .Where(node => (node is XElement) && (((XElement)node).Name.LocalName == "trk"))
                .SelectMany(trk => (trk as XElement).Nodes())
                .Where(node => (node is XElement) && (((XElement)node).Name.LocalName == "trkseg"))
                .SelectMany(trkseg => (trkseg as XElement).Nodes())
                .Where(node => (node is XElement) && (((XElement)node).Name.LocalName == "trkpt"))
                .Select(point => (XElement)point)
                .Select(point => new GeoData()
                {
                    Latitude = double.Parse(point.Attributes().First(attr => attr.Name.LocalName == "lat").Value, CultureInfo.InvariantCulture),
                    Longitude = double.Parse(point.Attributes().First(attr => attr.Name.LocalName == "lon").Value, CultureInfo.InvariantCulture),
                    CreatingDate = DateTime.ParseExact((point
                                    .Nodes()
                                    .First(node => (node is XElement) && ((node as XElement).Name.LocalName == "time"))
                                    as XElement).Value.ToString(), "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture).ToUniversalTime(),
                    Altitude = double.Parse((point.Nodes()
                                                .First(node => (node is XElement) && (((XElement)node).Name.LocalName == "ele"))
                                           as XElement).Value.ToString(), CultureInfo.InvariantCulture)

                })
                .ToList();
        }

        public void Dispose()
        {
            if (m_File != null)
                m_File.Close();
        }
    }
}
