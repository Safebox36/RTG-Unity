namespace rtg.api.util
{
    using System;
    using System.Reflection;
    using System.Collections.Generic;

    /**
     * @author Zeno410
     *         Modified by topisani to allow for multiple possible field names.
     *         This is useful for catching both the obfuscated and the deobfuscated field.
     */
    public class Accessor<ObjectType, FieldInfoType>
    {

        private readonly string[] fieldNames;
        private FieldInfo _field;

        public Accessor(string[] _fieldName)
        {
            fieldNames = _fieldName;
        }

        public FieldInfoType get(ObjectType _object)
        {

            try
            {
                return (FieldInfoType)(field(_object).GetValue(_object));
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private FieldInfo field(ObjectType example)
        {

            Type classObject = example.GetType();
            if (_field == null)
            {
                try
                {
                    setFieldInfo(classObject);
                }
                catch (AccessViolationException e)
                {
                    throw new Exception(e.Message);
                }
            }
            return _field;
        }

        private void setFieldInfo(Type classObject)
        {
            try
            {
                // hunts through the class object and all superclasses looking for the field name
                FieldInfo[] fields;
                do
                {
                    fields = classObject.GetFields();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        foreach (string fieldName in fieldNames)
                        {
                            if (fields[i].Name.Contains(fieldName))
                            {
                                _field = fields[i];
                                //_field.setAccessible(true);
                                return;
                            }
                        }
                    }
                    classObject = classObject.GetType().BaseType;
                }
                while (classObject != typeof(object));
                throw new Exception("None of " + string.Join(",", fieldNames) + " found in class " + classObject.Name);
            }
            catch (AccessViolationException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void setFieldInfo(ObjectType _object, FieldInfoType fieldValue)
        {

            try
            {
                field(_object).SetValue(_object, fieldValue);
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}