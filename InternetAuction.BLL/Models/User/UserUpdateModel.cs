namespace InternetAuction.BLL.Models.User
{
    /// <summary>
    /// Represents a user-update DTO.
    /// </summary>
    public class UserUpdateModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Balance { get; set; }
    }
}