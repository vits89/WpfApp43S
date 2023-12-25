using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp43S.ViewModels;

public partial class StudentViewModel : ObservableValidator, ICloneable
{
    private string? _firstName;
    private string? _lastName;
    private int? _gender;
    private int? _age;

    public int Id { get; set; } = -1;

    [Required(ErrorMessage = "Вы не ввели имя")]
    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;

            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(Info));
        }
    }

    [Required(ErrorMessage = "Вы не ввели фамилию")]
    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;

            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Info));
        }
    }

    [Required(ErrorMessage = "Вы не указали пол")]
    public int? Gender
    {
        get => _gender;
        set
        {
            _gender = value;

            OnPropertyChanged(nameof(Gender));
            OnPropertyChanged(nameof(Info));
        }
    }

    [Range(16, 100, ErrorMessage = "Возраст должен находиться на отрезке [{1}, {2}]")]
    public int? Age
    {
        get => _age;
        set
        {
            _age = value;

            OnPropertyChanged(nameof(Age));
            OnPropertyChanged(nameof(Info));
        }
    }

    public string Info
    {
        get
        {
            var gender = string.Empty;

            if (Gender.HasValue)
            {
                gender = Gender.Value == 0 ? "мужчина" : "женщина";
            }

            if (!Age.HasValue)
            {
                return $"{FirstName} {LastName}, {gender}";
            }

            var age = Age.Value.ToString();

            age += (Age.Value % 10) switch
            {
                1 => " год",
                2 or 3 or 4 => " года",
                _ => " лет"
            };

            return $"{FirstName} {LastName}, {gender}, {age}";
        }
    }

    public virtual object Clone() => MemberwiseClone();
}
