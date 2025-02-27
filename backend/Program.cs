
namespace orderApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.Configure<StaticUserOption>(builder.Configuration.GetSection("StaticUser"));

            builder.Services.AddCors(setupAction =>
            {
                setupAction.AddPolicy("AllowedOrgins", policy =>
                {
                    policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedOrigin"))
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowedOrgins");

            app.MapControllers();

            app.Run();
        }
    }
}
