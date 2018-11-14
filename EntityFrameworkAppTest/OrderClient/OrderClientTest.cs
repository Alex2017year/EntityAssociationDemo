using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityAssociationDemo;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace EntityFrameworkAppTest
{
    [TestClass]
    public class OrderClientTest
    {
        // 使用测试初始化避免 var context = new MyDBEntities()，多次重复定义
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }

            set
            {
                testContextInstance = value;
            }
        }



        private static MyDBEntities context;

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            context = new MyDBEntities();
        }

        [ClassCleanup]
        public static void MyClassCleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public void TestGetClients()
        {
            var clients = from c in context.OrderClients
                          select c;

            Assert.IsTrue(clients.Count() > 0);
            foreach (var client in clients)
            {
                OrderClientHelper.ShowClient(client);
            }
        }

        [TestMethod]
        public async Task TestGetClientAsync()
        {
            var clients = await context.OrderClients.ToListAsync();
            Assert.IsTrue(clients.Count() > 0);
            foreach (var client in clients)
            {
                OrderClientHelper.ShowClient(client);
            }
        }
    }
}
