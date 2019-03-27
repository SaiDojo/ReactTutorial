using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Models
{
    public abstract class AbstractModel
    {
        //read - copy values to instance properties
        public virtual void AssignValuesToInstance(string[] propertyValues)
        {
            var properties = GetType().GetProperties();
            for (var i = 0; i < propertyValues.Length; i++)
            {
                if (properties[i].PropertyType
                    .IsSubclassOf(typeof(AbstractModel)))
                {
                    var instance = Activator.CreateInstance(properties[i].PropertyType);
                    var instanceProperties = instance.GetType().GetProperties();
                    var propertyList = new List<string>();

                    for (var j = 0; j < instanceProperties.Length; j++)
                    {
                        propertyList.Add(propertyValues[i + j]);
                    }
                    var m = instance.GetType().GetMethod("AssignValuesToInstance", new Type[] { typeof(string[]) });
                    m.Invoke(instance, new object[] { propertyList.ToArray() });
                    properties[i].SetValue(this, instance);

                    i += instanceProperties.Length;
                }
                else
                {
                    var type = properties[i].PropertyType.Name;
                    switch (type)
                    {
                        case "Int32":
                            properties[i].SetValue(this,
                                            int.Parse(propertyValues[i]));
                            break;
                        default:
                            properties[i].SetValue(this, propertyValues[i]);
                            break;
                    }
                }
            }
        }

        //write instance values to array
        public virtual List<string> CopyPropValuesToArray()
        {
            var properties = GetType().GetProperties();
            List<string> propValues = new List<string>();

            for (var i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType.IsSubclassOf(typeof(AbstractModel)))
                {
                    var m = properties[i].PropertyType
                            .GetMethod("CopyPropValuesToArray", new Type[0]);
                    var subClassValues = (List<string>)m.Invoke(properties[i].GetValue(this),
                                        new object[0]);
                    propValues.AddRange(subClassValues);
                }
                else
                {
                    propValues.Add(PreProcess(properties[i]
                                        .GetValue(this).ToString()));
                }
            }

            return propValues;
        }

        //write instance values to array
        public virtual List<string> CopyPropValuesToArray(string[] propertyNames, bool isIgnore)
        {
            var properties = GetType().GetProperties();
            List<string> propValues = new List<string>();

            for (var i = 0; i < properties.Length; i++)
            {
                if (isIgnore)
                {
                    if (!propertyNames.Contains(properties[i].Name))
                    {
                        if (properties[i].PropertyType
                            .IsSubclassOf(typeof(AbstractModel)))
                        {
                            var m = properties[i].PropertyType
                            .GetMethod("CopyPropValuesToArray", new Type[0]);
                            var subClassValues = (List<string>)m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                            propValues.AddRange(subClassValues);
                        }
                        else
                        {
                            propValues.Add(PreProcess(properties[i]
                                        .GetValue(this).ToString()));
                        }
                    }
                }
                else
                {
                    if (propertyNames.Contains(properties[i].Name))
                    {
                        if (properties[i].PropertyType
                            .IsSubclassOf(typeof(AbstractModel)))
                        {
                            var m = properties[i].PropertyType
                            .GetMethod("CopyPropValuesToArray", new Type[0]);
                            var subClassValues = (List<string>)m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                            propValues.AddRange(subClassValues);
                        }
                        else
                        {
                            propValues.Add(PreProcess(properties[i]
                                        .GetValue(this).ToString()));
                        }

                    }
                }
            }

            return propValues;
        }
        
        private string PreProcess(string input)
        {
            input = input.Replace('ı', 'i')
                .Replace('ç', 'c')
                .Replace('ö', 'o')
                .Replace('ş', 's')
                .Replace('ü', 'u')
                .Replace('ğ', 'g')
                .Replace('İ', 'I')
                .Replace('Ç', 'C')
                .Replace('Ö', 'O')
                .Replace('Ş', 'S')
                .Replace('Ü', 'U')
                .Replace('Ğ', 'G')
                .Replace("\"", "\"\"")
                .Trim();
            if (input.Contains(","))
            {
                input = "\"" + input + "\"";
            }
            return input;
        }

    }
}
