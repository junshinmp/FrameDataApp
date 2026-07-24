using FrameDataApp.Commands;
using FrameDataApp.Models;
using FrameDataApp.Services;
using FrameDataApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace FrameDataApp.ViewModel
{
    public class MakeMoveViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly MoveService _moveService;
        private readonly CharacterService _characterService;

        private string _characterName = string.Empty;
        private string _moveName = string.Empty;
        private string _commandInput = string.Empty;

        private int _startUp;
        private int _active;
        private int _recovery;
        private int _onBlock;

        private CancelType? _selectedCancelType;

        // Main Constructor used during Navigation
        public MakeMoveViewModel(
            NavigationStore navigationStore,
            MoveService moveService,
            CharacterService characterService)
        {
            _navigationStore = navigationStore;
            _moveService = moveService;
            _characterService = characterService;

            SaveMoveCommand = new MakeMoveCommand(this, _moveService);

            // Command to navigate back home (optional / for cancel buttons)
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(
                navigationStore,
                () => new HomeViewModel(
                    navigationStore,
                    ServiceStore.Instance.GameService,
                    characterService,
                    moveService));
        }

        // Parameterless constructor for XAML Designer / Fallback
        public MakeMoveViewModel()
            : this(new NavigationStore(), ServiceStore.Instance.MoveService, ServiceStore.Instance.CharacterService)
        {
        }

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

        public List<string> AvailableCharacters => _characterService.GetAllCharacterNames();

        public ICommand SaveMoveCommand { get; private set; }
        public ICommand NavigateHomeCommand { get; }

        public void RefreshCharacters()
        {
            OnPropertyChanged(nameof(AvailableCharacters));
        }
    }
}