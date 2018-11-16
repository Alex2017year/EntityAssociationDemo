using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityAssociationDemo;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace EntityFrameworkAppTest.SingleEntity
{
    /// <summary>
    /// OrderClientForCRUDTest 的摘要说明
    /// </summary>
    [TestClass]
    public class OrderClientForCRUDTest
    {
        public OrderClientForCRUDTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
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

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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
        public async Task TestAdd()
        {
            OrderClient client = OrderClientUtils.CreateOrderClient();
            context.OrderClients.Add(client);
            // 将结果写入到数据库
            int result = await context.SaveChangesAsync();
            Assert.IsTrue(result == 1);
            Console.WriteLine(client);

            // 读取数据库
            OrderClient clientFromDB = await context.OrderClients.FirstOrDefaultAsync(c => c.ClientID == client.ClientID);
            Assert.IsNotNull(clientFromDB);
            //检测两个OrderClient对象是相等的
            Assert.IsTrue(clientFromDB.Equals(client));
        }

        [TestMethod]
        public async Task TestModify()
        {
            // 获取最后一条记录
            // 由于EF不支持Last()查询，因此，先对其进行降序排列，然后取得第一条记录
            var client = await (from c in context.OrderClients
                                orderby c.ClientID descending
                                select c).FirstOrDefaultAsync();

            if (client != null)
            {
                // 更新邮编，+1，再更新到数据库中
                string newPostCode = (Int32.Parse(client.PostCode) + 1).ToString();
                client.PostCode = newPostCode;
                int result = await context.SaveChangesAsync();
                Assert.IsTrue(result == 1);

                // 从数据库中再次提取原始值，以确定数据的更新
                OrderClient clientFromDB = context.OrderClients.First(c => c.PostCode == newPostCode);
                Assert.IsTrue(client.Equals(clientFromDB));
            }

        }

        [TestMethod]
        public async Task TestDelete()
        {
            //获取最后一条记录
            //由于EF不支持Last()查询，因此，先对其进行降序排列，然后取第一条记录
            var client = await (from c in context.OrderClients
                                orderby c.ClientID descending
                                select c).FirstAsync();

            if (client != null)
            {
                context.OrderClients.Remove(client);
                int result = await context.SaveChangesAsync();
                Assert.IsTrue(result == 1);
            }
        }

        [TestMethod]
        public void TestDelete2()
        {
            // 直接删除对象边界方法
            // 如果已经知道要删除对象的主键值，那么可以通过Attach一个“样板”对，再删除
            // 好处：不需要从数据库中提取数据，创建实体对象，再查找并删除
            OrderClient stubClient = new OrderClient { ClientID = 100 };
            context.OrderClients.Attach(stubClient);
            context.OrderClients.Remove(stubClient);
            context.SaveChanges();
        }
    }
}
