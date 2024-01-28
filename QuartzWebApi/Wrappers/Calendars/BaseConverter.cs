using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QuartzWebApi.Wrappers.Calendars;

/// <summary>
///    A json converter for the <see cref="BaseCalendar" />
/// </summary>
internal class BaseConverter : JsonConverter
{
    #region Properties
    public override bool CanWrite => false;
    #endregion

    #region CanConvert
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(BaseCalendar);
    }
    #endregion

    #region ReadJson
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jobObject = JObject.Load(reader);

        if (jobObject["Type"] == null)
            throw new ArgumentException("Type is not set");

        var type = Enum.Parse(typeof(CalendarType), jobObject["Type"].ToString());
        var settings = new JsonSerializerSettings { ContractResolver = new AbstractClassContractResolver() };

        return type switch
        {
            CalendarType.Cron => JsonConvert.DeserializeObject<CronCalendar>(jobObject.ToString(), settings),
            CalendarType.Daily => JsonConvert.DeserializeObject<DailyCalendar>(jobObject.ToString(), settings),
            CalendarType.Weekly => JsonConvert.DeserializeObject<WeeklyCalendar>(jobObject.ToString(), settings),
            CalendarType.Monthly => JsonConvert.DeserializeObject<MonthlyCalendar>(jobObject.ToString(), settings),
            CalendarType.Annual => JsonConvert.DeserializeObject<AnnualCalendar>(jobObject.ToString(), settings),
            CalendarType.Holiday => JsonConvert.DeserializeObject<HolidayCalendar>(jobObject.ToString(), settings),
            _ => throw new ArgumentException("Invalid type")
        };
    }
    #endregion

    #region WriteJson
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
    #endregion
}