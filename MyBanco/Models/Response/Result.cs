namespace MyBanco.Models.Response;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    
    public string ErrorMessage { get; set; }
    
    public T Value { get; private set; }

    private Result(bool IsSuccesse, T value, string errorMessage)
    {
        IsSuccess = IsSuccesse;
        Value =  value;
        ErrorMessage = errorMessage;
    }   

    private Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    
    public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);
    public static Result<T> Failure(T value, string errorMessage) => new Result<T>(false, value, errorMessage);

    public static Result<bool> Failure(string carteiraJaExiste)
    {
        throw new NotImplementedException();
    }
}