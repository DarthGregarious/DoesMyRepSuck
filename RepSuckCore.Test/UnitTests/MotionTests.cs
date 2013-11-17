using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepSuckCore.Ballots;

namespace RepSuckCore.Test.UnitTests
{
  [TestClass]
  public class MotionTests
  {
    [TestMethod]
    public void Motion_PresentsBallot()
    {
      var bill = new YesNoMotion();
      Assert.IsTrue(bill.GetBallot() is Ballot);
    }
  }
}
