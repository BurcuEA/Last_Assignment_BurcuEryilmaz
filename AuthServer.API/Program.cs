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
using SharedLibrary.Configurations;
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
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // generic old için typeof ...
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Normal DBContext ...
builder.Services.AddDbContext<AppDbContext>(x =>
{
    //x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOption =>
    //{
    //   // sqlOption.MigrationsAssembly("Last_Assignment.Data");  // yani migrations ý nerde gerçeklþtirmek istiyorsak orda ...  Data katmanýnda...
    //    sqlOption.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); //AppDbContext nerde olduðu , coredeðil data  katmanýnda olduðunu belirtmek lazým ...
    //});

    x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSqlServer"), npgsqloption =>
     {

         npgsqloption.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

     });

});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// üyelik sistemi ile ilgili kýsým....
//Identity Role ekleme ... ÖNEMLÝÝ... // 38. video Start Up 1  13:17 dk sn ... (E) UserApp olduðu class a git ..
builder.Services.AddIdentity<UserApp, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); // // þifre sýfýrlama gibi iþlemlerde  için vs AddDefaultTokenProviders ...

//builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
//builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));


//artýk bu CustomTokenOption ý istediðimiz yerde DI olarak geçebiliriz
//builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>(); builder da oldu xxx 
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

// üyelik sistemi içermeyen ... KARAR (B) ?? !! ... // 31.video -Option Pattern (Client)  (B) yaklaþýk 5. dk ..
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));




// token daðýttýktan sonra token ýn doðrulama iþlemleri ... // API controler ýmýz olacak  herhangi bir endpoint e istek yapýldýðýnda  bir token gelecek  onu doðrulama iþlemini gerçekleþtireceðiz ...
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // kullanýcý için ayrý üyelik, bayi için ayrý üyelik sitemi olabilir.... // LOGIN ... 39.video Startup -2 2:20 dk sn... 

    // Normal þema  ile jwtBearer dan gelen þemamýzýn birbirleriyle konuþturmak lazým ki birbirlerini anlasýnlar ... (AuthenticationScheme).Yani benim Authenticationým bir jsonwebtoken ý ve burdaki AuthenticationScheme yi  kullanacaðýný bilsin...
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  

    // Api olduðundan jwt bazlý doðrulama yapcaz ... // endpointime bir request yapýldýðýnda projem o request in header ýndaki token ý arayacak ...  
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts => 
{
    // Configuration daki TokenOption ý okuduk ve CustomTokenOption ye maplemek..
    var tokenOption = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>(); // appsetting ten "www.authserver.com" yi almak için,yukarda da yapýlmýþtý ...

    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOption.Issuer,  // appsetting ten "www.authserver.com" kontrolünü yapacak ...
        ValidAudience = tokenOption.Audience[0], // "www.authserver.com" sýfýrýncýsý bu //  ValidAudiences ile 1 den fazla da verilebilir ...
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey),

        ValidateIssuerSigningKey = true, // imzasý olmak zorunda  Valid Validate olanlarýn kontrolü,doðrula ...
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero //server lar arasýndaki zaman farkýný sýfýrlamak için ...
    };
});






// Add services to the container. hoca alta aldý... h1

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container. hoca alta aldý... h1






//// Add services to the container.   DEVAMmmmmm

//builder.Services.AddControllers().AddFluentValidation(options =>
//{
//    options.RegisterValidatorsFromAssemblyContaining<Program>();
//});

//builder.Services.UseCustomValidationResponse();


//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

































#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //  42. video UserController 7:23 dk sn ... sýrasý önemli ...
app.UseAuthorization();

app.MapControllers();

app.Run();
