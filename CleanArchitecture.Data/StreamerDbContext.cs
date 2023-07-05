using CleanArchitecture.Domian;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SQL8001.site4now.net;
                                          Initial Catalog=db_a9a3f4_streamer;
                                          User Id=db_a9a3f4_streamer_admin;
                                          Password=Alejo1031*"
                                        ).LogTo(Console.WriteLine , new[] { DbLoggerCategory.Database.Command.Name } , Microsoft.Extensions.Logging.LogLevel.Information)
                                        .EnableSensitiveDataLogging();
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Video>()
                .HasMany(P => P.Actores)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>
                (
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );



        }

        


        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }

        public DbSet<Actor>? Actores { get; set; }

        public DbSet<Director>? Directores { get; set; }

    }
}
