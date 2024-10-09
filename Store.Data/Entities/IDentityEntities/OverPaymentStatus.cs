using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.IDentityEntities
{
    public enum OverPaymentStatus
    {
        Placed,
        Running,
        Delivering,
        Delivered,
        Cancelled,
        Pending
    }
}
