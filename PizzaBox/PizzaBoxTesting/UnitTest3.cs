using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaBoxDomain;
using PizzaBoxData;
using PizzaBoxData.data;

namespace PizzaBoxTesting
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod3()
        {
            PizzaBoxData.Crud c=new Crud();
            var actualresult = c.OrderExist(23);
            var expectedResult = true;
            Assert.AreEqual(expectedResult, actualresult); 
        }
    }
}
