namespace rtg.api.config.property
{
    public class ConfigProperty
    {

        public Type type;
        public string name;
        public string category;
        public string description;

        public ConfigProperty(Type type, string name, string category, string description)
        {
            this.type = type;
            this.name = name;
            this.category = category;
            this.description = description;
        }

        public void formatDescription()
        {
            if (!string.IsNullOrEmpty(this.description))
            {
                this.description += '\n';
            }
        }

        public enum Type
        {
            INTEGER,
            FLOAT,
            BOOLEAN,
            STRING
        }
    }
}