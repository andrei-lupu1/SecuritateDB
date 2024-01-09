using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models.Person;
using Models.Users;
using Models.Vehicles;
using Repository.Interfaces;

namespace Repository
{
    public class Context : DbContext, IUnitOfWork
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Person> Person { get; set; }

        public DbSet<Roles> Role { get; set; }

        public DbSet<Vehicles> Vehicles { get; set; }

        public DbSet<PersonVehicle> PersonVehicle { get; set; }

        private IDbContextTransaction _transaction;

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Role)
                .WithMany(r => r.People)
                .HasForeignKey(p => p.ROLE_ID);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<Person>(p => p.USER_ID);

            base.OnModelCreating(modelBuilder);
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
