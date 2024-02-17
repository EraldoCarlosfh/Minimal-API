using Flunt.Notifications;
using Flunt.Validations;

namespace ApiPetChaoBicho.Data.ViewModels
{
    public class CreateProductViewModel : Notifiable<Notification>
    {
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public List<string> Images { get; set; } = [""];

        public Product ProductMap()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o título do produto")
                .IsNotNull(Category, "Informe a categoria do produto")
                .IsNotNull(Description, "Informe a descrição do produto")
                .IsNotNull(Price, "Informe o preço do produto")
                .IsGreaterThan(Title, 5, "O título do produto deve conter mais de 5 caracateres")
                .IsGreaterThan(Category, 5, "A categoria do produto deve conter mais de 5 caracateres")
                .IsGreaterThan(Description, 5, "A descrição do produto deve conter mais de 5 caracateres");

            AddNotifications(contract);

            return new Product
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Title = Title,
                Category = Category,
                Price = Price,
                Description = Description,
                Images = Images,
                IsActive = true
            };
        }
    }
}
