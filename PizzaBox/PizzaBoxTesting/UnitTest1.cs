using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaBoxDomain;
using PizzaBoxData;
using PizzaBoxData.data;

namespace PizzaBoxTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PizzaBoxDomain.DMItem i = new DMItem();
            var actualresult = i.calculateItemPrice("Large",5,1);
            var expectedResult = 13;
            Assert.AreEqual(expectedResult, actualresult); 
        }
    }
}
