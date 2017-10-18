using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.ServicesHandler;
using Utilities.Validation;


namespace Utilities.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NotNull_WithValidClass_ShouldReturnTrue()
        {
            UserInfo userInfoExpected = new UserInfo();
            var returnValue = Check.NotNull(userInfoExpected, "userInfo");

            Assert.AreEqual(returnValue.GetType(), typeof(UserInfo));
        }
    }
}
