namespace Utilities.ServicesHandler.Core
{
    public class ErrorMesssage
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string RequestUrl { get; set; }
        public string ErrorMessage { get; set; }
        public string Origin { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorType { get; set; }
        public string ComputerName { get; set; }
        public string Date { get; set; }
        public string InnerErrorMessage { get; set; }
        public string InnerErrorMessageStackTrace { get; set; }
    }
}