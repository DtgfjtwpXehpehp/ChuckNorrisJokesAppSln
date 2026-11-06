using ChuckNorrisJokesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChuckNorrisJokesApp.Services
{
    internal class ConfigurationService : IConfigurationService
    {
        public string ChuckNorrisApiUrl { get; }

        public ConfigurationService()
        {
            ChuckNorrisApiUrl = "https://api.chucknorris.io/jokes/random";
        }
    }
}
