using Microsoft.EntityFrameworkCore;

namespace SnakeSample.Models
{
    public class StateContext : DbContext
    {
        public StateContext(DbContextOptions<StateContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>(builder =>
            {
                builder.HasKey(x => x.GameID);

                builder.OwnsOne(x => x.Fruit);
                builder.OwnsOne(x => x.Snake);
                builder.Navigation(x => x.Fruit).IsRequired();
                builder.Navigation(x => x.Snake).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<State> States { get; set; } = null!;
        public DbSet<Snake> Snakes { get; set; } = null!;
    }
}
