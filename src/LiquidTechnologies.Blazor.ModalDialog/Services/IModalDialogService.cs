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

        Task<ModalDialogResult> ShowDialogAsync<TBlazorComponent>(string title, ModalDialogOptions options = null, ModalDialogParameters parameters = null) where TBlazorComponent : ComponentBase;
        Task<ModalDialogResult> ShowDialogAsync(Type dialogComponentType, string title, ModalDialogOptions options = null, ModalDialogParameters parameters = null);

        Task<MessageBoxDialogResult> ShowMessageBox(string title, string message, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1);

        void Close(bool success, ModalDialogParameters returnParameters = null);

        event Action Changed;
    }
}


/// <summary>
/// Shows the modal for the specified component type using the specified <paramref name="title"/>
/// and the specified <paramref name="parameters"/> and settings a custom modal <paramref name="options"/>
/// </summary>
/// <typeparam name="T">Type of component to display.</typeparam>
/// <param name="title">Modal title.</param>
/// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
/// <param name="options">Options to configure the modal.</param>