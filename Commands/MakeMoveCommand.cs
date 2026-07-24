using FrameDataApp.Models;
using FrameDataApp.Services;
using FrameDataApp.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace FrameDataApp.Commands
{
    public class MakeMoveCommand : CommandBase
    {
        private readonly MakeMoveViewModel _viewModel;
        private readonly MoveService _moveService;

        public MakeMoveCommand(MakeMoveViewModel viewModel, MoveService moveService)
        {
            _viewModel = viewModel;
            _moveService = moveService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.CharacterName) &&
                   !string.IsNullOrWhiteSpace(_viewModel.MoveName) &&
                   !string.IsNullOrWhiteSpace(_viewModel.CommandInput) &&
                   base.CanExecute(parameter);
        }

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