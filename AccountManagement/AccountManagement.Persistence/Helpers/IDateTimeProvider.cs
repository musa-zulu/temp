using System;

namespace AccountManagement.Persistence.Helpers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}
