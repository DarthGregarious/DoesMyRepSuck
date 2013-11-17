using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace RepSuckCore.Test
{
  [TestClass]
  public class SuckageCalculatorTest
  {
    [TestMethod]
    public void SuckageCalculator_ReturnsResult()
    {
      var motion = Substitute.For<Motion>();

      var idealVote1 = Substitute.For<AdvocateVote>();
      idealVote1.Motion.Returns(motion);
      idealVote1.Approval = true;

      var actualVote = Substitute.For<AdvocateVote>();
      actualVote.Motion.Returns(motion);
      actualVote.Approval = true;

      var calc = new SuckageCalculator(new List<AdvocateVote> { actualVote }, new List<AdvocateVote> { idealVote1 });
      Assert.AreEqual(1, calc.Calculate());
    }
  }
}
