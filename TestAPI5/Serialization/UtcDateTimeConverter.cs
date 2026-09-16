using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestAPI5.Serialization
{
    /// <summary>
    /// Every DateTime this API returns represents a UTC instant -- every write path in
    /// RabbitComputerHelper/ProbeDataProcessor converts to UTC before persisting -- but Npgsql
    /// reads "timestamp without time zone" columns back as DateTimeKind.Unspecified, and
    /// System.Text.Json serializes an Unspecified-kind DateTime with no "Z"/offset suffix at
    /// all. That leaves the frontend to guess, and `new Date(...)` guesses wrong: it treats an
    /// unmarked date-time string as already being local time. Explicitly marking every outgoing
    /// DateTime as UTC here means the JSON always carries an unambiguous "Z" suffix, so callers
    /// (vue-pi2's `new Date(...)`, Chart.js's date-fns adapter, etc.) parse the correct instant
    /// and then render it in the browser's own local timezone automatically.
    /// </summary>
    public class UtcDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.GetDateTime();

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var utcValue = value.Kind == DateTimeKind.Utc
                ? value
                : DateTime.SpecifyKind(value, DateTimeKind.Utc);

            writer.WriteStringValue(utcValue);
        }
    }
}
