using ApiCamisa10.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCamisa10.DB
{
    public partial class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>(entity =>
            {
                entity.Property(e => e.email)
                       .IsRequired()
                       .HasMaxLength(45);

                entity.Property(e => e.name)
                        .IsRequired()
                        .HasMaxLength(45);

                entity.Property(e => e.password)
                        .IsRequired()
                        .HasMaxLength(45);
                
            });

            mb.Entity<Level>(entity =>
            {
                entity.Property(e => e.name)
                        .IsRequired()
                        .HasMaxLength(25);
            });

            mb.Entity<Position>(entity =>
            {
                entity.Property(e => e.name)
                        .IsRequired()
                        .HasMaxLength(25);
            });

            mb.Entity<Team>(entity =>
            {
                entity.Property(e => e.dayofweak)
                                .IsRequired()
                                .HasMaxLength(25);

                entity.Property(e => e.hour)
                                .IsRequired()
                                .HasMaxLength(25);

                entity.HasOne(t => t.user)
                                .WithMany(u=>u.teams)
                                .HasForeignKey(u=>u.IdUser);
                    

            });

            mb.Entity<Player>(entity =>
            {
                entity.Property(p => p.name)
                                .IsRequired()
                                .HasMaxLength(25);
                            

                entity.HasOne(p => p.team)
                       .WithMany(t => t.players)
                       .HasForeignKey(p => p.idTeam);

                entity.HasOne(p => p.level)
                        .WithMany(l => l.players)
                        .HasForeignKey(p => p.idLevel);


                entity.HasOne(p => p.position)
                            .WithMany(p => p.players)
                            .HasForeignKey(p => p.idPosition);

            });
            

            base.OnModelCreating(mb);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Player> Players { get; set; }
       
    }
}
