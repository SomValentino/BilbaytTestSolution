using Bilbayt.Data.Context;
using Bilbayt.Data.Interfaces;
using Bilbayt.Domain.Models;
using Bilbayt.Web.API.BackgroundJob.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.BackgroundJob.Jobs
{
    public class EmailQueueJob : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private ILogger<EmailQueueJob> _logger;
        private readonly bool _enabled;

        public EmailQueueJob(IJobServiceConfig<EmailQueueJob> jobServiceConfig, 
            IServiceScopeFactory serviceScopeFactory,
            ILogger<EmailQueueJob> logger) : base(jobServiceConfig.CronExpression, jobServiceConfig.TimeZoneInfo)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _enabled = jobServiceConfig.IsEnabled;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("EmailQueue Job starts.");
            return base.StartAsync(cancellationToken);
        }
        protected override async Task DoWork(CancellationToken cancellationToken)
        {
            if (_enabled)
            {
                try
                {
                    IEnumerable<EmailQueue> pendingEmails = null;

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var emailQueueRepository = scope.ServiceProvider.GetRequiredService<IEmailQueueRepository>();
                        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                        pendingEmails = await emailQueueRepository.FindAsync(x => x.Status == Domain.Enums.EmailStatus.Pending);

                        if (pendingEmails != null && pendingEmails.Any())
                        {
                            foreach (var pendingEmail in pendingEmails)
                            {
                                pendingEmail.Status = Domain.Enums.EmailStatus.Processing;
                                await emailQueueRepository.UpdateAsync(pendingEmail.Id, pendingEmail);
                            }
                            var apiKey = configuration.GetValue<string>("bilbaytEmailAPIKey");
                            var client = new SendGridClient(apiKey);
                            foreach (var pendingEmail in pendingEmails)
                            {

                                try
                                {
                                    var from = new EmailAddress(pendingEmail.FromAddress);
                                    var subject = pendingEmail.Subject;
                                    var to = new EmailAddress(pendingEmail.ToAddresses);
                                    var msg = MailHelper.CreateSingleEmail(from, to, subject, pendingEmail.Body, null);
                                    var response = await client.SendEmailAsync(msg);
                                    pendingEmail.SentAttempts += 1;
                                    if (response.IsSuccessStatusCode) pendingEmail.Status = Domain.Enums.EmailStatus.Sent;
                                    else pendingEmail.Status = Domain.Enums.EmailStatus.Error;

                                    await emailQueueRepository.UpdateAsync(pendingEmail.Id, pendingEmail);
                                }
                                catch (Exception)
                                {
                                    pendingEmail.Status = Domain.Enums.EmailStatus.Error;
                                    await emailQueueRepository.UpdateAsync(pendingEmail.Id, pendingEmail);
                                    throw;
                                }
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("EmailQueue Job is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
