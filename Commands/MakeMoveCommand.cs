using FrameDataApp.Models;
using FrameDataApp.Services;
using FrameDataApp.ViewModel.MakeViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace FrameDataApp.Commands
{
    /// <summary>
    /// Class <c>MakeMoveCommand</c> inherits from <c>CommandBase</c>,
    /// ensuring that all properties required to create a new move and its frame data
    /// are collected properly from the <c>MakeMoveView</c>.
    /// 
    /// This inherits all properties from the <c>CommandBase</c> class.
    /// </summary>
    public class MakeMoveCommand : CommandBase
    {
        /// <summary>
        /// Holds the local viewmodel and MoveService
        /// for executing and transferring move data properly.
        /// </summary>
        private readonly MakeMoveViewModel _viewModel;
        private readonly MoveService _moveService;

        /// <summary>
        /// Default Constructor for <c>MakeMoveCommand</c>, requiring
        /// a pass of viewModel and moveService for proper functionality.
        /// Subscribes to the ViewModel's property change notifications.
        /// </summary>
        /// <param name="viewModel">Local ViewModel containing form state for creating a move.</param>
        /// <param name="moveService">Local Service for executing move database and storage operations.</param>
        public MakeMoveCommand(MakeMoveViewModel viewModel, MoveService moveService)
        {
            _viewModel = viewModel;
            _moveService = moveService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        /// <summary>
        /// Checks if all required text fields (CharacterName, MoveName, CommandInput)
        /// are populated, greying out the button if any are missing or whitespace.
        /// </summary>
        /// <param name="parameter">Optional parameter passed from the UI (unused in this command).</param>
        /// <returns>True if all required fields are non-empty; otherwise false.</returns>
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.CharacterName) &&
                   !string.IsNullOrWhiteSpace(_viewModel.MoveName) &&
                   !string.IsNullOrWhiteSpace(_viewModel.CommandInput) &&
                   base.CanExecute(parameter);
        }

        /// <summary>
        /// Executes the process of creating and assigning a move to a specified character.
        /// Constructs the <c>FrameData</c> object and cancel options from the ViewModel,
        /// submits the data via <c>MoveService</c>, provides user feedback, and resets input fields upon success.
        /// </summary>
        /// <param name="parameter">Optional parameter passed from the UI (unused in this command).</param>
        public override void Execute(object? parameter)
        {
            var frameData = new FrameData
            {
                StartUp = _viewModel.StartUp,
                Active = _viewModel.Active,
                Recovery = _viewModel.Recovery,
                OnBlock = _viewModel.OnBlock
            };

            var cancelTypes = _viewModel.SelectedCancelType.HasValue
                ? new List<CancelType> { _viewModel.SelectedCancelType.Value }
                : new List<CancelType>();

            bool success = _moveService.AddMoveToCharacter(
                _viewModel.CharacterName,
                _viewModel.MoveName,
                _viewModel.CommandInput,
                frameData,
                cancelTypes
            );

            if (success)
            {
                MessageBox.Show($"Successfully added move '{_viewModel.MoveName}' to {_viewModel.CharacterName}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset Move input fields
                _viewModel.MoveName = string.Empty;
                _viewModel.CommandInput = string.Empty;
                _viewModel.StartUp = 0;
                _viewModel.Active = 0;
                _viewModel.Recovery = 0;
                _viewModel.OnBlock = 0;
                _viewModel.SelectedCancelType = null;
            }
            else
            {
                MessageBox.Show($"Failed to add move. '{_viewModel.MoveName}' might already exist for {_viewModel.CharacterName}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles property change notifications from the <c>MakeMoveViewModel</c>.
        /// Specifically monitors essential input fields (<c>CharacterName</c>, <c>MoveName</c>, and <c>CommandInput</c>)
        /// to trigger <c>OnCanExecutedChanged</c>, enabling or disabling the save button in real time.
        /// </summary>
        /// <param name="sender">The object that raised the event (the ViewModel).</param>
        /// <param name="e">Event args containing the name of the property that changed.</param>
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeMoveViewModel.CharacterName) ||
                e.PropertyName == nameof(MakeMoveViewModel.MoveName) ||
                e.PropertyName == nameof(MakeMoveViewModel.CommandInput))
            {
                OnCanExecutedChanged();
            }
        }
    }
}