namespace MiniLibrary.DTOs;

public class ApiResponseDto<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponseDto<T> Success(T data)
    {
        return new ApiResponseDto<T>
        {
            IsSuccess = true,
            Data = data
        };
    }

    public static ApiResponseDto<T> Failure(string error) => Failure(new List<string> { error });

    public static ApiResponseDto<T> Failure(List<string> errors) => new()
    {
        IsSuccess = false,
        Errors = errors
    };
}