﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        private StudentViewModel _selectedStudent;

        public ObservableCollection<StudentViewModel> Students { get; }

        public Dictionary<string, int?> GenderOptions { get; }

        public StudentViewModel SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = (StudentViewModel)value?.Clone();

                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            GenderOptions = new Dictionary<string, int?>
            {
                [""] = null,
                ["мужчина"] = 0,
                ["женщина"] = 1
            };

            Students = new ObservableCollection<StudentViewModel>(_mapper
                .Map<IEnumerable<StudentViewModel>>(_repository.GetAll()));
        }
    }
}
