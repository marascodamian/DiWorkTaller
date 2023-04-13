namespace TallerMecanicoDiWork.Utils
{
    public class HttpResponse
    {
        public String Status { get; set; }
        public Object Data { get; set; }
        public HttpResponse()
        {
            Status = Constants.Ok;
        }
    }

    public static class Constants
    {
        public const String Ok = "OK";
        public const String Error = "Error";
    }

    public class HttpBadResponse
    {
        public String Status { get; set; }
        public ErrorMessage Error { get; set; }

        public HttpBadResponse(System.Exception e)
        {
            Status = Constants.Error;
            Error = new ErrorMessage { Code = e.GetHashCode().ToString(), OerrorMessage = e.Message };
        }
    }

    public class ErrorMessage
    {
        public String Code { get; set; }
        public String OerrorMessage { get; set; }
    }
}
