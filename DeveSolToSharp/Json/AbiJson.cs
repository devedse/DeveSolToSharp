// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var welcome = Welcome.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class AbiJson
    {
        [JsonProperty("constant", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Constant { get; set; }

        [JsonProperty("inputs", NullValueHandling = NullValueHandling.Ignore)]
        public Input[] Inputs { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("outputs", NullValueHandling = NullValueHandling.Ignore)]
        public Output[] Outputs { get; set; }

        [JsonProperty("payable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Payable { get; set; }

        [JsonProperty("stateMutability", NullValueHandling = NullValueHandling.Ignore)]
        public string StateMutability { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("anonymous", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Anonymous { get; set; }
    }

    public partial class Input
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("indexed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Indexed { get; set; }
    }

    public partial class Output
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    //public enum InputType { Address, Bool, TypeAddress, TypeUint256, Uint256 };

    //public enum StateMutability { Nonpayable, Payable, View };

    //public enum WelcomeType { Constructor, Event, Fallback, Function };

    //public partial class Welcome
    //{
    //    public static Welcome[] FromJson(string json) => JsonConvert.DeserializeObject<Welcome[]>(json, QuickType.Converter.Settings);
    //}

    //static class InputTypeExtensions
    //{
    //    public static InputType? ValueForString(string str)
    //    {
    //        switch (str)
    //        {
    //            case "address": return InputType.Address;
    //            case "bool": return InputType.Bool;
    //            case "address[]": return InputType.TypeAddress;
    //            case "uint256[]": return InputType.TypeUint256;
    //            case "uint256": return InputType.Uint256;
    //            default: return null;
    //        }
    //    }

    //    public static InputType ReadJson(JsonReader reader, JsonSerializer serializer)
    //    {
    //        var str = serializer.Deserialize<string>(reader);
    //        var maybeValue = ValueForString(str);
    //        if (maybeValue.HasValue) return maybeValue.Value;
    //        throw new Exception("Unknown enum case " + str);
    //    }

    //    public static void WriteJson(this InputType value, JsonWriter writer, JsonSerializer serializer)
    //    {
    //        switch (value)
    //        {
    //            case InputType.Address: serializer.Serialize(writer, "address"); break;
    //            case InputType.Bool: serializer.Serialize(writer, "bool"); break;
    //            case InputType.TypeAddress: serializer.Serialize(writer, "address[]"); break;
    //            case InputType.TypeUint256: serializer.Serialize(writer, "uint256[]"); break;
    //            case InputType.Uint256: serializer.Serialize(writer, "uint256"); break;
    //        }
    //    }
    //}

    //static class NameExtensions
    //{
    //    public static Name? ValueForString(string str)
    //    {
    //        switch (str)
    //        {
    //            case "": return Name.Empty;
    //            default: return null;
    //        }
    //    }

    //    public static Name ReadJson(JsonReader reader, JsonSerializer serializer)
    //    {
    //        var str = serializer.Deserialize<string>(reader);
    //        var maybeValue = ValueForString(str);
    //        if (maybeValue.HasValue) return maybeValue.Value;
    //        throw new Exception("Unknown enum case " + str);
    //    }

    //    public static void WriteJson(this Name value, JsonWriter writer, JsonSerializer serializer)
    //    {
    //        switch (value)
    //        {
    //            case Name.Empty: serializer.Serialize(writer, ""); break;
    //        }
    //    }
    //}

    //static class StateMutabilityExtensions
    //{
    //    public static StateMutability? ValueForString(string str)
    //    {
    //        switch (str)
    //        {
    //            case "nonpayable": return StateMutability.Nonpayable;
    //            case "payable": return StateMutability.Payable;
    //            case "view": return StateMutability.View;
    //            default: return null;
    //        }
    //    }

    //    public static StateMutability ReadJson(JsonReader reader, JsonSerializer serializer)
    //    {
    //        var str = serializer.Deserialize<string>(reader);
    //        var maybeValue = ValueForString(str);
    //        if (maybeValue.HasValue) return maybeValue.Value;
    //        throw new Exception("Unknown enum case " + str);
    //    }

    //    public static void WriteJson(this StateMutability value, JsonWriter writer, JsonSerializer serializer)
    //    {
    //        switch (value)
    //        {
    //            case StateMutability.Nonpayable: serializer.Serialize(writer, "nonpayable"); break;
    //            case StateMutability.Payable: serializer.Serialize(writer, "payable"); break;
    //            case StateMutability.View: serializer.Serialize(writer, "view"); break;
    //        }
    //    }
    //}

    //static class WelcomeTypeExtensions
    //{
    //    public static WelcomeType? ValueForString(string str)
    //    {
    //        switch (str)
    //        {
    //            case "constructor": return WelcomeType.Constructor;
    //            case "event": return WelcomeType.Event;
    //            case "fallback": return WelcomeType.Fallback;
    //            case "function": return WelcomeType.Function;
    //            default: return null;
    //        }
    //    }

    //    public static WelcomeType ReadJson(JsonReader reader, JsonSerializer serializer)
    //    {
    //        var str = serializer.Deserialize<string>(reader);
    //        var maybeValue = ValueForString(str);
    //        if (maybeValue.HasValue) return maybeValue.Value;
    //        throw new Exception("Unknown enum case " + str);
    //    }

    //    public static void WriteJson(this WelcomeType value, JsonWriter writer, JsonSerializer serializer)
    //    {
    //        switch (value)
    //        {
    //            case WelcomeType.Constructor: serializer.Serialize(writer, "constructor"); break;
    //            case WelcomeType.Event: serializer.Serialize(writer, "event"); break;
    //            case WelcomeType.Fallback: serializer.Serialize(writer, "fallback"); break;
    //            case WelcomeType.Function: serializer.Serialize(writer, "function"); break;
    //        }
    //    }
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this Welcome[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    //}

    //internal class Converter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(InputType) || t == typeof(Name) || t == typeof(StateMutability) || t == typeof(WelcomeType) || t == typeof(InputType?) || t == typeof(Name?) || t == typeof(StateMutability?) || t == typeof(WelcomeType?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (t == typeof(InputType))
    //            return InputTypeExtensions.ReadJson(reader, serializer);
    //        if (t == typeof(Name))
    //            return NameExtensions.ReadJson(reader, serializer);
    //        if (t == typeof(StateMutability))
    //            return StateMutabilityExtensions.ReadJson(reader, serializer);
    //        if (t == typeof(WelcomeType))
    //            return WelcomeTypeExtensions.ReadJson(reader, serializer);
    //        if (t == typeof(InputType?))
    //        {
    //            if (reader.TokenType == JsonToken.Null) return null;
    //            return InputTypeExtensions.ReadJson(reader, serializer);
    //        }
    //        if (t == typeof(Name?))
    //        {
    //            if (reader.TokenType == JsonToken.Null) return null;
    //            return NameExtensions.ReadJson(reader, serializer);
    //        }
    //        if (t == typeof(StateMutability?))
    //        {
    //            if (reader.TokenType == JsonToken.Null) return null;
    //            return StateMutabilityExtensions.ReadJson(reader, serializer);
    //        }
    //        if (t == typeof(WelcomeType?))
    //        {
    //            if (reader.TokenType == JsonToken.Null) return null;
    //            return WelcomeTypeExtensions.ReadJson(reader, serializer);
    //        }
    //        throw new Exception("Unknown type");
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var t = value.GetType();
    //        if (t == typeof(InputType))
    //        {
    //            ((InputType)value).WriteJson(writer, serializer);
    //            return;
    //        }
    //        if (t == typeof(Name))
    //        {
    //            ((Name)value).WriteJson(writer, serializer);
    //            return;
    //        }
    //        if (t == typeof(StateMutability))
    //        {
    //            ((StateMutability)value).WriteJson(writer, serializer);
    //            return;
    //        }
    //        if (t == typeof(WelcomeType))
    //        {
    //            ((WelcomeType)value).WriteJson(writer, serializer);
    //            return;
    //        }
    //        throw new Exception("Unknown type");
    //    }

    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters = {
    //            new Converter(),
    //            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //        },
    //    };
    //}
}
