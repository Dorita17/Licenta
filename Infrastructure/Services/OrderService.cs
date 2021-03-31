using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Address = Core.Entities.OrderAggregate.Address;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppIdentityDbContext _identityDbContext;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService,
        UserManager<AppUser> userManager, AppIdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
            _userManager = userManager;
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address deliveryAddress)
        {
            //get the user from the repo
            var user = await _userManager.FindByEmailAsync(buyerEmail);

            // get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // items from the meal repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var mealItem = await _unitOfWork.Repository<Meal>().GetByIdAsync(item.Id);
                var mealOrdered = new MealItemOrdered(mealItem.Id, mealItem.Name, mealItem.PictureUrl);
                var orderItem = new OrderItem(mealOrdered, mealItem.Price, item.Quantity, item.Grams, item.Calories, item.Proteins, item.Carbohydrates, item.Fats);
                items.Add(orderItem);
            }

            //update the user according to the items he ordered
            foreach (var item in items)
            {
                user.DailyCalories = user.DailyCalories - (item.Quantity * item.Calories);
                user.DailyProteins = user.DailyProteins - (item.Quantity * item.Proteins);
                user.DailyCarbohydrates = user.DailyCarbohydrates - (item.Quantity * item.Carbohydrates);
                user.DailyFats = user.DailyFats - (item.Quantity * item.Fats);
            }

            _identityDbContext.Set<AppUser>().Attach(user);
            _identityDbContext.Entry(user).State = EntityState.Modified;

            

            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calculate subtotal
            var subtotal = items.Sum(items => items.Price * items.Quantity);

            //check to see if orders exists
            var spec = new OrdersWithItemsAndOrderingSpecification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            }

            // create order
            var order = new Order(buyerEmail, subtotal, deliveryAddress, deliveryMethod, items, basket.PaymentIntentId);

            //add the order
            _unitOfWork.Repository<Order>().Add(order);

            //save to database
            var result = await _unitOfWork.Complete();
            await _identityDbContext.SaveChangesAsync();

            if (result <= 0) return null;

            // return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            var orders = await _unitOfWork.Repository<Order>().ListAsync(spec);

            return orders;
        }
    }
}