using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EXIFPhotoEditor.Helpers;

namespace EXIFPhotoEditorTest
{
    [TestFixture]
    class GeoFormatConvertorTest
    {
        private GeoFormatConvertor m_Class;
        [SetUp]
        public void Init()
        {
            m_Class = new GeoFormatConvertor();
        }
        [Test]
        [TestCase(0,0)]
        [TestCase(23.5, 23)]
        [TestCase(23.51, 23)]
        [TestCase(23.5123, 23)]
        public void GetDegreeFromFullForm_Value_CorrectResult(double input, double output)
        {
            Assert.AreEqual(output, m_Class.GetDegreeFromFullForm(input));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(23.5, 30)]
        [TestCase(23.51, 30)]
        public void GetMinutesFromFullForm_Value_CorrectResult(double input, double output)
        {
            Assert.AreEqual(output, m_Class.GetMinutesFromFullForm(input));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(23.5, 0)]
        [TestCase(23.51, 36)]
        public void GetSecondsFromFullForm_Value_CorrectResult(double input, double output)
        {
            Assert.AreEqual((int)output, (int)m_Class.GetSecondsFromFullForm(input));
        }
    }
}
