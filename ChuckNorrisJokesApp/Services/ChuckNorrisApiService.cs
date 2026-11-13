using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ChuckNorrisJokesApp.Models;
using ChuckNorrisJokesApp.Services.Interfaces;


namespace ChuckNorrisJokesApp.Services
{
    internal class ChuckNorrisApiService : IChuckNorrisApiService

    {
        private HttpClient _httpClient;
        private IConfigurationService _configurationService;

        public ChuckNorrisApiService(IConfigurationService service)
        {

            _configurationService = service;

            _httpClient = new HttpClient();

            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<string> GetJokeAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_configurationService.ChuckNorrisApiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    throw new JokeApiException($"The server returned an error ({(int)response.StatusCode}).");
                }

                string jsonContent = await response.Content.ReadAsStringAsync();

                ChuckNorrisJoke? joke = JsonSerializer.Deserialize<ChuckNorrisJoke>(jsonContent);

                if (joke == null || string.IsNullOrWhiteSpace(joke.value))
                {
                    throw new JokeApiException("The server returned an invalid response.");
                }

                return joke.value;
            }
            catch (JokeApiException)
            {
                throw;
            }

            catch (HttpRequestException)
            {
                throw new JokeApiException("Could not reach the server. Check your internet connection.");
            }
            catch (JsonException)
            {
                throw new JokeApiException("The server sent data in an unexpected format.");
            }
        }
    }
}