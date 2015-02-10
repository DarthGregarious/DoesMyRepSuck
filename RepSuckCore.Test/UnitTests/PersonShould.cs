using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace RepSuckCore.Test
{
  [TestClass]
  public class PersonShould
  {
    [TestMethod]
    public void RememberName()
    {
      var person = new Person("Andrew", "Kvochick");
      Assert.AreEqual(person.FirstLastName, "Andrew Kvochick");
      Assert.AreEqual(person.LastName, "Kvochick");
    }

    [TestMethod]
    public void HavePositions_WhenPositionIsRecorded()
    {
      var person = new Person("Andrew", "Kvochick");
      person.AddPosition(new Position(new Issue("Test", new[] { "Yes", "Extra Yes" }), "Extra Yes"));
      Assert.AreEqual(1, person.Positions.Count());
    }
  }
}
