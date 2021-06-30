using Bilbayt.Domain;
using Bilbayt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Data.Interfaces
{
    public interface IEmailQueueRepository : IBaseRepository<EmailQueue>
    {
        Task AddEmailToQueue(EmailQueue emailQueue);
    }
}
