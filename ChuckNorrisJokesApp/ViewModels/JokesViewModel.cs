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

        private IPopUp _popupService;

        private string _chuckNorrisJoke;

        public string ChuckNorrisJoke
        {
            get { return _chuckNorrisJoke; }
            set { _chuckNorrisJoke = value;

                OnPropertyChanged();
            }
        }

        public ICommand GetJokeCommand { get; set; }

        public JokesViewModel(IChuckNorrisApiService service)
        {
            _chuckNorrisApiService = service;

       

            GetJokeCommand = new Command(FatchJoke);
        }

        private async void FatchJoke(object param)
        {

            try
            {

                //throw new HttpRequestException("BOOM");
          ChuckNorrisJoke =  await _chuckNorrisApiService.GetJokeAsync();

            }
            catch(Exception ex)
            {
                await _popupService.ShowPopUpAsync("Error", ex.Message, "OK");
            }


        }
    }
}
