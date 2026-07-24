using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FrameDataApp.Commands;
using FrameDataApp.Services;
using FrameDataApp.Stores;

namespace FrameDataApp.ViewModel
{
    public class NavBarViewModel : ViewModelBase
    {
     
        public ICommand NavigateGameList { get; }
        public ICommand NavigateCharacterList { get; }
        public ICommand NavigateMoveList { get; }
        public ICommand NavigateHomeCommand { get;  }

        public NavBarViewModel(
            NavigationStore navigationStore,
            GameService gameService,
            CharacterService characterService,
            MoveService moveService)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(
                navigationStore,
                () => new HomeViewModel(navigationStore, gameService, characterService, moveService));

            NavigateGameList = NavigateHomeCommand;
            NavigateCharacterList = NavigateHomeCommand;
            NavigateMoveList = NavigateHomeCommand;
            /**
            NavigateGameList = new NavigateCommand<GamesListViewModel>(
                navigationStore,
                () => new GamesListViewModel(navigationStore, gameService));

            NavigateCharacterList = new NavigateCommand<CharactersListViewModel>(
                navigationStore,
                () => new CharactersListViewModel(navigationStore, characterService));

            NavigateMoveList = new NavigateCommand<MovesListViewModel>(
                navigationStore,
                () => new MovesListViewModel(navigationStore, moveService, characterService));
            */
        }
    }
}
