using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Utils
{
    public class EnumHelper
    {
        /// <summary>
        /// 根据枚举值，返回描述字符串
        /// 如果多选枚举，返回以","分割的字符串
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetAllEnumDesc(Enum e, Enum language = null)
        {
            if (e == null)
            {
                return "";
            }
            Type type = e.GetType();
            string ret = "";

            System.Reflection.FieldInfo fi = type.GetField(e.ToString());
            if (fi != null)
            {
                DescriptionAttribute[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                if (attrs != null && attrs.Length > 0)
                {
                    ret = attrs[0].Description;
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumKeyValue> GetEnumList<T>()
        {
            List<EnumKeyValue> list = new List<EnumKeyValue>();
            Type type = typeof(DescriptionAttribute);
            foreach (var item in typeof(T).GetFields())
            {
                object[] arr = item.GetCustomAttributes(type, true);
                if (arr.Length > 0)
                {
                    EnumKeyValue model = new EnumKeyValue();
                    model.Key = (int)Enum.Parse(typeof(T), item.Name);
                    model.Value = ((DescriptionAttribute)arr[0]).Description;
                    list.Add(model);
                }
            }
            return list;
        }

    }

    public class EnumKeyValue
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

}
