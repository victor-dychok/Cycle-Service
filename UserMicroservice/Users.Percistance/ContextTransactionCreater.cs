using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstraction.Percistance;

namespace Users.Percistance
{
    public class ContextTransactionCreater : IContextTransactionCreater
    {
        private readonly AppDBContext _dbContext;
        public ContextTransactionCreater(AppDBContext context) 
        {
            _dbContext = context;
        }
        public async Task<IContextTransaction> BeginTransaction(CancellationToken cancellationToken)
        {
            return new ContextTransaction(await _dbContext.Database.BeginTransactionAsync(cancellationToken));
        }
    }
}
