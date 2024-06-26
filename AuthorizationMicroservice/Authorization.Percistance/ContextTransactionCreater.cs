using Authorization.Application.Abstraction.Percistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Percistance
{
    public class ContextTransactionCreater : IContextTransactionCreater
    {
        private readonly DockerComposeDemoDbContext _dbContext;
        public ContextTransactionCreater(DockerComposeDemoDbContext context) 
        {
            _dbContext = context;
        }
        public async Task<IContextTransaction> BeginTransaction(CancellationToken cancellationToken)
        {
            return new ContextTransaction(await _dbContext.Database.BeginTransactionAsync(cancellationToken));
        }
    }
}
