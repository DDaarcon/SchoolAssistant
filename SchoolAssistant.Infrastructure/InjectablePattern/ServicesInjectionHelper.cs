using Microsoft.Extensions.DependencyInjection;

namespace SchoolAssistant.Infrastructure.InjectablePattern
{
    public static class ServicesInjectionHelper
    {
        public static void AddAllInjectable(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var injectablesToRegister = assemblies
                .SelectMany(x => x.GetTypes())
                .Select(x => new
                {
                    InjectableAttribute = x.GetCustomAttributes(typeof(InjectableAttribute), false).OfType<InjectableAttribute>().FirstOrDefault(),
                    Type = x
                }).Where(x => x.InjectableAttribute is not null)
                .Select(x => new
                {
                    x.InjectableAttribute!.Lifetime,
                    x.Type,
                    Interfaces = x.InjectableAttribute.Contracts ?? x.Type.GetInterfaces()
                }).SelectMany(x => x.Interfaces.Select(z => new
                {
                    x.Lifetime,
                    Interface = z,
                    Implementation = x.Type
                })).ToLookup(x => new
                {
                    x.Lifetime,
                    x.Implementation
                }, x => x.Interface);

            foreach (var injectable in injectablesToRegister)
            {
                switch (injectable.Key.Lifetime)
                {
                    case ServiceLifetime.Transcient:
                        foreach (var @interface in injectable.Where(x => x != injectable.Key.Implementation))
                            services.AddTransient(@interface, injectable.Key.Implementation);
                        break;
                    default:
                    case ServiceLifetime.Scoped:
                        foreach (var @interface in injectable.Where(x => x != injectable.Key.Implementation))
                            services.AddScoped(@interface, injectable.Key.Implementation);
                        break;
                    case ServiceLifetime.Singleton:
                        foreach (var @interface in injectable.Where(x => x != injectable.Key.Implementation))
                            services.AddSingleton(@interface, injectable.Key.Implementation);
                        break;
                }
            }
        }
    }
}
