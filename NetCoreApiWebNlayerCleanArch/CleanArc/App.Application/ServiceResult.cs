﻿using System.Net;
using System.Text.Json.Serialization;

namespace App.Application
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessages { get; set; }

        [JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
        [JsonIgnore] public bool IsFail => !IsSuccess;
        [JsonIgnore] public HttpStatusCode Status { get; set; }
        [JsonIgnore] public string? UrlAsCreated { get; set; }

        //Static Factory Method
        public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Status = status
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Status = HttpStatusCode.Created,
                UrlAsCreated = urlAsCreated
            };
        }

        public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessages = errorMessage,
                Status = status
            };
        }

        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessages = [errorMessage],
                Status = status
            };
        }
    }

    public class ServiceResult
    {
        public List<string>? ErrorMessages { get; set; }
        [JsonIgnore]
        public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }
        //Static Factory Method
        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult
            {
                Status = status
            };
        }

        public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessages = errorMessage,
                Status = status
            };
        }

        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessages = [errorMessage],
                Status = status
            };
        }
    }
}
