using E_Commerce.Errors;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("errors/{status}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {

        public ActionResult GetError(int status)
        {
            return new ObjectResult(new ApiResponse(status));
        }
    }
}