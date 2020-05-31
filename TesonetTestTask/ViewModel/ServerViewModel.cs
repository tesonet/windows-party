using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TesonetTestTask.Model;

namespace TesonetTestTask.ViewModel
{
    class ServerViewModel
    {
                
        public ObservableCollection<ServerModel> Servers { get; set;}

        public void LoadServers()
        {
            ConnectionMgt con = new ConnectionMgt();
            string json = con.GetServerList("https://playground.tesonet.lt/v1/servers");                                                   
            var sar = JsonConvert.DeserializeObject<ObservableCollection<ServerModel>>(json);
            Servers = sar;
        }
       

    }
}
