using FrameDataApp.Services;
using FrameDataApp.ViewModel.MakeViewModels;
using System.ComponentModel;
using System.Windows;

namespace FrameDataApp.Commands
{
    /// <summary>
    /// Class <c>MakeCharacterCommand</c> inherits from <c>CommandBase</c>,
    /// ensuring that all properties of making a character are collected
    /// properly from the <c>MakeCharacterView</c>.
    /// 
    /// This inherits all properties from the <c>CommandBase</c> class.
    /// </summary>
    public class MakeCharacterCommand : CommandBase
    {
        /// <summary>
        /// Holds the local viewmodel and CharacterService
        /// in for executing and transfering data properly.
        /// </summary>
        private readonly MakeCharacterViewModel _viewModel;
        private readonly CharacterService _characterService;

        /// <summary>
        /// Default Constructor for <c>MakeCharacterCommand</c>, requiring
        /// a pass of viewModel, characterService for proper functionality.
        /// </summary>
        /// <param name="viewModel"></param> Local Viewmodel for making Character
        /// <param name="characterService"></param> Local Service for Character functions
        public MakeCharacterCommand(MakeCharacterViewModel viewModel, CharacterService characterService)
        {
            _viewModel = viewModel;
            _characterService = characterService;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        /// <summary>
        /// Checks if the name field is properly implemented,
        /// greying out the button if the name field is not
        /// filled out in the form.
        /// </summary>
        /// <param name="parameter"></param> Name field in the form.
        /// <returns></returns> Whether the field is empty or not.
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Name) && base.CanExecute(parameter);
        }

        /// <summary>
        /// Executes the process of creating a new character.
        /// Validates input, checks for duplicate entries via <c>CharacterService</c>,
        /// saves the record, and clears the viewmodel fields upon success.
        /// </summary>
        /// <param name="parameter">Optional parameter passed from the UI (unused in this command).</param>
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

        /// <summary>
        /// Handles property change notifications from the <c>MakeCharacterViewModel</c>.
        /// Specifically monitors the <c>Name</c> property to trigger <c>OnCanExecutedChanged</c>,
        /// enabling or disabling the save button in real time.
        /// </summary>
        /// <param name="sender">The object that raised the event (the ViewModel).</param>
        /// <param name="e">Event args containing the name of the property that changed.</param>
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeCharacterViewModel.Name))
            {
                OnCanExecutedChanged();
            }
        }
    }
}