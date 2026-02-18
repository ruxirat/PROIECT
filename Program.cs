using Microsoft.EntityFrameworkCore;
using Ratiu_Ruxandra_Proiect.Data;
using Ratiu_Ruxandra_Proiect.Services;

var builder = WebApplication.CreateBuilder(args);

// =============================
// Add services to the container
// =============================

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<INoShowPredictionService, NoShowPredictionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:51144");
});

builder.Services.AddDbContext<Ratiu_Ruxandra_ProiectContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Ratiu_Ruxandra_ProiectContext")
        ?? throw new InvalidOperationException("Connection string 'Ratiu_Ruxandra_ProiectContext' not found.")
    )
);



var app = builder.Build();

// =====================================
// OPTIONAL - Seed baza de date (Lab2)
// =====================================
// Dacă nu ai DbInitializer, poți șterge acest bloc

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    // DbInitializer.Initialize(services);
}

// =====================================
// Configure the HTTP request pipeline
// =====================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();     // IMPORTANT pentru CSS/Bootstrap
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
