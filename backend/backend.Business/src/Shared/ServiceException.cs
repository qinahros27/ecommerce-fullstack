namespace backend.Business.src.Shared
{
    public class ServiceException : Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public static ServiceException NotFoundException(string message = "The entity is not found")
        {
            return new ServiceException
            {
                StatusCode = 404,
                Message = message
            };
        }

        public static ServiceException UnAuthenticatedException(string message = "Credentials are wrong")
        {
            return new ServiceException
            {
                StatusCode = 401,
                Message = message
            };
        }
    }
}