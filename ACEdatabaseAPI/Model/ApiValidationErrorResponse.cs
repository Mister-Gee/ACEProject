using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Model
{
    public class ApiValidationErrorResponse : ApiError
    {
        public ApiValidationErrorResponse() : base(400, "Bad Request")
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
