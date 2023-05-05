namespace TestCaseApp.Services;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow();
    DateTime Now();
}

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow() => DateTimeOffset.UtcNow;
    public DateTime Now() => DateTime.Now;
}