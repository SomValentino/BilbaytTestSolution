using Bilbayt.Data.Context;
using Bilbayt.Data.Interfaces;
using Bilbayt.Domain;
using Bilbayt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Data.Repositories
{
    public class EmailQueueRepository : BaseRepository<EmailQueue> , IEmailQueueRepository
    {
        public EmailQueueRepository(IDataContextSetting dataContextSetting): base(dataContextSetting, nameof(EmailQueue))
        {

        }

        public async Task AddEmailToQueue(EmailQueue emailQueue)
        {
            await CreateAsync(emailQueue);
        }
    }
}
