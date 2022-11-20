using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;

using KafkaCDC.Deals;
using KafkaCDC.Deals.Comands;
using KafkaCDC.Deals.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(typeof(AddDealCommand).GetTypeInfo().Assembly);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddDbContext<DealDbContext>(options => options
         .UseNpgsql(builder.Configuration.GetConnectionString("DealConnection"))
     );
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSeq(serverUrl: builder.Configuration.GetValue<string>("SeqUrl")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<SetUpHostedService>();
builder.Services.AddSwaggerGen(opt => { });
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
