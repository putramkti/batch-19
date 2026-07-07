namespace MiniLibrary.DTOs;

public class ApiResponseDto<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponseDto<T> Success(T data)
    {
        return new ApiResponseDto<T>
        {
            IsSuccess = true,
            Data = data
        };
    }

    public static ApiResponseDto<T> Failure(string errorMessage)
    {
        return new ApiResponseDto<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }

    public static ApiResponseDto<T> Failure(List<string> errors)
    {
        return new ApiResponseDto<T>
        {
            IsSuccess = false,
            ErrorMessage = "Validasi gagal.",
            Errors = errors
        };
    }
}