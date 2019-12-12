using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp43S.Models
{
    public partial class Student : IDataErrorInfo
    {
        public string Error { get; }

        public string this[string property]
        {
            get
            {
                IEnumerable<string> errors;

                switch (property)
                {
                    case (nameof(FirstName)):
                        errors = GetErrorsFromAnnotations(property, FirstName);

                        break;
                    case (nameof(LastName)):
                        errors = GetErrorsFromAnnotations(property, LastName);

                        break;
                    case (nameof(Gender)):
                        errors = GetErrorsFromAnnotations(property, Gender);

                        break;
                    case (nameof(Age)):
                        errors = GetErrorsFromAnnotations(property, Age);

                        break;
                    default:
                        return string.Empty;
                }

                if (errors != null)
                {
                    AddErrors(property, errors);
                }
                else
                {
                    ClearErrors(property);
                }

                return string.Empty;
            }
        }
    }
}
