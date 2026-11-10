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

            _httpClient.DefaultRequestHeaders.Accept.Add( new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
 );
        }

        public async Task<string> GetJokeAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_configurationService.DadJokeApiUrl);


            if (response.IsSuccessStatusCode)
            {

                string jsonContent = await response.Content.ReadAsStringAsync();

                //ChuckNorrisJoke joke = JsonConvert.DeserializeObject(jsonContent) as ChuckNorrisJoke;


                DadJoke joke = JsonSerializer.Deserialize<DadJoke>(jsonContent);

                if (joke != null)
                {
                    return joke.joke;
                }
            }

            return null;
        }
    }
}
