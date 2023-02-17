using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleBlogApp.Services.Models;

namespace SampleBlogApp.Services.Helpers
{
    public static class ActionResultHelper
    {

        public static ActionResult Success()
        {
            return new ActionResult
            {
                Success = true,
            };
        }

        public static ActionResult<T> Success<T>(string? message, T data)
        {
            return new ActionResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
        public static ActionResult<T> Success<T>(T data)
        {
            return new ActionResult<T>
            {
                Success = true,
                Data = data
            };
        }


        public static ActionResult Success(string? message)
        {
            return new ActionResult
            {
                Success = true,
                Message = message
            };
        }

        public static ActionResult Fail(string? message)
        {
            return new ActionResult
            {
                Success = false,
                Message = message
            };
        }

    }
}
