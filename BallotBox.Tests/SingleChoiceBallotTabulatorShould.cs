using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BallotBox.Tests
{
  [TestFixture]
  public class SingleChoiceBallotTabulatorShould
  {
    [Test]
    public void SayBushWon_WhenBushGetsTheOnlyVote()
    {
      var bush = new Candidate("Bush", "George");
      var ballot = new SingleChoiceBallot { Winner = bush };
      var result = new SingleChoiceBallotTabulator().GetResult(new[] { ballot });
      Assert.AreEqual(bush, result.FirstPlace);
    }

    [Test]
    public void SayBushWon_WhenBushGetsMajority()
    {
      var bush = new Candidate("Bush", "George");
      var gore = new Candidate("Gore", "Al");
      var bushBallot1 = new SingleChoiceBallot { Winner = bush };
      var bushBallot2 = new SingleChoiceBallot { Winner = bush };
      var goreBallot = new SingleChoiceBallot { Winner = gore };
      var result = new SingleChoiceBallotTabulator().GetResult(new[] { bushBallot1, bushBallot2, goreBallot });
      Assert.AreEqual(bush, result.FirstPlace);
    }

    [Test]
    public void SayItsTied_WhenBushAndGoreGetSameNumberOfVotes()
    {
      var bush = new Candidate("Bush", "George");
      var gore = new Candidate("Gore", "Al");
      var bushBallot = new SingleChoiceBallot { Winner = bush };
      var goreBallot = new SingleChoiceBallot { Winner = gore };
      var result = new SingleChoiceBallotTabulator().GetResult(new[] { bushBallot, goreBallot });
      Assert.AreEqual(ElectionOutcome.Tied, result.ElectionOutcome);
    }
  }

  public class ElectionResult
  {
    public ElectionOutcome ElectionOutcome { get; set; }
    public Candidate FirstPlace { get; set; }
  }

  public enum ElectionOutcome
  {
    Winner,
    Tied
  }

  public class SingleChoiceBallotTabulator
  {
    public ElectionResult GetResult(IEnumerable<SingleChoiceBallot> ballots)
    {
      var ballotGroups = ballots.GroupBy(b => b.Winner).OrderByDescending(g => g.Count());

      if(ballotGroups.Count() > 1 && ballotGroups.ElementAt(0).Count() == ballotGroups.ElementAt(1).Count())
        return DeclareTie();

      return DeclareWinner(ballotGroups.First().Key);
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

  public class SingleChoiceBallot
  {
    public Candidate Winner { get; set; }
  }

  [DebuggerDisplay("{FirstName} {LastName}")]
  public class Candidate
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Candidate(string last, string first)
    {
      LastName = last;
      FirstName = first;
    }
  }
}
