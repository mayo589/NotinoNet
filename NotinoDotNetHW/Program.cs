using Microsoft.AspNetCore.Mvc;
using NotinoDotNetHW.Data;
using NotinoDotNetHW.OutputFormatters;
using NotinoDotNetHW.Repositories;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DocumentsDb>();

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.Filters.Add(
        new ProducesAttribute(
            "text/json",
            "application/json",
            "application/msgpack",
            "application/x-msgpack",
            "application/xml",
            "application/yaml"
            // Additional input formatters
            )
        );

    options.OutputFormatters.Add(new YamlOutputFormatter((Serializer)new SerializerBuilder().WithNamingConvention(namingConvention: new CamelCaseNamingConvention()).Build()));
    options.OutputFormatters.Add(new MessagePackOutputFormatter());
    // Additional output formatters

})
    .AddXmlSerializerFormatters()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DocumentsRepository>();

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
