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
            ILoginService service = new ApplicationLoginService();
            service.TryToLoginAsync("pavel", "123");

            Assert.False(service.TryToLoginAsync("pavel", "12").Result, "Login must be failed.");
            Assert.True(service.TryToLoginAsync("pavel", "123").Result);
        }
    }
}
