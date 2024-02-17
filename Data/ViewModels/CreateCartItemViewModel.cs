using Flunt.Notifications;
using Flunt.Validations;

namespace ApiPetChaoBicho.Data.ViewModels
{
    public class CreateCartItemViewModel : Notifiable<Notification>
    {
        public string Product { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } = "";

        public CartItem CartItemMap()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Product, "Informe o nome do item")
                .IsNotNull(Quantity, "Informe a quantidade do item")
                .IsNotNull(Price, "Informe o preço do item")
                .IsGreaterThan(Product, 5, "O nome do item deve conter mais de 5 caracateres");

            AddNotifications(contract);

            return new CartItem
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Product = Product,
                Quantity = Quantity,
                Price = Price,
                Image = Image,
                IsActive = true
            };
        }
    }
}
