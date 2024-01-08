using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
