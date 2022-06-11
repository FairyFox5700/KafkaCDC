using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaCDC.Common
{
    public static class MigrationManager
    {
        public static IServiceProvider MigrateDatabase<TContext>(this IServiceProvider serviceProvider)
        where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<TContext>();
            appContext.Database.Migrate();

            return serviceProvider;
        }
    }
}
