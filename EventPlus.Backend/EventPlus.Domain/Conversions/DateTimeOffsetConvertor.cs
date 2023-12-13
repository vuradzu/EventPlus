using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventPlus.Domain.Conversions;

public class DateTimeOffsetConvertor : ValueConverter<DateTimeOffset, DateTime>
{
    public DateTimeOffsetConvertor() : base(
        doff => doff.UtcDateTime,
        dt => new DateTimeOffset(dt, TimeSpan.Zero)
    )
    { }
}