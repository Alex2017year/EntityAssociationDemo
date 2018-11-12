using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityAssociationDemo;

namespace EntityFrameworkAppTest
{
    class OrderClientHelper
    {
        public static void ShowClient(OrderClient client)
        {
            if(client != null)
                Console.WriteLine("{0}:{1}", client.ClientName, client.Address);
        }
    }
}
