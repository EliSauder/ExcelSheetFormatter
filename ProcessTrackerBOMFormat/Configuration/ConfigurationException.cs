using System;

namespace Formatter.Configuration
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationException'
    public class ConfigurationException : Exception
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationException'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationException.ConfigurationException()'
        public ConfigurationException() : base("Invalid configuration found.") { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationException.ConfigurationException()'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationException.ConfigurationException(string)'
        public ConfigurationException(string s) : base(s) { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationException.ConfigurationException(string)'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationException.ConfigurationException(string, ConfigurationException)'
        public ConfigurationException(string s, ConfigurationException innerException) : base(s, innerException) { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationException.ConfigurationException(string, ConfigurationException)'
    }
}
