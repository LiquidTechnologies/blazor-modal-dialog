using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Blazor.ModalDialog
{
    public abstract class DataInputFormField
    {
        internal DataInputFormField(string id)
        {
            this.ID = id;
        }

        public string ID { get; }
        public string Name { get; set; }
        public string ToolTip { get; set; } = null;

        internal abstract string StringValue { get; set; }
        internal abstract void Validate(ModalDataInputForm form);
    }

    public class DataInputFormStringField : DataInputFormField
    {
        internal DataInputFormStringField(string id)
            : base(id)
        {
        }
        public string Value { get; set; } = "";
        public string PlaceholderValue { get; set; } = null;
        public int MinLength { get; set; } = 0;
        public int MaxLength { get; set; } = -1;
        internal override string StringValue
        {
            get { return Value; }
            set
            {
                this.Value = value;
                if (TrimWhitespace)
                    this.Value = this.Value.Trim();
                if (Truncate && MaxLength > 0)
                    this.Value = this.Value.Substring(0, Math.Min(MaxLength, this.Value.Length));

            }
        }
        public bool Truncate { get; set; } = false;
        public bool TrimWhitespace { get; set; } = false;

        /// <summary>
        /// </summary>
        /// <returns>
        ///  null/empty string if no error.
        ///  A string containing a description of the error.
        /// </returns>

        public Action<string, ModalDataInputForm> Validator { get; set; }
        internal override void Validate(ModalDataInputForm form)
        {
            if (this.Value.Length < MinLength)
                throw new ArgumentException($"The minimum length is {MinLength}");
            if (MaxLength >= 0 && this.Value.Length > MaxLength)
                throw new ArgumentException($"The maximum length is {MaxLength}");
            this.Validator?.Invoke(this.Value, form);
        }
    }
    public class DataInputFormIntField : DataInputFormField
    {
        internal DataInputFormIntField(string id)
             : base(id)
        {
        }
        public int Value { get; set; } = 0;
        public int Min { get; set; } = int.MinValue;
        public int Max { get; set; } = int.MaxValue;
        internal override string StringValue
        {
            get { return Value.ToString(CultureInfo.InvariantCulture); }
            set
            {
                if (int.TryParse(value, out int intValue))
                    this.Value = intValue;
                else
                    throw new ArgumentException("Invalid integer value");
            }
        }

        public Action<int, ModalDataInputForm> Validator { get; set; }
        internal override void Validate(ModalDataInputForm form)
        {
            if (this.Value < Min)
                throw new ArgumentException($"The minimum value is {Max}");
            if (this.Value > Max)
                throw new ArgumentException($"The maximum value is {Max}");
            this.Validator?.Invoke(this.Value, form);
        }
    }
    public class DataInputFormBoolField : DataInputFormField
    {
        internal DataInputFormBoolField(string id)
          : base(id)
        {
        }
        public bool Value { get; set; } = false;
        internal override string StringValue
        {
            get { return Value ? bool.TrueString : bool.FalseString; }
            set
            {
                if (value == bool.TrueString)
                    this.Value = true;
                else if (value == bool.FalseString)
                    this.Value = false;
                else
                    throw new ArgumentException("A boolean value must be true or false");
            }
        }

        public Action<bool, ModalDataInputForm> Validator { get; set; }
        internal override void Validate(ModalDataInputForm form)
        {
            this.Validator?.Invoke(this.Value, form);
        }
    }

    public abstract class DataInputFormEnumField : DataInputFormField
    {
        internal DataInputFormEnumField(string id)
             : base(id)
        {
        }
        public int Value { get; set; }
        internal abstract Type EnumType { get; }

        internal Tuple<int, string>[] EnumValues
        {
            get
            {
                List<Tuple<int, string>> values = new List<Tuple<int, string>>();

                foreach (Enum enumValue in Enum.GetValues(EnumType))
                {
                    string enumDescription = GetDescription(enumValue);
                    int enumIntValue = ((IConvertible)enumValue).ToInt32(CultureInfo.InvariantCulture);
                    values.Add(new Tuple<int, string>(enumIntValue, enumDescription));
                }

                return values.ToArray();
            }
        }

        private static string GetDescription<T>(T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return source.ToString();
        }
    }
    public class DataInputFormEnumField<T> : DataInputFormEnumField
        where T : Enum
    {
        internal DataInputFormEnumField(string id)
          : base(id)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");
            Value = (T)Enum.GetValues(typeof(T)).GetValue(0);
        }
        internal override Type EnumType { get { return typeof(T); } }
        public new T Value { get { return (T)Enum.ToObject(typeof(T), base.Value); } set { base.Value = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture); } }
        internal override string StringValue
        {
            get { return Value.ToString(); }
            set
            {
                this.Value = (T)Enum.Parse(typeof(T), value);
            }
        }

        public Action<T, ModalDataInputForm> Validator { get; set; }
        internal override void Validate(ModalDataInputForm form)
        {
            this.Validator?.Invoke(this.Value, form);
        }
    }
}
