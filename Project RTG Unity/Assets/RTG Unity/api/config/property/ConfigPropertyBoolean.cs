namespace rtg.api.config.property
{
    public class ConfigPropertyBoolean : ConfigProperty
    {
        public bool valueBoolean;

        public ConfigPropertyBoolean(Type type, string name, string category, string description, bool defaultValue) : base(type, name, category, description)
        {
            this.valueBoolean = defaultValue;

            this.formatDescription();
        }

        public bool get()
        {
            return this.valueBoolean;
        }

        public void set(bool value)
        {
            this.valueBoolean = value;
        }
    }
}