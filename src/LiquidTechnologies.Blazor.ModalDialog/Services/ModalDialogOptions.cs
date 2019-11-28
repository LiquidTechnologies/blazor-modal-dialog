using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidTechnologies.Blazor.ModalDialog.Services
{
    public class ModalDialogOptions
    {
        public string Position { get; set; } = ModalDialogPositionOptions.Default;
        public string Style { get; set; } = ModalDialogStyleOptions.Default;
        public string Title { get; set; }
        public bool ShowCloseButton { get; set; } = true;
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
