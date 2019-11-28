using LiquidTechnologies.Blazor.ModalDialog.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModalDialog(this IServiceCollection services)
        {
            return services.AddScoped<IModalDialogService, ModalDialogService>();
        }
    }
}
