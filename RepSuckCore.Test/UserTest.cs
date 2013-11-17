using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace RepSuckCore.Test
{
  [TestClass]
  public class UserTest
  {
    [TestMethod]
    public void User_HasTrustedAdvocates()
    {
      var user = new User();
      Assert.IsTrue(user.TrustedAdvocates is IEnumerable<IAdvocate>);
    }
  }
}
