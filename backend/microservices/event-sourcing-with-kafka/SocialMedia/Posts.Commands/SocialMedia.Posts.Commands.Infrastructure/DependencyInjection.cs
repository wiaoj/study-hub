using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Posts.Commands.Infrastructure.Dispatchers;
using System.Reflection;

namespace SocialMedia.Posts.Commands.Infrastructure;
public static class DependencyInjection {
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services, Assembly assembly) {
        IEnumerable<Type> commandHandlerTypes = assembly
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces()
                    .Any(
                    interfaceType => interfaceType.IsGenericType 
                    && interfaceType.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

        foreach(Type commandHandlerType in commandHandlerTypes) {
            // CommandHandler'ın işlediği komut tipinin bulunması
            Type commandType = commandHandlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                    .GetGenericArguments()[0];

            services.AddScoped(typeof(ICommandHandler<>).MakeGenericType(commandType), commandHandlerType);
        }


        services.AddScoped(typeof(ICommandDispatcher<>), typeof(CommandDispatcher<>));

        return services;
    }
}