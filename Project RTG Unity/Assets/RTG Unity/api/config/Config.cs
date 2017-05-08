﻿namespace rtg.api.config
{
    using System.Collections.Generic;

    using property;

    public abstract class Config
    {

        protected List<ConfigProperty> properties = new List<ConfigProperty>();

        public List<ConfigProperty> getProperties()
        {

            return this.properties;
        }

        protected void addProp(ConfigProperty property)
        {

            for (int i = 0; i < this.properties.Count; i++)
            {

                if (this.properties[i].name.Equals(property.name))
                {
                    removeProp(property.name);
                    break;
                }
            }

            this.properties.Add(property);
        }

        protected void removeProp(string name)
        {

            for (int i = 0; i < this.properties.Count; i++)
            {

                if (this.properties[i].name.Equals(name))
                {
                    this.properties.RemoveAt(i);
                    return;
                }
            }
        }

        public ConfigPropertyBoolean addProperty(ConfigPropertyBoolean property)
        {
            this.addProp(property);
            return property;
        }

        public ConfigPropertyFloat addProperty(ConfigPropertyFloat property)
        {
            this.addProp(property);
            return property;
        }

        public ConfigPropertyInt addProperty(ConfigPropertyInt property)
        {
            this.addProp(property);
            return property;
        }

        public ConfigPropertyString addProperty(ConfigPropertyString property)
        {
            this.addProp(property);
            return property;
        }

        /*public void load(string configFile)
        {

            Configuration config = new Configuration(new File(configFile));

            try
            {
                config.load();

                List<ConfigProperty> properties = this.getProperties();

                for (int j = 0; j < properties.size(); j++)
                {

                    ConfigProperty prop = properties.get(j);

                    switch (prop.type)
                    {

                        case INTEGER:

                            ConfigPropertyInt propInt = (ConfigPropertyInt)properties.get(j);

                            propInt.set(config.getInt(
                                propInt.name,
                                propInt.category,
                                propInt.valueInt,
                                propInt.minValueInt,
                                propInt.maxValueInt,
                                prop.description
                            ));

                            break;

                        case FLOAT:

                            ConfigPropertyFloat propFloat = (ConfigPropertyFloat)properties.get(j);

                            propFloat.set(config.getFloat(
                                propFloat.name,
                                propFloat.category,
                                propFloat.valueFloat,
                                propFloat.minValueFloat,
                                propFloat.maxValueFloat,
                                propFloat.description
                            ));

                            break;

                        case BOOLEAN:

                            ConfigPropertyBoolean propBool = (ConfigPropertyBoolean)properties.get(j);

                            propBool.set(config.getBoolean(
                                propBool.name,
                                propBool.category,
                                propBool.valueBoolean,
                                propBool.description
                            ));

                            break;

                        case STRING:

                            ConfigPropertyString propString = (ConfigPropertyString)properties.get(j);

                            propString.set(config.getString(
                                propString.name,
                                propString.category,
                                propString.valueString,
                                propString.description
                            ));

                            break;

                        default:
                            throw new RuntimeException("ConfigProperty type not supported.");
                    }
                }

            }
            catch (Exception e)
            {
                FMLLog.log(Level.ERROR, "[RTG-ERROR] RTG had a problem loading config: %s", configFile);
            }
            finally
            {
                if (config.hasChanged())
                {
                    config.save();
                }
            }
        }*/
    }
}