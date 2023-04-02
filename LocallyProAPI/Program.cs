using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Repositories.Context;
using Repositories.Models;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDBContext>();

/*
 builder.Services.AddAuthentication("Default").AddJwtBearer("MyBearer")
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
                    .EnableTokenAcquisitionToCallDownstreamApi()
                        .AddInMemoryTokenCaches();
*/
builder.Services.AddAuthentication().AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
                        .EnableTokenAcquisitionToCallDownstreamApi()
                        .AddInMemoryTokenCaches();
//builder.Services.AddMvcCore().AddAuthorization();





/*
builder.Services
    .AddAuthentication(builder.Configuration.GetRequiredSection("Authentication")
        .GetValue<string>("DefaultScheme")!)
        .AddJwtBearer("MyTest", options =>
        {
            options.Authority = builder.Configuration["AzureAd:Instance"] + builder.Configuration["AzureAd:TenantId"];
            options.Audience = builder.Configuration["AzureAd:ClientId"];
            options.TokenValidationParameters.ValidateIssuer = false;
        })
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddDistributedTokenCaches();



/*
 * .AddJwtBearer(
    "FusionAuth",
    options => options.TokenValidationParameters.NameClaimType = "preferred_username")
.AddJwtBearer("UserJwts");


/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["AzureAd:Instance"] + builder.Configuration["AzureAd:TenantId"];
    options.Audience = builder.Configuration["AzureAd:ClientId"];
    options.TokenValidationParameters.ValidateIssuer = false;
})
.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
.EnableTokenAcquisitionToCallDownstreamApi()
.AddDistributedTokenCaches();
*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

/*app.UseRouting();*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
