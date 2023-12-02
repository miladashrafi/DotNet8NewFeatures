using System;
using System.Text.Json.Serialization;

namespace DotNet8NewFeatures.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter<Button>))]
    public enum Button : byte
    {
        Red = 1,
        Green = 2,
        Blue = 3,
        Yellow = 4
    }
}
