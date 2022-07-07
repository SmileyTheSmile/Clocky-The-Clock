using UnityEngine;
using Newtonsoft.Json;
using System;

namespace Web
{
    public class JsonSerializationOption : ISerializationOption
    {
        public string ContentType => "application/json";

        public T Deserialize<T>(string text)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(text);
                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError($"could not parse response {text}. {ex.Message}");
                return default;
            }
        }
    }
}
