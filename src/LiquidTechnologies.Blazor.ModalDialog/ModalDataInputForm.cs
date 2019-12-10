using LiquidTechnologies.Blazor.ModalDialog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidTechnologies.Blazor.ModalDialog
{
    public class ModalDataInputForm
    {
        private List<DataInputFormField> _fields = new List<DataInputFormField>();

        public ModalDataInputForm(string title, string description = "")
        {
            this.Title = title;
            this.Description = description;
        }

        /// <summary>
        /// The title of the Data Input Form
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description shown above the Input Form (can be empty if not required)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// All the fields that will be displayed for editing
        /// </summary>
        public DataInputFormField[] Fields { get { return _fields.ToArray(); } }

        /// <summary>
        /// Get a specific field from the form
        /// </summary>
        /// <param name="fieldID"></param>
        /// <returns></returns>
        public DataInputFormField this[string fieldID]
        {
            get
            {
                return _fields.FirstOrDefault(f => f.ID == fieldID);
            }
        }

        #region AddStringField
        /// <summary>
        /// Adds a <see cref="string"/> field to the Data Input Form
        /// </summary>
        /// <param name="id">The id of the field. This must be unique within the <see cref="ModalDataInputForm"/>. Used to identify the field</param>
        /// <param name="name">The name of the field as displayed to the user</param>
        /// <param name="initialValue">The initial value of the field (pre-populated in the TextBox input control)</param>
        /// <param name="tooltip">The tooltip displayed over the TextBox input control</param>
        /// <param name="placeholderText">If the TextBox input control is empty this value is displayed in ghosted text</param>
        /// <param name="minLength">The minimum length of the string</param>
        /// <param name="maxLength">The maximum length of the input string (-1 for no limit)</param>
        /// <param name="trimWhitespace">Removes any leading and tailing whitespace from the value the user enters (If the user entered "  X  " the <see cref="DataInputFormStringField.Value"/> would be "X").</param>
        /// <param name="truncate">Silently removes any data after the <paramref name="maxLength"/> character (any trimming is performed first if <paramref name="trimWhitespace"/> is set).</param>
        /// <param name="validator">
        /// A validation function used to check the field is valid before the form is submitted.
        /// The first argument is the value entered by the user, the second value is the <see cref="ModalDataInputForm"/>
        /// which provides access to all the other fields on the form in order to provide cross field validation.
        /// <code>
        /// (v,frm) => {if (v.Contains('s')) throw new ArgumentException("String can not contain an 's'");
        /// </code>
        /// </param>
        /// <returns>The new <see cref="DataInputFormStringField"/> object, the returned value can be accessed from this via the 
        /// <see cref="DataInputFormStringField.Value"/> property when the form is submitted.</returns>
        public DataInputFormStringField AddStringField(
            string id, string name, string initialValue, string tooltip = "",
            string placeholderText = "", int minLength = 0, int maxLength = -1,
            bool truncate = false, bool trimWhitespace = false,
            Action<string, ModalDataInputForm> validator = null)
        {
            if (_fields.Any(f => f.ID == id))
                throw new ArgumentException("Duplicate field 'id'", nameof(id));

            var newField = new DataInputFormStringField(id)
            {
                Name = name,
                Value = initialValue,
                ToolTip = tooltip,
                PlaceholderValue = placeholderText,
                MinLength = minLength,
                MaxLength = maxLength,
                TrimWhitespace = trimWhitespace,
                Truncate = truncate,
                Validator = validator
            };
            _fields.Add(newField);

            return newField;
        }
        #endregion

        #region AddIntField
        /// <summary>
        /// Adds a <see cref="int"/> field to the Data Input Form
        /// </summary>
        /// <param name="id">The id of the field. This must be unique within the <see cref="ModalDataInputForm"/>. Used to identify the field</param>
        /// <param name="name">The name of the field as displayed to the user</param>
        /// <param name="initialValue">The initial value of the field (pre-populated in the TextBox input control)</param>
        /// <param name="tooltip">The tooltip displayed over the TextBox input control</param>
        /// <param name="min">The minimum length of the string</param>
        /// <param name="max">The maximum length of the input string (-1 for no limit)</param>
        /// <param name="validator">
        /// A validation function used to check the field is valid before the form is submitted.
        /// The first argument is the value entered by the user, the second value is the <see cref="ModalDataInputForm"/>
        /// which provides access to all the other fields on the form in order to provide cross field validation.
        /// <code>
        /// (v,frm) => {if (v == 7) throw new ArgumentException("value can not be 7");
        /// </code>
        /// </param>
        /// <returns>The new <see cref="DataInputFormIntField"/> object, the returned value can be accessed from this via the 
        /// <see cref="DataInputFormIntField.Value"/> property when the form is submitted.</returns>
        public DataInputFormIntField AddIntField(
            string id, string name, int initialValue, string tooltip = "",
            int min = int.MinValue, int max = int.MaxValue,
            Action<int, ModalDataInputForm> validator = null)
        {
            if (_fields.Any(f => f.ID == id))
                throw new ArgumentException("Duplicate field 'id'", nameof(id));

            var newField = new DataInputFormIntField(id)
            {
                Name = name,
                Value = initialValue,
                ToolTip = tooltip,
                Min = min,
                Max = max,
                Validator = validator
            };
            _fields.Add(newField);

            return newField;
        }
        #endregion

        #region AddBoolField
        /// <summary>
        /// Adds a <see cref="bool"/> field to the Data Input Form
        /// </summary>
        /// <param name="id">The id of the field. This must be unique within the <see cref="ModalDataInputForm"/>. Used to identify the field</param>
        /// <param name="name">The name of the field as displayed to the user</param>
        /// <param name="initialValue">The initial value of the field (pre-populated in the TextBox input control)</param>
        /// <param name="tooltip">The tooltip displayed over the TextBox input control</param>
        /// <param name="validator">
        /// A validation function used to check the field is valid before the form is submitted.
        /// The first argument is the value entered by the user, the second value is the <see cref="ModalDataInputForm"/>
        /// which provides access to all the other fields on the form in order to provide cross field validation.
        /// <code>
        /// (v,frm) => {if (v.Contains('s')) throw new ArgumentException("String can not contain an 's'");
        /// </code>
        /// </param>
        /// <returns>The new <see cref="DataInputFormBoolField"/> object, the returned value can be accessed from this via the 
        /// <see cref="DataInputFormBoolField.Value"/> property when the form is submitted.</returns>
        public DataInputFormBoolField AddBoolField(
            string id, string name, bool initialValue, string tooltip = "",
            Action<bool, ModalDataInputForm> validator = null)
        {
            if (_fields.Any(f => f.ID == id))
                throw new ArgumentException("Duplicate field 'id'", nameof(id));

            var newField = new DataInputFormBoolField(id)
            {
                Name = name,
                Value = initialValue,
                ToolTip = tooltip,
                Validator = validator
            };
            _fields.Add(newField);

            return newField;
        }
        #endregion

        #region AddEnumField
        /// <summary>
        /// Adds a <typeparamref name="T"/> field to the Data Input Form (where <typeparamref name="T"/> is an enumerated value)
        /// </summary>
        /// <param name="id">The id of the field. This must be unique within the <see cref="ModalDataInputForm"/>. Used to identify the field</param>
        /// <param name="name">The name of the field as displayed to the user</param>
        /// <param name="initialValue">The initial value of the field (pre-populated in the TextBox input control)</param>
        /// <param name="tooltip">The tooltip displayed over the TextBox input control</param>
        /// <param name="placeholderText">If the TextBox input control is empty this value is displayed in ghosted text</param>
        /// <param name="minLength">The minimum length of the string</param>
        /// <param name="maxLength">The maximum length of the input string (-1 for no limit)</param>
        /// <param name="validator">
        /// A validation function used to check the field is valid before the form is submitted.
        /// The first argument is the value entered by the user, the second value is the <see cref="ModalDataInputForm"/>
        /// which provides access to all the other fields on the form in order to provide cross field validation.
        /// <code>
        /// (v,frm) => {if (v.Contains('s')) throw new ArgumentException("String can not contain an 's'");
        /// </code>
        /// </param>
        /// <returns>The new <see cref="DataInputFormEnumField{T}"/> object, the returned value can be accessed from this via the 
        /// <see cref="DataInputFormEnumField{T}.Value"/> property when the form is submitted.</returns>
        public DataInputFormEnumField<T> AddEnumField<T>(
            string id, string name, T initialValue, string tooltip = "",
            string placeholderText = "", int minLength = 0, int maxLength = -1,
            Action<T, ModalDataInputForm> validator = null)
             where T : Enum
        {
            if (_fields.Any(f => f.ID == id))
                throw new ArgumentException("Duplicate field 'id'", nameof(id));

            var newField = new DataInputFormEnumField<T>(id)
            {
                Name = name,
                Value = initialValue,
                ToolTip = tooltip,
                Validator = validator
            };
            _fields.Add(newField);

            return newField;
        }
        #endregion

        #region ShowAsync
        /// <summary>
        /// Shows the Data Input Form in a modal dialog container.
        /// </summary>
        /// <param name="modalDialogService">The Modal Dialog Service</param>
        /// <param name="options">options that affect how the dialog behaves (can be null)</param>
        /// <returns>
        /// false if the user cancelled for form.
        /// true if the user completed the form (including any validation)
        /// The values the user entered are in the <see cref="Fields"/> that were created with the AddXXField methods
        /// </returns>
        public async Task<bool> ShowAsync(IModalDialogService modalDialogService, ModalDialogOptions options = null)
        {
            if (modalDialogService == null) throw new ArgumentNullException(nameof(modalDialogService));
            ModalDialogParameters mParams = new ModalDialogParameters();
            mParams.Add("Form", this);

            var result = await modalDialogService.ShowDialogAsync<Components.DataInputForm>(this.Title, options, mParams);
            return result.Success;
        }
        #endregion
    }
}
