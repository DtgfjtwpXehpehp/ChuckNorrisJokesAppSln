using System;
using System.Collections.Generic;
using System.Text;

namespace ChuckNorrisJokesApp.Services.Interfaces
{
    internal interface IConfigurationService
    {
        string ChuckNorrisApiUrl { get; }

        string DadJokeApiUrl { get; }
    }
}
