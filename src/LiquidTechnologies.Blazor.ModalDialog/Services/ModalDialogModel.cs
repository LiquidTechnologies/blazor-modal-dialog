using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiquidTechnologies.Blazor.ModalDialog.Services
{
    public class ModalDialogModel
    {
        public ModalDialogModel(Type dialogComponentType, string title, ModalDialogParameters parameters, ModalDialogOptions options)
        {
            this.TaskSource = new TaskCompletionSource<ModalDialogResult>();
            this.DialogComponentType = dialogComponentType;
            this.Title = title;
            this.Parameters = parameters;
            this.Options = options;
        }

        private Type DialogComponentType { get; }
        internal TaskCompletionSource<ModalDialogResult> TaskSource { get; }

        public Task<ModalDialogResult> Task { get { return this.TaskSource.Task; } }
        public string Title{ get; }
        public ModalDialogParameters Parameters { get; }
        public ModalDialogOptions Options { get; }

        public RenderFragment DialogContents
        {
            get
            {
                RenderFragment content = new RenderFragment(x => { x.OpenComponent(1, DialogComponentType); x.CloseComponent(); });
                return content;
            }
        }
    }
}
