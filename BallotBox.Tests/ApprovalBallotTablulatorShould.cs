using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BallotBox.Tests
{
  [TestFixture]
  class ApprovalBallotTablulatorShould
  {
    [Test]
    public void DeclareGoreTheWinner_WhenGoreGetsTheOnlyApproval()
    {
      var gore = new Candidate("Gore", "Al");
      var ballot = new ApprovalBallot { Approvals = new[] { gore } };
      var result = new ApprovalBallotTabulator().GetResult(new[] { ballot });
      Assert.AreEqual(gore, result.FirstPlace);
    }

    [Test]
    public void DeclareGoreTheWinner_WhenGoreGetsTheMostApprovals()
    {
      var gore = new Candidate("Gore", "Al");
      var bush = new Candidate("Bush", "George");
      var goreBallot = new ApprovalBallot { Approvals = new[] { gore } };
      var goreBushBallot = new ApprovalBallot { Approvals = new[] { bush, gore } };
      var result = new ApprovalBallotTabulator().GetResult(new[] { goreBallot, goreBushBallot });
      Assert.AreEqual(gore, result.FirstPlace);
    }

    [Test]
    public void DeclareGoreTheWinner_WhenGoreGetsTheMostApprovals_AndHisBallotIsCastSecond()
    {
      var gore = new Candidate("Gore", "Al");
      var bush = new Candidate("Bush", "George");
      var goreBallot = new ApprovalBallot { Approvals = new[] { gore } };
      var goreBushBallot = new ApprovalBallot { Approvals = new[] { bush, gore } };
      var result = new ApprovalBallotTabulator().GetResult(new[] { goreBushBallot, goreBallot });
      Assert.AreEqual(gore, result.FirstPlace);
    }

    [Test]
    public void DeclareItTied_WhenEveryoneHasTheSameNumberOfApprovals()
    {
      var gore = new Candidate("Gore", "Al");
      var bush = new Candidate("Bush", "George");
      var goreBushBallot1 = new ApprovalBallot { Approvals = new[] { bush, gore } };
      var goreBushBallot2 = new ApprovalBallot { Approvals = new[] { bush, gore } };
      var result = new ApprovalBallotTabulator().GetResult(new[] { goreBushBallot1, goreBushBallot2 });
      Assert.AreEqual(ElectionOutcome.Tied, result.ElectionOutcome);
    }
  }

  public class ApprovalBallot
  {
    public IEnumerable<Candidate> Approvals { get; set; }
  }

  public class ApprovalBallotTabulator
  {
    public ElectionResult GetResult(IEnumerable<ApprovalBallot> ballots)
    {
      var approvalGroups = ballots.SelectMany(b => b.Approvals).GroupBy(c => c).OrderByDescending(g => g.Count());

      if (approvalGroups.Count() > 1 && approvalGroups.ElementAt(0).Count() == approvalGroups.ElementAt(1).Count())
        return DeclareTie();

      return DeclareWinner(approvalGroups.First().Key);
    }

    private ElectionResult DeclareWinner(Candidate c)
    {
      return new ElectionResult
      {
        ElectionOutcome = ElectionOutcome.Winner,
        FirstPlace = c
      };
    }

    private ElectionResult DeclareTie()
    {
      return new ElectionResult
      {
        ElectionOutcome = ElectionOutcome.Tied
      };
    }
  }
}
