namespace E_Commerce.Errors;

public class ApiResponse
{
    public ApiResponse()
    {
    }

    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }

    private string? GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Authorized, you are not",
            404 => "Resource Not Found",
            500 => "Server Error Occuired",
            _ => null
        };
    }
}