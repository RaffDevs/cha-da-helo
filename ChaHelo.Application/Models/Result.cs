using System;

namespace ChaHelo.Application.Models;

public class Result<T>
{
    public T? Value { get; set; }
    public Exception? exception { get; set; }
    public bool IsSuccess => exception is null;

    public Result(T? value, Exception? ex)
    {
        Value = value;
        exception = ex;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, null);
    }

    public static Result<T> Failure(Exception ex)
    {
        return new Result<T>(default, ex);
    }
}
