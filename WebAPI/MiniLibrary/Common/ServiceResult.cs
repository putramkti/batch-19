// namespace MiniLibrary.Common;

// public class ServiceResult<T>
// {
//     public bool IsSuccess { get; set; }
//     public T? Data { get; set; }
//     public string? ErrorMessage { get; set; }
//     public int StatusCode { get; set; }

//     public static ServiceResult<T> Success(T data, int statusCode = 200)
//     {
//         return new ServiceResult<T>
//         {
//             IsSuccess = true,
//             Data = data,
//             StatusCode = statusCode
//         };
//     }

//     public static ServiceResult<T> Failure(string errorMessage, int statusCode = 400)
//     {
//         return new ServiceResult<T>
//         {
//             IsSuccess = false,
//             ErrorMessage = errorMessage,
//             StatusCode = statusCode
//         };
//     }
// }