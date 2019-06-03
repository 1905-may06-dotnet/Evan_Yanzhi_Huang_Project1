using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaBoxDomain;
using PizzaBoxData;
using PizzaBoxData.data;

namespace PizzaBoxTesting
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void TestMethod5()
        {
            PizzaBoxData.Crud c=new Crud();
            var actualresult = c.UserHasOrder(4);
            var expectedResult = true;
            Assert.AreEqual(expectedResult, actualresult); 
        }
    }
}
