using Microsoft.EntityFrameworkCore;
using social_justice_BE.Models;
using System.Runtime.CompilerServices;

public class socialJusticeDbContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Meetup> Meetups { get; set; }
    public DbSet<Member> Members { get; set; }


    public socialJusticeDbContext(DbContextOptions<socialJusticeDbContext> context) : base(context)
    {

    }
}