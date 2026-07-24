using FrameDataApp.Commands;
using FrameDataApp.Services;
using FrameDataApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FrameDataApp.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly GameService _gameService;
        private readonly CharacterService _characterService;
        private readonly MoveService _moveService;

        public ICommand NavigateMakeGameCommand { get; }
        public ICommand NavigateMakeCharacterCommand { get; }
        public ICommand NavigateMakeMoveCommand { get; }

        public HomeViewModel(
            NavigationStore navigationStore,
            GameService gameService,
            CharacterService characterService,
            MoveService moveService)
        {
            _navigationStore = navigationStore;
            _gameService = gameService;
            _characterService = characterService;
            _moveService = moveService;

            NavigateMakeGameCommand = new NavigateCommand<MakeGameViewModel>(
                _navigationStore,
                () => new MakeGameViewModel(_navigationStore, _gameService));

            NavigateMakeCharacterCommand = new NavigateCommand<MakeCharacterViewModel>(
                _navigationStore,
                () => new MakeCharacterViewModel(_navigationStore, _characterService));

            NavigateMakeMoveCommand = new NavigateCommand<MakeMoveViewModel>(
                _navigationStore,
                () => new MakeMoveViewModel(_navigationStore, _moveService, _characterService));
        }

        public HomeViewModel() : this(
            new NavigationStore(),
            ServiceStore.Instance.GameService,
            ServiceStore.Instance.CharacterService,
            ServiceStore.Instance.MoveService)
        {
        }
    }
}
