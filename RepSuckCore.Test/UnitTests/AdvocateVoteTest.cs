using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace RepSuckCore.Test
{
  [TestClass]
  public class AdvocateVoteTest
  {
    [TestMethod]
    public void AdvocateVote_HasAdvocate()
    {
      var motion = Substitute.For<Motion>();
      var advocate = Substitute.For<IAdvocate>();
      var vote = new AdvocateVote(motion, advocate, approval: false);
      Assert.IsTrue(vote.Advocate is IAdvocate);
    }

    [TestMethod]
    public void AdvocateVote_HasApproval()
    {
      var motion = Substitute.For<Motion>();
      var advocate = Substitute.For<IAdvocate>();
      var vote = new AdvocateVote(motion, advocate, approval: false);
      Assert.IsFalse(vote.Approval);
    }

    [TestMethod]
    public void AdvocateVote_HasMotion()
    {
      var motion = Substitute.For<Motion>();
      var advocate = Substitute.For<IAdvocate>();
      var vote = new AdvocateVote(motion, advocate, approval: false);
      Assert.IsTrue(vote.Motion is Motion);
    }
  }
}
