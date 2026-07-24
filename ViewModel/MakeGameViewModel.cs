using FrameDataApp.Commands;
using FrameDataApp.Services;
using FrameDataApp.Stores;
using System.Windows.Input;

namespace FrameDataApp.ViewModel
{
    public class MakeGameViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly GameService _service;

        private string _title = string.Empty;
        private string _developer = string.Empty;
        private int _releaseYear;

        // Main constructor used during runtime navigation
        public MakeGameViewModel(NavigationStore navigationStore, GameService gameService)
        {
            _navigationStore = navigationStore;
            _service = gameService;

            SubmitCommand = new MakeGameCommand(this, _service);

            // Command for a "Cancel" or "Back" button on MakeGameView
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(
                navigationStore,
                () => new HomeViewModel(
                    navigationStore,
                    gameService,
                    ServiceStore.Instance.CharacterService,
                    ServiceStore.Instance.MoveService));
        }

        // Parameterless constructor for XAML Designer / Fallback
        public MakeGameViewModel()
            : this(new NavigationStore(), ServiceStore.Instance.GameService)
        {
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Developer
        {
            get => _developer;
            set { _developer = value; OnPropertyChanged(nameof(Developer)); }
        }

        public int ReleaseYear
        {
            get => _releaseYear;
            set { _releaseYear = value; OnPropertyChanged(nameof(ReleaseYear)); }
        }

        public ICommand SubmitCommand { get; }
        public ICommand NavigateHomeCommand { get; }
    }
}