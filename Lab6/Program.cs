using System.Text.Json.Serialization;
using Lab6.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Auth0:Domain"];
        options.Audience = builder.Configuration["Auth0:ApiAudience"];
                
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Auth0:Domain"],
            ValidAudience = builder.Configuration["Auth0:ApiAudience"]
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiAccess", policy =>
        policy.RequireClaim("scope", "read:transactions"));
});

switch (builder.Configuration["DB"])
{
    case "Sqlite":
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration["Connection"])
        );
        break;
    case "Postgres":
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration["Connection"])
        );
        break;
    case "InMemory":
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("MemoryDb")
        );
        break;
    case "MSSQL":
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration["Connection"])
        );
        break;
}
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    //context.Seed();
}

app.MapControllers();

app.Run();
