using Bilbayt.Web.API.BackgroundJob.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.BackgroundJob.Jobs
{
    public class EmailQueueJob : BackgroundService
    {
        public EmailQueueJob(IJobServiceConfig<EmailQueueJob> jobServiceConfig) : base(jobServiceConfig.CronExpression, jobServiceConfig.TimeZoneInfo)
        {

        }
        protected override Task DoWork(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
