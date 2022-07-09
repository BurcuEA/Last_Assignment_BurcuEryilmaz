using Last_Assignment.Core.Configuration;
using SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region BERY


//art�k bu CustomTokenOption � istedi�imiz yerde DI olarak ge�ebiliriz
//builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>(); builder da oldu xxx 
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

// �yelik sistemi i�ermeyen ... KARAR (B) ?? !! ... // 31.video -Option Pattern (Client)  (B) yakla��k 5. dk ..
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));






















#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
