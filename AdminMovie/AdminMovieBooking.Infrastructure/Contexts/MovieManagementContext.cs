using System;
using System.Collections.Generic;
using AdminMovieBooking.Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AdminMovieBooking.Infrastructure.Contexts;

public partial class MovieManagementContext : DbContext
{
    public MovieManagementContext()
    {
    }

    public MovieManagementContext(DbContextOptions<MovieManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieSeatConfiguration> MovieSeatConfigurations { get; set; }

    public virtual DbSet<TheaterConfiguration> TheaterConfigurations { get; set; }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User_Id");

            entity.ToTable("User");

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Movie_Id");

            entity.ToTable("Movie");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.Place).HasMaxLength(1000);
            entity.Property(e => e.ScheduledDays).HasMaxLength(50);
            entity.Property(e => e.ShowType).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<MovieSeatConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MovieConfiguration_Id");

            entity.ToTable("MovieSeatConfiguration");

            entity.Property(e => e.NonVipSeatCost).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.NonVipSeatTax).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.VipSeatCost).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.VipSeatTax).HasColumnType("decimal(20, 2)");
        });

        modelBuilder.Entity<TheaterConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TheaterConfiguration_Id");

            entity.ToTable("TheaterConfiguration");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
