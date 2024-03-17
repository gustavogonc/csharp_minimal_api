﻿using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {

    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(500).IsRequired(false);
        modelBuilder.Entity<Product>()
            .Property(p => p.Name).HasMaxLength(120).IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Code).HasMaxLength(20).IsRequired();
        modelBuilder.Entity<Category>()
            .ToTable("Categories");
    }
}
