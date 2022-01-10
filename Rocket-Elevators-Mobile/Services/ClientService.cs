using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Rocket_Elevators_Mobile.Services
{
    class ClientService : IClientService
    {
        // We are trying to only have to create the client once for the whole app.
        private readonly HttpClient _client = new HttpClient();

        private readonly string _baseUrl = "https://rocketapiyenxm.azurewebsites.net/api/";

        public async Task<bool> VerifyEmployeeEmail(string email)
        {
            // Api call to check if an employee with the entered email exist.

            // Clear previous headers
            _client.DefaultRequestHeaders.Accept.Clear();
            // Important to add if or it will break the api
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Will return true or false depending on whether the employee exists or not.
            Task<string> request = _client.GetStringAsync(_baseUrl + $"Employees/verification/{email}");

            string response = await request;

            return Convert.ToBoolean(response);


        }
    }
}
