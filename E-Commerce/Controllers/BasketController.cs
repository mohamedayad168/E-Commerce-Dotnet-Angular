using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            return Ok(await basketRepository.UpdateBasketAsync(basket));
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await basketRepository.DeleteBasketAsync(id);
        }
    }
}