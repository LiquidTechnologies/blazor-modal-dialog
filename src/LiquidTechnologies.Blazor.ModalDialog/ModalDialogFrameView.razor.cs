using LiquidTechnologies.Blazor.ModalDialog.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Diagnostics;

namespace LiquidTechnologies.Blazor.ModalDialog
{
    public class ModalDialogFrameViewBase : ComponentBase
    {

        [Parameter]
        public ModalDialogModel ModalDialogEntry { get; set; }

        [Parameter] 
        public string Title { get; set; }

        [Inject] protected IModalDialogService DialogService { get; set; }

        protected void HandleBackgroundClick()
        {
            if (ModalDialogEntry.Options.BackgroundClickToClose)
                DialogService.Close(false);
        }

        //protected void HandleBackgroundKeyPress(KeyboardEventArgs args)
        //{
        //    if (ModalDialogEntry.Options.EscapeKeyToClose && args.Key == "escape")
        //        DialogService.Close(false);
        //}
        

        protected void HandleCloseBtnClick()
        {
            DialogService.Close(false);
        }
    }
}
