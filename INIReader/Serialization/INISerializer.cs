using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace INIReader.Serialization
{
    public class INISerializer
    {

        public T Deserialize<T>(string INIContent, bool EscapeMode)
        {
            return (T)new object();
        }

        public T CreateObject<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public object CreateObject(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public object  SetValue(object obj, string Name, object Value, Type type)
        {
            string FieldName = Name;
            FieldInfo publicField = type.GetField(FieldName, BindingFlags.Instance | BindingFlags.Public);
            FieldInfo privateField = type.GetField(FieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            PropertyInfo publicproperty = type.GetProperty(FieldName, BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo privateproperty = type.GetProperty(FieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            PropertyInfo pi = publicproperty ?? privateproperty ?? null;
            FieldInfo fi = publicField ?? privateField ?? null;
            if (!(fi is null))
                goto fieldSet;
            else if (!(pi is null))
                goto propertySet;
            else
                return obj;

            propertySet:
            {
                pi.SetValue(obj, Value);
                return obj;
            }

        fieldSet:
            {
                fi.SetValue(obj, Value);
                return obj;
            }
        }

                                                                                                                                                                                                                                                        
    }
}

