using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Munin.web
{
    public static class Utils
    {
        public static JsonSerializerSettings JsonSettings()
        {
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };

            return settings;
        }

        public static List<UISelectItem> SelectListOf<T>()
        {

            List<UISelectItem> keyList = new List<UISelectItem>();
            Type t = typeof(T);
            if (t.IsEnum)
            {
                var v = Enum.GetValues(t);
                foreach (var e in v)
                {
                    keyList.Add(new UISelectItem() {Value = (int)e, Text= e.ToString()});
                }

                return keyList;
            }
            return null;
        }


        public static string GetEnumDescription<T>(T source)
        {
            var enumType = source.GetType().GetField(source.ToString());
            if (enumType != null)
            {
                var display = ((DisplayAttribute[])enumType.GetCustomAttributes(typeof(DisplayAttribute), false)).FirstOrDefault();
                if (display != null)
                {
                    return display.Name;
                }
            }
            return "";
        }
    }

    public enum BilledeMateriale
    {
        [Description("Papirbillede")]
        Papirbillede = 1,
        [Description("Negativ")]
        Negativ = 2,
        [Description("Dias")]
        Dias = 3,
        [Description("CD")]
        Cd = 4,
        [Description("DVD")]
        Dvd = 5,
        [Description("Andet")]
        Andet = 6
    }

    public class KeyString
    {
        public int Key { get; set; }
        public string Text  { get; set; }
    }

    public class UISelectItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Datoen er ikke godkendt.");

            if ((DateTime)value == DateTime.MinValue)
                return new ValidationResult("Datoen er ikke godkendt.");
            
            return ValidationResult.Success;
        }
    }
}