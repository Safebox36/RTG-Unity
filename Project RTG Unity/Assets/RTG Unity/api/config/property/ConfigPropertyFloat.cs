namespace rtg.api.config.property
{
    public class ConfigPropertyFloat : ConfigProperty
    {
        public float minValueFloat;
        public float maxValueFloat;
        public float valueFloat;

        public ConfigPropertyFloat(Type type, string name, string category, string description, float defaultValue, float minValueFloat, float maxValueFloat) : base(type, name, category, description)
        {
            this.valueFloat = defaultValue;
            this.minValueFloat = minValueFloat;
            this.maxValueFloat = maxValueFloat;

            this.formatDescription();
        }

        public float get()
        {
            return this.valueFloat;
        }

        public void set(float value)
        {
            this.valueFloat = value;
        }
    }
}