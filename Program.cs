using Microsoft.EntityFrameworkCore;
using PaymentApi.Data;
using PaymentApi.Data.Seeders;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tambahkan Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Payment API",
        Version = "v1",
        Description = "API untuk manajemen pembayaran"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
});

// Tambahkan controllers
builder.Services.AddControllers();


//validation request
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var firstError = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault() ?? "Validation error";

            var apiResponse = new
            {
                meta = new
                {
                    success = false,
                    code = 42200,
                    message = firstError
                },
                data = (object?)null
            };

            return new BadRequestObjectResult(apiResponse);
        };
    });

//register filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<RecursiveValidationFilter>();
});


var app = builder.Build();

// Jalankan seeder
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DatabaseSeeder.Seed(dbContext);
}

// Aktifkan Swagger di development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // Sebelum UseRouting()/MapControllers()
    app.UseSwaggerUI();
    app.MapControllers();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment API V1");
    });
}

//middleware
app.UseMiddleware<ApiExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();