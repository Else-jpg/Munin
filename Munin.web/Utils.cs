using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}