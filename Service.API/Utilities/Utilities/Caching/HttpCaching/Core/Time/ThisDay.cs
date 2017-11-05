using System; 

namespace Fantasy.API.Utilities.Caching.HttpCaching.Core.Time
{
    public class ThisDay : IModelQuery<DateTime, CacheTime>
    {
        private readonly int hour;
        private readonly int minute;
        private readonly int second;

        public ThisDay(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public CacheTime Execute(DateTime model)
        {
            var cacheTime = new CacheTime
            {
                AbsoluteExpiration = new DateTime(model.Year,
                                                  model.Month,
                                                  model.Day,
                                                  hour,
                                                  minute,
                                                  second),
            };

            if (cacheTime.AbsoluteExpiration <= model)
                cacheTime.AbsoluteExpiration = cacheTime.AbsoluteExpiration.AddDays(1);

            cacheTime.ClientTimeSpan = cacheTime.AbsoluteExpiration.Subtract(model);

            return cacheTime;
        }
    }
    public class UpToHours : IModelQuery<DateTime, CacheTime>
    {
        private readonly int hour;

        public UpToHours(int hour)
        {
            this.hour = hour;
        }

        public CacheTime Execute(DateTime model)
        {
            var date = new DateTime(model.Year,
                model.Month,
                model.Day,
                model.Hour,
                59,
                59);
           var newDate = date.AddHours(hour);
            var cacheTime = new CacheTime
            {
                AbsoluteExpiration = newDate
            };

            if (cacheTime.AbsoluteExpiration <= model)
                cacheTime.AbsoluteExpiration = cacheTime.AbsoluteExpiration.AddDays(1);

            cacheTime.ClientTimeSpan = cacheTime.AbsoluteExpiration.Subtract(model);

            return cacheTime;
        }
    }
}
