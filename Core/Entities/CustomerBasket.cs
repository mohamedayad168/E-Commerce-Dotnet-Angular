﻿namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        { }

        public CustomerBasket(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
        public List<BasketItem> items { get; set; } = new();
    }
}