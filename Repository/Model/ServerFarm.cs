using System.Collections.ObjectModel;
using System.Net;

namespace Repository.Model
{
    public class ServerFarm
    {
        public ObservableCollection<Server> Servers { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
