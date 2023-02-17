using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBlogApp.Services.Models
{
    public class ActionResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static implicit operator ActionResult<T>(ActionResult result)
        {
            return new ActionResult<T>
            {
                Success = result.Success,
                Message = result.Message
            };
        }

    }

    public class ActionResult : ActionResult<object>
    {

    }

}
