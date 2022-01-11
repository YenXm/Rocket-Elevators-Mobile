﻿using Rocket_Elevators_Mobile.Models;
using System;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Rocket_Elevators_Mobile.Services
{
    class ClientService : IClientService
    {
        // We are trying to only have to create the client once for the whole app.
        private readonly HttpClient _client = new HttpClient();

        private readonly string _baseUrl = "https://rocketapiyenxm.azurewebsites.net/api/";

        private void SetClientHeader()
        {
            // Clear previous headers
            _client.DefaultRequestHeaders.Accept.Clear();
            // Important to add if or it will break the api
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> VerifyEmployeeEmail(string email)
        {
            // Api call to check if an employee with the entered email exist.

            SetClientHeader();

            // Will return true or false depending on whether the employee exists or not.
            string response = await _client.GetStringAsync(_baseUrl + $"Employees/verification/{email}");


            return Convert.ToBoolean(response);


        }

        public ElevatorList GetListOfElevatorOffline()
        {
            // Api call to get the list of all elevator not running (not restricted to only offline elevator).

            SetClientHeader();

            // Map the json object from the request to the ElevatorList model.
            var elevatorList = _client.GetFromJsonAsync<ElevatorList>(_baseUrl + "elevators/elevators-not-in-use").Result;
            return elevatorList;
        }
    }
}
