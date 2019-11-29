using LiquidTechnologies.Blazor.ModalDialog.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidTechnologies.Blazor.ModalDialog
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModalDialog(this IServiceCollection services)
        {
            return services.AddScoped<IModalDialogService, ModalDialogService>();
        }
    }
}
