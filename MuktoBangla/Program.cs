using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using MuktoBangla.Data;
using MuktoBangla.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<MuktoBanglaDbContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("MuktoBanglaConnectionString")));

//Cloudinary cloudinary = new Cloudinary("cloudinary://<763754599679497:<4cO_kTNvl5mz-maBYYkaFFd5myk>");

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF5cWWtCf1FpQnxbf1x0ZFxMY11bRXFPIiBoS35RckRiW35cc3ZTRWJdVEV2");


builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRapository, CloudinaryImageRepository>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
