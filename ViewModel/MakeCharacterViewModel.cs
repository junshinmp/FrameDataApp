using FrameDataApp.Commands;
using FrameDataApp.Services;
using FrameDataApp.Stores;
using System.Windows.Input;

namespace FrameDataApp.ViewModel
{
    public class MakeCharacterViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CharacterService _service;

        private string _name = string.Empty;
        private double _dashSpeed;
        private double _walkSpeed;

        // Main constructor used during runtime navigation
        public MakeCharacterViewModel(NavigationStore navigationStore, CharacterService characterService)
        {
            _navigationStore = navigationStore;
            _service = characterService;

            SubmitCommand = new MakeCharacterCommand(this, _service);

            // Command for a "Cancel" or "Back" button on MakeCharacterView
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(
                navigationStore,
                () => new HomeViewModel(
                    navigationStore,
                    ServiceStore.Instance.GameService,
                    characterService,
                    ServiceStore.Instance.MoveService));
        }

        // Parameterless constructor for XAML Designer / Fallback
        public MakeCharacterViewModel()
            : this(new NavigationStore(), ServiceStore.Instance.CharacterService)
        {
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public double WalkSpeed
        {
            get => _walkSpeed;
            set { _walkSpeed = value; OnPropertyChanged(nameof(WalkSpeed)); }
        }

        public double DashSpeed
        {
            get => _dashSpeed;
            set { _dashSpeed = value; OnPropertyChanged(nameof(DashSpeed)); }
        }

        public ICommand SubmitCommand { get; }
        public ICommand NavigateHomeCommand { get; }
    }
}