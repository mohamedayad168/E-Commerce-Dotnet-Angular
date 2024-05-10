using _InfraStructure.Data;
using E_Commerce.Errors;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            var result = context.Products.Find(42);

            if (result == null) return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("not-found/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            var result = context.Products.Find(42);
            var thing = result.ToString();
            return Ok(new ApiResponse(500));
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
    }
}