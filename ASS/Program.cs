using ASS.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Config connect to database

// Config AutoMapper



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//ApplyMigration();

app.MapControllers();

app.Run();

//void ApplyMigration()
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//        if (_db.Database.GetMigrations().Count() > 0)
//        {
//            _db.Database.Migrate();
//        }

//    }
//}
