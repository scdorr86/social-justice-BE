using Microsoft.EntityFrameworkCore;
using social_justice_BE.Models;
using System.Runtime.CompilerServices;

public class socialJusticeDbContext : DbContext
{



    public socialJusticeDbContext(DbContextOptions<socialJusticeDbContext> context) : base(context)
    {

    }
}