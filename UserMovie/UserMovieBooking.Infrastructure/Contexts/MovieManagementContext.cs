using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UserMovieBooking.Infrastructure.DataModels;

namespace UserMovieBooking.Infrastructure.Contexts;

public partial class MovieManagementContext : DbContext
{
    public MovieManagementContext()
    {
    }

    public MovieManagementContext(DbContextOptions<MovieManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieSeatConfiguration> MovieSeatConfigurations { get; set; }

    public virtual DbSet<TheaterConfiguration> TheaterConfigurations { get; set; }    

    public virtual DbSet<UserBookingDatum> UserBookingData { get; set; }

    public virtual DbSet<TicketBookingDatum> TicketBookingData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<UserBookingDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserBookingData_Id");

            entity.Property(e => e.SeatNumbers).HasMaxLength(50);
            entity.Property(e => e.UserEmail).HasMaxLength(1000);
            entity.Property(e => e.UserName).HasMaxLength(1000);
        });

        modelBuilder.Entity<TicketBookingDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TicketBookingData_Id");

            entity.Property(e => e.TicketNumber).HasMaxLength(20);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(20, 2)");
        });

        OnModelCreatingPartial(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
