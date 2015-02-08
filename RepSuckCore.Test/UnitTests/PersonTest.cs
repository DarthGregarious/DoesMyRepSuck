using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace RepSuckCore.Test
{
  [TestClass]
  public class PersonTest
  {
    [TestMethod]
    public void User_RemembersName()
    {
      var person = new Person("Andrew", "Kvochick");
      Assert.AreEqual(person.FirstLastName, "Andrew Kvochick");
    }
  }
}
