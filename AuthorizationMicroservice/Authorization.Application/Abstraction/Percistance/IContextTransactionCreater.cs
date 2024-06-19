using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Abstraction.Percistance
{
    public interface IContextTransactionCreater
    {
        Task<IContextTransaction> BeginTransaction(CancellationToken cancellationToken);
    }
}
