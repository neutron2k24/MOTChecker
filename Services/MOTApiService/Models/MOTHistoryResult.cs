namespace MOTChecker.Services.MOTApiService.Models {
    /// <summary>
    /// Model for History Result from MOT API
    /// </summary>
    public class MOTHistoryResult {
        
        public string? ErrorMessage { get; set; }
        public VehicleDetail? VehicleDetails { get; set; }
    }
}