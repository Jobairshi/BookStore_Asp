
using course.Repository;
using course.Repository.Irepository;
using courseAnotherPartDataAccess;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using CourseUtilty;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<database>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<database>();
builder.Services.AddDbContext<database>(options => options.UseSqlServer(
     builder.Configuration.GetConnectionString("DefaultConnection")
     ));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
//builder.Services.AddDbContext<ProductData>(options => options.UseSqlServer(
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddDefaultTokenProviders()
.AddEntityFrameworkStores<database>();//     b => b.MigrationsAssembly("courseAnotherPartDataAccess")));
//builder.Services.AddScoped<ICategoryRespository, CategoryRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWorkCover, UnitOfWorkCover>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
//builder.Services.AddScoped<IUnitOfWorkProduct, UnitOfWorkProduct>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();



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
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages(); // to go to razor pages in mvc 
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
