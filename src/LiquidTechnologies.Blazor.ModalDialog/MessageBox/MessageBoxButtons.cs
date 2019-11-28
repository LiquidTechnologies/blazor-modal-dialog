using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidTechnologies.Blazor.ModalDialog
{
    public enum MessageBoxButtons
    {
        /// <summary>
        ///       Specifies that the
        ///       message box contains an OK button. This field is
        ///       constant.
        /// </summary>
        OK = 0x00000000,

        /// <summary>
        ///       Specifies that the
        ///       message box contains OK and Cancel buttons. This field
        ///       is
        ///       constant.
        /// </summary>
        OKCancel = 0x00000001,

        /// <summary>
        ///       Specifies that the
        ///       message box contains Abort, Retry, and Ignore buttons.
        ///       This field is
        ///       constant.
        /// </summary>
        AbortRetryIgnore = 0x00000002,

        /// <summary>
        ///       Specifies that the
        ///       message box contains Yes, No, and Cancel buttons. This
        ///       field is
        ///       constant.
        /// </summary>
        YesNoCancel = 0x00000003,

        /// <summary>
        ///       Specifies that the
        ///       message box contains Yes and No buttons. This field is
        ///       constant.
        /// </summary>
        YesNo = 0x00000004,

        /// <summary>
        ///       Specifies that the
        ///       message box contains Retry and Cancel buttons. This field
        ///       is
        ///       constant.
        /// </summary>
        RetryCancel = 0x00000005

    }
}
