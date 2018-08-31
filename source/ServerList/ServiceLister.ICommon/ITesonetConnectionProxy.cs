using RestSharp;

namespace ServiceLister.Common.Interfaces
{
    public interface ITesonetConnectionProxy
    {
        T Execute<T>(RestRequest request) where T : new();
    }
}