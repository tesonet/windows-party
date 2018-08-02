namespace tesonet.windowsparty.data
{
    public interface IQuery<T>
    {
        T Payload { get; set; }
    }
}
