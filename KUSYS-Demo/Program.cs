using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EntityContext>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<EntityContext>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<EntityContext>();
builder.Services.AddControllersWithViews();
builder.Services.ContainerDependencies();
builder.Services.AddControllersWithViews().AddNToastNotifyNoty(new NotyOptions(){ProgressBar = true,Timeout = 5000,Theme = "mint"});
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.ConfigureApplicationCookie(options =>{
    options.LoginPath = "/Login/SignIn";
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();
app.UseNToastNotify();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=GetListStudentandCourses}/{id?}");

app.Run();
