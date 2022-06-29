namespace InternetAuction.BLL.Models.User
{
    /// <summary>
    /// Represents a logged-in-user DTO.
    /// </summary>
    public class LoggedInUserModel
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public string ProfileImage { get; set; }
    }
}