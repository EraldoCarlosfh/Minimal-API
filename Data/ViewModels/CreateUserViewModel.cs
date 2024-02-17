using Flunt.Notifications;
using Flunt.Validations;

namespace ApiPetChaoBicho.Data.ViewModels
{
    public class CreateUserViewModel : Notifiable<Notification>
    {
        public string Name { get; set; } = "";
        public string Document { get; set; } = "";
        public string Email { get; set; } = "";

        public User UserMap()
        {
            //Pode ser usado assim também
            //AddNotifications(new Contract<Notification>()
            //    .Requires()
            //    .IsNotNull(Name, "Informe o nome do usuário")
            //    .IsNotNull(Document, "Informe o documento do usuário")
            //    .IsNotNull(Email, "Informe o e-mail do usuário")
            //    .IsGreaterThan(Name, 5, "O nome do usuário deve conter mais de 5 caracateres")
            //    .IsGreaterThan(Document, 11, "O nome do usuário deve conter no máximo 11 caracteres")
            //    .IsEmail(Email, "Utilize um e-mail válido")
            //    );

            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Name, "Informe o nome do usuário")
                .IsNotNull(Document, "Informe o documento do usuário")
                .IsNotNull(Email, "Informe o e-mail do usuário")
                .IsGreaterThan(Name, 5, "O nome do usuário deve conter mais de 5 caracateres")
                .IsLowerOrEqualsThan(Document, 11, "O documento do usuário deve conter no máximo 11 caracteres")
                .IsEmail(Email, "Utilize um e-mail válido");

            AddNotifications(contract);

            return new User
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Name = Name,
                Document = Document,
                Email = Email,
                IsActive = true
            };
        }
    }
}
