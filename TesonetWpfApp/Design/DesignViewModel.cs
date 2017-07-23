using System;
using System.Collections.Generic;
using TesonetWpfApp.Business.Models;

namespace TesonetWpfApp.Design
{
    public class DesignViewModel
    {
        public List<Server> Servers { get; set; }

        public DesignViewModel()
        {
            Servers = new List<Server>();
            var cities = new List<string> { "vilnius", "london", "paris", "panevezys", "kaunas" };
            Random r = new Random();
            for (int i = 0; i < 30; i++)
            {
                var s = new Server { Name = cities[r.Next(0, 4)], Distance = r.Next(1, 9999) };
                Servers.Add(s);
            }
        }
    }
}
