using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using TransitionApp.Data;
using TransitionApp.Services;
using TransitionApp.ViewModel;
using TransitionApp.View;

namespace TransitionApp
{
    public static class MauiProgram
    {

        public static IServiceProvider ServiceProvider { get; private set; }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Add DbContext with SQL Server connection string
            builder.Services.AddDbContext<TransitionContext>(options =>
            options.UseSqlite($"Filename={System.IO.Path.Combine(FileSystem.AppDataDirectory, "TapDB.db")}")
);

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<TaskService>(); // Task service for CRUD operations
            builder.Services.AddScoped<TaskViewModel>();  // ViewModel for TaskPage
            builder.Services.AddTransient<TaskPage>();   // Register TaskPage
            builder.Services.AddScoped<TaskTemplateService>();

            // Automatically apply migrations
            try
            {
                using (var scope = builder.Services.BuildServiceProvider().CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TransitionContext>();
                    dbContext.Database.EnsureCreated();
                    Console.WriteLine("Database ensured/created successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during startup: {ex.Message}");
            }


#if DEBUG
            builder.Logging.AddDebug();

#endif


            // Build and store the service provider
            var app = builder.Build();
            ServiceProvider = app.Services; // Store the built service provider

            return app;
        }
    }
}
