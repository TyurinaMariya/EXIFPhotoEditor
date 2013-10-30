using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EXIFPhotoEditor;
using System.Collections;

namespace EXIFPhotoEditorTest
{
    [TestFixture]
    public class GeoDataWorkerTest
    {
        GeoDataWorker m_Class;
        [SetUp]
        public void Init()
        {
            m_Class = new GeoDataWorker();
        }
        
        [Test]
        [TestCase(2039, 1, 1)]
        [TestCase(2009, 1, 1)]
        public void GetGeoForPointByTime_PointNotInRange_NullResult(int year, int month, int day)
        {
            Assert.IsNull(m_Class.GetGeoForPointByTime(new DateTime(year, month, day), m_GeoData));
        }
      
        [Test]
        [TestCaseSource(typeof(MyFactoryClass),"TestCases")]
        public GeoData? GetGeoForPointByTime_InRange_CorrectResult (DateTime pointTime)
        {
            var res = m_Class.GetGeoForPointByTime(pointTime, m_GeoData);
            return res;
        }

        IEnumerable<GeoData> m_GeoData = new List<GeoData>()
            {
                new GeoData() 
                {
                    Latitude = 30,
                    Longitude = 50,
                    Altitude = 80,
                    CreatingDate = new DateTime(2000,01,01,1,1,0)
                },
                new GeoData() 
                {
                    Latitude = 31,
                    Longitude = 51,
                    Altitude = 40,
                    CreatingDate = new DateTime(2000,01,01,1,1,30)
                },
                 new GeoData() 
                {
                    Latitude = 32,
                    Longitude = 52,
                    Altitude = 20,
                    CreatingDate = new DateTime(2000,01,01,1,1,30)
                },
                new GeoData() 
                {
                    Latitude = 30,
                    Longitude = 50,
                    Altitude = 80,
                    CreatingDate = new DateTime(2000,01,01,1,2,0)
                }
            };
        public class MyFactoryClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    // fist bound
                    yield return new TestCaseData(new DateTime(2000, 01, 01, 1, 1, 0)).Returns(new GeoData()
                    {
                        Latitude = 30,
                        Longitude = 50,
                        Altitude = 80,
                        CreatingDate = new DateTime(2000, 01, 01, 1, 1, 0)
                    });
                    // last bound
                    yield return new TestCaseData(new DateTime(2000, 01, 01, 1, 2, 0)).Returns(new GeoData()
                    {
                        Latitude = 30,
                        Longitude = 50,
                        Altitude = 80,
                        CreatingDate = new DateTime(2000, 01, 01, 1, 2, 0)
                    });
                    // in range
                    yield return new TestCaseData(new DateTime(2000, 01, 01, 1, 1, 45)).Returns(new GeoData()
                    {
                        Latitude = 31,
                        Longitude = 51,
                        Altitude = 50,
                        CreatingDate = new DateTime(2000, 01, 01, 1, 1, 45)
                    });
                }
            }
        }
    }
}
