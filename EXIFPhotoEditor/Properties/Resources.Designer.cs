﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EXIFPhotoEditor.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EXIFPhotoEditor.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Было обработано {0} файлов. {1}.
        /// </summary>
        internal static string CountFilesWasProcessed {
            get {
                return ResourceManager.GetString("CountFilesWasProcessed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка.
        /// </summary>
        internal static string Error {
            get {
                return ResourceManager.GetString("Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка  открытия файла {0}.
        /// </summary>
        internal static string ErrorOpenFileUserMessage {
            get {
                return ResourceManager.GetString("ErrorOpenFileUserMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error read EXIF info of file {0}.
        /// </summary>
        internal static string ErrorReadEXIFInfoOfFile {
            get {
                return ResourceManager.GetString("ErrorReadEXIFInfoOfFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка чтения EXIF информации файла {0}.
        /// </summary>
        internal static string ErrorReadInfoFromFile {
            get {
                return ResourceManager.GetString("ErrorReadInfoFromFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Некорректное время в минутах {0}.
        /// </summary>
        internal static string IncorrectTimeInMinutes {
            get {
                return ResourceManager.GetString("IncorrectTimeInMinutes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Некорректное число минут.
        /// </summary>
        internal static string NotCorrectDeltaTime {
            get {
                return ResourceManager.GetString("NotCorrectDeltaTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Некорректый часовой пояс (0..12).
        /// </summary>
        internal static string NotCorrectTimeZone {
            get {
                return ResourceManager.GetString("NotCorrectTimeZone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Результат.
        /// </summary>
        internal static string Result {
            get {
                return ResourceManager.GetString("Result", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Без ошибок.
        /// </summary>
        internal static string WithoutErrors {
            get {
                return ResourceManager.GetString("WithoutErrors", resourceCulture);
            }
        }
    }
}