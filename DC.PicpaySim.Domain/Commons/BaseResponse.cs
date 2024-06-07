using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Commons
{
    public class BaseResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; }

        public static BaseResponse<T> Success(T data) => new BaseResponse<T> { Data = data, isSuccess = true, Status = 200, Message = string.Empty };
        public static BaseResponse<T> Error(int status, string message) => new BaseResponse<T> { isSuccess = false, Status = status, Message = message };
        public static BaseResponse<T> NotFound(string message) => new BaseResponse<T> { isSuccess = false, Status = 404, Message = message };
        public static BaseResponse<T> Unauthorized(string message) => new BaseResponse<T> { isSuccess = false, Status = 401, Message = message };
        public static BaseResponse<T> Forbidden(string message) => new BaseResponse<T> { isSuccess = false, Status = 403, Message = message };
    }
}
