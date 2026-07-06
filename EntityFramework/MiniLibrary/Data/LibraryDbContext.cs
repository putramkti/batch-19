using Microsoft.EntityFrameworkCore;
using MiniLibrary.Models;

namespace MiniLibrary.Data;

public class LibraryDbContext : DbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=library.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithMany(b => b.Authors);

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
    