﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalculatorShell.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class HelpTexts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal HelpTexts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CalculatorShell.Properties.HelpTexts", typeof(HelpTexts).Assembly);
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
        ///   Looks up a localized string similar to Clears the current console.
        /// </summary>
        internal static string Clear {
            get {
                return ResourceManager.GetString("Clear", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Prints out current date.
        /// </summary>
        internal static string Date {
            get {
                return ResourceManager.GetString("Date", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Prints out the divsors of an integer number.
        ///Usage: Divisors [number: integer].
        /// </summary>
        internal static string Divisors {
            get {
                return ResourceManager.GetString("Divisors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Evaluates an expression and writes out the result. Result is also stored into the Ans variable.
        ///Usage: Eval [expression].
        /// </summary>
        internal static string Eval {
            get {
                return ResourceManager.GetString("Eval", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exits the program.
        /// </summary>
        internal static string Exit {
            get {
                return ResourceManager.GetString("Exit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Prints out basic help information about commands.
        /// </summary>
        internal static string Help {
            get {
                return ResourceManager.GetString("Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Display currently set variables or a specific variable value.
        ///Usage: Mem or Mem [variable name].
        /// </summary>
        internal static string Mem {
            get {
                return ResourceManager.GetString("Mem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Define a variable or change it&apos;s value
        ///Usage: MemSet [Variable name] [Expression].
        /// </summary>
        internal static string MemSet {
            get {
                return ResourceManager.GetString("MemSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sleeps for the given ammount of seconds.
        ///Usage: Sleep  [number: positive integer].
        /// </summary>
        internal static string Sleep {
            get {
                return ResourceManager.GetString("Sleep", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Prints out current time.
        /// </summary>
        internal static string Time {
            get {
                return ResourceManager.GetString("Time", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Clear memory or delete a specific variable
        ///Usage: Unset or Unset [Variable name].
        /// </summary>
        internal static string Unset {
            get {
                return ResourceManager.GetString("Unset", resourceCulture);
            }
        }
    }
}
