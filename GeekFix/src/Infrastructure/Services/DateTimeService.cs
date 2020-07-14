using GeekFix.Application.Common.Interfaces;
using System;

namespace GeekFix.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
