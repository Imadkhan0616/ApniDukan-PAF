using System.Security.Claims;
using Newtonsoft.Json;

namespace ApniDukan.Common
{
    public static class ExtensionMethods
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static T FromJsonToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static string GetEmailAddress(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value ?? "";
        }
    }
}