using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ChuckNorrisJokesApp.Models;
using ChuckNorrisJokesApp.Services.Interfaces;

namespace ChuckNorrisJokesApp.Services
{
    internal class DadJokeApiService : IDadJokeApiService
    {

        private HttpClient _httpClient;
        private IConfigurationService _configurationService;

        public DadJokeApiService(IConfigurationService service)
        {

            _configurationService = service;

            _httpClient = new HttpClient();

            _httpClient.Timeout = TimeSpan.FromSeconds(10);

            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
 );
        }

        public async Task<string> GetJokeAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_configurationService.DadJokeApiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    throw new JokeApiException($"The server returned an error ({(int)response.StatusCode}).");
                }

                string jsonContent = await response.Content.ReadAsStringAsync();

                DadJoke? joke = JsonSerializer.Deserialize<DadJoke>(jsonContent);

                if (joke == null || string.IsNullOrWhiteSpace(joke.joke))
                {
                    throw new JokeApiException("The server returned an invalid response.");
                }

                return joke.joke;
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