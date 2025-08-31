using App.Repositories.Extensions;
using App.Services;
using App.Services.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // Referans tiplerin kontrol�n� FluentValidation'da yapt���m�z i�in compiler'a kendisi ek olarak yapmamas� i�in true atanm��t�r.
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Repository ConnectionStrings'lerin ba�lant�s� ve Service Extensionslar�n�n eklenmesi.
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);


WebApplication app = builder.Build();

app.UseExceptionHandler(x => { });

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
