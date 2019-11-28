using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidTechnologies.Blazor.ModalDialog.Services
{
    public class ModalDialogParameters : Dictionary<string, object>
    {

        public T Get<T>(string parameterName)
        {
            if (!this.ContainsKey(parameterName))
            {
                throw new KeyNotFoundException($"{parameterName} does not exist in modal parameters");
            }

            return (T)this[parameterName];
        }
        public T Get<T>(string parameterName, T defaultValue)
        {
            if (!TryGetValue(parameterName, out object parameterValue))
            {
                parameterValue = defaultValue;
                this[parameterName] = parameterValue;
            }
            return (T)parameterValue;
        }

        public void Set<T>(string parameterName, T parameterValue)
        {
            this[parameterName] = parameterValue;
        }

    }
}
