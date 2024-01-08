using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models.Users;
using Repository.Interfaces;

namespace Repository
{
    public class Context : DbContext, IUnitOfWork
    {
        public DbSet<Users> Users { get; set; }

        private IDbContextTransaction _transaction;

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
               _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        public int Save()
        {
            return SaveChanges();
        }
    }
}
