using CategoryOperations.Core.Handlers;
using CategoryOperations.Core.Repositories;
using CategoryOperations.Infra.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using OrganizaDespensa.Extensions.Middlewares;
using OrganizaDespensa.SharedKernel.Core.QueueHandlers;
using ProductOperations.Core.Eventhandlers;
using ProductOperations.Core.Handlers;
using ProductOperations.Core.Repositories;
using ProductOperations.Infra.Repositories;
using System.Linq;
using UserOperations.Core.EventHandlers;
using UserOperations.Core.Handlers;
using UserOperations.Core.Repositories;
using UserOperations.Infra.Repositories;

namespace OrganizaDespensa.Api.Configurations
{
    public static class ApiConfigurations
    {
        public static void SolveHandlerDependencyInjection(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateProductHandler).Assembly);
            services.AddMediatR(typeof(UpdateProductHandler).Assembly);
            services.AddMediatR(typeof(DeleteProductHandler).Assembly);
            services.AddMediatR(typeof(CreateCategoryHandler).Assembly);
            services.AddMediatR(typeof(CreateUserHandler).Assembly);

            services.AddMediatR(typeof(ProductEventHandler).Assembly);
            services.AddMediatR(typeof(UserEventHandler).Assembly);

            services.AddScoped<IQueueHandler, QueueHandler>();
        }

        public static void  SolveRepositoryDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

        }

        
        public static void CompressRequests(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });
        }

        public static void SerializeJson(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opcoes =>
            {
                var serializerOptions = opcoes.JsonSerializerOptions;
                serializerOptions.IgnoreNullValues = true;
                serializerOptions.IgnoreReadOnlyProperties = true;
                serializerOptions.WriteIndented = true;
            });
        }

        public static void ConfigurarGDPR(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        public static void AddExceptionHandler(this IApplicationBuilder app)
        {
            app.UseApiExceptionHandler(options =>
            {
                options.AddResponseDetails = ApiErrorResponseExtension.UpdateApiErrorResponse;
                options.DetermineLogLevel = ApiErrorResponseExtension.DetermineLogLevel;
            });
        }


    }
}
