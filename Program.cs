using MyMvcApp.IService;
using MyMvcApp.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IEmployeeService, EmployeeApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5149"); // Replace with your API URL
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddControllersWithViews();

// Add services to the container.
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

// app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
