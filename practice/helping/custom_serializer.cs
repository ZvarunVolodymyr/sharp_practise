using System.Text.Json;
using Newtonsoft.Json;

namespace practice.helping;

public class custom_serializer
{
    public class DateOnlySerializer : System.Text.Json.Serialization.JsonConverter<DateOnly>
    {
        private readonly string serializationFormat;

        public DateOnlySerializer() : this(null)
        {
        
        }

        public DateOnlySerializer(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat;
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }

}