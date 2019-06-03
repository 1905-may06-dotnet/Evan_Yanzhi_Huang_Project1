using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaBoxDomain;
using PizzaBoxData;
using PizzaBoxData.data;

namespace PizzaBoxTesting
{
    [TestClass]
    public class UnitTest6
    {
        [TestMethod]
        public void TestMethod6()
        {
            PizzaBoxData.Crud c=new Crud();
            var actualresult = c.GetUserByUserName("fred123").DMUserId;
            var expectedResult = 4;
            Assert.AreEqual(expectedResult, actualresult); 
        }
    }
}
