﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OxigenIIAdvertising.ContentExchanger.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OxigenIIAdvertising.ContentExchanger.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Cannot Save General Data. The application has encountered an error: CESMS:001.
        /// </summary>
        internal static string CannotConvertStreamToByteArray {
            get {
                return ResourceManager.GetString("CannotConvertStreamToByteArray", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to General Data cannot be downloaded at this time. The error message was:\r\n.
        /// </summary>
        internal static string CannotDownloadGeneralDataSafeMode {
            get {
                return ResourceManager.GetString("CannotDownloadGeneralDataSafeMode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot Save General Data. The application has encountered an error: CESMS:002.
        /// </summary>
        internal static string CannotSaveStream {
            get {
                return ResourceManager.GetString("CannotSaveStream", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oxigen.
        /// </summary>
        internal static string LowAssetSpaceHeader {
            get {
                return ResourceManager.GetString("LowAssetSpaceHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oxigen cannot download all the Assets playable by your subscribed Channels at this time because your allocated Asset folder size is too low. Please increase the allocated Asset folder size from the Screensaver Options menu so Oxigen can continue..
        /// </summary>
        internal static string LowAssetSpaceText {
            get {
                return ResourceManager.GetString("LowAssetSpaceText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oxigen.
        /// </summary>
        internal static string LowDiskSpaceHeader {
            get {
                return ResourceManager.GetString("LowDiskSpaceHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oxigen cannot download all the Assets playable by your subscribed Channels at this time because your disk space is low. Please free up some disk space or unsubscribe from some Channels so Oxigen can proceed..
        /// </summary>
        internal static string LowDiskSpaceText {
            get {
                return ResourceManager.GetString("LowDiskSpaceText", resourceCulture);
            }
        }
        
        internal static System.Drawing.Icon oxigen {
            get {
                object obj = ResourceManager.GetObject("oxigen", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data has been downloaded..
        /// </summary>
        internal static string SMCompleted {
            get {
                return ResourceManager.GetString("SMCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The application has encountered an error..
        /// </summary>
        internal static string SMGeneralError {
            get {
                return ResourceManager.GetString("SMGeneralError", resourceCulture);
            }
        }
    }
}
