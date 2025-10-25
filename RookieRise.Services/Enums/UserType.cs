using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RookieRise.Data.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RookieRise.Data.Enums
{
    [JsonConverter(typeof(ValidEnumValue<UserType>))]
    #region NoteForMe
    //1. When converting from JSON to C#:
    //• If the value is "admin" or "Admin" → it will be converted to UserType.Admin.
    //• If the value is "unknown" → it will throw a JsonException instead of leaving it invalid or setting it to 0.
    //2. When converting from C# to JSON:
    //• Instead of outputting "UserType": 1,
    //• It will write "UserType": "Admin" (as a readable string).
    #endregion
    public enum UserType
    {
        Company = 2,
        Employee = 3,
        Admin = 1
    }
}
