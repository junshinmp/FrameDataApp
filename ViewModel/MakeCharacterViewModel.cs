using FrameDataApp.Services;
using System.Windows.Input;

namespace FrameDataApp.ViewModel
{
    public class MakeCharacterViewModel : BaseViewModel
    {
        private readonly CharacterService _service;
        private string _name = string.Empty;
        private double _dashSpeed;
        private double _walkSpeed;

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

        // Parameterless constructor for WPF
        public MakeCharacterViewModel() : this(ServiceStore.Instance.CharacterService)
        {
        }

        public MakeCharacterViewModel(CharacterService characterService)
        {
            _service = characterService;
        }
    }
}