﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequireDigit = true;
	opt.Password.RequiredLength = 6;
	opt.Lockout.AllowedForNewUsers = false;
	opt.User.AllowedUserNameCharacters = "aAbBcCdDeEəƏIğĞüÜöÖçÇşŞfFgGhHiİjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ1234567890";
	opt.User.RequireUniqueEmail = true;
}).AddDefaultTokenProviders().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<AppDBContext>();

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
app.UseAuthentication();


app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}"
	);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();
