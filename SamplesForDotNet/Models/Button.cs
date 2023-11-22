using System;
using System.Text.Json.Serialization;

namespace DotNet8NewFeatures.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter<Button>))]
    public enum Button : byte
    {
        Red,
        Green,
        Blue,
        Yellow
    }
}
