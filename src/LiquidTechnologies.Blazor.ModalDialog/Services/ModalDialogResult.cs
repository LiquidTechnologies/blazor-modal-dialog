using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidTechnologies.Blazor.ModalDialog.Services
{
    public class ModalDialogResult
    {
        internal ModalDialogResult(bool success, ModalDialogParameters returnParameters)
        {
            this.Success = success;
            this.ReturnParameters = returnParameters;
        }
        public bool Success { get; }
        public ModalDialogParameters ReturnParameters { get; }

    }
}
