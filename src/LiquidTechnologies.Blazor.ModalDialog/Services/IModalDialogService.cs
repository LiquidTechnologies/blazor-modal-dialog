using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiquidTechnologies.Blazor.ModalDialog.Services
{
    public interface IModalDialogService
    {
        /// <summary>
        /// A list of all the modal dialogs that are currently open
        /// </summary>
        IEnumerable<ModalDialogModel> ModalDialogFrames { get; }

        /// <summary>
        /// Shows a modal dialog built from the specified Blazor component <typeparamref name="TBlazorComponent"/>.
        /// </summary>
        /// <typeparam name="TBlazorComponent">Type of the Blazor component to display.</typeparam>
        /// <param name="title">Modal Dialog title</param>
        /// <param name="options">
        /// Options to configure the Modal Dialog.
        /// If null then default options are used
        /// </param>
        /// <param name="parameters">
        /// Key/Value collection of parameters to pass to Blazor component being displayed.
        /// These are accessible within the Blazor Components via a cascading value i.e.
        /// <code>
        /// [CascadingParameter] ModalDialogParameters Parameters { get; set; }
        /// </code>
        /// If null then an empty <see cref="ModalDialogParameters"/> object is passed to the <typeparamref name="TBlazorComponent"/> Blazor Component
        /// </param>
        /// <returns>
        /// A task that is completed when the Modal Dialog has been <see cref="Close(bool, ModalDialogParameters)"/>d.
        /// The <see cref="ModalDialogResult"/> value contained in the task indicates success (as set by the defined by <typeparamref name="TBlazorComponent"/> 
        /// when it closes the dialog) and contains the return parameters.
        /// </returns>
        /// <remarks>
        /// Shows the <typeparamref name="TBlazorComponent"/> Blazor Component as a Modal Dialog.
        /// 
        /// The <typeparamref name="TBlazorComponent"/> should call the <see cref="Close(bool, ModalDialogParameters)"/> to close the Modal Dialog.
        /// 
        /// This is typically called from an async function. The use of await in the returned task will cause execution to continue when the 
        /// modal dialog is closed.
        /// </remarks>
        /// <example>
        /// <code>
        /// async void ShowModal()
        ///     {
        ///         // Arguments can be passed into the modal dialog
        ///         var parameters = new ModalDialogParameters();
        ///         parameters.Add("FormId", 11);
        /// 
        ///         // using await will cause execution to resume when the dialog has been closed.
        ///         ModalDialogResult result = await ModalDialog.ShowDialogAsync&lt;SignUpForm&gt;("Sign Up Form", null, parameters);
        ///         if (result.Success)
        ///             CreateNewUser(result.ReturnParameters.Get&lt;string&gt;("FirstName"), result.ReturnParameters.Get&lt;string&gt;("LastName"));
        ///     }
        /// </code>
        /// </example>
        Task<ModalDialogResult> ShowDialogAsync<TBlazorComponent>(string title, ModalDialogOptions options = null, ModalDialogParameters parameters = null) where TBlazorComponent : ComponentBase;

        /// <summary>
        /// Shows a modal dialog built from the specified Blazor component <paramref name="dialogComponentType"/>.
        /// </summary>
        /// <param name="dialogComponentType">Type of the Blazor component to display (must derive from <see cref="ComponentBase"/>.</param>
        /// <param name="title">Modal Dialog title</param>
        /// <param name="options">
        /// Options to configure the Modal Dialog.
        /// If null then default options are used
        /// </param>
        /// <param name="parameters">
        /// Key/Value collection of parameters to pass to Blazor component being displayed.
        /// These are accessible within the Blazor Components via a cascading value i.e.
        /// <code>
        /// [CascadingParameter] ModalDialogParameters Parameters { get; set; }
        /// </code>
        /// If null then an empty <see cref="ModalDialogParameters"/> object is passed to the <paramref name="dialogComponentType"/> Blazor Component
        /// </param>
        /// <returns>
        /// A task that is completed when the Modal Dialog has been <see cref="Close(bool, ModalDialogParameters)"/>d.
        /// The <see cref="ModalDialogResult"/> value contained in the task indicates success (as set by the defined by <paramref name="dialogComponentType"/> 
        /// when it closes the dialog) and contains the return parameters.
        /// </returns>
        /// <remarks>
        /// Shows the <paramref name="dialogComponentType"/> Blazor Component as a Modal Dialog.
        /// 
        /// The <paramref name="dialogComponentType"/> should call the <see cref="Close(bool, ModalDialogParameters)"/> to close the Modal Dialog.
        /// 
        /// This is typically called from an async function. The use of await in the returned task will cause execution to continue when the 
        /// modal dialog is closed.
        /// </remarks>
        /// <example>
        /// <code>
        /// async void ShowModal()
        ///     {
        ///         // Arguments can be passed into the modal dialog
        ///         var parameters = new ModalDialogParameters();
        ///         parameters.Add("FormId", 11);
        /// 
        ///         // using await will cause execution to resume when the dialog has been closed.
        ///         ModalDialogResult result = await ModalDialog.ShowDialogAsync&lt;SignUpForm&gt;("Sign Up Form", null, parameters);
        ///         if (result.Success)
        ///             CreateNewUser(result.ReturnParameters.Get&lt;string&gt;("FirstName"), result.ReturnParameters.Get&lt;string&gt;("LastName"));
        ///     }
        /// </code>
        /// </example>
        /// <seealso cref="Close(bool, ModalDialogParameters)"/>
        /// <seealso cref="Close(Exception)"/>
        Task<ModalDialogResult> ShowDialogAsync(Type dialogComponentType, string title, ModalDialogOptions options = null, ModalDialogParameters parameters = null);

        /// <summary>
        /// Opens a message box as a modal dialog.
        /// </summary>
        /// <param name="title">The title heading of message box</param>
        /// <param name="message">The message within the message box</param>
        /// <param name="buttons">The buttons that will be displayed on the message box</param>
        /// <param name="defaultButton">The button that will be the 'selected' one.</param>
        /// <returns>The button that is clicked.</returns>
        /// <remarks>
        /// If the <paramref name="buttons"/> allows 'Cancel' then clicking on the close X button or the background is the same as pressing the cancel button.
        /// If the <paramref name="buttons"/> does not allow 'Cancel' then there is no close X button and clicking on the background does nothing (a button must be pressed to close the dialog).
        /// 
        /// TODO : make the return key select the <paramref name="defaultButton"/>, make the escape key act as cancel where applicable.
        /// </remarks>
        /// <example>
        /// <code>
        /// async void ShowMessageBox()
        ///     {
        ///         // using await will cause execution to resume when the dialog has been closed.
        ///         MessageBoxDialogResult dialogResult = await ModalDialog.ShowMessageBoxAsync("Confirm", "Delete All Files?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.MessageBoxDefaultButton2);
        ///         if (dialogResult == MessageBoxDialogResult.Yes)
        ///             DeleteAllFiles();
        ///     }
        /// </code>
        /// </example>
        /// <seealso cref="Close(bool, ModalDialogParameters)"/>
        /// <seealso cref="Close(Exception)"/>
        Task<MessageBoxDialogResult> ShowMessageBoxAsync(string title, string message, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1);

        /// <summary>
        /// Closes the topmost Modal Dialog
        /// </summary>
        /// <param name="success">
        /// Indicates if the action performed by the dialog was a success.
        /// The exact meaning of this is specific to the dialog, but typically false means the action was cancelled, 
        /// true means it completed OK, and an exception should have been raised if there was an error.
        /// </param>
        /// <param name="returnParameters">Optional return parameters (as name value pairs)</param>
        /// <remarks>
        /// Should only be called from within an open Modal Dialog's Blazor Component.
        /// Should only be called once for each open Modal Dialog's Blazor Component.
        /// </remarks>
        /// <example>
        ///     @inject IModalDialogService ModalDialogService
        ///     
        ///     <div class="simple-form">
        ///     
        ///         <div class="form-group">
        ///             <label for="first-name">First Name</label>
        ///             <input @bind="FirstName" type="text" class="form-control" id="first-name" placeholder="Enter First Name" />
        ///         </div>
        ///     
        ///         <div class="form-group">
        ///             <label for="last-name">Last Name</label>
        ///             <input @bind="LastName" type="text" class="form-control" id="last-name" placeholder="Enter Last Name" />
        ///         </div>
        ///     
        ///         <button @onclick="Ok_Clicked" class="btn btn-primary">Submit</button>
        ///         <button @onclick="Cancel_Clicked" class="btn btn-secondary">Cancel</button>
        ///     </div>
        ///     
        ///     
        ///     @code {
        ///     
        ///         [CascadingParameter] ModalDialogParameters Parameters { get; set; }
        ///     
        ///         string FirstName { get; set; }
        ///         string LastName { get; set; }
        ///     
        ///         async void Ok_Clicked()
        ///         {
        ///             try
        ///             {
        ///                 if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
        ///                 {
        ///                     ModalDialogResult result = await ModalDialogService.ShowDialogAsync<ValidationErrorForm>("Validation Errors");
        ///                     if (result.Success == false)
        ///                         ModalDialogService.Close(false);
        ///                 }
        ///                 else
        ///                 {
        ///                     ModalDialogParameters resultParameters = new ModalDialogParameters();
        ///                     resultParameters.Set("FirstName", FirstName);
        ///                     resultParameters.Set("LastName", LastName);
        ///                     resultParameters.Set("FullName", FirstName + " " + LastName);
        ///                     ModalDialogService.Close(true, resultParameters);
        ///                 }
        ///             }
        ///             catch (Exception ex)
        ///             {
        ///                 // pass the exception back to the ShowDialogAsync call that opened the Dialog
        ///                 ModalDialogService.Close(ex);
        ///             }
        ///         }
        ///     
        ///         void Cancel_Clicked()
        ///         {
        ///             ModalDialogService.Close(false);
        ///         }
        ///     }
        /// 
        /// </example>
        void Close(bool success, ModalDialogParameters returnParameters = null);

        /// <summary>
        /// Closes the topmost Modal Dialog, as the result of an error
        /// </summary>
        /// <param name="error">
        /// The exception that will be reported back to the code that Opened the Modal Dialog.
        /// </param>
        /// <remarks>
        /// Should only be called from within an open Modal Dialog's Blazor Component.
        /// Should only be called once for each open Modal Dialog's Blazor Component.
        /// </remarks>
        /// <example>
        ///     @inject IModalDialogService ModalDialogService
        ///     
        ///     <div class="simple-form">
        ///     
        ///         <div class="form-group">
        ///             <label for="first-name">First Name</label>
        ///             <input @bind="FirstName" type="text" class="form-control" id="first-name" placeholder="Enter First Name" />
        ///         </div>
        ///     
        ///         <div class="form-group">
        ///             <label for="last-name">Last Name</label>
        ///             <input @bind="LastName" type="text" class="form-control" id="last-name" placeholder="Enter Last Name" />
        ///         </div>
        ///     
        ///         <button @onclick="Ok_Clicked" class="btn btn-primary">Submit</button>
        ///         <button @onclick="Cancel_Clicked" class="btn btn-secondary">Cancel</button>
        ///     </div>
        ///     
        ///     
        ///     @code {
        ///     
        ///         [CascadingParameter] ModalDialogParameters Parameters { get; set; }
        ///     
        ///         string FirstName { get; set; }
        ///         string LastName { get; set; }
        ///     
        ///         async void Ok_Clicked()
        ///         {
        ///             try
        ///             {
        ///                 if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
        ///                 {
        ///                     ModalDialogResult result = await ModalDialogService.ShowDialogAsync<ValidationErrorForm>("Validation Errors");
        ///                     if (result.Success == false)
        ///                         ModalDialogService.Close(false);
        ///                 }
        ///                 else
        ///                 {
        ///                     ModalDialogParameters resultParameters = new ModalDialogParameters();
        ///                     resultParameters.Set("FirstName", FirstName);
        ///                     resultParameters.Set("LastName", LastName);
        ///                     resultParameters.Set("FullName", FirstName + " " + LastName);
        ///                     ModalDialogService.Close(true, resultParameters);
        ///                 }
        ///             }
        ///             catch (Exception ex)
        ///             {
        ///                 // pass the exception back to the ShowDialogAsync call that opened the Dialog
        ///                 ModalDialogService.Close(ex);
        ///             }
        ///         }
        ///     
        ///         void Cancel_Clicked()
        ///         {
        ///             ModalDialogService.Close(false);
        ///         }
        ///     }
        /// 
        /// </example>
        void Close(Exception error);

        /// <summary>
        /// An event that indicates <see cref="ModalDialogFrames"/> has changed 
        /// </summary>
        event Action Changed;
    }
}


