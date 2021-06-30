using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.BackgroundJob.Config
{
    public class JobServiceConfig<TService> : IJobServiceConfig<TService> where TService : BackgroundService
    {
        private TimeZoneInfo _timeZoneInfo;

        public string CronExpression { get ; set ; }
        public TimeZoneInfo TimeZoneInfo 
        {
            get
            {
                if (_timeZoneInfo == null) return TimeZoneInfo.Local;

                return _timeZoneInfo;
            }
            set
            {
                _timeZoneInfo = value;
            }
        }

        public bool IsEnabled { get; set; }
    }
}
