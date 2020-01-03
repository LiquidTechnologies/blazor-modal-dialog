using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.ModalDialog
{
    public class ModalDialogParameters : Dictionary<string, object>
    {
        /// <summary>
        /// Gets a parameter of a given type (either succeeds or throws, never returns an invalid value)
        /// </summary>
        /// <typeparam name="T">The expected type of the parameter value</typeparam>
        /// <param name="parameterName">The name of the parameter to retrieve the value for</param>
        /// <returns>The value of the <paramref name="parameterName"/> requested</returns>
        /// <exception cref="KeyNotFoundException">If the parameter does not exist (consider using <see cref="Get{T}(string, T)"/>)</exception>
        /// <exception cref="InvalidCastException">If the parameter exists but is of the wrong type (<typeparamref name="T"/>)</exception>
        public T Get<T>(string parameterName)
        {
            if (!this.ContainsKey(parameterName))
            {
                throw new KeyNotFoundException($"{parameterName} does not exist in modal parameters");
            }

            return (T)this[parameterName];
        }

        /// <summary>
        /// Gets a parameter of a given type, uses the <paramref name="defaultValue"/> if the parameter does not exist
        /// (either succeeds or throws, never returns an invalid value)
        /// </summary>
        /// <typeparam name="T">The expected type of the parameter value</typeparam>
        /// <param name="parameterName">The name of the parameter to retrieve the value for</param>
        /// <param name="defaultValue">If a parameter <paramref name="parameterName"/> does not exist then this value is returned instead</param>
        /// <returns>The value of the <paramref name="parameterName"/> requested (or the <paramref name="defaultValue"/> if its not in the collection</returns>
        /// <exception cref="InvalidCastException">If the parameter exists but is of the wrong type (<typeparamref name="T"/>)</exception>
        public T Get<T>(string parameterName, T defaultValue)
        {
            if (!TryGetValue(parameterName, out object parameterValue))
            {
                parameterValue = defaultValue;
                this[parameterName] = parameterValue;
            }
            return (T)parameterValue;
        }

        /// <summary>
        /// Sets a value into the parameter collection
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="parameterValue"/> being stored</typeparam>
        /// <param name="parameterName">The name of the parameter (if it already exists it will be overwritten)</param>
        /// <param name="parameterValue">The value to be associated with the <paramref name="parameterName"/></param>
        public void Set<T>(string parameterName, T parameterValue)
        {
            this[parameterName] = parameterValue;
        }

    }
}
