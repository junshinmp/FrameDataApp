using FrameDataApp.Models;
using FrameDataApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace FrameDataApp.ViewModel
{
    public class MakeMoveViewModel : BaseViewModel
    {
        private readonly MoveService _service;

        private string _characterName = string.Empty;
        private string _moveName = string.Empty;
        private string _commandInput = string.Empty;

        private int _startUp;
        private int _active;
        private int _recovery;
        private int _onBlock;

        private CancelType? _selectedCancelType;

        public string CharacterName
        {
            get => _characterName;
            set { _characterName = value; OnPropertyChanged(nameof(CharacterName)); }
        }

        public string MoveName
        {
            get => _moveName;
            set { _moveName = value; OnPropertyChanged(nameof(MoveName)); }
        }

        public string CommandInput
        {
            get => _commandInput;
            set { _commandInput = value; OnPropertyChanged(nameof(CommandInput)); }
        }

        public int StartUp
        {
            get => _startUp;
            set { _startUp = value; OnPropertyChanged(nameof(StartUp)); }
        }

        public int Active
        {
            get => _active;
            set { _active = value; OnPropertyChanged(nameof(Active)); }
        }

        public int Recovery
        {
            get => _recovery;
            set { _recovery = value; OnPropertyChanged(nameof(Recovery)); }
        }

        public int OnBlock
        {
            get => _onBlock;
            set { _onBlock = value; OnPropertyChanged(nameof(OnBlock)); }
        }

        public CancelType? SelectedCancelType
        {
            get => _selectedCancelType;
            set { _selectedCancelType = value; OnPropertyChanged(nameof(SelectedCancelType)); }
        }

        public IEnumerable<CancelType> CancelOptions => Enum.GetValues(typeof(CancelType)).Cast<CancelType>();

        public ICommand SaveMoveCommand { get; }

        // Parameterless constructor for WPF XAML DataContext instantiation
        public MakeMoveViewModel() : this(ServiceStore.Instance.MoveService)
        {
        }

        public MakeMoveViewModel(MoveService moveService)
        {
            _service = moveService;
        }
    }
}