﻿using System;
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
      idealVote1.Motion = motion;
      idealVote1.Approval = true;

      var actualVote = Substitute.For<AdvocateVote>();
      actualVote.Motion = motion;
      actualVote.Approval = true;
    }
  }
}
