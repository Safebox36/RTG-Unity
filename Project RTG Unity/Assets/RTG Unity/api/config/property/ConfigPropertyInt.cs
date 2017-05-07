namespace rtg.api.config.property
{
    public class ConfigPropertyInt : ConfigProperty
    {
        public int minValueInt;
        public int maxValueInt;
        public int valueInt;

        public ConfigPropertyInt(Type type, string name, string category, string description, int defaultValue, int minValueInt, int maxValueInt) : base(type, name, category, description)
        {
            this.valueInt = defaultValue;
            this.minValueInt = minValueInt;
            this.maxValueInt = maxValueInt;

            this.formatDescription();
        }

        public int get()
        {
            return this.valueInt;
        }

        public void set(int value)
        {
            this.valueInt = value;
        }
    }
}