using FrameDataApp.Commands;
using FrameDataApp.Services;
using System.Windows.Input;

namespace FrameDataApp.ViewModel
{
    public class MakeGameViewModel : BaseViewModel
    {
        private readonly GameService _service;

        private string _title = string.Empty;
        private string _developer = string.Empty;
        private int _releaseYear;

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

        // Parameterless constructor for WPF XAML
        public MakeGameViewModel() : this(ServiceStore.Instance.GameService)
        {
        }

        public MakeGameViewModel(GameService gameService)
        {
            _service = gameService;
            SubmitCommand = new MakeGameCommand(this, _service);
        }
    }
}