using FrameDataApp.Services;
using FrameDataApp.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace FrameDataApp.Commands
{
    /// <summary>
    /// Class <c>MakeGameCommand</c> inherits from <c>CommandBase</c>,
    /// collecting required parameters from <c>MakeGameViewModel</c> to register
    /// a new fighting game title into the application.
    /// </summary>
    public class MakeGameCommand : CommandBase
    {
        private readonly MakeGameViewModel _viewModel;
        private readonly GameService _gameService;

        /// <summary>
        /// Initializes a new instance of <c>MakeGameCommand</c>.
        /// Subscribes to ViewModel property notifications to enable/disable the submit button dynamically.
        /// </summary>
        /// <param name="viewModel">The ViewModel containing the form input state.</param>
        /// <param name="gameService">The service handling game storage and operations.</param>
        public MakeGameCommand(MakeGameViewModel viewModel, GameService gameService)
        {
            _viewModel = viewModel;
            _gameService = gameService;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        /// <summary>
        /// Checks if the title field is populated, enabling or greying out the submit button.
        /// </summary>
        /// <param name="parameter">Optional command parameter from the UI (unused).</param>
        /// <returns>True if the game title is non-whitespace; otherwise false.</returns>
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Title) && base.CanExecute(parameter);
        }

        /// <summary>
        /// Executes validation checks and submits the new game to <c>GameService</c>.
        /// Displays user feedback and clears inputs upon success.
        /// </summary>
        /// <param name="parameter">Optional command parameter from the UI (unused).</param>
        public override void Execute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Title))
            {
                MessageBox.Show("Cannot add Game: Title is blank.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_gameService.GameExists(_viewModel.Title))
            {
                MessageBox.Show($"Game '{_viewModel.Title}' already exists!", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool success = _gameService.AddGame(
                _viewModel.Title,
                _viewModel.Developer,
                _viewModel.ReleaseYear
            );

            if (success)
            {
                MessageBox.Show($"Successfully added '{_viewModel.Title}'!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _viewModel.Title = string.Empty;
                _viewModel.Developer = string.Empty;
                _viewModel.ReleaseYear = 0;
            }
            else
            {
                MessageBox.Show("Failed to add Game. Check service constraints.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles property change notifications from the <c>MakeGameViewModel</c>.
        /// Specifically monitors the <c>Title</c> property to trigger <c>OnCanExecutedChanged</c>,
        /// enabling or disabling the save button in real time.
        /// </summary>
        /// <param name="sender">The object that raised the event (the ViewModel).</param>
        /// <param name="e">Event args containing the name of the property that changed.</param>
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeGameViewModel.Title))
            {
                OnCanExecutedChanged();
            }
        }
    }
}