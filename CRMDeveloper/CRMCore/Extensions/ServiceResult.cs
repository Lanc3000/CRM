using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Extensions
{
    public class ServiceResult
    {
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public int Id { get; private set; }

        public static ServiceResult SuccessResult()
        {
            return new ServiceResult() { Success = true };
        }

        public static ServiceResult SuccessResult(int id)
        {
            return new ServiceResult() { Success = true, Id = id };
        }

        public static ServiceResult ErrorResult(string message)
        {
            return new ServiceResult() { Success = false, ErrorMessage = message };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Result { get; private set; }

        public static ServiceResult<T> SuccessResult(T result)
        {
            return new ServiceResult<T>() { Result = result, Success = true };
        }

        public new static ServiceResult<T> ErrorResult(string message)
        {
            return new ServiceResult<T>() { Success = false, ErrorMessage = message };
        }

    }
}
