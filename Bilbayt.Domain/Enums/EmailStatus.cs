using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Domain.Enums
{
    public enum EmailStatus
    {
        Pending = 1,
        Sent = 2,
        Processing = 3,
        Error = 4
    }

    public enum EmailMessageType
    {
        CommentsAddedBP = 1,
        NewMetricAdded,
        CommentsAddedBE,
        SubmitBE,
        DeclineBE,
        ApproveBE
    }
}
