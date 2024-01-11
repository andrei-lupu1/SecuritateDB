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
