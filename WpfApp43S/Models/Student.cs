using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfApp43S.Models
{
    public partial class Student : BaseModel, ICloneable, INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private int? _gender;
        private int? _age;

        public int Id { get; set; } = -1;

        [Required(ErrorMessage = "Вы не ввели имя")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;

                NotifyPropertyChanged(nameof(FirstName));
                NotifyPropertyChanged(nameof(Info));
            }
        }

        [Required(ErrorMessage = "Вы не ввели фамилию")]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;

                NotifyPropertyChanged(nameof(LastName));
                NotifyPropertyChanged(nameof(Info));
            }
        }

        [Required(ErrorMessage = "Вы не указали пол")]
        public int? Gender
        {
            get => _gender;
            set
            {
                _gender = value;

                NotifyPropertyChanged(nameof(Gender));
                NotifyPropertyChanged(nameof(Info));
            }
        }

        [Range(16, 100, ErrorMessage = "Возраст должен находиться на отрезке [16, 100]")]
        public int? Age
        {
            get => _age;
            set
            {
                _age = value;

                NotifyPropertyChanged(nameof(Age));
                NotifyPropertyChanged(nameof(Info));
            }
        }

        public string Info
        {
            get
            {
                var gender = Gender.HasValue ? (Gender.Value == 0 ? "мужчина" : "женщина") : string.Empty;

                if (!Age.HasValue)
                {
                    return $"{FirstName} {LastName}, {gender}";
                }

                var age = Age.Value.ToString();
                var ageLastDigit = Age.Value % 10;

                if (ageLastDigit == 1)
                {
                    age += " год";
                }
                else if (Array.Exists(new[] { 2, 3, 4 }, e => e == ageLastDigit))
                {
                    age += " года";
                }
                else
                {
                    age += " лет";
                }

                return $"{FirstName} {LastName}, {gender}, {age}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone() => MemberwiseClone();

        private void NotifyPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
