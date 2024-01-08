﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models.Person;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Role)
                .WithMany(r => r.People)
                .HasForeignKey(p => p.ROLE_ID);

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
