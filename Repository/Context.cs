using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models.Catalogs;
using Models.Orders;
using Models.Person;
using Models.Users;
using Models.Vehicles;
using Repository.Interfaces;

namespace Repository
{
    public class Context : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<PersonVehicle> PersonsVehicles { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<HistoryOrder> HistoryOrders { get; set; }

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
            modelBuilder.Entity<City>()
                .HasOne(c => c.Courier);

            modelBuilder.Entity<Address>()
                .HasOne(c => c.City);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Courier);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.HistoryOrders);

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
