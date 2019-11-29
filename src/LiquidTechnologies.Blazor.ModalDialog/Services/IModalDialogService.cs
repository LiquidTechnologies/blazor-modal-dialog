using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiquidTechnologies.Blazor.ModalDialog.Services
{
    public interface IModalDialogService
    {
        IEnumerable<ModalDialogModel> ModalDialogFrames { get; }

        /// <summary>
        /// Shows a modal dialog built from the specified component type <typeparamref name="TBlazorComponent"/>.
        /// </summary>
        /// <typeparam name="TBlazorComponent">Type of the Blazor component to display.</typeparam>
        /// <param name="title">Modal Dialog title</param>
        /// <param name="options">Options to configure the Modal Dialog.</param>
        /// <param name="parameters">
        /// Key/Value collection of parameters to pass to Blazor component being displayed.
        /// These are accessible within the Blazor Components via a cascading value i.e.
        /// <code>
        /// [CascadingParameter] ModalDialogParameters Parameters { get; set; }
        /// </code>
        /// </param>
        /// <returns>
        /// </returns>
        /// <remarks>
        /// </remarks>
        Task<ModalDialogResult> ShowDialogAsync<TBlazorComponent>(string title, ModalDialogOptions options = null, ModalDialogParameters parameters = null) where TBlazorComponent : ComponentBase;
        Task<ModalDialogResult> ShowDialogAsync(Type dialogComponentType, string title, ModalDialogOptions options = null, ModalDialogParameters parameters = null);

        Task<MessageBoxDialogResult> ShowMessageBox(string title, string message, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1);

        void Close(bool success, ModalDialogParameters returnParameters = null);

        event Action Changed;
    }
}


