using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Retrospective.Domain.AggregatesModel.SprintAggregate;
using Retrospective.Domain.SeedWork;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Retrospective.Infrastructure.Persistence
{
    public class SprintContext : DbContext, IUnitOfWork
    {
        public SprintContext(DbContextOptions<SprintContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public const string DEFAULT_SCHEMA = "Retrospective";

        public DbSet<Sprint> Sprints { get; set; }

        public DbSet<SprintItem> SprintItems { get; set; }

        public DbSet<Item> Items { get; set; }

        private IDbContextTransaction dbContextTransaction;

        public IDbContextTransaction GetDbContextTransaction() => dbContextTransaction;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (dbContextTransaction != null) return null;

            dbContextTransaction = await Database.BeginTransactionAsync();

            return dbContextTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                    dbContextTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                dbContextTransaction?.Rollback();
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                    dbContextTransaction = null;
                }
            }
        }
    }
}
