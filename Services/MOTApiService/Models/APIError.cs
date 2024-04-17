namespace MOTChecker.Services.MOTApiService.Models {

    public class APIError {
        public string HttpStatus { get; set; }
        public string ErrorMessage { get; set; }
        public string AwsRequestId { get; set; }
    }

}
