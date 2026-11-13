using System;
using System.Collections.Generic;
using System.Text;

namespace ChuckNorrisJokesApp.Services
{
    public class JokeApiException : Exception
    {
        public JokeApiException(string message) : base(message)
        {
        }
    }
}
