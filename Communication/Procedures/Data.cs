using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Procedures
{
    public class UserRoute
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Client> Clients { get; set; }

        public bool Primary { get; set; }

        public UserRoute(int id, string name, List<Client> clients, bool primary)
        {
            Id = id;
            Name = name;
            Clients = clients;
            Primary = primary;
        }
    }

    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Free { get; set; }

        public Client(int id, string name, bool free)
        {
            Id = id;
            Name = name;
            Free = free;
        }
    }

    
}
