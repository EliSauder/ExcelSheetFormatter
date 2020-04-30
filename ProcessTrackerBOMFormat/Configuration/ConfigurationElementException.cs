namespace Formatter.Configuration {
    public class ConfigurationElementException : ConfigurationException {
        public ConfigurationElementException() : base("Invalid element configuration.") { }

        public ConfigurationElementException(string s) : base(s) { }

        public ConfigurationElementException(string s, ConfigurationElementException innerException) : base(s, innerException) { }
    }
}
