using FluentValidation.AspNetCore;
using Costs.Application.Common.Exceptions;
using Costs.Application.CostCategoriesApplication.Commands.CreateCostCategory;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Costs.Api
{
    public static class ServiceRegistery
    {
        public static IServiceCollection AddServiceRegistery(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                })
                //This feature has been disabled so that we can display the desired error in
                //Application layer in our own custom template(FluentResult or ApiResult or etc.).
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<CreateCostCategoryCommandValidator>();
                    options.AutomaticValidationEnabled = false;
                });

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAutoMapper(typeof(CostCategoryProfile));

            builder.Services.AddMediatR(typeof(CreateCostCategoryCommand));


            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


            return builder.Services;
        }
    }
}
