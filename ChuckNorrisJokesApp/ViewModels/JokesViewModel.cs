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


        public ICommand GetChuckNorrisJokeCommand { get; set; }

        public ICommand GetDadJokeCommand { get; set; }

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
        }

        private async Task FetchChuckNorrisJokeAsync()
        {
            ChuckNorrisJoke =
                await _chuckNorrisApiService.GetJokeAsync();
        }

        private async Task FetchDadJokeAsync()
        {
            DadJoke =
                await _dadJokeApiService.GetJokeAsync();
        }

    }
}
