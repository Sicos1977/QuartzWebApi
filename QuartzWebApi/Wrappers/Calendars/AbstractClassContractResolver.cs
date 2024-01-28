using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QuartzWebApi.Wrappers.Calendars;

internal class AbstractClassContractResolver : DefaultContractResolver
{
    protected override JsonConverter ResolveContractConverter(Type objectType)
    {
        if (typeof(BaseCalendar).IsAssignableFrom(objectType) && !objectType.IsAbstract)
            return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
        return base.ResolveContractConverter(objectType);
    }
}