using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RepSuckCore.Test
{
  [TestClass]
  public class RepresentativeTest
  {
    [TestMethod]
    public void Representative_HasVotes()
    {
      var rep = new Representative();
      Assert.AreEqual(0, rep.Votes.Count());
    }
  }
}
