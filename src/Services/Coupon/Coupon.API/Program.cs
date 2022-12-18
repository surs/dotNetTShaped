using Microsoft.AspNetCore.Mvc;
using Repositories = Coupon.API.Repositories;
using Dtos = Coupon.API.Dtos;
using Models = Coupon.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RegisterDependencies(builder.Services);

void RegisterDependencies(IServiceCollection services)
{
    services.AddSingleton<Repositories.ICouponRepository, Repositories.CouponRepository>();
    services.AddSingleton<Dtos.IMapper<Dtos.CouponDto, Models.Coupon>, Dtos.Mapper>();
    services.AddSingleton<Repositories.CouponContext>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/v1/coupon/{code}", async (
    [FromRoute] string code,
    Repositories.ICouponRepository couponRepository,
    Dtos.IMapper<Dtos.CouponDto, Models.Coupon> mapper) =>
{
    var coupon = await couponRepository.FindCouponByCodeAsync(code);

    if (coupon is null || coupon.Consumed)
    {
        return Results.NotFound(code);
    }

    var couponDto = mapper.Translate(coupon);

    return Results.Ok(couponDto);
})
.WithName("GetCouponByCodeAsync");

app.Run();
