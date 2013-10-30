using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EXIFPhotoEditor;
using Moq;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using EXIFPhotoEditor.Helpers;

namespace EXIFPhotoEditorTest
{
    [TestFixture]
    public class GpxFileReaderTest
    {
        [Test]
        public void Constructor_NullEmptyFileName_AgrumentNUllException([Values("", null)] string fileName)
        {
            Assert.Catch
                (
                    typeof(ArgumentNullException),
                    () => new GpxFileReader(fileName)
                );
        }


        [Test]
        public void Constructor_FileNotExists_AgrumentException()
        {
            var l_fileManagerMock = new Mock<IFileManager>();
            l_fileManagerMock.Setup(l_sc => l_sc.Exists(It.IsAny<string>())).Returns(() => false);
            StubFactory.FileManager = l_fileManagerMock.Object;

            Assert.Catch
                (
                    typeof(ArgumentException),
                    () => new GpxFileReader("someFilePath")
                );
        }

        
        [Test]
        public void Constructor_NotXMLTextInFile_XmlException([Values("", "Not XML text")] string textInFile)
        {
           setUpFileManagerWithText(textInFile);

            Assert.Catch
               (
                   typeof(XmlException),
                   () => new GpxFileReader("someFilePath")
               );
        }
        private void setUpFileManagerWithText(string xmlDocText)
        {
            var l_fileManagerMock = new Mock<IFileManager>();
            l_fileManagerMock.Setup(l_sc => l_sc.Exists(It.IsAny<string>())).Returns(() => true);
            l_fileManagerMock.Setup(l_sc => l_sc.Open(It.IsAny<string>(), It.IsAny<FileMode>(),
                It.IsAny<FileAccess>())).Returns
                (
                  new MemoryStream(ASCIIEncoding.Default.GetBytes(xmlDocText))

                );
            StubFactory.FileManager = l_fileManagerMock.Object;
        }

        [Test]
        public void GetData_OnePointInXml_OneCorrectPoint()
        {

            setUpFileManagerWithText
                (
                @"<gpx><trk>
                    <trkseg>
                        <trkpt lat = ""40.1"" lon = ""30.23"">
                            <ele>4600.5</ele><time>2011-07-20T06:19:57Z</time>
                        </trkpt>
                    </trkseg>
                 </trk></gpx>"
                );
            var res = new GpxFileReader("filePath").GetData();
            Assert.AreEqual(1, res.Count());
            Assert.AreEqual(res.First(), new GeoData()
            {
                    Latitude = 40.1,
                    Longitude = 30.23,
                    Altitude = 4600.5,
                    CreatingDate = new DateTime(2011, 07, 20, 6, 19, 57)}
                );
        }

        [Test]
        public void GetData_2Segment3PointsInXml_3Points()
        {
            setUpFileManagerWithText
                 (
                 @"<gpx><trk>
                    <trkseg>
                        <trkpt lat = ""40.1"" lon = ""30.23"">
                            <ele>4600.5</ele><time>2011-07-20T06:19:57Z</time>
                        </trkpt>
                        <trkpt lat = ""40.1"" lon = ""30.23"">
                            <ele>4600.5</ele><time>2011-07-20T06:19:57Z</time>
                        </trkpt>
                    </trkseg>
                    <trkseg>
                        <trkpt lat = ""40.1"" lon = ""30.23"">
                            <ele>4600.5</ele><time>2011-07-20T06:19:57Z</time>
                        </trkpt>
                    </trkseg>
                 </trk></gpx>"
                 );
            var res = new GpxFileReader("filePath").GetData();
            Assert.AreEqual(3, res.Count());
        }

    }
}
