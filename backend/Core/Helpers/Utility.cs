using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class Utility
    {
        public static ApiResponse<T> CreateApiResponse<T>(T data, int resultCode, string message = "", string errorMsg = "") where T : class
        {
            return ApiResponse<T>.Error("An error occurred while processing your request.");
        }
    }
}