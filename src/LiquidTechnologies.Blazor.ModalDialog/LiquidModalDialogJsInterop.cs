using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiquidTechnologies.Blazor.ModalDialog
{
    internal static class LiquidModalDialogJsInterop
    {
        public static ValueTask<string> Prompt(this IJSRuntime jsRuntime, string message, string defaultValue = null)
        {
            // Implemented in exampleJsInterop.js
            return jsRuntime.InvokeAsync<string>(
                "liquidModalDialog.showPrompt",
                message,
                defaultValue);
        }

        public static async Task Focus(this IJSRuntime jsRuntime, ElementReference elementRef)
        {
            await jsRuntime.InvokeVoidAsync(
                "liquidModalDialog.focusElement", elementRef);
        }
    }
}
