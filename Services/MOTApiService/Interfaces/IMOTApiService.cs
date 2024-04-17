using MOTChecker.Services.MOTApiService.Models;

namespace MOTChecker.Services.MOTApiService.Interfaces
{
    public interface IMOTApiService
    {
        public Task<MOTHistoryResult?> RequestVehicleDetailsAsync(string reg);
    }
}
