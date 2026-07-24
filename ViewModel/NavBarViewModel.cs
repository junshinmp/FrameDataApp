using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FrameDataApp.ViewModel
{
    public class NavBarViewModel : ViewModelBase
    {
     


        public ICommand NavigateGameList { get; }
        public ICommand NavigateCharacterList { get; }
        public ICommand NavigateMoveList { get; }
        public ICommand NavigateHomeCommand { get;  }
    }
}
