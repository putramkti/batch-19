using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniLibrary.Models;

namespace MiniLibrary.Data;

public class LibraryDataContext : IdentityDbContext<ApplicationUser>
{
    public LibraryDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Books)
            .WithMany(b => b.Categories);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Loans)
            .WithOne(l => l.Book)
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Member>()
            .HasMany(m => m.Loans)
            .WithOne(l => l.Member)
            .HasForeignKey(l => l.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
