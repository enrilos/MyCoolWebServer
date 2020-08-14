namespace MyCoolWebServer.ByTheCakeApplication.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<int> productIds)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = new Order
                {
                    UserId = userId,
                    CreationDate = DateTime.UtcNow,
                    Products = productIds.Select(id => new OrderProduct
                    {
                        ProductId = id
                    })
                    .ToList()
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
    }
}
