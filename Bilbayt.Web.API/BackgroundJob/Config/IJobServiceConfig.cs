using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.BackgroundJob.Config
{
    public interface IJobServiceConfig<TService> where TService : BackgroundService
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
        public bool IsEnabled { get; set; }
    }
}
