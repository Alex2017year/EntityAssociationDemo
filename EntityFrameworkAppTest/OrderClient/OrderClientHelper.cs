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


        public static void ShowPerson(Person p)
        {
            if (p != null)
                Console.WriteLine("姓名：{0}， 身份证：{1}", p.Name, p.IdentityCard);
        }
    }
}
