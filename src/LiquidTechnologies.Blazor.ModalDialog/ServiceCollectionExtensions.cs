using Microsoft.Extensions.DependencyInjection;

namespace Blazor.ModalDialog
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModalDialog(this IServiceCollection services)
        {
            return services.AddScoped<IModalDialogService, ModalDialogService>();
        }
    }
}
