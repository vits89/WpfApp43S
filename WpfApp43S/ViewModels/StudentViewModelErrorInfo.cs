using System.ComponentModel;

namespace WpfApp43S.ViewModels
{
    public partial class StudentViewModel : IDataErrorInfo
    {
        public string Error { get; }

        public string this[string property]
        {
            get
            {
                switch (property)
                {
                    case nameof(FirstName):
                        ValidateProperty(FirstName, property);

                        break;
                    case nameof(LastName):
                        ValidateProperty(LastName, property);

                        break;
                    case nameof(Gender):
                        ValidateProperty(Gender, property);

                        break;
                    case nameof(Age):
                        ValidateProperty(Age, property);

                        break;
                    default:
                        return string.Empty;
                }

                return string.Empty;
            }
        }
    }
}
