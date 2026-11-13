using ChuckNorrisJokesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ChuckNorrisJokesApp.Services;
using System.Windows.Input;

namespace ChuckNorrisJokesApp.ViewModels
{
    public class JokesViewModel : BaseViewModel
    {
        private IChuckNorrisApiService _chuckNorrisApiService;

        private IDadJokeApiService _dadJokeApiService;

        private IPopUp _popupService;

        private string _chuckNorrisJoke;

        public string ChuckNorrisJoke
        {
            get { return _chuckNorrisJoke; }
            set
            {
                _chuckNorrisJoke = value;

                OnPropertyChanged();
            }
        }

        private string _dadJoke;

        public string DadJoke
        {
            get { return _dadJoke; }
            set
            {
                _dadJoke = value;

                OnPropertyChanged();
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;

                OnPropertyChanged();
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(ErrorMessage); }
        }


        public ICommand GetChuckNorrisJokeCommand { get; set; }

        public ICommand GetDadJokeCommand { get; set; }

        public ICommand GetBothJokesCommand { get; set; }

        public JokesViewModel(
            IChuckNorrisApiService chuckNorrisApiService,
            IDadJokeApiService dadJokeApiService)
        {
            _chuckNorrisApiService = chuckNorrisApiService;
            _dadJokeApiService = dadJokeApiService;

            GetChuckNorrisJokeCommand = new Command(async () =>
                await FetchChuckNorrisJokeAsync());

            GetDadJokeCommand = new Command(async () =>
                await FetchDadJokeAsync());

            GetBothJokesCommand = new Command(async () =>
                await FetchBothJokesAsync());
        }

        private async Task FetchChuckNorrisJokeAsync()
        {
            if (IsLoading) return;

            IsLoading = true;
            ErrorMessage = null;

            try
            {
                ChuckNorrisJoke =
                    await _chuckNorrisApiService.GetJokeAsync();
            }
            catch (JokeApiException ex)
            {
                ErrorMessage = $"Chuck Norris joke: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task FetchDadJokeAsync()
        {
            if (IsLoading) return;

            IsLoading = true;
            ErrorMessage = null;

            try
            {
                DadJoke =
                    await _dadJokeApiService.GetJokeAsync();
            }
            catch (JokeApiException ex)
            {
                ErrorMessage = $"Dad joke: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task FetchBothJokesAsync()
        {
            if (IsLoading) return;

            IsLoading = true;
            ErrorMessage = null;

            try
            {
                // Start both requests so they run at the same time
                Task<string> chuckNorrisTask = _chuckNorrisApiService.GetJokeAsync();
                Task<string> dadJokeTask = _dadJokeApiService.GetJokeAsync();

                List<string> errors = new List<string>();

                try
                {
                    ChuckNorrisJoke = await chuckNorrisTask;
                }
                catch (JokeApiException ex)
                {
                    errors.Add($"Chuck Norris joke: {ex.Message}");
                }

                try
                {
                    DadJoke = await dadJokeTask;
                }
                catch (JokeApiException ex)
                {
                    errors.Add($"Dad joke: {ex.Message}");
                }

                if (errors.Count > 0)
                {
                    ErrorMessage = string.Join(Environment.NewLine, errors);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}