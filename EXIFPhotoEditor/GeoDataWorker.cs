using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EXIFPhotoEditor
{
    /// <summary>
    ///  Class work with geo points
    /// </summary>
    public class GeoDataWorker
    {
        /// <summary>
        /// Get coords and altitude for point by it time and track
        /// </summary>
        /// <param name="pointTimeGTC">time for point</param>
        /// <param name="track">track</param>
        /// <returns>geo information for point</returns>
        public GeoData? GetGeoForPointByTime(DateTime pointTimeGTC, IEnumerable<GeoData> track)
        {
            if (pointTimeGTC > track.Last().CreatingDate)
                return null;
            if (pointTimeGTC < track.First().CreatingDate)
                return null;
            var trackArray = track.OrderBy(el=> el.CreatingDate).ToArray();
            for (int i = 0; i < trackArray.Length - 1; i++)
            {
                var before = trackArray[i];
                var after = trackArray[i + 1];
                if (after.CreatingDate.Ticks == before.CreatingDate.Ticks)
                    continue;

                if (pointTimeGTC >= before.CreatingDate &&
                    pointTimeGTC <= after.CreatingDate)
                {
                    // acount real coords by time
                    var koef = (double)(pointTimeGTC.Ticks- before.CreatingDate.Ticks)
                        / (double)(after.CreatingDate.Ticks - before.CreatingDate.Ticks);
                    return new GeoData()
                    {
                        Latitude = getRatioValue(before.Latitude, after.Latitude, koef),
                        Longitude = getRatioValue(before.Longitude, after.Longitude, koef),
                        Altitude = getRatioValue(before.Altitude, after.Altitude, koef),
                        CreatingDate = pointTimeGTC
                    };
                }
            }
            return null;
        }

        private double getRatioValue(double stVal, double endVal, double koef)
        {
            return stVal + (endVal - stVal) * koef;
        }
    }  
}
