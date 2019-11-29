using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace LiquidTechnologies.Blazor.ModalDialog.Services
{
    public class ModalDialogService : IModalDialogService
    {
        private Stack<ModalDialogModel> _dialogs = new Stack<ModalDialogModel>();

        public ModalDialogService() { }

        public Task<ModalDialogResult> ShowDialogAsync<TBlazorComponent>(string title, ModalDialogOptions options, ModalDialogParameters parameters)
            where TBlazorComponent : ComponentBase
        {
            return ShowDialogAsync(typeof(TBlazorComponent), title, options, parameters);
        }


        public Task<ModalDialogResult> ShowDialogAsync(Type dialogComponentType, string title, ModalDialogOptions options, ModalDialogParameters parameters)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(dialogComponentType))
                throw new ArgumentException($"{dialogComponentType.FullName} must be a Blazor Component");

            parameters = parameters ?? new ModalDialogParameters();
            options = options ?? new ModalDialogOptions();

            ModalDialogModel newDialogWindow = new ModalDialogModel(dialogComponentType, title, parameters, options);
            _dialogs.Push(newDialogWindow);

            OnChanged();

            return newDialogWindow.Task;
        }

        public async Task<MessageBoxDialogResult> ShowMessageBox(string title, string message, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            ModalDialogOptions options = new ModalDialogOptions();
            if (buttons == MessageBoxButtons.YesNo || buttons == MessageBoxButtons.AbortRetryIgnore)
            {
                options.ShowCloseButton = false;
                options.BackgroundClickToClose = false;
            }

            ModalDialogParameters parameters = new ModalDialogParameters();
            parameters.Set("MessageBoxText", message);
            parameters.Set("MessageBoxButtons", buttons);
            parameters.Set("MessageBoxDefaultButton", defaultButton);

            ModalDialogResult result = await ShowDialogAsync<LiquidTechnologies.Blazor.ModalDialog.Components.MessageBoxForm>(title, options, parameters);
            if (result.Success == false)
            {
                Debug.Assert(buttons != MessageBoxButtons.YesNo && buttons != MessageBoxButtons.AbortRetryIgnore, "Should not be able dismiss message boxes with no cancel (except OK)");

                if (buttons == MessageBoxButtons.OK)
                    return MessageBoxDialogResult.OK;
                else 
                    return MessageBoxDialogResult.Cancel;
            }
            else
            {
                return result.ReturnParameters.Get<MessageBoxDialogResult>("MessageBoxDialogResult");
            }
        }

        public void Close(bool success, ModalDialogParameters returnParameters)
        {
            returnParameters = returnParameters ?? new ModalDialogParameters();

            ModalDialogModel closingDialogWindow = _dialogs.Pop();
            closingDialogWindow.TaskSource.SetResult(new ModalDialogResult(success, returnParameters));
            OnChanged();
        }


        public IEnumerable<ModalDialogModel> ModalDialogFrames { get { return _dialogs; } }


        public event Action Changed;
        protected virtual void OnChanged()
        {
            if (Changed != null)
                Changed();
        }
    }
}
