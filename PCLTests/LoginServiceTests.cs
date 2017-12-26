using System;
using NUnit.Framework;
using Lesson1.Models.Interfaces;
using Lesson1.Models;

namespace PCLTests
{
    [TestFixture]
    public class LoginServiceTests
    {
        [Test]
        public void LoginTryTest()
        {
            ILoginService service = new LoginService();
            service.TryToLogin("pavel", "123");

            Assert.False(service.TryToLogin("pavel", "12"), "Login must be failed.");
            Assert.True(service.TryToLogin("pavel", "123"));
        }
    }
}
