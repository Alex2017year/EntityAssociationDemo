using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityAssociationDemo;

namespace EntityFrameworkAppTest.SingleEntity
{
    class OrderClientUtils
    {
        public static OrderClient CreateOrderClient()
        {
            String clientName = "客户" + new Random().Next(1, 1000);
            OrderClient client = new OrderClient
            {
                ClientName = clientName,
                Address = clientName + "的地址",
                Email = clientName + "的电子邮箱",
                PostCode = new Random().Next(100000, 999999).ToString(),
                Telephone = "1" + new Random().Next(10000, 99999) + new Random().Next(10000, 99999)
            };

            return client;
        }
    }
}
