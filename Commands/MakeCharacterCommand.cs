using FrameDataApp.Services;
using FrameDataApp.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace FrameDataApp.Commands
{
    public class MakeCharacterCommand : CommandBase
    {
        private readonly MakeCharacterViewModel _viewModel;
        private readonly CharacterService _characterService;

        public MakeCharacterCommand(MakeCharacterViewModel viewModel, CharacterService characterService)
        {
            _viewModel = viewModel;
            _characterService = characterService;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Name) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Name))
            {
                MessageBox.Show("Cannot add character: Name is blank.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_characterService.CharacterExists(_viewModel.Name))
            {
                MessageBox.Show($"Character '{_viewModel.Name}' already exists!", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Attempt save
            bool success = _characterService.AddCharacter(
                _viewModel.Name,
                _viewModel.WalkSpeed,
                _viewModel.DashSpeed
            );

            if (success)
            {
                MessageBox.Show($"Successfully added '{_viewModel.Name}'!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _viewModel.Name = string.Empty;
                _viewModel.WalkSpeed = 0;
                _viewModel.DashSpeed = 0;
            }
            else
            {
                MessageBox.Show("Failed to add character. Check service constraints.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeCharacterViewModel.Name))
            {
                OnCanExecutedChanged();
            }
        }
    }
}