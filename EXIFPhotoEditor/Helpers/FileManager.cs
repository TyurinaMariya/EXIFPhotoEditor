using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EXIFPhotoEditor.Helpers
{
    /// <summary>
    /// File manager interface (for testing)
    /// </summary>
    public interface IFileManager
    {
        bool Exists(string fileName);
        Stream Open(string path, FileMode mode, FileAccess access);
        bool DirectoryExists(string path);
    }
    public class FileManager : IFileManager
    {

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
        public bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }
        public Stream Open(string path, FileMode mode, FileAccess access)
        {
            return File.Open(path, mode, access);
        }
    }
}
