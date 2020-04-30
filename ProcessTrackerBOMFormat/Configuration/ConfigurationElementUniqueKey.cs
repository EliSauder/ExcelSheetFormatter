using System;
using System.Configuration;

namespace Formatter.Configuration {

    /// <summary>
    /// <para>Class <c>ConfigurationElementUniqueKey</c> defines a bom find location/value.</para>  
    /// <para>This is where the program will look in the input file to determin if the file provided is of a paticular type.</para>
    /// </summary>
    /// <see cref="ConfigurationElement"/>
    [Obsolete("Not used in preference for a form user selection.")]
    public class ConfigurationElementUniqueKey : ConfigurationElement {

        /// <value>Property <c>WhereLook</c> tells the program where to look for the value.</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// <para>Any value must match this regex: @"^(\w+\d+(?:(?:,\w+\d+)+)?|)$"</para>
        /// </remarks>
        [ConfigurationProperty("whereLook", IsRequired = true)]
        [RegexStringValidator(@"^[A-Z]+\d+(?:,[A-Z]+\d+)*$")]
        public string WhereLook { 
            get { return (string)this["whereLook"]; } 
            set { this["whereLook"] = value; } 
        }

        /// <value>Property <c>ValueFind</c> is the value the program will look for in the WhereLook location.</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// </remarks>
        [ConfigurationProperty("valueFind", IsRequired = true)]
        public string ValueFind {
            get { return (string)this["valueFind"]; }
            set { this["valueFind"] = value; }
        }

    }
}
