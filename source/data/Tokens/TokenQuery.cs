using tesonet.windowsparty.contracts;

namespace tesonet.windowsparty.data.Tokens
{
    public class TokenQuery : IQuery<Credentials>
    {
        public Credentials Payload { get; set; }
    }
}
