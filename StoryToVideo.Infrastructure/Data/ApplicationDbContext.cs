using Microsoft.EntityFrameworkCore;
using StoryToVideo.Core.Entities;

namespace StoryToVideo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryImage> StoryImages { get; set; }
        public DbSet<StoryAudio> StoryAudios { get; set; }
        public DbSet<StoryVideo> StoryVideos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
                entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
            });

            // Story configuration
            modelBuilder.Entity<Story>(entity =>
            {
                entity.ToTable("Stories", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Content).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(50);
                
                // Relationships
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Stories)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // StoryImage configuration
            modelBuilder.Entity<StoryImage>(entity =>
            {
                entity.ToTable("StoryImages", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.Order).IsRequired();
                entity.HasOne(e => e.Story)
                      .WithMany(s => s.Images)
                      .HasForeignKey(e => e.StoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // StoryAudio configuration
            modelBuilder.Entity<StoryAudio>(entity =>
            {
                entity.ToTable("StoryAudios", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AudioUrl).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.VoiceCharacter).HasMaxLength(100);
                entity.HasOne(e => e.Story)
                      .WithOne(s => s.Audio)
                      .HasForeignKey<StoryAudio>(e => e.StoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // StoryVideo configuration
            modelBuilder.Entity<StoryVideo>(entity =>
            {
                entity.ToTable("StoryVideos", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.VideoUrl).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.Format).HasMaxLength(50);
                entity.HasOne(e => e.Story)
                      .WithOne(s => s.Video)
                      .HasForeignKey<StoryVideo>(e => e.StoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
} 