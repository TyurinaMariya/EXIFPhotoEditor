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
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace EXIFPhotoEditor
{
    public class ChangeDataClass
    {
        private ILog m_Log = log4net.LogManager.GetLogger(typeof(ChangeDataClass));
      
        private const string NEW_PATH = "\\new\\";

        public ChangeDataClass()
        {
            
        }

        /// <summary>
        /// Change time creating photo for all photofiles in folder
        /// </summary>
        /// <param name="pathToFiles">path to files</param>
        /// <param name="l_minutes">minutes for change (allow plus or minus value)</param>
        /// <returns>count changed files</returns>
        public int ChangeCreatingDateTimeForFiles(IEnumerable<string> pathToFiles, int minutes, out string errors)
        {
            StringBuilder errorFiles = new StringBuilder();
            errors = "";
            if (pathToFiles.Count() == 0)
                return 0;

            var countChangedFiles =
                pathToFiles.AsParallel()
                         .Where(filePath =>
                            {
                                string error;
                                var res =
                                     changeExifInformationInFile(filePath,
                                     createDirectoryForOutput(filePath), out error,
                                     (EXIFFileWorker photoExifInfo, out string errorInMethod) =>
                                     {
                                         errorInMethod = "";
                                         photoExifInfo.ChangeTime(minutes);
                                         return true;
                                     });
                                if (!res)
                                    errorFiles.AppendLine(error);
                                return res;
                            })
                          .Count();

            // return count changed files
            errors = errorFiles.ToString();
            return countChangedFiles;
        }
        private bool changeExifInformationInFile(
            string fileName, string pathToNewDir, out string errors,
            ActionOnExifInfoInJpgFile action)
        {
            errors = "";

             try
             {
                 using (var exifWorker = new EXIFFileWorker(fileName))
                 {
                     if (!action(exifWorker, out errors))
                         return false;

                     string newFileName = pathToNewDir + Path.GetFileName(fileName);
                     try
                     {
                         exifWorker.SaveToOutputStream(newFileName);
                     }
                     catch (Exception e)
                     {
                         errors = String.Format(Resources.ErrorOpenFileUserMessage, newFileName);
                         m_Log.Error(String.Format(Resources.ErrorOpenFileUserMessage, newFileName), e);
                         return false;
                     }
                 }
             }
             catch (Exception e)
             {
                 m_Log.Error(String.Format(Resources.ErrorReadEXIFInfoOfFile, fileName), e);
                 errors = String.Format(Resources.ErrorReadInfoFromFile, fileName);
                 return false;
             }
            return true;
        }



        delegate bool ActionOnExifInfoInJpgFile(EXIFFileWorker photoExifInfo,  out string error);


        private static string createDirectoryForOutput(string l_path)
        {
            var l_newPath = Path.GetDirectoryName(l_path) + NEW_PATH;
            if (!Directory.Exists(l_newPath))
                Directory.CreateDirectory(l_newPath);
            return l_newPath;
        }



        public int ChangeGeoDataForFiles(IEnumerable<string> pathToImageFiles,
            string pathToTrack, int timezone, Action actionDuringIteration, out string errors)
        {
            errors = "";
            if (pathToImageFiles.Count() == 0)
                return 0;

            IEnumerable<GeoData> track;
            try
            {
                using (var gpxeReader = new GpxFileReader(pathToTrack))
                {
                    track = gpxeReader.GetData();
                }
            }
            catch (Exception e)
            {
                errors = String.Format("Ошибка чтения файла трека {0}", pathToTrack);
                m_Log.Error(String.Format("Error read track file {0}", pathToTrack), e);
                return 0;
            }
            int countChangedFiles = 0;
            BlockingCollection<string> errosInProcces = new BlockingCollection<string>();
            SynchronizationContext ctx = SynchronizationContext.Current;

            Parallel.ForEach(pathToImageFiles, file =>
                {

                    string error;
                    var res =
                         changeExifInformationInFile(file, createDirectoryForOutput(file), out error,
                                (EXIFFileWorker photoExifInfo, out string er) =>
                                {
                                    er = "";
                                    GeoData? geoData =
                                        new GeoDataWorker().GetGeoForPointByTime(photoExifInfo.GetDateTimeTaken().AddHours(-timezone), track);
                                    if (geoData == null)
                                    {
                                        er = String.Format("Координаты для фотографии {0} не найдены",
                                            file);
                                        m_Log.ErrorFormat("Can't find coords for photo {0}", file);
                                        return false;
                                    }
                                    photoExifInfo.ChangeGeoData(geoData.Value.Latitude,geoData.Value.Longitude, geoData.Value.Altitude);
                                    return true;
                                });
                    if (res)
                        countChangedFiles++;
                    else
                        errosInProcces.Add(error);

                    actionDuringIteration();

                });

            errors = errosInProcces.Aggregate("", (acum, str) => acum + str + "\r\n");
            return countChangedFiles;
        }
    }
}
