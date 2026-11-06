using ChuckNorrisJokesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChuckNorrisJokesApp.Services
{
    internal class MauPopupService : IPopUp
    {
        public async Task ShowPopUpAsync(string title, string message, string buttons)
        {
            await Shell.Current.DisplayAlert(title, message, buttons);
        }
    }
}
