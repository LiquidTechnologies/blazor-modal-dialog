using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.ModalDialog
{
    public class ModalDialogOptions
    {
        /// <summary>
        /// The position of the Modal Dialog
        /// </summary>
        /// <seealso cref="ModalDialogPositionOptions"/>
        public string Position { get; set; } = ModalDialogPositionOptions.Default;
        /// <summary>
        /// The css style applied to the modal dialog
        /// </summary>
        /// <seealso cref="ModalDialogStyleOptions"/>
        public string Style { get; set; } = ModalDialogStyleOptions.Default;
        /// <summary>
        /// The title of the modal dialog
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Indicates of the dialog should show a Close X in the top right
        /// </summary>
        public bool ShowCloseButton { get; set; } = true;
        /// <summary>
        /// Indicates of the dialog should automatically Close is a click is performed outside of the dialog (in the background)
        /// </summary>
        public bool BackgroundClickToClose { get; set; } = true;
//        public bool EscapeKeyToClose { get; set; } = true;
    }

    public static class ModalDialogStyleOptions
    {
        public const string Default = Dialog;

        public const string Dialog = "liquid-modal-dialog";
    }
    public static class ModalDialogPositionOptions
    {
        public const string Center = "liquid-modal-dialog-center";
        public const string TopLeft = "liquid-modal-dialog-topleft";
        public const string TopRight = "liquid-modal-dialog-topright";
        public const string BottomLeft = "liquid-modal-dialog-bottomleft";
        public const string BottomRight = "liquid-modal-dialog-bottomright";
        public const string Default = Center;
    }
}
