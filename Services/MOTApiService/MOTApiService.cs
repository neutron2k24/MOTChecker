using System.Text.Json;
using MOTChecker.Services.MOTApiService.Models;
using MOTChecker.Services.MOTApiService.Interfaces;
using System.Text.Json.Nodes;
using MOTChecker.Services.MOTApiService.Configuration;
using System.Text.RegularExpressions;
using System.Net;
namespace MOTChecker.Services.MOTApiService
{
    /// <summary>
    /// Provides Restful Calls to the GOV MOT History API.
    /// </summary>
    public class MOTApiService : IMOTApiService {

        private readonly HttpClient _httpClient;
        private readonly ILogger<MOTApiService> _logger;
        private readonly string _endPointUrl = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=";

        public static string _vehicleRegistrationRexExPattern = @"(?<Current>^[A-Z]{2}[0-9]{2}[A-Z]{3}$)|(?<Prefix>^[A-Z][0-9]{1,3}[A-Z]{3}$)|(?<Suffix>^[A-Z]{3}[0-9]{1,3}[A-Z]$)|(?<DatelessLongNumberPrefix>^[0-9]{1,4}[A-Z]{1,2}$)|(?<DatelessShortNumberPrefix>^[0-9]{1,3}[A-Z]{1,3}$)|(?<DatelessLongNumberSuffix>^[A-Z]{1,2}[0-9]{1,4}$)|(?<DatelessShortNumberSufix>^[A-Z]{1,3}[0-9]{1,3}$)|(?<DatelessNorthernIreland>^[A-Z]{1,3}[0-9]{1,4}$)|(?<DiplomaticPlate>^[0-9]{3}[DX]{1}[0-9]{3}$)";

        public MOTApiService(HttpClient httpClient, ILogger<MOTApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Execute an API Call using the specified vehicle registration number.
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public async Task<MOTHistoryResult?> RequestVehicleDetailsAsync(string registration)
        {
            MOTHistoryResult result = new MOTHistoryResult();
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _endPointUrl + registration)) {
                request.Headers.Add("x-api-key", MOTApiServiceSettings.ApiKey);
                
                //try {
                    using (HttpResponseMessage response = await _httpClient.SendAsync(request)) {
                        string? responseBody = await response.Content.ReadAsStringAsync();
                        _logger.Log(LogLevel.Information, responseBody);
                        if (response.IsSuccessStatusCode) {
                            if (responseBody != null) {
                                //Deserialize to a generic list of VehicleDetail objects
                                List<VehicleDetail>? vehicleDetails = JsonSerializer.Deserialize<List<VehicleDetail>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                                if (vehicleDetails != null) {
                                    result.VehicleDetails = vehicleDetails[0];
                                }
                                else {
                                    result.ErrorMessage = "No vehicle details were returned for the specified registration.";
                                }
                            }
                        }
                        else {
                            //Deserialize to dynamic rather than a concrete object.
                            APIError? error = JsonSerializer.Deserialize<APIError>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            result.ErrorMessage = error != null ? error.ErrorMessage : "An unknown error occured requesting details.";
                        }
                    }
                /*}
                catch (Exception e) {
                    _logger.Log(LogLevel.Error, "An error occured attempting to request details from the MOT History API.");
                }*/
            }
            return result;
        }

        /// <summary>
        /// Check if a specified vehicle registration is valid.
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public static bool RegistrationIsValid(string registration) {
            Regex rg = new Regex(_vehicleRegistrationRexExPattern);
            return rg.Match(registration.ToUpper().Replace(" ", "")).Success;
        }

    }
}
