using ChuckNorrisJokesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ChuckNorrisJokesApp.Services;
using System.Windows.Input;
using ChuckNorrisJokesApp.Services.Interfaces;

namespace ChuckNorrisJokesApp.ViewModels
{
    public class JokesViewModel : BaseViewModel
    {
        private readonly IChuckNorrisApiService _chuckNorrisApiService;
        private readonly IDadJokeApiService _dadJokeApiService;

        private string? _chuckNorrisJoke;
        public string? ChuckNorrisJoke
        {
            get => _chuckNorrisJoke;
            set { _chuckNorrisJoke = value; OnPropertyChanged(); }
        }

        private string? _dadJoke;
        public string? DadJoke
        {
            get => _dadJoke;
            set { _dadJoke = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                OnPropertyChanged();

                // Re-evaluate CanExecute so the buttons disable/enable
                GetChuckNorrisJokeCommand.ChangeCanExecute();
                GetDadJokeCommand.ChangeCanExecute();
            }
        }

        public Command GetChuckNorrisJokeCommand { get; }
        public Command GetDadJokeCommand { get; }

        public JokesViewModel(
            IChuckNorrisApiService chuckNorrisApiService,
            IDadJokeApiService dadJokeApiService)
        {
            _chuckNorrisApiService = chuckNorrisApiService;
            _dadJokeApiService = dadJokeApiService;

            GetChuckNorrisJokeCommand = new Command(
                async () => await FetchChuckNorrisJokeAsync(),
                () => !IsLoading);

            GetDadJokeCommand = new Command(
                async () => await FetchDadJokeAsync(),
                () => !IsLoading);
        }

        private async Task FetchChuckNorrisJokeAsync()
        {
            IsLoading = true;
            try
            {
                ChuckNorrisJoke = await _chuckNorrisApiService.GetJokeAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task FetchDadJokeAsync()
        {
            IsLoading = true;
            try
            {
                DadJoke = await _dadJokeApiService.GetJokeAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
