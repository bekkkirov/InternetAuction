namespace InternetAuction.BLL.Models.User
{
    /// <summary>
    /// Represents a register DTO.
    /// </summary>
    public class RegisterModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}