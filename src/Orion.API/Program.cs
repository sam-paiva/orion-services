using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Orion.API.OData;
using Orion.CrossCutting.IoC;
using SimpleInjector;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("", ODataHelper.GetEdmModel()).Filter().Select().Count().Expand().SetMaxTop(100));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("TokenSecret"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
     .AddJwtBearer(x =>
     {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = false,
             ValidateAudience = false
         };
     });

var _container = builder.Services.RegisterDependencies(builder.Configuration);
builder.Services.AddSimpleInjector(_container, x => { x.AddAspNetCore().AddControllerActivation(); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration.GetValue<string>("FrontEndpoint")));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.Run();
