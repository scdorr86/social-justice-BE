using social_justice_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7040")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<socialJusticeDbContext>(builder.Configuration["socialJusticeDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

//Add for Cors 
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//MEMBER API CALLS
app.MapGet("/members", (socialJusticeDbContext db) =>
{
    return db.Members.ToList();
});

app.MapGet("/members/{id}", (socialJusticeDbContext db, int id) =>
{
    return db.Members.Single(member => member.Id == id);
});

app.MapPost("/members", (socialJusticeDbContext db, Member member) =>
{
    try
    {
        db.Members.Add(member);
        db.SaveChanges();
        return Results.Created($"members/{member.Id}", member);
    }
    catch (DbUpdateException)
    {
        return Results.NotFound();
    }
});

app.MapPut("/members/{id}", (socialJusticeDbContext db, int id, Member member) =>
{
    Member memberToUpdate = db.Members.SingleOrDefault(member => member.Id == id);
    if (memberToUpdate == null)
    {
        return Results.NotFound();
    }
    memberToUpdate.FirstName = member.FirstName;
    memberToUpdate.LastName = member.LastName;
    memberToUpdate.Email = member.Email;
    memberToUpdate.Phone = member.Phone;
    memberToUpdate.ImageUrl = member.ImageUrl;

    db.Update(memberToUpdate);
    db.SaveChanges();
    return Results.Ok(memberToUpdate);
});

app.MapDelete("/members/{id}", (socialJusticeDbContext db, int id) =>
{
    Member member = db.Members.SingleOrDefault(member => member.Id == id);
    if (member == null)
    {
        return Results.NotFound();
    }
    db.Members.Remove(member);
    db.SaveChanges();
    return Results.NoContent();
});

//USER APIs
app.MapGet("/checkuser/{uid}", (socialJusticeDbContext db, string uid) =>
{
    var userExist = db.Members.Where(member => member.Uid == uid).FirstOrDefault();
    if (userExist == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(userExist);
});

// MEETUP ENDPOINTS

// Create Meetup:
app.MapPost("/meetup", (socialJusticeDbContext db, Meetup meetupPayload) =>
{
    Meetup NewMeetup = new Meetup()
    {
        Title = meetupPayload.Title,
        Description = meetupPayload.Description,
        ImageUrl = meetupPayload.ImageUrl,
        MeetTime = meetupPayload.MeetTime,
        Location = meetupPayload.Location,
        OrganizationId = meetupPayload.OrganizationId,
    };

    db.Meetups.Add(NewMeetup);
    db.SaveChanges();
    return Results.Created($"/meetup/{NewMeetup.Id}", NewMeetup);
});

//Get All Meetups:
app.MapGet("/meetups", (socialJusticeDbContext db) =>
{
    return db.Meetups.Include(m => m.Members)
                   .Include(m => m.Organization)
                   .ToList();
});

// Get Meetup by Id
app.MapGet("/meetups/{id}", (socialJusticeDbContext db, int id) =>
{
    return db.Meetups.Where(m => m.Id == id)
                   .Include(m => m.Members)
                   .Include(m => m.Organization)
                   .FirstOrDefault();
});

// Delete Meetup by id
app.MapDelete("/meetups/{id}", (socialJusticeDbContext db, int id) =>
{
    Meetup meetupToDelete = db.Meetups.SingleOrDefault(m => m.Id == id);
    if (meetupToDelete == null)
    {
        return Results.NotFound();
    }
    db.Meetups.Remove(meetupToDelete);
    db.SaveChanges();
    return Results.Ok(db.Meetups);
});

// Update Meetup by id
app.MapPut("/updateMeetup/{id}", (socialJusticeDbContext db, int id, Meetup meetup) =>
{
    Meetup meetupToUpdate = db.Meetups.Include(m => m.Members)
                                      .Include(m => m.Organization)
                                      .SingleOrDefault(m => m.Id == id);
    if (meetupToUpdate == null)
    {
        return Results.NotFound();
    }

    meetupToUpdate.Title = meetup.Title;
    meetupToUpdate.Description = meetup.Description;
    meetupToUpdate.ImageUrl = meetup.ImageUrl;
    meetupToUpdate.MeetTime = meetup.MeetTime;
    meetupToUpdate.Location = meetup.Location;
    meetupToUpdate.OrganizationId = meetup.OrganizationId;

    db.SaveChanges();
    return Results.Ok(meetupToUpdate);
});

// Add Member to Meetup
app.MapPost("/meetup/{meetupId}/member/{memberId}", (socialJusticeDbContext db, int meetupId, int memberId) =>
{
    var meetup = db.Meetups.Include(m => m.Members)
                         .FirstOrDefault(m => m.Id == meetupId);
    if (meetup == null)
    {
        return Results.NotFound("Meetup not found");
    }

    var memberToAdd = db.Members?.Find(memberId);


    if (memberToAdd == null)
    {
        return Results.NotFound("Member not found");
    }

    meetup?.Members?.Add(memberToAdd);
    db.SaveChanges();
    return Results.Ok(meetup);
});

// Delete Member from Meetup
app.MapDelete("/meetup/{meetupId}/removemember/{memberId}", (socialJusticeDbContext db, int meetupId, int memberId) =>
{
    var meetup = db.Meetups.Include(m => m.Members)
                         .FirstOrDefault(m => m.Id == meetupId);
    if (meetup == null)
    {
        return Results.NotFound("Meetup not found");
    }

    var memberToDelete = db.Members?.Find(memberId);


    if (memberToDelete == null)
    {
        return Results.NotFound("Member not found");
    }

    meetup?.Members?.Remove(memberToDelete);
    db.SaveChanges();
    return Results.Ok(meetup);
});

// ORGANIZATION FETCHES:

//Get All Orgs (for stretch):
app.MapGet("/organizations", (socialJusticeDbContext db) =>
{
    return db.Organizations.Include(o => o.Members)
                   .Include(o => o.Meetups)
                   .ToList();
});

// Get Org by Id (for FE main org display properties
app.MapGet("/organization/{id}", (socialJusticeDbContext db, int id) =>
{
    return db.Organizations.Where(o => o.Id == id)
                   .Include(o => o.Members)
                   .Include(o => o.Meetups)
                   .FirstOrDefault();
});

app.Run();

