namespace InternetAuction.BLL.Models.User
{
    public class LoggedInUserModel
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public string ProfileImage { get; set; }
    }
}