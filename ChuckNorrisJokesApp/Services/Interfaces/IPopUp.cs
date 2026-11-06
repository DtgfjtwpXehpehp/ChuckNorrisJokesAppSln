using System;
using System.Collections.Generic;
using System.Text;

namespace ChuckNorrisJokesApp.Services.Interfaces
{
    internal interface IPopUp
    {
        Task ShowPopUpAsync(string title, string message, string buttons);
    }
}
