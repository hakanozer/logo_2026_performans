using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Data
{
    // dataxyz
    public class ApplicationDbContext : DbContext
    {
        private static int _activeConnections;

        // Approximates the DB "connection pool" pressure: how many DbContext instances are open right now.
        public static int ActiveConnectionCount => _activeConnections;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Interlocked.Increment(ref _activeConnections);
        }

        public override void Dispose()
        {
            Interlocked.Decrement(ref _activeConnections);
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Interlocked.Decrement(ref _activeConnections);
            return base.DisposeAsync();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}