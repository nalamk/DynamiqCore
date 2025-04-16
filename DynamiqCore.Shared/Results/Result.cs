namespace DynamiqCore.Shared.Results;

public class Result<T>
{
    public bool Succeeded { get; private set; }
    public T Data { get; private set; }
    public string[] Errors { get; private set; }
    public int StatusCode { get; private set; }
    public string Message { get; set; }

    // Private constructor to force usage of static methods
    private Result(bool succeeded, T data, string[] errors, int statusCode, string message = null)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors;
        StatusCode = statusCode;
        Message = message;
    }

    // Success (200 OK)
    public static Result<T> Success(T data, int statusCode = 200)
    {
        return new Result<T>(true, data, null, statusCode);
    }

    // Success with message (200 OK)
    public static Result<T> Success(string message, int statusCode = 200)
    {
        return new Result<T>(true, (T)Convert.ChangeType(message, typeof(T)), null, statusCode);
    }

    // Created (201 Created)
    public static Result<T> Created(T data, int statusCode = 201)
    {
        return new Result<T>(true, data, null, statusCode);
    }

    // Created (201 Created) with message
    public static Result<T> Created(T data, string message, int statusCode = 201)
    {
        return new Result<T>(true, data, null, statusCode, message);
    }

    // No Content (204 No Content)
    public static Result<T> NoContent(int statusCode = 204)
    {
        return new Result<T>(true, default(T), null, statusCode);
    }

    // Bad Request (400 Bad Request)
    public static Result<T> BadRequest(params string[] errors)
    {
        return new Result<T>(false, default(T), errors, 400);
    }

    // Unauthorized (401 Unauthorized)
    public static Result<T> Unauthorized(params string[] errors)
    {
        return new Result<T>(false, default(T), errors, 401);
    }

    // Forbidden (403 Forbidden)
    public static Result<T> Forbidden(params string[] errors)
    {
        return new Result<T>(false, default(T), errors, 403);
    }

    // Not Found (404 Not Found)
    public static Result<T> NotFound(params string[] errors)
    {
        return new Result<T>(false, default(T), errors, 404);
    }

    // Conflict (409 Conflict)
    public static Result<T> Conflict(params string[] errors)
    {
        return new Result<T>(false, default(T), errors, 409);
    }

    // Internal Server Error (500 Internal Server Error)
    public static Result<T> InternalServerError(params string[] errors)
    {
        return new Result<T>(false, default(T), errors, 500);
    }

    // Custom Status Code
    public static Result<T> CustomError(int statusCode, params string[] errors)
    {
        return new Result<T>(false, default(T), errors, statusCode);
    }

    // Create a result from an exception
    public static Result<T> FromException(Exception ex, int statusCode = 500)
    {
        return new Result<T>(false, default(T), new[] { ex.Message }, statusCode, ex.StackTrace);
    }
}
