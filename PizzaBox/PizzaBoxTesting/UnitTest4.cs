using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaBoxDomain;
using PizzaBoxData;
using PizzaBoxData.data;

namespace PizzaBoxTesting
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestMethod4()
        {
            PizzaBoxData.Crud c=new Crud();
            var actualresult = c.PasswordMatched("aaa123","pw123");
            var expectedResult = true;
            Assert.AreEqual(expectedResult, actualresult); 
        }
    }
}
