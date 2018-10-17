using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesonet
{
    interface IViewer
    {
        void LogIn();
    }

    interface IModeler
    {
        void SetListToDataBind(List<Server> serverList);
    }
    class VM: IViewer, IModeler
    {
        private DataBinding dataBind;
        private PostRequest postRequest;
        ServerList serverView;
        public VM(DataBinding dataBind)
        {
            this.dataBind = dataBind;
            postRequest = new PostRequest(dataBind, this);
        }

        public void LogIn()
        {
            postRequest.GenerateRequest();  
        }

        public void SetListToDataBind(List<Server> serverList)
        {
            dataBind.Locationtable = serverList;
            StartTheNewView();
        }

        private void StartTheNewView()
        {
            serverView = new ServerList(dataBind);
            serverView.Show();
        }

        public void LogOut()
        {
            serverView.Close();
            dataBind.Password = string.Empty;
        }
    }
}
