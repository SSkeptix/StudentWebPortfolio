using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, long>
    {
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var dateTime = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<IUpdatableEntity>()
                    .Where(_ => _.State == EntityState.Added || _.State == EntityState.Modified))
                entry.Entity.UpdatedOnUtc = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(_ => _.Id);

                entity.Property(_ => _.FirstName).HasMaxLength(64);
                entity.Property(_ => _.LastName).HasMaxLength(64);

                entity.HasOne(_ => _.ValidatedByUser)
                    .WithMany(_ => _.ValidatedUsers)
                    .HasForeignKey(_ => _.ValidatedByUserId)
                    .HasConstraintName("FK_dbo.AspNetUsers_ValidatedByUserId__dbo.AspNetUsers_Id")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skills", "dbo");
                entity.HasKey(_ => _.SkillId);

                entity.Property(_ => _.UpdatedOnUtc).HasColumnType("datetime");
                entity.Property(_ => _.Name).HasMaxLength(64);

                entity.HasOne(_ => _.ValidatedByUser)
                    .WithMany(_ => _.ValidatedSkills)
                    .HasForeignKey(_ => _.ValidatedByUserId)
                    .HasConstraintName("FK_dbo.Skills_ValidatedByUserId__dbo.AspNetUsers_Id")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserSkill>(entity =>
            {
                entity.ToTable("UserSkills", "dbo");
                entity.HasKey(_ => _.UserSkillId);

                entity.Property(_ => _.UpdatedOnUtc).HasColumnType("datetime");

                entity.HasIndex(_ => _.UserId)
                    .HasName("IX_UserId");
                entity.HasIndex(_ => _.SkillId)
                    .HasName("IX_SkillId");

                entity.HasOne(_ => _.User)
                    .WithMany(_ => _.UserSkills)
                    .HasForeignKey(_ => _.UserId)
                    .HasConstraintName("FK_dbo.UserSkills_UserId__dbo.AspNetUsers_Id")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(_ => _.Skill)
                    .WithMany(_ => _.UserSkills)
                    .HasForeignKey(_ => _.SkillId)
                    .HasConstraintName("FK_dbo.UserSkills_UserId__dbo.Skills_SkillId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(_ => _.ValidatedByUser)
                    .WithMany(_ => _.ValidatedUserSkills)
                    .HasForeignKey(_ => _.ValidatedByUserId)
                    .HasConstraintName("FK_dbo.UserSkills_ValidatedByUserId__dbo.AspNetUsers_Id")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.ToTable("Portfolios", "dbo");
                entity.HasKey(_ => _.UserId);

                entity.Property(_ => _.GitHubUrl).HasMaxLength(64);
                entity.Property(_ => _.Group).HasMaxLength(64);
                entity.Property(_ => _.English).HasColumnType("tinyint");
                entity.Property(_ => _.Description).HasMaxLength(64);

                entity.HasIndex(_ => _.English)
                    .HasName("IX_English");

                entity.HasOne(_ => _.User)
                    .WithMany(_ => _.Portfolios)
                    .HasForeignKey(_ => _.UserId)
                    .HasConstraintName("FK_dbo.Portfolios_UserId__dbo.AspNetUsers_Id")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
