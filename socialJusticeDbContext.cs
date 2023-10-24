using Microsoft.EntityFrameworkCore;
using social_justice_BE.Models;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;

public class socialJusticeDbContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Meetup> Meetups { get; set; }
    public DbSet<Member> Members { get; set; }


    public socialJusticeDbContext(DbContextOptions<socialJusticeDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>().HasData(new Organization[]
        {
            new Organization { Id = 1, Name = "OrgSeed1", Mission = "mission 1" },
        });

        modelBuilder.Entity<Meetup>().HasData(new Meetup[]
        {
            new Meetup { 
                Id = 1,
                Title="meetup 1",
                Description="meetup 1 desc",
                ImageUrl="https://assets.hvmag.com/2023/09/pumpkin-world-AdobeStock_626050040.jpg",
                Location="location 1",
                MeetTime=new DateTime(2023, 10, 31),
                OrganizationId=1,
            },
            new Meetup {
                Id = 2,
                Title="meetup 2",
                Description="meetup 2 desc",
                ImageUrl="https://assets.hvmag.com/2023/09/pumpkin-world-AdobeStock_626050040.jpg",
                Location="location 2",
                MeetTime=new DateTime(2023, 10, 31),
                OrganizationId=1,
            },
        });

        modelBuilder.Entity<Member>().HasData(new Member[]
        {
            new Member {
                Id = 1,
                FirstName="Test", 
                LastName="user 1", 
                Email="user1@gmail.com", 
                Phone="123-456-7890",
                ImageUrl="https://i0.wp.com/theverybesttop10.com/wp-content/uploads/2015/03/Top-10-Dogs-With-Funny-Things-In-Their-Mouth-8-510x700.jpg?resize=600%2C824",
                Uid="uid1",
                OrganizationId=1,
            },
            new Member {
                Id = 2,
                FirstName="Test",
                LastName="user 2",
                Email="user2@gmail.com",
                Phone="123-456-7890",
                Uid="uid2",
                ImageUrl="https://joyrideharness.com/cdn/shop/articles/AdobeStock_274099078.jpg?v=1620400547",
                OrganizationId=1,
            },
            new Member {
                Id = 3,
                FirstName="Test",
                LastName="user 3",
                Email="user3@gmail.com",
                Phone="123-456-7890",
                ImageUrl="https://hips.hearstapps.com/hmg-prod/images/dog-puns-1581708208.jpg",
                Uid="uid3",
                OrganizationId=1,
            },

        });
    }
}