using FrameDataApp.Services;
using FrameDataApp.Stores;
using FrameDataApp.ViewModel;
using FrameDataApp.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FrameDataApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private readonly NavigationStore _navigationStore;
    private readonly CharacterService _characterService;
    private readonly GameService _gameService;
    private readonly MoveService _moveService;

    public App()
    {
        _navigationStore = new NavigationStore();
        _characterService = ServiceStore.Instance.CharacterService;
        _gameService = ServiceStore.Instance.GameService;
        _moveService = ServiceStore.Instance.MoveService;
    }

    protected override void OnStartup(StartupEventArgs e)
    {

        _navigationStore.CurrentViewModel = new HomeViewModel(
            _navigationStore,
            _gameService,
            _characterService,
            _moveService);

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };

        MainWindow.Show();

        base.OnStartup(e);
    }
}

