namespace Tesonet.Windows_party.Infrastructure
{
    static class StringExtensions
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
