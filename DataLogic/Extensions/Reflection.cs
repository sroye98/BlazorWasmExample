using System;
using System.Reflection;

namespace DataLogic.Extensions
{
    public static class Reflection
    {
        public static Object GetPropValue(this Object obj, string propName)
        {
            string[] nameParts = propName.Split(".");
            if (nameParts.Length == 1)
            {
                return obj.GetType()
                    .GetProperty(propName)
                    .GetValue(obj, null);
            }

            foreach (string part in nameParts)
            {
                if (obj == null)
                {
                    return null;
                }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);

                if (info == null)
                {
                    return null;
                }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }
    }
}
