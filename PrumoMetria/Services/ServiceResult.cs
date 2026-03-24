namespace PrumoMetria.Services;

public class ServiceResult<T>
{
    public T? Data { get; private set; }
    public string? Error { get; private set; }
    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }

    private ServiceResult() { }

    public static ServiceResult<T> Success(T data, int statusCode = 200)
    {
        return new ServiceResult<T>
        {
            Data = data,
            IsSuccess = true,
            StatusCode = statusCode
        };
    }

    public static ServiceResult<T> Fail(string error, int statusCode)
    {
        return new ServiceResult<T>
        {
            Error = error,
            IsSuccess = false,
            StatusCode = statusCode
        };
    }
}