namespace backend.Business.src.Shared
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public CustomException(int statusCode = 500, string message = "Internal server error")
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }

        public static CustomException NotFoundException(string message = "Item cannot be found")
        {
            return new CustomException(404, message);
        }

        public static CustomException BadRequestException(string message = "Bad Request")
        {
            return new CustomException(400, message);
        }
    }
}