namespace MOTChecker.Services.MOTApiService.Models
{
    /// <summary>
    /// Model for Vehicle Details from API result.
    /// </summary>
    public class VehicleDetail
    {
        public string Registration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string FirstUsedDate { get; set; }
        public string FuelType { get; set; }
        public string PrimaryColour { get; set; }
        public MotTest[] MotTests { get; set; }

        public MotTest? MostRecentMOTTest {
            get {
                if(MotTests!= null && MotTests.Length>0) return MotTests[0];
                return null;
            }
        }
    }
}
