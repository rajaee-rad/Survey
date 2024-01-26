using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomerSurveySystem.Services.Class
{
    public class NetTypeConverter : JsonConverter
    {
        private const string NetType = "$NetType";

        private readonly Dictionary<string, Type> _concreteTypes;

        public NetTypeConverter(params Type[] concreteTypes)
        {
            _concreteTypes = concreteTypes.ToDictionary(type => type.Name);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                JValue jValue = new JValue((object)null);
                jValue.WriteTo(writer);
                return;
            }

            JObject data = JObject.FromObject(value);
            data.Add(NetType, new JValue(value.GetType().Name));
            data.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JToken token = JToken.ReadFrom(reader);
            if (token.Type == JTokenType.Null)
                return null;
            if (token.Type != JTokenType.Object)
                throw new Exception("Only object type is accepted as data");
            JObject jObject = (JObject)token;
            JProperty netTypeProperty = jObject.Property(NetType);
            if (netTypeProperty == null || netTypeProperty.Value.Type != JTokenType.String)
                throw new Exception($"{NetType} property required");

            string dataType = netTypeProperty.Value.Value<string>();
            jObject.Remove(netTypeProperty.Name);
            if (!_concreteTypes.TryGetValue(dataType, out Type targetType))
                throw new Exception($"Type '{dataType}' cannot be located on registered concrete types");
            return jObject.ToObject(targetType);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsInterface;
        }
    }
}