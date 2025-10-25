using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace RookieRise.Data.Attributes
{
    #region NoteForMe
     //- Reads enum values from JSON strings.
    // - Makes sure the value exists in the enum.
   // - Throws an error if the value is invalid.
  // - Writes enum values as strings when saving to JSON.
    #endregion
    public class ValidEnumValue<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (Enum.TryParse<T>(value, true, out var result) && Enum.IsDefined(typeof(T), result))
                #region NoteForMe
                // 'true' means case-insensitive when matching enum names (e.g., "active" == "Active")
                #endregion
                return result;
            throw new JsonException($"invalid value '{value}' for Enum '{typeof(T).Name}'");
        }
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
