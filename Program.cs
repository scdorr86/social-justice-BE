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
    var userExist = db.Members.Where(member => member.Uid == uid).ToList();
    if (userExist == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(userExist);
});

app.Run();

