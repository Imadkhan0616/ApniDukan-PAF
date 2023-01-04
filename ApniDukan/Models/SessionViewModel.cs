namespace ApniDukan.Models
{
    public class SessionViewModel
    {
        public bool IsAuthenticated { get; set; } = false;
        public string UserName { get; set; } = "User";
        public string Type { get; set; } = "Customer";
    }
}