using CustomerOrders.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("CustomerOrdersAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/api/");
});
builder.Services.AddScoped<CustomerService>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();

app.UseRouting();

app.UseAuthorization();

app.Run();