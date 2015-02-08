using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BallotBox.Tests
{
  [TestFixture]
  public class InstantRunoffBallotTabulatorShould
  {
    [Test]
    public void DeclareBushTheWinner_WhenBushWinsTheOnlyBallot()
    {
      var bush = new Candidate("Bush", "George");
      var ballot = new RankedBallot();
      ballot.Add(bush, 1);
      var result = new InstantRunoffBallotTabulator().GetResult(new[] {ballot});
      Assert.AreEqual(bush, result.FirstPlace);
    }

    [Test]
    public void DeclareBushTheWinner_WhenBushWinsMajorityOfFirstPlaceVotes()
    {
      var bush = new Candidate("Bush", "George");
      var gore = new Candidate("Gore", "Al");

      var bushBallot1 = new RankedBallot();
      bushBallot1.Add(bush, 1);

      var bushBallot2 = new RankedBallot();
      bushBallot2.Add(bush, 1);

      var goreBallot = new RankedBallot();
      goreBallot.Add(gore, 1);

      var result = new InstantRunoffBallotTabulator().GetResult(new[] { bushBallot1, bushBallot2, goreBallot });
      Assert.AreEqual(bush, result.FirstPlace);
    }

    [Test]
    public void DeclareGoreTheWinner_WhenGoreWinsMajorityOfFirstPlaceVotes()
    {
      var bush = new Candidate("Bush", "George");
      var gore = new Candidate("Gore", "Al");

      var bushBallot1 = new RankedBallot();
      bushBallot1.Add(bush, 1);

      var goreBallot1 = new RankedBallot();
      goreBallot1.Add(gore, 1);

      var goreBallot2 = new RankedBallot();
      goreBallot2.Add(gore, 1);

      var result = new InstantRunoffBallotTabulator().GetResult(new[] { bushBallot1, goreBallot1, goreBallot2 });
      Assert.AreEqual(gore, result.FirstPlace);
    }

    [Test]
    public void DeclareGoreTheWinner_WhenNoOneWinsTheMajorityAndNadersVotesAreRedistributed()
    {
      var bush = new Candidate("Bush", "George");
      var gore = new Candidate("Gore", "Al");
      var nader = new Candidate("Nader", "Ralph");

      var bushBallot1 = new RankedBallot();
      bushBallot1.Add(bush, 1);

      var bushBallot2 = new RankedBallot();
      bushBallot2.Add(bush, 1);

      var goreBallot1 = new RankedBallot();
      goreBallot1.Add(gore, 1);

      var goreBallot2 = new RankedBallot();
      goreBallot2.Add(gore, 1);

      var naderBallot = new RankedBallot();
      naderBallot.Add(nader, 1);
      naderBallot.Add(gore, 2);

      var result = new InstantRunoffBallotTabulator().GetResult(new[] { bushBallot1, bushBallot2, goreBallot1, goreBallot2, naderBallot });
      Assert.AreEqual(gore, result.FirstPlace);
    }
  }

  public class InstantRunoffBallotTabulator
  {
    public ElectionResult GetResult(IEnumerable<RankedBallot> ballots)
    {
      return new ElectionResult { FirstPlace = ballots.SelectMany(b => b.Rankings).Where(r => r.Rank == 1).Select(r => r.Candidate).GroupBy(c => c).OrderByDescending(g => g.Count()).First().Key };
    }
  }

  public class RankedBallot
  {
    List<CandidateRanking> rankings;
    public IEnumerable<CandidateRanking> Rankings { get { return rankings.OrderBy(r => r.Rank); } }

    public RankedBallot()
    {
      rankings = new List<CandidateRanking>();
    }

    public void Add(Candidate c, int rank)
    {
      rankings.Add(new CandidateRanking(c, rank));
    }
  }

  public class CandidateRanking
  {
    public int Rank {get; private set;}
    public Candidate Candidate {get; private set;}

    public CandidateRanking (Candidate c, int rank)
	  {
      Candidate = c;
      Rank = rank;
	  }
  }
}
