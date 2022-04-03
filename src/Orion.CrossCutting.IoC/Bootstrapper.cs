using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orion.Application.Commands.CreateUser;
using Orion.Application.Pipelines;
using Orion.Application.Services;
using Orion.Core;
using Orion.Core.Base;
using Orion.Core.Entities.Properties;
using Orion.Core.Entities.Users;
using Orion.Infra.Data;
using Orion.Infra.Data.Repositories;
using Orion.Shared;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Reflection;

namespace Orion.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static Container RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            Container container = new();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            //services.AddEventBus(configuration);
            ConfigureMediatR(container);
            RegisterRepositories(services, configuration);
            services.AddSingleton<ITokenService, TokenService>();
            services.AddAutoMapper(typeof(ImmobileProfile).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateUserCommand).GetTypeInfo().Assembly);

            return container;
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAsyncRepository<Entity>, BaseRepository<Entity>>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IImmobileRepository, ImmobileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());
        }

        private static Assembly[] GetAssemblies()
        {
            var assemblies = new[]
            {
                typeof(CreateUserCommand).GetTypeInfo().Assembly
            };

            return assemblies;
        }

        private static void ConfigureMediatR(Container container)
        {
            var assemblies = GetAssemblies().ToArray();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);

            var handlerTypes = container.GetTypesToRegister(typeof(INotificationHandler<>), assemblies, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false
            });
            container.Collection.Register(typeof(INotificationHandler<>), handlerTypes);
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(LoggingBehavior<,>),
                typeof(FailFastRequestBehaviour<,>),
                typeof(RequestPostProcessorBehavior<,>),
            });

            container.Collection.Register(typeof(IRequestPostProcessor<,>), new[]
            {
              typeof(PostProcessorUnitOfWork<,>),
            });
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
        }
    }
}