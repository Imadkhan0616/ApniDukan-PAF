using System.Collections.Concurrent;
using ApniDukan.Models;

namespace ApniDukan
{
    public static class SessionStorage
    {
        public static string Session { get; set; }

        public static ConcurrentDictionary<string, List<Product>> CartProducts { get; } = new();
    }
}