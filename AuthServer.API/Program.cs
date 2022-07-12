using FluentValidation.AspNetCore;
using Last_Assignment.Core.Configuration;
using Last_Assignment.Core.Models;
using Last_Assignment.Core.Repositories;
using Last_Assignment.Core.Services;
using Last_Assignment.Core.UnitOfWork;
using Last_Assignment.Data;
using Last_Assignment.Data.Repository;
using Last_Assignment.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using SharedLibrary.Configurations;
using SharedLibrary.Extensions;
using SharedLibrary.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.

////builder.Services.AddControllers();
////// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();


#region BERY


//DI Register // 38. video Start Up 1 

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // generic old i�in typeof ...
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

builder.Services.AddScoped(typeof(ICustomerActivityRepository), typeof(CustomerActivityRepository));
builder.Services.AddScoped(typeof(ICustomerActivityService), typeof(CustomerActivityService));

builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(ICustomerService), typeof(CustomerService));


builder.Services.AddScoped(typeof(IExcelDtoRepository), typeof(ExcelDtoRepository));
builder.Services.AddScoped(typeof(IExcelDtoService), typeof(ExcelDtoService));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Normal DBContext ...
builder.Services.AddDbContext<AppDbContext>(x =>
{
    //x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOption =>
    //{
    //   // sqlOption.MigrationsAssembly("Last_Assignment.Data");  // yani migrations � nerde ger�ekl�tirmek istiyorsak orda ...  Data katman�nda...
    //    sqlOption.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); //AppDbContext nerde oldu�u , corede�il data  katman�nda oldu�unu belirtmek laz�m ...
    //});

    x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSqlServer"), npgsqloption =>
     {

         npgsqloption.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

     });

});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// �yelik sistemi ile ilgili k�s�m....
//Identity Role ekleme ... �NEML��... // 38. video Start Up 1  13:17 dk sn ... (E) UserApp oldu�u class a git ..
builder.Services.AddIdentity<UserApp, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); // // �ifre s�f�rlama gibi i�lemlerde  i�in vs AddDefaultTokenProviders ...

//builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
//builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));


//art�k bu CustomTokenOption � istedi�imiz yerde DI olarak ge�ebiliriz
//builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>(); builder da oldu xxx 
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

// �yelik sistemi i�ermeyen ... KARAR (B) ?? !! ... // 31.video -Option Pattern (Client)  (B) yakla��k 5. dk ..
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));




// token da��tt�ktan sonra token �n do�rulama i�lemleri ... // API controler �m�z olacak  herhangi bir endpoint e istek yap�ld���nda  bir token gelecek  onu do�rulama i�lemini ger�ekle�tirece�iz ...
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // kullan�c� i�in ayr� �yelik, bayi i�in ayr� �yelik sitemi olabilir.... // LOGIN ... 39.video Startup -2 2:20 dk sn... 

    // Normal �ema��ile jwtBearer dan gelen �emam�z�n birbirleriyle konu�turmak laz�m ki birbirlerini anlas�nlar ... (AuthenticationScheme).Yani benim Authentication�m bir jsonwebtoken � ve burdaki AuthenticationScheme yi  kullanaca��n� bilsin...
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  

    // Api oldu�undan jwt bazl� do�rulama yapcaz ... // endpointime bir request yap�ld���nda projem o request in header �ndaki token � arayacak ...  
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts => 
{
    // Configuration daki TokenOption � okuduk ve CustomTokenOption ye maplemek..
    var tokenOption = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>(); // appsetting ten "www.authserver.com" yi almak i�in,yukarda da yap�lm��t� ...

    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOption.Issuer,  // appsetting ten "www.authserver.com" kontrol�n� yapacak ...
        ValidAudience = tokenOption.Audience[0], // "www.authserver.com" s�f�r�nc�s� bu //  ValidAudiences ile 1 den fazla da verilebilir ...
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey),

        ValidateIssuerSigningKey = true, // imzas� olmak zorunda  Valid Validate olanlar�n kontrol�,do�rula ...
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero //server lar aras�ndaki zaman fark�n� s�f�rlamak i�in ...
    };
});






// Add services to the container. hoca alta ald�... h1

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Add services to the container. hoca alta ald�... h1






//// Add services to the container.   DEVAMmmmmm

builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<Program>();
});

builder.Services.UseCustomValidationResponse();


//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();





// yeni ekledim  -- 51. video 10:54 te g�r�n�yor
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1",new OpenApiInfo { Title= "AuthServer.API", Version="v1"});

});








#endregion

builder.Services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

builder.Services.AddSingleton<RabbitMQPublisher>();
builder.Services.AddSingleton<RabbitMQClientService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // 52.video 13:13 te g�r�n�yor
    
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthServer.API v1")); // 52.video 13:13 te g�r�n�yor
}

app.UseCustomException();

app.UseHttpsRedirection();

app.UseAuthentication(); //  42. video UserController 7:23 dk sn ... s�ras� �nemli ...
app.UseAuthorization();

app.MapControllers();

app.Run();
