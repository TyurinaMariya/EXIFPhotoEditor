using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EXIFPhotoEditor.Helpers
{
    public class GeoFormatConvertor
    {
        public  double GetDegreeFromFullForm(double value)
        {
            return Math.Floor(value);
        }
        public  double GetMinutesFromFullForm(double value)
        {
            return Math.Floor(((value - Math.Floor(value)) * 60.0));
        }
        public  double GetSecondsFromFullForm(double value)
        {
            return (((value - Math.Floor(value)) * 60.0) - Math.Floor(((value - Math.Floor(value)) * 60.0))) * 60;
        }
    }
}
