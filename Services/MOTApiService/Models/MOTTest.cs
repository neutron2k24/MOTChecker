using System.Globalization;
using System.Text.Json.Serialization;

namespace MOTChecker.Services.MOTApiService.Models
{
    /// <summary>
    /// Model for MOT Test API result.
    /// </summary>
    public class MotTest {
        public string CompletedDate { get; set; }
        public string TestResult { get; set; }

        [JsonPropertyName("ExpiryDate")]
        public string ExpiryDateString { get; set; }

        [JsonIgnore]
        public DateTime ExpiryDate {  
            get {
                return DateTime.ParseExact(ExpiryDateString, "yyyy.MM.dd", CultureInfo.InvariantCulture);
            } 
        }

        [JsonIgnore]
        public bool HasExpired {
            get {
                return ExpiryDate.Date < DateTime.Now.Date;
            }
        }

        public string OdometerValue { get; set; }
        public string OdometerUnit { get; set; }
        public string MotTestNumber { get; set; }
        public RfrAndComment[] rfrAndComments { get; set; }
    }
}
