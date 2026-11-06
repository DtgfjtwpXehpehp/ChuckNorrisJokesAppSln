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
        }

        public async Task<string>  GetJokeAsync()
        {
           HttpResponseMessage response =  await _httpClient.GetAsync(_configurationService.ChuckNorrisApiUrl);


            if(response.IsSuccessStatusCode)
            {

                string jsonContent  =  await response.Content.ReadAsStringAsync();

                //ChuckNorrisJoke joke = JsonConvert.DeserializeObject(jsonContent) as ChuckNorrisJoke;


                ChuckNorrisJoke joke = JsonSerializer.Deserialize<ChuckNorrisJoke>(jsonContent);

                if(joke != null)
                {
                    return joke.value;
                }
            }
           
            return null;
        }
    }
}
