namespace Shop_Models.Dto
{
    public class LoginResponesDto
    {
        public string Mess { get; set; } = "Login Falied";
        public bool Successful { get; set; } = false;
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
