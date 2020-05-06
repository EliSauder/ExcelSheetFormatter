namespace Formatter.Configuration
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationElementException'
    public class ConfigurationElementException : ConfigurationException
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationElementException'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationElementException.ConfigurationElementException()'
        public ConfigurationElementException() : base("Invalid element configuration.") { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationElementException.ConfigurationElementException()'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationElementException.ConfigurationElementException(string)'
        public ConfigurationElementException(string s) : base(s) { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationElementException.ConfigurationElementException(string)'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationElementException.ConfigurationElementException(string, ConfigurationElementException)'
        public ConfigurationElementException(string s, ConfigurationElementException innerException) : base(s, innerException) { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationElementException.ConfigurationElementException(string, ConfigurationElementException)'
    }
}
