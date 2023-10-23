using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { Status = true, Message = "Operation successful.", Data = data };
        }

        public static ApiResponse<T> Success(List<T> dataList)
        {
            return new ApiResponse<T> { Status = true, Message = "Operation successful.", Data = default };
        }

        public static ApiResponse<T> NotFound()
        {
            return new ApiResponse<T> { Status = false, Message = "Object not found.", Data = default };
        }

        public static ApiResponse<T> Error(string errorMessage)
        {
            return new ApiResponse<T> { Status = false, Message = errorMessage, Data = default };
        }
    }
}