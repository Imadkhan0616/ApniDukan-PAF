using ApniDukan.Models;

namespace ApniDukan
{
    public static class SessionStorage
    {
        public static string Session { get; set; }

        public static List<Product> CartProducts { get; set; }
    }
}
