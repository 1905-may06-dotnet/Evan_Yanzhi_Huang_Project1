using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaBoxDomain;
using PizzaBoxData;
using PizzaBoxData.data;

namespace PizzaBoxTesting
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod2()
        {
            PizzaBoxData.Crud c=new Crud();
            var actualresult = c.UsernameExist("evanh123");
            var expectedResult = true;
            Assert.AreEqual(expectedResult, actualresult); 
        }
    }
}
