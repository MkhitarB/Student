namespace Student.DTO.Authentication
{
    public class UserTokenModel
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
