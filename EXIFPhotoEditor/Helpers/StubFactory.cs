using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace EXIFPhotoEditor.Helpers
{
    public class StubFactory
    {
        static IFileManager m_FileManager = new FileManager();
        public static IFileManager FileManager
        {
            get
            {
                return m_FileManager;
            }
#if DEBUG
            set
            {
                m_FileManager = value;
            }
#endif
        }
    }
}
