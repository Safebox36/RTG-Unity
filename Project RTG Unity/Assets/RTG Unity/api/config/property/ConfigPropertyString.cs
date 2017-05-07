namespace rtg.api.config.property
{
    public class ConfigPropertyString : ConfigProperty
    {
        public string valueString;

        public ConfigPropertyString(Type type, string name, string category, string description, string defaultValue) : base(type, name, category, description)
        {
            this.valueString = defaultValue;

            this.formatDescription();
        }

        public string get()
        {
            return this.valueString;
        }

        public void set(string value)
        {
            this.valueString = value;
        }
    }
}