using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WpfApp43S.ViewModels
{
    public abstract class ModelViewModelBase : ViewModelBase, ICloneable, IDataErrorInfo, INotifyDataErrorInfo
    {
        protected readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public string Error { get; }

        public virtual string this[string property] => string.Empty;

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public virtual object Clone() => MemberwiseClone();

        public IEnumerable GetErrors(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                return _errors.ContainsKey(property) ? _errors[property] : null;
            }

            return _errors.Values;
        }

        protected void AddErrors(string property, IEnumerable<string> errors)
        {
            var changed = false;

            if (!_errors.ContainsKey(property))
            {
                _errors.Add(property, new List<string>());

                changed = true;
            }

            foreach (var error in errors)
            {
                if (_errors[property].Contains(error)) continue;

                _errors[property].Add(error);

                changed = true;
            }

            if (changed)
            {
                NotifyErrorsChanged(property);
            }
        }

        protected void ClearErrors(string property = "")
        {
            if (!string.IsNullOrEmpty(property))
            {
                _errors.Remove(property);
            }
            else
            {
                _errors.Clear();
            }

            NotifyErrorsChanged(property);
        }

        protected IEnumerable<string> GetErrorsFromAnnotations<T>(string property, T value)
        {
            var context = new ValidationContext(this) { MemberName = property };
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateProperty(value, context, results);

            return !isValid ? results.Select(r => r.ErrorMessage) : null;
        }

        protected void NotifyErrorsChanged(string property)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(property));
        }
    }
}
