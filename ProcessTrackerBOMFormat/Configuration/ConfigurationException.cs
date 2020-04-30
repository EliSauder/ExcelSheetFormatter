using System;

namespace Formatter.Configuration {
    public class ConfigurationException : Exception {
        public ConfigurationException() : base("Invalid configuration found.") { }

        public ConfigurationException(string s) : base(s) { }

        public ConfigurationException(string s, ConfigurationException innerException) : base(s, innerException) { }
    }
}
