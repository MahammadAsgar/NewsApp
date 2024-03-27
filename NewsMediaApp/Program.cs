using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using NewsMedia.Application;
using NewsMedia.Infrastructure;
using NewsMedia.Persistance;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistanceServices();

//builder.Services.AddAuthentication()
//      .AddJwtBearer()
//      .AddCookie();
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
    options.KeyLengthLimit = int.MaxValue;
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = 1000000; // Limit to 1 MB

});
builder.Services.AddAuthentication()
    .AddJwtBearer(jwt =>
    {
        jwt.RequireHttpsMetadata = true;
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issure"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        };
    });
// .AddCookie();

builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
