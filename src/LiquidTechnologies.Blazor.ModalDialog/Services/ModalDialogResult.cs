using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidTechnologies.Blazor.ModalDialog.Services
{
    /// <summary>
    /// The results returned from a Modal Dialog
    /// </summary>
    public class ModalDialogResult
    {
        internal ModalDialogResult(bool success, ModalDialogParameters returnParameters)
        {
            this.Success = success;
            this.ReturnParameters = returnParameters;
        }
        /// <summary>
        /// Indicates if the action performed by the dialog was a success.
        /// The exact meaning of this is specific to the dialog, but typically false means the action was cancelled, 
        /// true means it completed OK, and an exception should have been raised if there was an error.
        /// </summary>
        public bool Success { get; }
        /// <summary>
        /// Return parameters (as name value pairs)
        /// </summary>
        public ModalDialogParameters ReturnParameters { get; }
    }
}
