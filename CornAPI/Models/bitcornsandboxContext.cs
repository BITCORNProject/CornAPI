using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CornAPI.Models
{
    public partial class bitcornsandboxContext : DbContext
    {
        public bitcornsandboxContext()
        {
        }

        public bitcornsandboxContext(DbContextOptions<bitcornsandboxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ErrorLogs> ErrorLogs { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserIdentity> UserIdentity { get; set; }
        public virtual DbSet<UserStat> UserStat { get; set; }
        public virtual DbSet<UserWallet> UserWallet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ErrorLogs>(entity =>
            {
                entity.Property(e => e.Application)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.StackTrace)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .HasColumnName("TImestamp")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Avatar)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.Level)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserIdentity>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tmp_ms_x__1788CC4C32CEDA3C");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Auth0Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Auth0Nickname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Discordid)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Redditid)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TwitchUsername)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Twitchid)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Twitterid)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserIdentity)
                    .HasForeignKey<UserIdentity>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserIdentity_User");
            });

            modelBuilder.Entity<UserStat>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserStat__3214EC0730174CB5");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.RainTotal).HasColumnType("numeric(19, 8)");

                entity.Property(e => e.RainedOnTotal).HasColumnType("numeric(19, 8)");

                entity.Property(e => e.TipTotal).HasColumnType("numeric(19, 8)");

                entity.Property(e => e.TippedTotal)
                    .HasColumnName("TIppedTotal")
                    .HasColumnType("numeric(19, 8)");

                entity.Property(e => e.TopRain).HasColumnType("numeric(19, 8)");

                entity.Property(e => e.TopRainedOn).HasColumnType("numeric(19, 8)");

                entity.Property(e => e.TopTip).HasColumnType("numeric(19, 8)");

                entity.Property(e => e.TopTiped).HasColumnType("numeric(19, 8)");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserStat)
                    .HasForeignKey<UserStat>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserStat_Users");
            });

            modelBuilder.Entity<UserWallet>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserWall__1788CC4C85ECFC41");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Balance).HasColumnType("numeric(19, 8)");

                entity.Property(e => e.CornAddy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserWallet)
                    .HasForeignKey<UserWallet>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserWallet_User");
            });
        }
    }
}
