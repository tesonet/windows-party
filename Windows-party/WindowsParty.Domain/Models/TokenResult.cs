namespace WindowsParty.Domain.Models
{
    public class TokenResult
    {
        public bool IsSuccess => !string.IsNullOrWhiteSpace(Token);
        
        public string Token { get; set; }
    }
}